using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GoalEvent : IComparable<GoalEvent>
{
    string m_PlayerName;
    int m_minute;
    private bool m_IsHomeTeam;

    public bool IsHomeTeam
    {
        get { return m_IsHomeTeam; }
        set { m_IsHomeTeam = value; }
    }

    public int Minute
    {
        get { return m_minute; }
        set { m_minute = value; }
    }
    public string PlayerName
    {
        get { return m_PlayerName; }
        set { m_PlayerName = value; }
    }

    public int CompareTo(GoalEvent i_otherEvent)
    {
        if (this.Minute < i_otherEvent.Minute)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}

public class Scoarboard : MonoBehaviour
{
    public float m_TimeToSimulateGame = 30;
    public Text m_HomeTeamName;
    public Image m_HomeTeamLogo;
    public Text m_AwayTeamName;
    public Image m_AwayTeamLogo;
    public Text m_CrowdAttendence;
    public Text m_FinalScore;
    public Text m_CurrentMinute;
    public GameObject m_Popup;
    public Text m_SkipButtonText;
    public Button m_SkipButton;
    public Button m_BackButton;
    public GoalScorerGUI[] m_GoalScorers;

    private GoalEvent[] m_GoalEvents;
    private TimeSpan m_MinutesLeft;
    private MatchInfo m_LastMatchInfo;

    private float m_SecondsToDeacresEachUpdate;
    private const float k_SecondsInGame = 90 * 60;
    private static readonly TimeSpan sr_NintyMins = new TimeSpan(0, 90, 0);
    private int m_LastGoalIdx = 0;
    private int m_HomeGoals = 0;
    private int m_AwayGoals = 0;
    private bool m_HasPressedSkip = false;
    private bool m_HasPlayedSound = false;

    void Start()
    {
        m_LastMatchInfo = GameManager.s_GameManger.m_myTeam.GetLastMatchInfo();
        //m_LastMatchInfo = new MatchInfo("TeamA", "TeamB", 3, 4, 255, "3 5 8 4", 2, 3);

        m_GoalEvents = Simulate(m_LastMatchInfo.GetHomeGoals(), m_LastMatchInfo.GetAwayGoals(), m_LastMatchInfo.ScoredPlayers);
        m_HomeTeamName.text = m_LastMatchInfo.GetHomeTeamString();
        m_AwayTeamName.text = m_LastMatchInfo.GetAwayTeamString();
        m_FinalScore.text = string.Format("0 - 0");
        //m_FinalScore.text = string.Format("{0} - {1}", lastMatch.GetHomeGoals(), lastMatch.GetAwayGoals());
        m_CurrentMinute.text = "00:00";
        m_CrowdAttendence.text = string.Format("Crowd: {0}", m_LastMatchInfo.GetTotalCrowd());

        // Init logos
        m_HomeTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(m_LastMatchInfo.HomeTeamLogoIdx);
        m_AwayTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(m_LastMatchInfo.AwayTeamLogoIdx);

        m_MinutesLeft = TimeSpan.Zero;
        m_SecondsToDeacresEachUpdate = k_SecondsInGame / m_TimeToSimulateGame;

        if (GameManager.s_GameManger.HasWatchedMatch)
        {
            m_GoalEvents = GameManager.s_GameManger.LastGameSimulation;
            dontDisplaySimulation();
        }
        else
        {
            GameManager.s_GameManger.HasWatchedMatch = true;
            GameManager.s_GameManger.LastGameSimulation = m_GoalEvents;
        }
    }

    void Update()
    {
        if (!m_HasPressedSkip && m_MinutesLeft < sr_NintyMins)
        {
            m_CurrentMinute.text = string.Format("{0:D2}:{1:D2}", (int) m_MinutesLeft.TotalMinutes,
                m_MinutesLeft.Seconds);

            m_MinutesLeft = m_MinutesLeft.Add(TimeSpan.FromSeconds(Time.deltaTime * m_SecondsToDeacresEachUpdate));
            shouldAddGoal();
        }
        else
        {
            m_CurrentMinute.text = "90:00";
            m_FinalScore.text = string.Format("{0} - {1}", m_LastMatchInfo.GetHomeGoals(),
                m_LastMatchInfo.GetAwayGoals());
            if (!m_HasPlayedSound)
            {
                playSound();
                m_HasPlayedSound = true;
                m_SkipButtonText.text = "CONTINUE";
            }
        }
    }

    private void shouldAddGoal()
    {
        if (m_LastGoalIdx < m_GoalEvents.Length && m_GoalEvents[m_LastGoalIdx].Minute <= (int) m_MinutesLeft.TotalMinutes)
        {
            if (m_GoalEvents[m_LastGoalIdx].IsHomeTeam)
            {
                m_HomeGoals++;
            }
            else
            {
                m_AwayGoals++;
            }

            m_FinalScore.text = string.Format("{0} - {1}", m_HomeGoals, m_AwayGoals);
            createNewGoalGUI();
            m_LastGoalIdx++;
        }
    }

    private void createNewGoalGUI()
    {
        int currectIndx = m_GoalScorers.Length - m_LastGoalIdx - 1;
        m_GoalScorers[currectIndx].MySetActive(m_GoalEvents[m_LastGoalIdx].IsHomeTeam, m_GoalEvents[m_LastGoalIdx].PlayerName, m_GoalEvents[m_LastGoalIdx].Minute);
    }

    private void dontDisplaySimulation()
    {
        m_HasPressedSkip = true;
        m_SkipButton.gameObject.SetActive(false);
        m_BackButton.gameObject.SetActive(true);
        for (int i = m_LastGoalIdx; i < m_GoalEvents.Length; i++)
        {
            createNewGoalGUI();
            m_LastGoalIdx++;
        }
    }

    public void OnSkipClick()
    {
        m_HasPressedSkip = true;
        for (int i = m_LastGoalIdx; i < m_GoalEvents.Length; i++)
        {
            createNewGoalGUI();
            m_LastGoalIdx++;
        }
        m_Popup.SetActive(true);
    }

	private void playSound()
    {
		eResult lastMatch = GameManager.s_GameManger.m_myTeam.GetLastResult();
		switch (lastMatch) {

			case eResult.Draw:
				SoundManager.s_SoundManager.playDrawSound();
			break;

			case eResult.Won:
                SoundManager.s_SoundManager.playWinSound();
			break;

			case eResult.Lost:
                SoundManager.s_SoundManager.playLoseSound();
			break;

		}
	}

    public GoalEvent[] Simulate(int i_HomeTeamGoals, int i_AwayTeamGoals, string i_PlayersScore)
    {
        GoalEvent[] timeLine = new GoalEvent[i_HomeTeamGoals + i_AwayTeamGoals];
        string playersScoreIndx = i_PlayersScore.Trim();
        string[] playersIndexs = new string[0];
        if (!string.IsNullOrEmpty(playersScoreIndx))
        {
            playersIndexs = playersScoreIndx.Split(' ');
        }
        
        bool isMyTeamAtHome = m_LastMatchInfo.GetHomeTeamString() == GameManager.s_GameManger.m_myTeam.Name;

        for (int i = 0; i < timeLine.Length; i++)
        {
            timeLine[i] = new GoalEvent();
            timeLine[i].Minute = Random.Range(1, 90);
        }

        for (int i = 0; i < playersIndexs.Length; i++)
        {
            timeLine[i].PlayerName = GameManager.s_GameManger.m_MySquad.Players[int.Parse(playersIndexs[i])].GetFullName();
            //timeLine[i].PlayerName = NamesUtilsScript.GetRandomName();
            timeLine[i].IsHomeTeam = isMyTeamAtHome;
        }

        for (int i = playersIndexs.Length; i < i_HomeTeamGoals + i_AwayTeamGoals; i++)
        {
            timeLine[i].PlayerName = NamesUtilsScript.GetRandomName();
            timeLine[i].IsHomeTeam = !isMyTeamAtHome;
        }

        Array.Sort(timeLine);
        return timeLine;
    }

    string getWinOrLoseStr()
    {
        MatchInfo lastGame = GameManager.s_GameManger.m_myTeam.GetLastMatchInfo();
        bool k_IsHomeMatch = GameManager.s_GameManger.m_myTeam.IsLastGameIsHomeGame;
        string vs = k_IsHomeMatch ? lastGame.GetAwayTeamString() : lastGame.GetHomeTeamString();
        switch (GameManager.s_GameManger.m_myTeam.GetLastResult())
        {
            case eResult.Draw:
                return string.Format("drawn {0}-{1} vs. {2}", lastGame.GetHomeGoals(), lastGame.GetAwayGoals(), vs);
            case eResult.Lost:
                return string.Format("lost {0}-{1} vs. {2}", lastGame.GetHomeGoals(), lastGame.GetAwayGoals(), vs);
            case eResult.Won:
                return string.Format("won {0}-{1} vs. {2}", lastGame.GetHomeGoals(), lastGame.GetAwayGoals(), vs);
        }

        // Not suppose to reach here!
        return "";
    }
}
