using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeaderGUI : MonoBehaviour
{

    public Image m_ProfilePic;
    public Text m_CashText;
    public Text m_TeamNameText;

	// Use this for initialization
	void Start () 
    {
        m_CashText.text = string.Format("{0:C0}", GameManager.s_GameManger.GetCash());
        m_TeamNameText.text = GameManager.s_GameManger.m_myTeam.Name;
        if (GameManager.s_GameManger.m_User.ProfilePic != null)
        {
            m_ProfilePic.sprite = GameManager.s_GameManger.m_User.ProfilePic;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_CashText.text = string.Format("{0:C0}", GameManager.s_GameManger.GetCash());
	}
}
