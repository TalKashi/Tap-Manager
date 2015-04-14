using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour {

    public float m_HomeAdvantage = 1f;
    public float m_MaxCrowdMultiplier = 1.5f;
    public float m_MinCrowdMultiplier = 0.8f;

    public MatchResult CalcResult(Team i_HomeTeam, Team i_AwayTeam)
    {
        float randomCrowdMultiplier = Random.Range(m_MinCrowdMultiplier, m_MaxCrowdMultiplier);
        //float homeTeamOdds = i_HomeTeam.GetWinOdds();
        //float awayTeamOdds = i_AwayTeam.GetWinOdds();
        int crowdAtMatch = i_HomeTeam.GetFanBase() * randomCrowdMultiplier; // / 100000 * randomFansMultiplier;
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
            eHomeResult = Won;
            eAwayResult = Lost;
        }
        else if (outcome < 0.6)
        {
            // Tie
            homeTeamGoals = Random.Range(1, 5);
            awayTeamGoals = homeTeamGoals;
            eHomeResult = Draw;
            eAwayResult = Draw;
        }
        else
        {
            // Away team win
            awayTeamGoals = Random.Range(1, 5);
            homeTeamGoals = Random.Range(0, awayTeamGoals);
            eHomeResult = Lost;
            eAwayResult = Won;
        }
        bool v_isHomeTeam = true;
        i_HomeTeam.Update(eHomeResult, homeTeamGoals, awayTeamGoals, crowdAtMatch, v_isHomeTeam);
        i_AwayTeam.Update(eAwayResult, homeTeamGoals, awayTeamGoals, crowdAtMatch, !v_isHomeTeam);
    }
}
