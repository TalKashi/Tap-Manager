using UnityEngine;
using System.Collections;

public class InboxScene : MonoBehaviour
{
    public GameObject m_GenericPopup;
    public GameObject m_MessageLinePrefab;
    public RectTransform m_contentPanel;

    private GameObject[] m_AllMessagesObjects;
    private MessageLineScript[] m_AllMessagesScripts;
    private Inbox m_Inbox;

	// Use this for initialization
	void Start () 
    {
        GameManager.s_GameManger.CurrentSceneHeaderName = GameManager.k_Inbox;
        GameManager.s_GameManger.CurrentScene = GameManager.k_Inbox;
        m_Inbox = GameManager.s_GameManger.m_User.Inbox;

        m_AllMessagesObjects = new GameObject[m_Inbox.TotalMessages];
        m_AllMessagesScripts = new MessageLineScript[m_Inbox.TotalMessages];
        int count = m_Inbox.Messages.Count - 1;
	    int id = 0;
        foreach (Message message in m_Inbox.Messages)
        {
            m_AllMessagesObjects[count] = Instantiate(m_MessageLinePrefab);
            m_AllMessagesObjects[count].transform.SetParent(m_contentPanel.transform);
            m_AllMessagesObjects[count].transform.localScale = new Vector3(1, 1, 1);

            m_AllMessagesScripts[count] = m_AllMessagesObjects[count].GetComponent<MessageLineScript>();
            m_AllMessagesScripts[count].m_HeaderText.text = message.Header;
            m_AllMessagesScripts[count].m_MailImage.sprite = message.HasReadMessage
                ? GameManager.s_GameManger.m_ReadMailSprite
                : GameManager.s_GameManger.m_UnreadMailSprite;
            m_AllMessagesScripts[count].m_Index = id;
            m_AllMessagesScripts[count].m_GenericPopup = m_GenericPopup;

            count--;
            id++;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        int count = 0;
        foreach (Message message in m_Inbox.Messages)
        {
            m_AllMessagesScripts[count].m_MailImage.sprite = message.HasReadMessage
                ? GameManager.s_GameManger.m_ReadMailSprite
                : GameManager.s_GameManger.m_UnreadMailSprite;
            m_AllMessagesScripts[count].m_HeaderText.text = message.Header;
            m_AllMessagesScripts[count].m_Index = count;

            count++;
        }
	}
}
