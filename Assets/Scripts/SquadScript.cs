using UnityEngine;
using System.Collections;

public class SquadScript : MonoBehaviour {

	string m_teamName;
	public PlayerScript[] m_players;


	public PlayerScript GetPlayerByName(string i_name)
	{
		for (int i = 0; i < m_players.Length; i++)
		{
			if (m_players[i].getPlayerName() == i_name)
			{
				return m_players[i];
			}
		}
		return null;
	}
}
