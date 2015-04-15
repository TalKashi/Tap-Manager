using UnityEngine;
using System.Collections;

public enum eResult {Won, Lost, Draw}

public class TeamScript {

	float m_fansLevel = 0 ;
	float m_facilitiesLevel = 0;
	float m_stadiumLevel = 0;
	int m_played = 0;
	int m_won = 0;
	int m_lost = 0;
	int m_drawn = 0;
	int m_for = 0;
	int m_against = 0;
	int m_points = 0;
	int m_TotalCrowd = 0;
	int m_homeGames = 0;
    int m_AdditionalFans = 0;
    string m_Name;
    MatchInfo m_LastGameInfo;
    bool m_IsLastGameIsHomeGame;
	

	public void UpdateFansLevel(float i_fans)
	{
		m_fansLevel += i_fans;
	}

	public void UpdateFacilitiesLevel(float i_facilitiesLevel)
	{
		m_facilitiesLevel += i_facilitiesLevel;
	}

	public void UpdateStadiumLevel(float i_stadiumLevel)
	{
		m_stadiumLevel += i_stadiumLevel;
	}

    public void UpdateMatchPlayed(eResult i_result, MatchInfo i_matchInfo, bool i_isHomeMatch)
	{
		switch (i_result) 
		{
		case eResult.Won:
			m_won++;
            m_AdditionalFans += 10;
			break;

		case eResult.Lost:
			m_lost++;
            m_AdditionalFans -= 10;
			break;

		case eResult.Draw:
			m_drawn++;
            m_AdditionalFans++;
			break;
		}

        if (i_isHomeMatch)
        {
            m_for += i_matchInfo.GetHomeGoals();
            m_against += i_matchInfo.GetAwayGoals();
            m_homeGames++;
            m_TotalCrowd += i_matchInfo.GetTotalCrowd();
        }
        else
        {
            m_against += i_matchInfo.GetHomeGoals();
            m_for += i_matchInfo.GetAwayGoals();
        }
        m_IsLastGameIsHomeGame = i_isHomeMatch;
        m_LastGameInfo = i_matchInfo;
	}

	public void UpdateMatchLost()
	{
		m_lost++;
	}

	public void UpdateMatchDrawn()
	{
		m_drawn++;
	}

	public void UpdateForGoals(int i_for)
	{
		m_for += i_for;

	}

	public void UpdateAgainstGoals(int i_against)
	{
		m_against += i_against;
		
	}

    public float GetAverageCrowd()
    {
        return (float) m_TotalCrowd/m_homeGames;
    }

	public float GetFansLevel()
	{
		return m_fansLevel;
	}

	public float GetFacilitiesLevel()
	{
		return m_facilitiesLevel;
	}

	public float GetStadiumLevel()
	{
		return m_stadiumLevel;
	}

	public int GetMatchPlayed()
	{
		return m_won + m_lost + m_drawn;
	}

	public int GetMatchWon()
	{
		return m_won;
	}
	public int GetMatchLost()
	{
		return m_lost;
	}

    public int GetMatchDrawn()
	{
		return m_drawn;
	}

	public int GetGoalsFor()
	{
		return m_for;
	}

	public int GetGoalsAgainst()
	{
		return m_against;
	}

    public int GetGoalDiff()
    {
        return GetGoalsFor() - GetGoalsAgainst();
    }

	public int GetPoints()
	{
		return 3*m_won + m_drawn;
	}

    public int GetCrowdAtLastMatch()
    {
        if (m_IsLastGameIsHomeGame)
        {
            return m_LastGameInfo.GetTotalCrowd();
        }
        return 0;
    }

    public int GetTicketPrice()
    {
        return 10;
    }

	public float GetWinOdds()
	{
		/* Temp solution should returnssome function of(
		 * 	float m_fansLevel;
			float m_facilitiesLevel;
			float m_stadiumLevel;
			)
		 */
		return m_fansLevel + m_facilitiesLevel + m_stadiumLevel;
	}

	public int GetFanBase()
	{
		//Temp solution
		return (int)m_fansLevel * 10 + m_AdditionalFans;
	}

    public string GetName()
    {
        return m_Name;
    }

    public void SetName(string i_Name)
    {
        m_Name = i_Name;
    }

    public MatchInfo GetLastMatchInfo()
    {
        return m_LastGameInfo;
    }

    public int GetMerchandisePrice()
    {
        return 5;
    }

    public int GetSalary()
    {
        // 400 is avrage player salary
        // 15 is num of player
        // 600 is coach salary
        return 400 * 15 + 600;
    }

}
