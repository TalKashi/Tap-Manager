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
    public Image m_TopBar;
    public Image m_MiddleBar;
    public Image m_BottomBar;

    public GameObject m_PopupGameObject;

	// Use this for initialization
	void Start () 
    {
	    //m_TeamLogo.sprite = GameManager.s_GameManger.GetMyTeamLogoSmall();
	    Color headerColor = MyUtils.GetColorByTeamLogo(GameManager.s_GameManger.m_myTeam.LogoIdx);
	    headerColor.a = 0.59765625f;
        m_TopBar.color = headerColor; // alpha 153
	    headerColor.a = 0.84765625f;
	    m_MiddleBar.color = headerColor; // alpha 217
        headerColor.a = 1;
	    m_BottomBar.color = headerColor; // alpha 256

	    everyFrameUpdate();
	    m_Position.text = GameManager.s_GameManger.GetTeamPosition(GameManager.s_GameManger.m_myTeam).ToString();
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
        m_MailImg.sprite = GameManager.s_GameManger.m_User.Inbox.HasUnreadMessages
            ? GameManager.s_GameManger.m_UnreadMailSprite
            : GameManager.s_GameManger.m_ReadMailSprite;
    }

    public void OnBackClick()
    {
        GameManager.s_GameManger.GoBack();
    }

    public void OnChangeNameClick()
    {
        m_PopupGameObject.SetActive(true);
    }
}
