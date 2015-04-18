using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class SquadScript {

	string m_teamName;
    [SerializeField]
	private PlayerScript[] m_players;

    private const int k_NumOfPlayers = 11;

    public void Init()
    {
        NamesUtilsScript nameUtils = new NamesUtilsScript();
        m_players = new PlayerScript[k_NumOfPlayers];
        for (int i = 0; i < k_NumOfPlayers; i++)
        {
			
            m_players[i] = new PlayerScript();
			m_players[i].SetFirstName(nameUtils.GetFirstName());
			m_players[i].SetLastName(nameUtils.GetLastName());
            m_players[i].SetPlayerLevel(Random.Range(1, 11));
            switch (i)
            {
                case 0:
                    m_players[i].SetPosition(ePosition.GK);
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    m_players[i].SetPosition(ePosition.D);
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    m_players[i].SetPosition(ePosition.MF);
                    break;
                case 9:
                case 10:
                    m_players[i].SetPosition(ePosition.S);
                    break;
                default:
                    m_players[i].SetPosition(ePosition.S);
                    break;
            }
            m_players[i].SetPriceToBoostPlayer(m_players[i].GetLevel() * 100);
            m_players[i].SetIsPlaying(true);
            m_players[i].SetSalary(m_players[i].GetLevel() * 50);
            m_players[i].SetAge(Random.Range(18, 36));
            m_players[i].SetYearJoinedTheClub(DateTime.Now.Year);
        }
    }

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

    public PlayerScript[] GetAllSquad()
    {
        return m_players;
    }

	public PlayerScript GetPlayerInIndex(int i)
	{
		return m_players[i];
	}
}
