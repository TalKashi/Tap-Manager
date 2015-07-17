using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageLineScript : MonoBehaviour
{
    public Text m_HeaderText;
    public Image m_MailImage;
    public int m_Index;
    public GameObject m_GenericPopup;

    public void OnClick()
    {
        GenericPopup popUpScript = m_GenericPopup.GetComponent<GenericPopup>();
        Message message = GameManager.s_GameManger.m_User.Inbox[m_Index];
        popUpScript.m_HeaderText.text = message.Header;
        popUpScript.m_ContentText.text = message.Content;
        popUpScript.m_HeaderImage.color = MyUtils.GetColorByTeamLogo(GameManager.s_GameManger.m_myTeam.LogoIdx);

        m_GenericPopup.SetActive(true);
    }
}
