using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewMainGUI : MonoBehaviour
{
    public Text m_ShopText;
    public Text m_BucketText;
    public Text m_StartMatchText;
    public Text m_StartMatchTextTitle;
    public Text m_SquadText;

	// Use this for initialization
	void Start ()
	{
	    updateStartMatchGUI();
	    m_ShopText.text = string.Format("Total Fans: {1}{0}Facilities Level: {2}{0}Stadium: {3}k seats",
	        Environment.NewLine, GameManager.s_GameManger.m_myTeam.GetFanBase(),
	        GameManager.s_GameManger.m_myTeam.Facilities + 1, GameManager.s_GameManger.m_myTeam.TotalSeats / 1000);
	    updateBucketGUI();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    updateStartMatchGUI();
	    updateBucketGUI();
	}

    void updateStartMatchGUI()
    {
        if (GameManager.s_GameManger.GetNextMatchTimeSpan() <= TimeSpan.Zero)
        {
            m_StartMatchTextTitle.text = "Go To Match!";
            m_StartMatchText.text = string.Format("vs. {0}", GameManager.s_GameManger.GetNextOpponent());
        }
        else
        {
            m_StartMatchTextTitle.text = "Next Match";
            m_StartMatchText.text = string.Format("{1} Until Kickoff{0}vs. {2}", Environment.NewLine,
            GameManager.s_GameManger.GetNextMatchTimeSpan().ToString().Split('.')[0]
            , GameManager.s_GameManger.GetNextOpponent());
        }
    }

    void updateBucketGUI()
    {
        if (GameManager.s_GameManger.IsBucketFull())
        {
            m_BucketText.text = string.Format("{1} {0:C0}", GameManager.s_GameManger.GetMoneyInBucket(), "Collect Money!");
        }
        else
        {
            m_BucketText.text = string.Format("{0}", GameManager.s_GameManger.GetNextEmptyTimeSpan().ToString().Split('.')[0]);
        }
    }

    public void OnNextMatchClick()
    {
        //if (GameManager.s_GameManger.GetNextMatchTimeSpan() > TimeSpan.Zero)
        //{
        //    // TODO: Tell user to wait until the time of the match
        //    return;
        //}

        StartCoroutine(GameManager.s_GameManger.SyncClientDB("MatchResultScene"));
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
