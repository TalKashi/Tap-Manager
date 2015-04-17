using UnityEngine;
using System.Collections;

public class SquadScript : MonoBehaviour {

	string m_teamName;
	public PlayerScript[] m_players;


	public PlayerScript GetPlayerByFirstName(string i_name)
	{
		for (int i = 0; i < m_players.Length; i++)
		{
			if (m_players[i].getPlayerFirstName() == i_name)
			{
				return m_players[i];
			}
		}
		return null;
	}

	public PlayerScript GetPlayerByLastName(string i_name)
	{
		for (int i = 0; i < m_players.Length; i++)
		{
			if (m_players[i].getPlayerLastName() == i_name)
			{
				return m_players[i];
			}
		}
		return null;
	}

	 




}
