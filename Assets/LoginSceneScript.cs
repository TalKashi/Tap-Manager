using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook;

public class LoginSceneScript : MonoBehaviour {

    public GameObject m_Login;
    public GameObject m_MainScene;

    bool k_IsDataLoaded = false;

    const string SERVER = GameManager.URL;

    void Awake()
    {
        m_Login.SetActive(false);
        m_MainScene.SetActive(false);
        FB.Init(onInit);
    }

    void Update()
    {
        if (k_IsDataLoaded)
        {
            m_MainScene.SetActive(true);
        }
    }

    void onInit()
    {
        if (FB.IsLoggedIn)
        {
            m_Login.SetActive(false);
        }
        else
        {
            m_Login.SetActive(true);
        }
    }

    public void Login()
    {
        FB.Login("public_profile,email,user_birthday", fbLoginCallback);
    }

    void fbLoginCallback(FBResult i_FbResult)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("facebookLogIn worked");
            m_Login.SetActive(false);
            FB.API("me?fields=id,name,email,birthday", HttpMethod.GET, onGettingUserDataFromFB);

        }
        else
        {
            m_Login.SetActive(true);
            Debug.Log("facebookLogIn failed");
        }
    }

    void onGettingUserDataFromFB(FBResult i_FBResult)
    {
        if (!string.IsNullOrEmpty(i_FBResult.Error))
        {
            Debug.LogError("ERROR: " + i_FBResult.Error);
            return;
        }

        if (GameManager.s_GameManger.m_User == null)
        {
            GameManager.s_GameManger.m_User = new User();
        }

        if (verifyAllDataPresent(i_FBResult.Text))
        {
            StartCoroutine(addNewFBUser(i_FBResult.Text));
        }
        else
        {
            Debug.LogError("Missing user data from facebook!");
        }
    }

    private IEnumerator addNewFBUser(string i_NewUserJson)
    {
        //UTF8Encoding encoding = new UTF8Encoding();
        //Dictionary<string, string> headers = new Dictionary<string, string>();
        //headers.Add("Content-Type", "text/json");
        //headers.Add("Content-Length", i_NewUserJson.Length.ToString());
        WWWForm wwwform = new WWWForm();
        wwwform.AddField("json", i_NewUserJson);
        //WWW www = new WWW("http://serge-pc:3000/newUser", encoding.GetBytes(i_NewUserJson), headers);
        WWW www = new WWW(SERVER + "loginUser", wwwform);

        Debug.Log("Sending user data");
        yield return www;
        Debug.Log("Finished sending user data");

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            print(www.text);
            if (www.text == "null")
            {
                // Go to new team scene
                Application.LoadLevel("Input");
            }
            else
            {
                StartCoroutine(syncClientDB());
            }
            // Check ok response
            // if new user go to new team screen
            // else go to home page with team data
        }
        Debug.Log("End of addNewFBUser()");
    }

    IEnumerator syncClientDB()
    {
        WWWForm form = new WWWForm();
        Debug.Log("sending sync request for user: " + PlayerPrefs.GetString("email"));
        form.AddField("email", PlayerPrefs.GetString("email"));
        WWW request = new WWW(SERVER + "getInfoByEmail", form);

        yield return request;
        
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            print(request.text);
            Dictionary<string, object> json = Facebook.MiniJSON.Json.Deserialize(request.text) as Dictionary<string, object>;
            MyUtils.LoadTeamData(json, ref GameManager.s_GameManger.m_myTeam);
            MyUtils.LoadLeagueData(json, ref GameManager.s_GameManger.m_AllTeams);
            MyUtils.LoadBucketData(json, ref GameManager.s_GameManger.m_Bucket);
            MyUtils.LoadSquadData(json, ref GameManager.s_GameManger.m_MySquad);
            MyUtils.LoadGameSettings(json, ref GameManager.s_GameManger.m_GameSettings);
            MyUtils.LoadUserData(json, ref GameManager.s_GameManger.m_User);
            k_IsDataLoaded = true;
        }
        Debug.Log("End of addNewFBUser()");

    }

    private bool verifyAllDataPresent(string i_JsonStr)
    {
        object id;
        object name;
        object email;
        object birthday;
        bool isValid = true;

        if (string.IsNullOrEmpty(i_JsonStr))
        {
            Debug.Log("Recieved and empty/null string");
            return false;
        }
        Debug.Log(i_JsonStr);
        Dictionary<string, object> json = Facebook.MiniJSON.Json.Deserialize(i_JsonStr) as Dictionary<string, object>;
        
        if (!json.TryGetValue("id", out id))
        {
            Debug.Log("Didn't recieved <user ID> in json");
            isValid = false;
        }
        else
        {
            print("id=" + id);
            PlayerPrefs.SetString("id", id.ToString());
            GameManager.s_GameManger.m_User.ID = id.ToString();
        }

        if (!json.TryGetValue("name", out name))
        {
            Debug.Log("Didn't recieved <user Name> in json");
//            isValid = false;
        }
        else
        {
            print("name=" + name);
            PlayerPrefs.SetString("name", name.ToString());
            GameManager.s_GameManger.m_User.Name = name.ToString();
        }

        if (!json.TryGetValue("email", out email))
        {
            Debug.Log("Didn't recieved <user Email> in json");
            isValid = false;
        }
        else
        {
            print("email=" + email);
            PlayerPrefs.SetString("email", email.ToString());
            GameManager.s_GameManger.m_User.Email = email.ToString();
        }

        if (!json.TryGetValue("birthday", out birthday))
        {
            Debug.Log("Didn't recieved <user Birthday> in json");
            //isValid = false;
        }
        else
        {
            print("birthday=" + birthday);
            PlayerPrefs.SetString("birthday", birthday.ToString());
            GameManager.s_GameManger.m_User.Birthday = birthday.ToString();
        }

        return isValid;
    }
}
