using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using Facebook;

public class FacebookManager : MonoBehaviour {

    public static FacebookManager s_FBManger;

    public GameObject m_LoginButton;
    public GameObject m_LogoutButton;

    public const string URL = "http://serge-pc:";
    public const string PORT = "3000";

    void Awake()
    {
        if (s_FBManger == null)
        {
            s_FBManger = this;
            //FB.Init(null);
            //loadData();
            //StartCoroutine(loadDataFromServer());
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //FB.Init(onFacebookInit, onHideUnity);
        //StartCoroutine(loadDataFromServer());
    }

    public void Init(InitDelegate i_Init)
    {
        FB.Init(i_Init);
    }

    private IEnumerator loadDataFromServer()
    {
        Debug.Log("Requesting data from server: " + URL + PORT + "/getUser");
        WWWForm form = new WWWForm();
        form.AddField("email", "shakikashi@gmail.com");
        WWW www = new WWW(URL + PORT + "/getUser", form);
        yield return www;
        Debug.Log("Got response");
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("ERROR: " + www.error);
            // Notify player to try again or something
        }
        else
        {
            Debug.Log("Parsing json data data\n" + www.text);
            Dictionary<string, object> json = Facebook.MiniJSON.Json.Deserialize(www.text) as Dictionary<string, object>;
            loadDataFromJson(json);
            Debug.Log("Finished parsing json data data");
        }
    }

    private void loadDataFromJson(Dictionary<string, object> i_Json)
    {
        object dict;
        TeamScript m_myTeam = null;

        if (m_myTeam == null)
        {
            m_myTeam = new TeamScript();
        }

        foreach (string key in i_Json.Keys) 
        {
            print("key=" + key + ";value=" + i_Json[key]);
        }
        if (i_Json.TryGetValue("team", out dict))
        {
            object id, shopDict;
            if (((Dictionary<string, object>)dict).TryGetValue("id", out id))
            {
                m_myTeam.ID = id.ToString();
                Debug.Log("id=" + id);
            }
            if (((Dictionary<string, object>)dict).TryGetValue("shop", out shopDict))
            {
                object fansLevel, facilitiesLevel, stadiumLevel;
                if (((Dictionary<string, object>)shopDict).TryGetValue("facilitiesLevel", out facilitiesLevel))
                {
                    m_myTeam.Facilities = float.Parse(facilitiesLevel.ToString());
                    Debug.Log("facilitiesLevel=" + facilitiesLevel);
                }

                if (((Dictionary<string, object>)shopDict).TryGetValue("fansLevel", out fansLevel))
                {
                    m_myTeam.Fans = float.Parse(fansLevel.ToString());
                    Debug.Log("fansLevel=" + fansLevel);
                }

                if (((Dictionary<string, object>)shopDict).TryGetValue("stadiumLevel", out stadiumLevel))
                {
                    m_myTeam.Stadium = float.Parse(stadiumLevel.ToString());
                    Debug.Log("stadiumLevel=" + stadiumLevel);
                }
            }
        }
    }

    private void onFacebookInit()
    {
        Debug.Log("Started Facebook Init");

        if (FB.IsLoggedIn)
        {
            Debug.Log("Already logged in");
            m_LoginButton.SetActive(false);
            m_LogoutButton.SetActive(true);
        }
        else
        {
            Debug.Log("Is NOT logged in");
            m_LoginButton.SetActive(true);
            m_LogoutButton.SetActive(false);
        }
    }

    private void onHideUnity(bool i_IsGameShown)
    {
        // Dont care ATM
    }

    public void FacebookLogIn()
    {
        FB.Login("public_profile,email", authCallback);
    }

    public void FacebookLogOut()
    {
        FB.Logout();
        if (FB.IsLoggedIn)
        {
            Debug.Log("facebooklogout didnt worked");
            m_LoginButton.SetActive(false);
            m_LogoutButton.SetActive(true);
            

        }
        else
        {
            m_LoginButton.SetActive(true);
            m_LogoutButton.SetActive(false);
            Debug.Log("facebookLogout worked");
        }
    }

    private void authCallback(FBResult i_FBResult)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("facebookLogIn worked");
            m_LoginButton.SetActive(false);
            m_LogoutButton.SetActive(true);
            FB.API("me?fields=id,name,email", HttpMethod.GET, onGettingUserDataFromFB);

        }
        else
        {
            m_LoginButton.SetActive(true);
            m_LogoutButton.SetActive(false);
            Debug.Log("facebookLogIn failed");
        }
    }

    private void onGettingUserDataFromFB(FBResult i_FBResult)
    {
        if (i_FBResult.Error != null)
        {
            Debug.LogError("ERROR: " + i_FBResult.Error);
            return;
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

    private bool verifyAllDataPresent(string i_JsonStr)
    {
        object id;
        object name;
        object email;
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
        }
        if (!json.TryGetValue("name", out name))
        {
            Debug.Log("Didn't recieved <user Name> in json");
            isValid = false;
        }
        else
        {
            print("name=" + name);
        }
        if (!json.TryGetValue("email", out email))
        {
            Debug.Log("Didn't recieved <user Email> in json");
            isValid = false;
        }
        else
        {
            print("email=" + email);
        }

        return isValid;
    }

    private IEnumerator sendAuthParametersToServer(string i_AuthData)
    {
        WWWForm form = new WWWForm();
        Debug.Log("i_AuthData=" + i_AuthData);
        form.AddField("user", i_AuthData);
        Dictionary<string,string> headers = form.headers;
        foreach (string key in headers.Keys)
        {
            print(key + " = " + headers[key]);
        }
        
        
        WWW www = new WWW("http://localhost:3000/facebookauth", form);
        
        
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            foreach (string key in www.responseHeaders.Keys)
            {
                print(key + "=" + www.responseHeaders[key]);
            }
        }
        Debug.Log("End of connectToServer()");
    }

    private IEnumerator addNewFBUser(string i_NewUserJson)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        Dictionary<string, string> headers = new Dictionary<string, string>();
        //headers.Add("Content-Type", "text/json");
        //headers.Add("Content-Length", i_NewUserJson.Length.ToString());
        WWWForm wwwform = new WWWForm();
        wwwform.AddField("json", i_NewUserJson);
        //WWW www = new WWW("http://serge-pc:3000/newUser", encoding.GetBytes(i_NewUserJson), headers);
        WWW www = new WWW("http://serge-pc:3000/newUser", wwwform);
        
        Debug.Log("Sending user data");
        yield return www;
        Debug.Log("Finished sending user data");

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            // Check ok response
            // if new user go to new team screen
            // else go to home page with team data
        }
        Debug.Log("End of addNewFBUser()");
    }
}
