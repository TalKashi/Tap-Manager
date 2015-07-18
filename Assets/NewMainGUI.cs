using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewMainGUI : MonoBehaviour
{
    public Button m_NextMatchButton;
    public Text m_StartMatchTextBody;
    public Text m_StartMatchTextTitle;
    public Image m_HomeTeamLogo;
    public Image m_AwayTeamLogo;

    public Text m_LastMatchTextBody;
    public Image m_LastMatchHomeTeamLogo;
    public Image m_LastMatchAwayTeamLogo;

    public Image m_SquadIconBackground;
    public Image m_LeagueIconBackground;
    public Image m_ShopIconBackground;

    public Image m_ClubInfoImage;

    public Image m_InboxImage;
    public Text m_BonusText;
    
    public Button m_CollectButton;
    public GameObject m_LoadingImage;
	// Use this for initialization
	void Start ()
	{
	    GameManager.s_GameManger.CurrentSceneHeaderName = GameManager.k_Lobby;
        GameManager.s_GameManger.CurrentScene = GameManager.k_Lobby;

	    m_StartMatchTextTitle.text = string.Format("Vs. {0}", GameManager.s_GameManger.GetNextOpponent());
	    if (GameManager.s_GameManger.m_GameSettings.IsNextMatchAtHome)
	    {
	        m_HomeTeamLogo.sprite = GameManager.s_GameManger.GetMyTeamLogoBig();
	        m_AwayTeamLogo.sprite =
	            GameManager.s_GameManger.GetTeamLogoByName(GameManager.s_GameManger.m_GameSettings.NextOpponent);
	    }
	    else
	    {
            m_AwayTeamLogo.sprite = GameManager.s_GameManger.GetMyTeamLogoBig();
            m_HomeTeamLogo.sprite =
                GameManager.s_GameManger.GetTeamLogoByName(GameManager.s_GameManger.m_GameSettings.NextOpponent);
	    }

	    Color myColor = MyUtils.GetColorByTeamLogo(GameManager.s_GameManger.m_myTeam.LogoIdx);
	    m_SquadIconBackground.color = myColor;
        m_LeagueIconBackground.color = myColor;
        m_ShopIconBackground.color = myColor;

	    m_ClubInfoImage.sprite = GameManager.s_GameManger.GetMyTeamLogoBig();

	    m_InboxImage.sprite = GameManager.s_GameManger.m_User.Inbox.HasUnreadMessages
	        ? GameManager.s_GameManger.m_UnreadMailSprite
	        : GameManager.s_GameManger.m_ReadMailSprite;
	    updateStartMatchGUI();
        updateBucketGUI();
        updateLastMatcGUI();
        //m_ShopText.text = string.Format("Total Fans: {1}{0}Facilities Level: {2}{0}Stadium: {3}k seats",
        //    Environment.NewLine, GameManager.s_GameManger.m_myTeam.GetFanBase(),
        //    GameManager.s_GameManger.m_myTeam.Facilities + 1, GameManager.s_GameManger.m_myTeam.TotalSeats / 1000);
	   
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameManager.s_GameManger.IsLoadingData)
	    {
	        m_LoadingImage.SetActive(true);
	    }
	    else
	    {
            m_LoadingImage.SetActive(false);
            updateStartMatchGUI();
            updateBucketGUI();
            updateLastMatcGUI();
	    }
	    
	}

    void updateStartMatchGUI()
    {
        TimeSpan nextMatchTimeSpan = GameManager.s_GameManger.GetNextMatchTimeSpan();
        if (nextMatchTimeSpan <= TimeSpan.Zero)
        {
            m_NextMatchButton.interactable = true;
            if (GameManager.s_GameManger.m_GameSettings.NextOpponent == "none")
            {
                m_StartMatchTextTitle.text = "Go To New Season!";
                //m_StartMatchText.text = string.Format("You have finished in {0} place",
                //    MyUtils.AddOrdinal(GameManager.s_GameManger.GetMyPosition()));
            }
            else
            {
                m_StartMatchTextBody.text = "GO TO MATCH!";
                //m_StartMatchText.text = string.Format("vs. {0}", GameManager.s_GameManger.GetNextOpponent());
            }
            
        }
        else
        {
            //m_StartMatchTextTitle.text = "Last Match";
            m_NextMatchButton.interactable = false;
            if (GameManager.s_GameManger.m_GameSettings.NextOpponent == "none")
            {
                m_StartMatchTextTitle.text = string.Format("{0:D2}:{1:D2} until next season starts",
                    nextMatchTimeSpan.Minutes, nextMatchTimeSpan.Seconds);
            }
            else
            {
                
                m_StartMatchTextBody.text = string.Format("{0:D2}:{1:D2}", nextMatchTimeSpan.Minutes, nextMatchTimeSpan.Seconds);
            }
        }
    }

    private void updateLastMatcGUI()
    {
        MatchInfo lastMatchInfo = GameManager.s_GameManger.m_myTeam.GetLastMatchInfo();
        m_LastMatchTextBody.text = string.Format("{0} - {1}", lastMatchInfo.GetHomeGoals(), lastMatchInfo.GetAwayGoals());
        m_LastMatchHomeTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(lastMatchInfo.HomeTeamLogoIdx);
        m_LastMatchAwayTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(lastMatchInfo.AwayTeamLogoIdx);
    }

    void updateBucketGUI()
    {
        if (GameManager.s_GameManger.IsBucketFull())
        {
            m_BonusText.text = string.Format("{1} {0}", GameManager.s_GameManger.GetMoneyInBucket(), "COLLECT BONUS!");
            m_CollectButton.interactable = true;
        }
        else
        {
            TimeSpan nextEmptyTimeSpan = GameManager.s_GameManger.GetNextEmptyTimeSpan();
            m_BonusText.text = string.Format("BONUS: {0:D2}:{1:D2}", nextEmptyTimeSpan.Minutes, nextEmptyTimeSpan.Seconds);
            m_CollectButton.interactable = false;
        }
    }

    public void OnNextMatchClick()
    {
        StartCoroutine(GameManager.s_GameManger.SyncClientDB("MatchDevelopment"));
    }

    public void OnLastMatchClick()
    {
        Application.LoadLevel("MatchDevelopment");
    }

    IEnumerator sendNextMatchClick()
    {
        WWWForm form = new WWWForm();
        Debug.Log("sending request match result for user: " + GameManager.s_GameManger.m_User.Email);
        form.AddField("email", GameManager.s_GameManger.m_User.Email);
        WWW request = new WWW(GameManager.URL + "matchResult", form);

        Debug.Log("Sending matchResult request");
        yield return request;
        Debug.Log("Recieved matchResult response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            print(request.text);
            if (request.text == "null")
            {
                // TODO: Tell user to try again soon
            }
            else
            {
                Dictionary<string, object> json = Facebook.MiniJSON.Json.Deserialize(request.text) as Dictionary<string, object>;
                MyUtils.LoadTeamData(json, ref GameManager.s_GameManger.m_myTeam);
                MyUtils.LoadLeagueData(json, ref GameManager.s_GameManger.m_AllTeams);
                MyUtils.LoadBucketData(json, ref GameManager.s_GameManger.m_Bucket);
                MyUtils.LoadSquadData(json, ref GameManager.s_GameManger.m_MySquad);
                MyUtils.LoadGameSettings(json, ref GameManager.s_GameManger.m_GameSettings);
                MyUtils.LoadUserData(json, ref GameManager.s_GameManger.m_User);
                
            }
        }
        Debug.Log("End of sendNextMatchClick()");
    }

    IEnumerator syncClientDB()
    {
        WWWForm form = new WWWForm();
        Debug.Log("sending sync request for user: " + GameManager.s_GameManger.m_User.Email);
        form.AddField("email", GameManager.s_GameManger.m_User.Email);
        form.AddField("fbid", GameManager.s_GameManger.m_User.FBId);
        WWW request = new WWW(GameManager.URL + "getInfoByEmail", form);

        yield return request;

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            Debug.Log(request.text);
            Dictionary<string, object> json = Facebook.MiniJSON.Json.Deserialize(request.text) as Dictionary<string, object>;
            MyUtils.LoadTeamData(json, ref GameManager.s_GameManger.m_myTeam);
            MyUtils.LoadLeagueData(json, ref GameManager.s_GameManger.m_AllTeams);
            MyUtils.LoadBucketData(json, ref GameManager.s_GameManger.m_Bucket);
            MyUtils.LoadSquadData(json, ref GameManager.s_GameManger.m_MySquad);
            MyUtils.LoadGameSettings(json, ref GameManager.s_GameManger.m_GameSettings);
            MyUtils.LoadUserData(json, ref GameManager.s_GameManger.m_User);
            Application.LoadLevel("MatchResultScene");
        }
        Debug.Log("End of syncClientDB()");

    }

    public void OnCollectMoneyClick()
    {
        if (GameManager.s_GameManger.IsBucketFull())
        {
            StartCoroutine(sendEmptyBucketClick());
        }
    }

    public void OnChangeTeamNameClickTest(string i_NewTeamName)
    {
        string newTeamName;
        if (!string.IsNullOrEmpty(i_NewTeamName) && (newTeamName = i_NewTeamName.Trim()) != string.Empty)
        {
            StartCoroutine(changeTeamName(newTeamName));
        }
    }

    IEnumerator changeTeamName(string i_NewTeamName)
    {
        //m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        Debug.Log("id=" + GameManager.s_GameManger.m_User.ID);
        
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("name", i_NewTeamName);

        Debug.Log("newTeamName=" + i_NewTeamName);
        Debug.Log("Sending changeTeamName to server");
        WWW request = new WWW(GameManager.URL + "changeTeamName", form);
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            Debug.Log(request.text);
            // Check ok response
            switch (request.text)
            {
                case "ok":
                    GameManager.s_GameManger.m_myTeam.Name = i_NewTeamName;
                    break;
                case "null":
                    Debug.Log("WARN: DB out of sync!");
                    // TODO: Sync DB
                    break;
            }
        }

        //m_WaitingForServer = false;
    }

    IEnumerator sendEmptyBucketClick()
    {
        //m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        Debug.Log("id=" + GameManager.s_GameManger.m_User.ID);
        
        form.AddField("id", GameManager.s_GameManger.m_User.ID);

        Debug.Log("Sending sendEmptyBucketClick to server");
        WWW request = new WWW(GameManager.URL + "collectBucket", form);
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            Debug.Log(request.text);
            // Check ok response
            switch (request.text)
            {
                case "ok":
                    GameManager.s_GameManger.EmptyBucket();
                    break;
                case "null":
                    Debug.Log("WARN: DB out of sync!");
                    // TODO: Sync DB
                    break;
            }
        }

        //m_WaitingForServer = false;
    }
}
