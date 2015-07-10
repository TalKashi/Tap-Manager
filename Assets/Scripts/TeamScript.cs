using System;
using UnityEngine;
using System.Collections;

public enum eResult {Won = 0, Lost=1, Draw=2}

public struct GamesStatistics 
{
    public int wins;
    public int losts;
    public int draws;
    public int goalsFor;
    public int goalsAgainst;
    public int homeGames;
    public int crowd;
}

public struct RecordsStatistics
{
    public int longestWinStreak;
    public int longestLoseStreak;
    public int longestWinlessStreak;
    public int longestUndefeatedStreak;
    public int biggestWinRecord;
    public int biggestLoseRecord;

    public int currentWinStreak;
    public int currentLoseStreak;
    public int currentWinlessStreak;
    public int currentUndefeatedStreak; 
}

[Serializable]
public class TeamScript
{
    GamesStatistics m_ThisSeasonStats;
    GamesStatistics m_AllTimeStats;
    RecordsStatistics m_RecordsStats;
	eResult m_lastResult;
	int m_fansLevel = 0 ;
	int m_facilitiesLevel = 0;
	int m_stadiumLevel = 0;
    int m_AdditionalFans = 0;
    private int m_LogoIdx;
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

    private int m_incomeFromTickets;
    private int m_incomeFromMerchandise;
    private int m_facilitiesCost;
    private int m_stadiumCost;
    private int m_salary;
    private int m_LastMatchInstantTrain;
    
    private int m_TotalInstantTrain;

    public string ID { get; set; }

    public GamesStatistics AllTimeStatistics
    {
        get { return m_AllTimeStats;}
    }

    public RecordsStatistics TeamRecords
    {
        get { return m_RecordsStats; }
        set { m_RecordsStats = value; }
    }

    public int TotalInstantTrain
    {
        get { return m_TotalInstantTrain; }
        set { m_TotalInstantTrain = value; }
    }

    public int LastMatchInstantTrain
    {
        get { return m_LastMatchInstantTrain; }
        set { m_LastMatchInstantTrain = value; }
    }

    public int LogoIdx
    {
        get { return m_LogoIdx; }
        set { m_LogoIdx = value; }
    }

    public float Salary
    {
        get { return m_salary; }
        set { m_salary = (int) value; }
    }

    public float StadiumCost
    {
        get { return m_stadiumCost; }
        set { m_stadiumCost = (int) value; }
    }

    public float FacilitiesCost
    {
        get { return m_facilitiesCost; }
        set { m_facilitiesCost = (int) value; }
    }

    public float IncomeFromMerchandise
    {
        get { return m_incomeFromMerchandise; }
        set { m_incomeFromMerchandise = (int) value; }
    }

    public float IncomeFromTickets
    {
        get { return m_incomeFromTickets; }
        set { m_incomeFromTickets = (int)value; }
    }

    public int TotalSeats
    {
        get { return Stadium * 1500 + 1000; }
    }

    public string Name 
    {
        get { return m_TeamName; } 
        set { m_TeamName = value; }
    }

    public int Fans
    {
        get { return m_fansLevel; }
        set { m_fansLevel = value; }
    }

    public int Facilities
    {
        get { return m_facilitiesLevel; }
        set { m_facilitiesLevel = value; }
    }

    public int Stadium
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

    public void SetAdditionalFans(int i_AdditionalFans)
    {
        m_AdditionalFans = i_AdditionalFans;
    }

    public void SetGamesStatistics(GamesStatistics i_GamesStatistics, bool i_IsThisSeason)
    {
        if (i_IsThisSeason)
        {
            m_ThisSeasonStats = i_GamesStatistics;
        }
        else
        {
            m_AllTimeStats = i_GamesStatistics;
        }
    }

    public void SetLastGameInfo(MatchInfo i_LatGameInfo)
    {
        m_LastGameInfo = i_LatGameInfo;
    }

