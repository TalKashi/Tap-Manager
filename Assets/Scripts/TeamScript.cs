﻿using UnityEngine;
using System.Collections;

public enum eResult {Won, Lost, Draw}

public class TeamScript {

	float m_fansLevel;
	float m_facilitiesLevel;
	float m_stadiumLevel;
	int m_played;
	int m_won;
	int m_lost;
	int m_drawn;
	int m_for;
	int m_against;
	int m_points;
	int m_TotalCrowd;
	int m_homeGames;
    string m_Name;
    MatchInfo m_LastGameInfo;
	

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
			break;

		case eResult.Lost:
			m_lost++;
			break;

		case eResult.Draw:
			m_drawn++;
			break;
		}

		m_for += i_matchInfo.GetHomeGoals();
        m_against += i_matchInfo.GetAwayGoals();

		if (i_isHomeMatch) 
		{
			m_homeGames++;
            m_TotalCrowd += i_matchInfo.GetTotalCrowd();
		}
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
		return m_played;
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

	public int GetPoints()
	{
		return 3*m_won + m_drawn;
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
		return (int)m_fansLevel * 10;
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

}