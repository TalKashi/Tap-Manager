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
	private int m_salary;
	private bool m_isInjured;
	private int m_age;
	private int m_gamePlayed = 0;
	private int m_goalScored = 0;
	private int m_level;
	private int m_price;
	private int m_priceToBoost;
	private int m_boost = 0;
	private bool m_isPlaying;
	private int m_gamePower = 100;
	private int m_yearOfJoiningTheClub;
    [NonSerialized]
	public Sprite m_PlayerImage;


	public void SetPriceToBoostPlayer(int i_priceToBoost)
	{
		m_priceToBoost = i_priceToBoost;

	}

	public void SetSalary(int i_salary)
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
		return m_goalScored;
	}

	public bool isInjered()
	{
		return m_isInjured;
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
		return m_gamePlayed;
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
		m_gamePlayed++;
	}

	public void BoostPlayer(int i_boost)
	{
		m_boost += i_boost;
		if (m_boost >= 100) 
		{
			m_boost = m_boost%100;
			m_level++;
		}
	}

    public int GetBoostLevel()
    {
        return m_boost;
    }

	public void AddPlayerGamePower(int i_gamePower)
	{
		m_gamePower += i_gamePower;
	}

    public string GetShortName()
    {
        return m_firstName.Substring(0, 1) + " " + m_lastName;
    }

    public int GetSalary()
    {
        return m_salary;
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
		m_gamePlayed = 0;
		m_goalScored = 0;
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
}
