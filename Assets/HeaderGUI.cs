using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeaderGUI : MonoBehaviour
{

    //public Image m_TeamLogo;
    public Text m_CashText;
    public Text m_TeamNameText;
    public Text m_TeamLevel;
    public Text m_Position;
    public Image m_MailImg;
    public Text m_InstantTrainText;
    public Text m_SceneHeader;
    public GameObject m_BackButton;
    public GameObject m_EditNameButton;

	// Use this for initialization
	void Start () 
    {
	    //m_TeamLogo.sprite = GameManager.s_GameManger.GetMyTeamLogoSmall();

	    everyFrameUpdate();
	    m_Position.text = GameManager.s_GameManger.GetTeamPosition(GameManager.s_GameManger.m_myTeam).ToString();

	    //m_MailImg.sprite = GameManager.s_GameManger.m_User.Inbox.HasUnreadMessages ? spriteUnreadMsg : spriteReadMsg;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    everyFrameUpdate();
	}

    private void everyFrameUpdate()
    {
        m_BackButton.SetActive(GameManager.s_GameManger.CurrentScene != GameManager.k_Lobby);

        m_EditNameButton.SetActive(GameManager.s_GameManger.CurrentScene == GameManager.k_Player);

        m_TeamNameText.text = GameManager.s_GameManger.m_myTeam.Name;
        m_CashText.text = string.Format("{0}", GameManager.s_GameManger.GetCash());
        m_TeamLevel.text = GameManager.s_GameManger.m_MySquad.GetLevel().ToString();
        //m_MailImg.sprite = GameManager.s_GameManger.m_User.Inbox.HasUnreadMessages ? spriteUnreadMsg : spriteReadMsg;
        m_InstantTrainText.text = GameManager.s_GameManger.m_myTeam.TotalInstantTrain.ToString();
        m_SceneHeader.text = GameManager.s_GameManger.CurrentSceneHeaderName;
    }

    public void OnBackClick()
    {
        GameManager.s_GameManger.GoBack();
    }

    public void OnChangeNameClick()
    {
        //TODO: popup with input field to enter new name
    }
}
