using UnityEngine;

public class GuestLogin : MonoBehaviour
{

    private const string k_GuestLoginKey = "id";

    private LoginSceneScript m_LoginSceneScript;

	void Start ()
	{
	    m_LoginSceneScript = GetComponentInParent<LoginSceneScript>();
        PlayerPrefs.DeleteAll();
        login();
	}


    private void login()
    {
        string id = PlayerPrefs.GetString(k_GuestLoginKey);

        if(!string.IsNullOrEmpty(id))
        {
            login(id);
        }
    }

    public void OnLoginButtonClicked()
    {
        Debug.Log("Button Clicked!");
      
        login(System.Guid.NewGuid().ToString());
    }

    private void login(string i_ID)
    {
        // {'id':'10153270532886624','name':'Tal Kashi','email":'shakikashi\u0040gmail.com'}
        //string jsonString = string.Format("{{\"id\":\"{0}\", }}", i_ID);
        PlayerPrefs.SetString(k_GuestLoginKey, i_ID);
        string jsonString = string.Format("{{\"id\":\"{0}\"}}", i_ID);
        Debug.Log(jsonString);

        StartCoroutine(m_LoginSceneScript.AddNewFBUser(jsonString));

    }
}
