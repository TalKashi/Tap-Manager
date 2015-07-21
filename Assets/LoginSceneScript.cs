using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine.UI;

public class LoginSceneScript : MonoBehaviour {

    public GameObject m_Login;
    private Sprite m_ProfilePic;
    public GameObject m_LoadingText;
    public GameObject m_GenericPopup;

    bool k_IsDataLoaded = false;
    bool k_LoadingData = true;

    const string SERVER = GameManager.URL;

    void Awake()
    {
#if UNITY_EDITOR
        //PlayerPrefs.DeleteAll();
#endif
        m_Login.SetActive(false);
        m_LoadingText.SetActive(k_LoadingData);
        FB.Init(onInit);
    }

    void Start()
    {
        GameManager.s_GameManger.m_GenericPopup = m_GenericPopup;
    }

    void Update()
    {
        if (k_IsDataLoaded)
        {
            Application.LoadLevel("LobbyDevelopment");
        }
        m_LoadingText.SetActive(k_LoadingData);
    }

    void onInit()
    {
        
        if (FB.IsLoggedIn)
        {
            m_Login.SetActive(false);
            fbLoginCallback(null);
        }
        else
        {
            string id = PlayerPrefs.GetString("id");

            if (!string.IsNullOrEmpty(id))
            {
                login(id);
            }
            else
            {
                k_LoadingData = false;
                m_Login.SetActive(true);
            }
            
        }
    }

    public void OnGuesLoginButtonClicked()
    {
        Debug.Log("Button Clicked!");

        login(System.Guid.NewGuid().ToString());
    }

    private void login(string i_ID)
    {
        // {'id':'10153270532886624','name':'Tal Kashi','email":'shakikashi\u0040gmail.com'}
        //string jsonString = string.Format("{{\"id\":\"{0}\", }}", i_ID);
        k_LoadingData = true;
        PlayerPrefs.SetString("id", i_ID);
        string jsonString = string.Format("{{\"id\":\"{0}\"}}", i_ID);
        Debug.Log(jsonString);

        StartCoroutine(AddNewFBUser(jsonString));

    }

    public void Login()
    {
        k_LoadingData = true;
        FB.Login("public_profile,email", fbLoginCallback);
    }

    void fbLoginCallback(FBResult i_FbResult)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("facebookLogIn worked");
            m_Login.SetActive(false);
            FB.API("me?fields=id,name,email", HttpMethod.GET, onGettingUserDataFromFB);
            //FB.API("me/picture?width=128&height=128", HttpMethod.GET, onGettingUserPicture);
        }
        else
        {
            k_LoadingData = false;
            m_Login.SetActive(true);
            Debug.Log("facebookLogIn failed");
        }
    }

    void onGettingUserPicture(FBResult i_Response)
    {
        if (!string.IsNullOrEmpty(i_Response.Error))
        {
            Debug.LogError("ERROR: " + i_Response.Error);
            return;
        }

        m_ProfilePic = Sprite.Create(i_Response.Texture, new Rect(0, 0, 128, 128), new Vector2(0, 0));
    }

    void onGettingUserDataFromFB(FBResult i_FBResult)
    {
        if (!string.IsNullOrEmpty(i_FBResult.Error))
        {
            Debug.LogError("ERROR: " + i_FBResult.Error);

            const string k_ErrorMsg =
                @"Failed to get information from Facebook
Check that you are logged into Facebook or connect as a guest";

            MyUtils.DisplayErrorMessage(m_GenericPopup, k_ErrorMsg);

            return;
        }

        if (GameManager.s_GameManger.m_User == null)
        {
            GameManager.s_GameManger.m_User = new User();
        }

        if (verifyAllDataPresent(i_FBResult.Text))
        {
            StartCoroutine(AddNewFBUser(i_FBResult.Text));
        }
        else
        {
            Debug.LogError("Missing user data from facebook!");
            const string k_ErrorMsg =
@"Failed to get required information from Facebook
Please check that you give permission for basic information from Facebook or connect as guest";

            MyUtils.DisplayErrorMessage(m_GenericPopup, k_ErrorMsg);
        }
    }

    public IEnumerator AddNewFBUser(string i_NewUserJson)
    {
        //UTF8Encoding encoding = new UTF8Encoding();
        //Dictionary<string, string> headers = new Dictionary<string, string>();
        //headers.Add("Content-Type", "text/json");
        //headers.Add("Content-Length", i_NewUserJson.Length.ToString());

        WWWForm wwwform = new WWWForm();
        wwwform.AddField("json", i_NewUserJson);

        WWW www = new WWW(SERVER + "loginUser", wwwform);

        Debug.Log("Sending user data");
        yield return www;
        Debug.Log("Finished sending user data");

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("ERROR: " + www.error);
            MyUtils.DisplayErrorMessage(m_GenericPopup);
            k_LoadingData = false;
            m_Login.SetActive(true);
        }
        else
        {
            print(www.text);
            if (www.text == "null")
            {
                // Go to new team scene
                PlayerPrefs.DeleteKey(InputScript.k_InputScene);
                Application.LoadLevel("InputDevelopment");
            }
            else
            {
                StartCoroutine(GameManager.s_GameManger.SyncClientDB("LobbyDevelopment"));
            }
            // Check ok response
            // if new user go to new team screen
            // else go to home page with team data
        }
        Debug.Log("End of AddNewFBUser()");
    }

    public IEnumerator syncClientDB()
    {
        WWWForm form = new WWWForm();
        Debug.Log("sending sync request for user: " + PlayerPrefs.GetString("id"));
        form.AddField("email", PlayerPrefs.GetString("email"));
        form.AddField("id", PlayerPrefs.GetString("id"));
        WWW request = new WWW(SERVER + "getInfoById", form);

        yield return request;
        
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
            MyUtils.DisplayErrorMessage(m_GenericPopup);
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
            if (m_ProfilePic != null)
            {
                GameManager.s_GameManger.m_User.ProfilePic = m_ProfilePic;
            }
            k_IsDataLoaded = true;
        }
        Debug.Log("End of AddNewFBUser()");

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
            //isValid = false;
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
