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
	public Image m_PlayerImage;


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

	public string getPlayerPosition ()
	{
		return m_position.ToString();
	}

	public bool isInjered()
	{
		return m_isInjured;
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

	public Image getPlayerImage ()
	{
		return m_PlayerImage;
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
			m_boost = 0;
			m_level++;
		}
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

}
