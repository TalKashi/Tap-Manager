using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour
{

    public static MatchManager s_MatchManager;

    public float m_HomeAdvantage = 1f;
    public float m_MaxCrowdMultiplier = 1.5f;
    public float m_MinCrowdMultiplier = 0.8f;

    void Awake()
    {
        if (s_MatchManager == null)
        {
            s_MatchManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CalcResult(TeamScript i_HomeTeam, TeamScript i_AwayTeam)
    {
        float randomCrowdMultiplier = Random.Range(m_MinCrowdMultiplier, m_MaxCrowdMultiplier);
        //float homeTeamOdds = i_HomeTeam.GetWinOdds();
        //float awayTeamOdds = i_AwayTeam.GetWinOdds();
        int crowdAtMatch = (int) (i_HomeTeam.GetFanBase() * randomCrowdMultiplier); // / 100000 * randomFansMultiplier;
        // crowdAtMatch should be bounded by stadium size

        float outcome = Random.Range(0, 1);


        int homeTeamGoals;
        int awayTeamGoals;
        eResult eHomeResult;
        eResult eAwayResult;
        if (outcome < 0.3)
        {
            // Home team win
            homeTeamGoals = Random.Range(1, 5);
            awayTeamGoals = Random.Range(0, homeTeamGoals);
            eHomeResult = eResult.Won;
            eAwayResult = eResult.Lost;
        }
        else if (outcome < 0.6)
        {
            // Tie
            homeTeamGoals = Random.Range(1, 5);
            awayTeamGoals = homeTeamGoals;
            eHomeResult = eResult.Draw;
            eAwayResult = eResult.Draw;
        }
        else
        {
            // Away team win
            awayTeamGoals = Random.Range(1, 5);
            homeTeamGoals = Random.Range(0, awayTeamGoals);
            eHomeResult = eResult.Lost;
            eAwayResult = eResult.Won;
        }
        bool v_isHomeTeam = true;
        MatchInfo matchInfo = new MatchInfo(i_HomeTeam, i_AwayTeam, homeTeamGoals, awayTeamGoals, crowdAtMatch);
        i_HomeTeam.UpdateMatchPlayed(eHomeResult, matchInfo, v_isHomeTeam);
        i_AwayTeam.UpdateMatchPlayed(eAwayResult, matchInfo, !v_isHomeTeam);
    }
}

public class MatchInfo
{
    private TeamScript m_HomeTeam;
    private TeamScript m_AwayTeam;
    private int m_HomeTeamGoals;
    private int m_AwayTeamGoals;
    private int m_CrowdAtMatch;

    public MatchInfo(TeamScript i_HomeTeam, TeamScript i_AwayTeam, int i_HomeTeamGoals, int i_AwayTeamGoals, int i_CrowdAtMatch)
    {
        m_HomeTeam = i_HomeTeam;
        m_AwayTeam = i_AwayTeam;
        m_HomeTeamGoals = i_HomeTeamGoals;
        m_AwayTeamGoals = i_AwayTeamGoals;
        m_CrowdAtMatch = i_CrowdAtMatch;
    }

    public int GetHomeGoals()
    {
        return m_HomeTeamGoals;
    }

    public int GetAwayGoals()
    {
        return m_AwayTeamGoals;
    }

    public int GetTotalCrowd()
    {
        return m_CrowdAtMatch;
    }

    public string GetHomeTeamString()
    {
        return m_HomeTeam.GetName();
    }

    public string GetAwayTeamString()
    {
        return m_AwayTeam.GetName();
    }
}
