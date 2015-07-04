using UnityEngine;
using UnityEngine.UI;

public class SquadTableUI : MonoBehaviour {

	public GameObject[] m_playersLines;

    public Text m_InstantTrain;
    public RectTransform m_ContentPanel;
    public GameObject m_PlayerLine;


    private PlayerScript[] m_AllPlayers;
    private GameObject[] m_PlayerLineGameObject;
    private OneLinePlayerRow[] m_PlayerLineScript;

    void Start()
    {
        init();
        m_InstantTrain.text = string.Format("Instant Train: {0}",
            GameManager.s_GameManger.m_myTeam.TotalInstantTrain);
    }

    void Update()
    {
        m_InstantTrain.text = string.Format("Instant Train: {0}",
            GameManager.s_GameManger.m_myTeam.TotalInstantTrain);
        updatePlayers();
    }

    public void init()
    {
        m_AllPlayers = GameManager.s_GameManger.m_MySquad.Players;
        m_PlayerLineGameObject = new GameObject[m_AllPlayers.Length];
        m_PlayerLineScript = new OneLinePlayerRow[m_AllPlayers.Length];
        initPlayers();
    }

    private void initPlayers()
    {
        int count = 0;
        foreach (PlayerScript player in m_AllPlayers)
        {
            m_PlayerLineGameObject[count] = Instantiate(m_PlayerLine);
            m_PlayerLineGameObject[count].transform.SetParent(m_ContentPanel.transform);
            m_PlayerLineGameObject[count].transform.localScale = new Vector3(1, 1, 1);

            m_PlayerLineScript[count] = m_PlayerLineGameObject[count].GetComponent<OneLinePlayerRow>();
            m_PlayerLineScript[count].m_Position.text = player.getPlayerPosition().ToString();
            m_PlayerLineScript[count].m_PlayerNameText.text = player.GetFullName();
            m_PlayerLineScript[count].m_XP.text = string.Format("{0}/{1}", player.CurrentBoost, player.NextBoostCap);
            m_PlayerLineScript[count].m_XPSlider.maxValue = player.NextBoostCap;
            m_PlayerLineScript[count].m_XPSlider.minValue = 0;
            m_PlayerLineScript[count].m_XPSlider.value = player.CurrentBoost;
            m_PlayerLineScript[count].m_Level.text = player.GetLevel().ToString();
            
            
            //m_PlayerLineScript[count].m_Age.text = player.GetAge().ToString();
            //m_PlayerLineScript[count].m_Wage.text = player.GetSalary().ToString();
            m_PlayerLineScript[count].m_MyPlayer = player;
            count++;
        }
    }

    private void updatePlayers()
    {
        int count = 0;
        foreach (PlayerScript player in m_AllPlayers)
        {
            m_PlayerLineScript[count].m_XP.text = string.Format("{0}/{1}", player.CurrentBoost, player.NextBoostCap);
            m_PlayerLineScript[count].m_XPSlider.maxValue = player.NextBoostCap;
            m_PlayerLineScript[count].m_XPSlider.minValue = 0;
            m_PlayerLineScript[count].m_XPSlider.value = player.CurrentBoost;
            m_PlayerLineScript[count].m_Level.text = player.GetLevel().ToString();
            //m_PlayerLineScript[count].m_Age.text = player.GetAge().ToString();
            //m_PlayerLineScript[count].m_Wage.text = player.GetSalary().ToString();
            count++;
        }
    }

    //public void UpdatePlayerLine(int i, Sprite i_sprite,string i_name,string i_level,string i_position)
    //{
    //    PlayerLineGUIScript playerLineGUIScript = m_playersLines [i].GetComponent<PlayerLineGUIScript> ();
    //    playerLineGUIScript.SetPicture (i_sprite);
    //    playerLineGUIScript.m_level.text = i_level;
    //    playerLineGUIScript.m_name.text = i_name;
    //    playerLineGUIScript.m_position.text = i_position;

    //}

    //public void UpdatePlayerLine(int i,PlayerScript i_playerScript)
    //{
    //    PlayerLineGUIScript playerLineGUIScript = m_playersLines [i].GetComponent<PlayerLineGUIScript> ();
    //    //playerLineGUIScript.SetPicture (i_playerScript.getPlayerSprite());
    //    playerLineGUIScript.m_level.text = ""+i_playerScript.GetLevel();
    //    playerLineGUIScript.m_name.text = i_playerScript.getPlayerShortName();
    //    //playerLineGUIScript.m_position.text = i_playerScript.getPlayerPosition ();;
		
    //}
	
}
