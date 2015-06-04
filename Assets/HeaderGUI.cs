using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeaderGUI : MonoBehaviour
{

    public Image m_ProfilePic;
    public Text m_CashText;
    public Text m_TeamNameText;
    public Text m_TeamLevel;
    public Text m_Position;

	// Use this for initialization
	void Start () 
    {
        m_CashText.text = string.Format("{0}", GameManager.s_GameManger.GetCash());
        m_TeamNameText.text = GameManager.s_GameManger.m_myTeam.Name;
        if (GameManager.s_GameManger.m_User.ProfilePic != null)
        {
            m_ProfilePic.sprite = GameManager.s_GameManger.m_User.ProfilePic;
        }

	    m_TeamLevel.text = GameManager.s_GameManger.m_MySquad.GetLevel().ToString();
	    m_Position.text = GameManager.s_GameManger.GetTeamPosition(GameManager.s_GameManger.m_myTeam).ToString();
    }
	
	// Update is called once per frame
	void Update () 
    {
        m_CashText.text = string.Format("{0}", GameManager.s_GameManger.GetCash());
	}
}
