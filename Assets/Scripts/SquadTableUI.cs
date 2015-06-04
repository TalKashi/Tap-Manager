using UnityEngine;
using System.Collections;

public class SquadTableUI : MonoBehaviour {

	public GameObject[] m_playersLines;

    public GameObject m_ContentPanel;
    public GameObject m_PlayerLine;

    private PlayerScript[] m_AllPlayers;
    private OneLinePlayerRow[] m_PlayerLineScript;

    void Awake()
    {
        init();
    }

    void Update()
    {
        updatePlayers();
    }

    public void init()
    {
        m_AllPlayers = GameManager.s_GameManger.m_MySquad.Players;
        m_PlayerLineScript = new OneLinePlayerRow[m_AllPlayers.Length];
        initPlayers();
    }

    private void initPlayers()
    {
        int count = 0;
        foreach (PlayerScript player in m_AllPlayers)
        {
            GameObject newPlayerRow = Instantiate(m_PlayerLine);
            newPlayerRow.transform.SetParent(m_ContentPanel.transform);
            //newPlayerRow.transform.localScale = new Vector3(1, 1, 1);
            m_PlayerLineScript[count] = newPlayerRow.GetComponent<OneLinePlayerRow>();
            m_PlayerLineScript[count].m_XP.text = string.Format("({0})", player.CurrentBoost);
            m_PlayerLineScript[count].m_Name.text = player.GetShortName();
            m_PlayerLineScript[count].m_Position.text = player.getPlayerPosition().ToString();
            m_PlayerLineScript[count].m_Age.text = player.GetAge().ToString();
            m_PlayerLineScript[count].m_Wage.text = player.GetSalary().ToString();
            count++;
        }
    }

    private void updatePlayers()
    {
        int count = 0;
        foreach (PlayerScript player in m_AllPlayers)
        {
            m_PlayerLineScript[count].m_XP.text = string.Format("({0})", player.CurrentBoost);
            m_PlayerLineScript[count].m_Name.text = player.GetShortName();
            m_PlayerLineScript[count].m_Position.text = player.getPlayerPosition().ToString();
            m_PlayerLineScript[count].m_Age.text = player.GetAge().ToString();
            m_PlayerLineScript[count].m_Wage.text = player.GetSalary().ToString();
        }
    }

	public void UpdatePlayerLine(int i, Sprite i_sprite,string i_name,string i_level,string i_position)
	{
		PlayerLineGUIScript playerLineGUIScript = m_playersLines [i].GetComponent<PlayerLineGUIScript> ();
		playerLineGUIScript.SetPicture (i_sprite);
		playerLineGUIScript.m_level.text = i_level;
		playerLineGUIScript.m_name.text = i_name;
		playerLineGUIScript.m_position.text = i_position;

	}

	public void UpdatePlayerLine(int i,PlayerScript i_playerScript)
	{
		PlayerLineGUIScript playerLineGUIScript = m_playersLines [i].GetComponent<PlayerLineGUIScript> ();
		//playerLineGUIScript.SetPicture (i_playerScript.getPlayerSprite());
		playerLineGUIScript.m_level.text = ""+i_playerScript.GetLevel();
		playerLineGUIScript.m_name.text = i_playerScript.getPlayerShortName();
		//playerLineGUIScript.m_position.text = i_playerScript.getPlayerPosition ();;
		
	}
	
}
