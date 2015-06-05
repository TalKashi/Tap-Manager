using UnityEngine;
using System.Collections;

public class MatchResultPopup : MonoBehaviour
{

    public GameObject m_LostPopup;
    public GameObject m_WonPopup;
    public GameObject m_DrawPopup;

    // Use this for initialization
    void Start()
    {
        switch (GameManager.s_GameManger.m_myTeam.LastResult)
        {
            case eResult.Draw:
                m_DrawPopup.SetActive(true);
                break;
            case eResult.Lost:
                m_LostPopup.SetActive(true);
                break;
            case eResult.Won:
                m_WonPopup.SetActive(true);
                break;
        }
    }

    public void OnCloseClick()
    {
        Application.LoadLevel("NewDesignMainScene");
    }
}
