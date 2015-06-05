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

    public void Login(FacebookDelegate i_Callback)
    {
        FB.Login("public_profile,email", i_Callback);
    }
}
