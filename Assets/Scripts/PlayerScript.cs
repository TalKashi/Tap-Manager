using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ePosition {GK,D,MF,S};

[System.Serializable]
public class PlayerScript {

	private ePosition m_position;
	private string m_firstName;
	private string m_lastName;
	private float m_salary;
	private bool m_isInjured;
	private int m_age;
	private int m_gamesPlayed = 0;
	private int m_goalsScored = 0;
	private int m_level;
	private int m_price;
	private int m_priceToBoost;
    private int m_NextBoostCap;
	private int m_boost = 0;
    private int m_CurrentBoost;
	private bool m_isPlaying;
	private int m_gamePower = 100;
	private int m_yearOfJoiningTheClub;
    [NonSerialized]
	public Sprite m_PlayerImage; // to remove?

    public int PlayerSpriteIndex { get; set; }
    public int ID { get; set; }

    public int NextBoostCap
    {
        get { return m_NextBoostCap; }
        set { m_NextBoostCap = value; }
    }

    public int CurrentBoost
    {
        get { return m_CurrentBoost; }
        set { m_CurrentBoost = value; }
    }

	public void SetPriceToBoostPlayer(int i_priceToBoost)
	{
		m_priceToBoost = i_priceToBoost;

	}

    public void SetSalary(float i_salary)
    {
        m_salary = i_salary;
    }
	
	public void SetPlayerLevel(int i_level)
	{
		m_level = i_level;
	}

	public void SetFirstName(string i_name)
	{
		m_firstName = i_name;
	}
	public void SetLastName(string i_name)
	{
		m_lastName = i_name;
	}

	public void SetIsPlaying(bool i_isPlaying)
	{
		m_isPlaying = i_isPlaying;
	}

	public void SetPosition(ePosition i_position)
	{
		m_position = i_position;
	}

	public int GetAge()
	{
		return m_age;
	}

    public void SetAge(int i_Age)
    {
        m_age = i_Age;
    }

	public ePosition getPlayerPosition ()
	{
		return m_position;
	}

	public int GetGoalScored ()
	{
		return m_goalsScored;
	}

	public bool IsInjered()
	{
		return m_isInjured;
	}

    public void SetIsInjured(bool i_IsInjured)
    {
        m_isInjured = i_IsInjured;
    }

    public void SetGamesPlayed(int i_GamesPlayed)
    {
        m_gamesPlayed = i_GamesPlayed;
    }

	public int GetPriceToBoostPlayer ()
	{
		return m_priceToBoost;
	}

	public int GetLevel()
	{
		return m_level;
	}

	public int GetGamePlayed()
	{
		return m_gamesPlayed;
	}

	public string getPlayerFirstName ()
	{
		return m_firstName;
	}

	public string getPlayerLastName ()
	{
		return m_lastName;
	}

	public string getPlayerShortName()
	{
		return m_firstName.Substring(0,1) + "." + m_lastName;
	}

	public int GetPlayerPrice()
	{
		return m_price;
	}

    public void SetPlayerPrice(int i_Price)
    {
        m_price = i_Price;
    }

	public Sprite getPlayerImage ()
	{
		return m_PlayerImage;
	}

    public void SetPlayerImage(Sprite i_Sprite)
    {
        m_PlayerImage = i_Sprite;
    }

	public void UpdateGamePlayed()
	{
		m_gamesPlayed++;
	}

	private void boostPlayer(int i_boost)
	{
        Debug.Log("i_boost=" + i_boost + ";m_CurrentBoost=" + m_CurrentBoost + ";m_NextBoostCap=" + m_NextBoostCap + ";PlayerBoostCostMultiplier=" + GameManager.s_GameManger.m_GameSettings.PlayerBoostCostMultiplier);
        m_CurrentBoost += i_boost;
        if (m_CurrentBoost >= m_NextBoostCap) 
		{
            Debug.Log("Old Salary=" + m_salary);
            m_CurrentBoost = m_CurrentBoost % m_NextBoostCap;
		    m_salary *= 1.1f;
		    m_NextBoostCap *= 2;
			m_level++;
            m_priceToBoost = (int)(m_priceToBoost*GameManager.s_GameManger.m_GameSettings.PlayerBoostCostMultiplier);
            Debug.Log("New Salary=" + m_salary);
		}
	}

    public void BoostPlayer()
    {
        boostPlayer(m_boost);
    }

    public int GetBoostLevel()
    {
        return m_CurrentBoost;
    }

    public void SetBoostLevel(int i_Boost)
    {
        m_boost = i_Boost;
    }

	public void AddPlayerGamePower(int i_gamePower)
	{
		m_gamePower += i_gamePower;
	}

    public string GetShortName()
    {
        return string.Format("{0}. {1}", m_firstName.Substring(0, 1), m_lastName);
    }

    public int GetSalary()
    {
        return (int) m_salary;
    }

	public int GetYearJoinedTheClub ()
	{
		return m_yearOfJoiningTheClub;
	}

    public void SetYearJoinedTheClub(int i_Year)
    {
        m_yearOfJoiningTheClub = i_Year;
    }

	public void InitYoungPlayer()
	{
		NamesUtilsScript nameUtils = new NamesUtilsScript();
		m_firstName = nameUtils.GetFirstName();
		m_lastName = nameUtils.GetLastName();
		m_isInjured = false;
		m_age = UnityEngine.Random.Range(16,24);
		m_gamesPlayed = 0;
		m_goalsScored = 0;
		m_level = UnityEngine.Random.Range(2,4);
		m_price = m_price/2 ;
		m_boost = 0;
		m_gamePower = 100;
		m_yearOfJoiningTheClub = System.DateTime.Today.Year;
		m_priceToBoost = m_level * 100;
		m_isPlaying = true;
		m_salary = m_level * 50;


	}


    public string GetFullName()
    {
        return m_firstName + " " + m_lastName;
    }

    public void SetGoalsScored(int i_GoalsScored)
    {
        m_goalsScored = i_GoalsScored;
    }
}
