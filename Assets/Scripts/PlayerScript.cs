using UnityEngine;
using System.Collections;
public enum ePosition {GK,CB,LB,RB,DM,CM,LW,RW,CF};
public class PlayerScript : MonoBehaviour {

	private ePosition m_position;
	private string m_name;
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

	public void SetName(string i_name)
	{
		m_name = i_name;
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

	public string getPlayerName ()
	{
		return m_name;
	}

	public int GetPlayerPrice()
	{
		return m_price;
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

}
