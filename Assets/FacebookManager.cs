using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using Facebook;

public class FacebookManager : MonoBehaviour {

    public GameObject m_LoginButton;
    public GameObject m_LogoutButton;

    void Awake()
    {
        FB.Init(onFacebookInit, onHideUnity);
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
        if (www.error != null)
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
        headers.Add("Content-Type", "text/json");
        headers.Add("Content-Length", i_NewUserJson.Length.ToString());
        WWW www = new WWW("http://serge-pc:8080/newfbuser", encoding.GetBytes(i_NewUserJson), headers);
        
        Debug.Log("Sending user data");
        yield return www;
        Debug.Log("Finished sending user data");

        if (www.error != null)
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
