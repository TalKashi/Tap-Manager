using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TableScript : MonoBehaviour {

	public RectTransform m_contentPanel;
	public GameObject m_rowPrefab;
    public GameObject m_LoadingDataImage;

	private GameObject[] m_lines;

	void Start()
	{
	    GameManager.s_GameManger.CurrentSceneHeaderName = GameManager.k_League;
        GameManager.s_GameManger.CurrentScene = GameManager.k_League;
		GameManager.s_GameManger.updateTableLeague ();
	}

    void Update()
    {
        m_LoadingDataImage.SetActive(GameManager.s_GameManger.IsLoadingData);
    }

	public void InitTable(int i_numOfTeams)
    {
		m_lines = new GameObject[i_numOfTeams];
		m_lines[0] = Instantiate(m_rowPrefab);
	    m_lines[0].gameObject.transform.SetParent(m_contentPanel.transform);
	    m_lines[0].gameObject.transform.localScale = new Vector3(1,1,1);

		for (int i = 1; i < m_lines.Length ; i++) 
        {
			m_lines[i] = Instantiate(m_rowPrefab);
            m_lines[i].gameObject.transform.SetParent(m_contentPanel.transform);
            m_lines[i].gameObject.transform.localScale = new Vector3(1, 1, 1);
		}

    }

	public void UpdateLine(int i_lineNum, int i_place, string i_team, int i_played, int i_won, int i_lost, int i_drawn, int i_for, int i_against, int i_points, bool i_IsMyTeam)
    {

		OneLineUITableScript oneLineUITableScript = m_lines [i_lineNum].GetComponent<OneLineUITableScript> ();
		//oneLineUITableScript.m_place.text = string.Format("{0}.", +i_place);
	    oneLineUITableScript.m_team.text = string.Format("{0}. {1}", i_place, i_team);
		oneLineUITableScript.m_played.text = i_played.ToString();
		oneLineUITableScript.m_won.text = i_won.ToString();
		oneLineUITableScript.m_lost.text = i_lost.ToString();
		oneLineUITableScript.m_drawn.text =  i_drawn.ToString();
		oneLineUITableScript.m_for.text =  i_for.ToString();
		oneLineUITableScript.m_against.text =  i_against.ToString();
		oneLineUITableScript.m_points.text =  i_points.ToString();

	    if (i_IsMyTeam)
	    {
            Color myTeamTextColor = new Color(0.8984375f, 0.8984375f, 0.8984375f);
            oneLineUITableScript.m_BubbleImg.color = MyUtils.GetColorByTeamLogo(GameManager.s_GameManger.m_myTeam.LogoIdx);
	        oneLineUITableScript.m_team.color = myTeamTextColor;
            oneLineUITableScript.m_played.color = myTeamTextColor;
            oneLineUITableScript.m_won.color = myTeamTextColor;
            oneLineUITableScript.m_lost.color = myTeamTextColor;
            oneLineUITableScript.m_drawn.color = myTeamTextColor;
            oneLineUITableScript.m_for.color = myTeamTextColor;
            oneLineUITableScript.m_against.color = myTeamTextColor;
            oneLineUITableScript.m_points.color = myTeamTextColor;
	    }

	    int leagueSize = GameManager.s_GameManger.m_AllTeams.Length;
	    if ((i_place == 1 || i_place == 2) && GameManager.s_GameManger.IsPromotionLeague)
	    {
	        oneLineUITableScript.m_PromotionIcon.SetActive(true);
	    }
        else if ((i_place == leagueSize || (i_place == leagueSize - 1)) && GameManager.s_GameManger.IsRelegationLeague)
        {
            oneLineUITableScript.m_RelegationIcon.SetActive(true);
        }
	}



}
