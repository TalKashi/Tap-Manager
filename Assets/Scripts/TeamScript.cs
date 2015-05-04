using System;
using UnityEngine;
using System.Collections;

public enum eResult {Won, Lost, Draw}

[Serializable]
public class TeamScript
{

	eResult m_lastResult;
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
    string m_TeamName;
    string m_StadiumName;
    [SerializeField]
    MatchInfo m_LastGameInfo;
    bool m_IsLastGameIsHomeGame;
	//Stats
	private int m_longestWinRecord = 0; 
	private int m_longestLoseRecord = 0; 
	private int m_longestNotLoseRecord = 0;
	private int m_longestNotWinRecord = 0; 
	private int m_biggestGameWinRecord = 0;
	private int m_biggestGameLoseRecord = 0; 

	private int m_currentSequenceWin = 0; 
	private int m_currentSequenceLose = 0; 
	private int m_currentSequenceNotLose = 0;
	private int m_currentSequenceNotWin = 0; 
	private int m_currentResult = 0;

    public string ID { get; set; }

    public float Fans
    {
        get { return m_fansLevel; }
        set { m_fansLevel = value; }
    }

    public float Facilities
    {
        get { return m_facilitiesLevel; }
        set { m_facilitiesLevel = value; }
    }

    public float Stadium
    {
        get { return m_stadiumLevel; }
        set { m_stadiumLevel = value; }
    }

    public eResult LastResult
    {
        get { return m_lastResult; }
        set { m_lastResult = value; }
    }

    public bool IsLastGameIsHomeGame
    {
        get { return m_IsLastGameIsHomeGame; }
        set { m_IsLastGameIsHomeGame = value; }
    }

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
            m_AdditionalFans += 25;

            m_lastResult = eResult.Won;
			m_currentSequenceWin++; 
			m_currentSequenceLose = 0; 
			m_currentSequenceNotLose++;
			m_currentSequenceNotWin = 0;  
			break;

		case eResult.Lost:
			m_lost++;
            m_AdditionalFans -= 10;

            m_lastResult = eResult.Lost;
			m_currentSequenceWin = 0; 
			m_currentSequenceLose++; 
			m_currentSequenceNotLose = 0;
			m_currentSequenceNotWin++;
			break;

		case eResult.Draw:
			m_drawn++;
            m_AdditionalFans++;

            m_lastResult= eResult.Draw;
			m_currentSequenceWin = 0; 
			m_currentSequenceLose = 0; 
			m_currentSequenceNotLose++;
			m_currentSequenceNotWin++;
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
		m_currentResult = m_for - m_against;
        m_IsLastGameIsHomeGame = i_isHomeMatch;
        m_LastGameInfo = i_matchInfo;
		checkRecords ();
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
	public eResult GetLastResult()
	{
		return m_lastResult;

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
        int fanBase =(int) (m_fansLevel+1) * 1000 + m_AdditionalFans;
	    if (fanBase < 0)
	    {
	        fanBase = 0;
	    }

	    return fanBase;
	}

    public string GetName()
    {
        return m_TeamName;
    }

    public void SetName(string i_Name)
    {
        m_TeamName = i_Name;
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
        PlayerScript[] allPlayers = GameManager.s_GameManger.m_MySquad.GetAllSquad();
        int total = 0;
        for (int i = 0; i < allPlayers.Length; i++)
        {
            total += allPlayers[i].GetSalary();
        }

        return total;
    }

    public void SetStadiumName(string i_StadiumName)
    {
        m_StadiumName = i_StadiumName;
    }

	public int GetlongestLoseRecord()
	{
		return m_longestLoseRecord;
	}
	public int GetlongestWinRecord()
	{
		return m_longestWinRecord;
	}
	public int GetlongestNotLoseRecord()
	{
		return m_longestNotLoseRecord;
	}
	public int GetlongestNotWinRecord()
	{
		return m_longestNotWinRecord;
	}

	private void checkRecords ()
	{
		if (m_currentSequenceLose > m_longestLoseRecord) {
			m_longestLoseRecord = m_currentSequenceLose;
		}

		if (m_currentSequenceNotLose > m_longestNotLoseRecord) {
			m_longestNotLoseRecord = m_currentSequenceNotLose;
		}

		if (m_currentSequenceNotWin > m_longestNotWinRecord) {
			m_longestNotWinRecord = m_currentSequenceNotWin;
		}

		if (m_currentSequenceWin > m_longestWinRecord) {
			m_longestWinRecord = m_currentSequenceWin;
		}

		m_currentResult = - m_for + m_against;

		if (m_currentResult > 0)
		{
			if(m_currentResult > m_biggestGameWinRecord)
			{
				m_biggestGameWinRecord = m_currentResult;
			}
			
		}else{
			if(m_currentResult < m_biggestGameLoseRecord)
			{
				m_biggestGameLoseRecord = m_currentResult;
			}
		}
		
	}

	private void updateLastResult()
	{
		if (m_currentResult > 0) {
			m_lastResult = eResult.Won;
		} else if (m_currentResult < 0) {
			m_lastResult = eResult.Lost;
		} else {
			m_lastResult = eResult.Draw;
		}
	}
}
