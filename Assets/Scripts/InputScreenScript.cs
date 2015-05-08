using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputScreenScript : MonoBehaviour {

	public InputField m_teamName;
	public InputField m_stadiumName;
    public InputField m_ManagerName;
	public Text m_error;
	private bool m_isValid;
	public string m_nextScene;
	// Use this for initialization
	void Start () {
		m_error.text = "";
	}


	public void OnContinueClick()
    {
		m_isValid = true;
        SoundManager.s_SoundManager.playClickSound();
		if (m_teamName.textComponent.text == null || m_teamName.textComponent.text.Length == 0) {
			m_error.text = "Team name cant be empty String";
			m_isValid = false;
		}

		if (m_stadiumName.textComponent.text == null || m_stadiumName.textComponent.text.Length == 0) {
			m_error.text = "Stadium name cant be empty String";
			m_isValid = false;
		}

        if (string.IsNullOrEmpty(m_ManagerName.text))
        {
            m_error.text = "Your name cant be empty String";
            m_isValid = false;
        }

		if (m_isValid)
		{
		    StartCoroutine(sendNewTeam());
			//GameManager.s_GameManger.m_myTeam.SetName(m_teamName.textComponent.text);
			//GameManager.s_GameManger.m_myTeam.SetStadiumName(m_stadiumName.textComponent.text);
			//Debug.Log("Stadium name is "+m_stadiumName.textComponent.text);
			//Debug.Log("Team name is "+m_teamName.textComponent.text);
			//Application.LoadLevel(m_nextScene);

		}

	}

    IEnumerator sendNewTeam()
    {
        WWWForm form = new WWWForm();
        
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("email", GameManager.s_GameManger.m_User.Email);
        form.AddField("teamName", m_teamName.text);
        form.AddField("stadiumName", m_stadiumName.text);
        form.AddField("coachName", m_ManagerName.text);

        WWW request = new WWW(GameManager.URL + "newTeamUser", form);

        Debug.Log("Sending team data");
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            print(request.text);
            if (request.text == "ok")
            {
                StartCoroutine(syncClientDB());
            }
        }
        Debug.Log("End of addNewTeamUser()");
    }

    IEnumerator syncClientDB()
    {
        WWWForm form = new WWWForm();
        Debug.Log("sending sync request for user: " + PlayerPrefs.GetString("email"));
        form.AddField("email", PlayerPrefs.GetString("email"));
        WWW request = new WWW(GameManager.URL + "getInfoByEmail", form);

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
            //k_IsDataLoaded = true;
            Application.LoadLevel("MainScene");
        }
        Debug.Log("End of addNewFBUser()");

    }
}