    public void UpdateFansLevel(int i_fans)
	{
		m_fansLevel += i_fans;
	}

	public void UpdateFacilitiesLevel(int i_facilitiesLevel)
	{
		m_facilitiesLevel += i_facilitiesLevel;
	}

	public void UpdateStadiumLevel(int i_stadiumLevel)
	{
		m_stadiumLevel += i_stadiumLevel;
	}

    public void UpdateMatchPlayed(eResult i_result, MatchInfo i_matchInfo, bool i_isHomeMatch)
	{
		switch (i_result) 
		{
		case eResult.Won:
			m_ThisSeasonStats.wins++;
            m_AdditionalFans += 25;

            m_lastResult = eResult.Won;
			m_currentSequenceWin++; 
			m_currentSequenceLose = 0; 
			m_currentSequenceNotLose++;
			m_currentSequenceNotWin = 0;  
			break;

		case eResult.Lost:
			m_ThisSeasonStats.losts++;
            m_AdditionalFans -= 10;

            m_lastResult = eResult.Lost;
			m_currentSequenceWin = 0; 
			m_currentSequenceLose++; 
			m_currentSequenceNotLose = 0;
			m_currentSequenceNotWin++;
			break;

		case eResult.Draw:
			m_ThisSeasonStats.draws++;
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
            m_ThisSeasonStats.goalsFor += i_matchInfo.GetHomeGoals();
            m_ThisSeasonStats.goalsAgainst += i_matchInfo.GetAwayGoals();
            m_ThisSeasonStats.homeGames++;
            m_ThisSeasonStats.crowd += i_matchInfo.GetTotalCrowd();

        }
        else
		{
            m_ThisSeasonStats.goalsAgainst += i_matchInfo.GetHomeGoals();
            m_ThisSeasonStats.goalsFor += i_matchInfo.GetAwayGoals();
        }
		//m_currentResult = m_for - m_against;
        m_IsLastGameIsHomeGame = i_isHomeMatch;
        m_LastGameInfo = i_matchInfo;
		checkRecords ();
	}

    //public void UpdateMatchLost()
    //{
    //    m_lost++;
    //}

    //public void UpdateMatchDrawn()
    //{
    //    m_drawn++;
    //}

    //public void UpdateForGoals(int i_for)
    //{
    //    m_for += i_for;

    //}

    //public void UpdateAgainstGoals(int i_against)
    //{
    //    m_against += i_against;
		
    //}

	public eResult GetLastResult()
	{
		return m_lastResult;

	}

    public float GetAverageCrowd()
    {
        return (float) m_ThisSeasonStats.crowd/m_ThisSeasonStats.homeGames;
    }

	public int GetFansLevel()
	{
		return m_fansLevel;
	}

	public int GetFacilitiesLevel()
	{
		return m_facilitiesLevel;
	}

	public int GetStadiumLevel()
	{
		return m_stadiumLevel;
	}

	public int GetMatchPlayed()
	{
		return m_ThisSeasonStats.wins + m_ThisSeasonStats.losts + m_ThisSeasonStats.draws;
	}

	public int GetMatchWon()
	{
		return m_ThisSeasonStats.wins;
	}
	public int GetMatchLost()
	{
		return m_ThisSeasonStats.losts;
	}

    public int GetMatchDrawn()
	{
		return m_ThisSeasonStats.draws;
	}

	public int GetGoalsFor()
	{
		return m_ThisSeasonStats.goalsFor;
	}

	public int GetGoalsAgainst()
	{
		return m_ThisSeasonStats.goalsAgainst;
	}

    public int GetGoalDiff()
    {
        return GetGoalsFor() - GetGoalsAgainst();
    }

	public int GetPoints()
	{
		return 3*m_ThisSeasonStats.wins + m_ThisSeasonStats.draws;
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

    public int CalculateSalary()
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

		//m_currentResult = - m_for + m_against;

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
