using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainGUI : MonoBehaviour
{

    public Text m_LeagueText;
    public Text m_DetailsText;
    public Text m_BucketText;
    public Text m_TeamNameText;
    public string m_NextScene = "MatchResultScene";

    void Start()
    {
        m_LeagueText.text = "LEAGUE\n" + getTeamPosition(GameManager.s_GameManger.m_myTeam);
        TeamScript opponent = FixturesManager.s_FixturesManager.GetOpponentByTeam(GameManager.s_GameManger.m_myTeam);
        m_DetailsText.text = "Total Fans: " + GameManager.s_GameManger.m_myTeam.GetFanBase() + "\n" +
                                 "Next Match: " + opponent.GetName() + " - " + getTeamPosition(opponent);
        m_TeamNameText.text = GameManager.s_GameManger.m_myTeam.GetName();
    }

    void Update()
    {
        updateBucketGUI();
    }

    private void updateBucketGUI()
    {
        if (GameManager.s_GameManger.IsBucketFull())
        {
            m_BucketText.text = "COLLECT MONEY!\n$" + GameManager.s_GameManger.GetMoneyInBucket();
        }
        else
        {
            m_BucketText.text = GameManager.s_GameManger.GetNextEmptyTimeSpan().ToString().Split('.')[0] + "\n" +
                                "$" + GameManager.s_GameManger.GetMoneyInBucket() + "\n" +
                                "Collect Now";
        }
    }

    private string getTeamPosition(TeamScript i_Team)
    {
        int pos = GameManager.s_GameManger.GetTeamPosition(i_Team);
        string suffix = string.Empty;

        int ones = pos % 10;
        int tens = (int)Math.Floor(pos / 10M) % 10;

        if (tens == 1)
        {
            suffix = "th";
        }
        else
        {
            switch (ones)
            {
                case 1:
                    suffix = "st";
                    break;

                case 2:
                    suffix = "nd";
                    break;

                case 3:
                    suffix = "rd";
                    break;

                default:
                    suffix = "th";
                    break;
            }
        }
        return string.Format("{0}{1}", pos, suffix);
    }

    public void OnNextMatchClick()
    {
        FixturesManager.s_FixturesManager.ExecuteNextFixture();
        Application.LoadLevel(m_NextScene);
    }

    public void OnCollectMoneyClick()
    {
        GameManager.s_GameManger.EmptyBucket();
    }
}
