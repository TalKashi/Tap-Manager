using UnityEngine;
using UnityEngine.UI;

public class NextMatchPopup : MonoBehaviour
{
    public Text m_HomeTeamText;
    public Image m_HomeTeamLogo;
    public Text m_AwayTeamText;
    public Image m_AwayTeamLogo;

    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(canvas.transform);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        ////rectTransform.anchoredPosition3D = Vector3.zero;
        ////rectTransform.offsetMax = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;
        ////rectTransform.transform.SetParent(canvas);
        ////Debug.Log(rectTransform.localPosition);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    void Start()
    {
        bool isHomeGame = GameManager.s_GameManger.m_GameSettings.IsHomeOrAway;

        if (isHomeGame)
        {
            m_HomeTeamText.text = GameManager.s_GameManger.m_myTeam.Name;
            m_HomeTeamLogo.sprite = GameManager.s_GameManger.GetMyTeamLogoBig();

            m_AwayTeamText.text = GameManager.s_GameManger.m_GameSettings.NextOpponent;
            m_AwayTeamLogo.sprite =
                GameManager.s_GameManger.GetTeamLogoByName(GameManager.s_GameManger.m_GameSettings.NextOpponent);
        }
        else
        {
            m_AwayTeamText.text = GameManager.s_GameManger.m_myTeam.Name;
            m_AwayTeamLogo.sprite = GameManager.s_GameManger.GetMyTeamLogoBig();

            m_HomeTeamText.text = GameManager.s_GameManger.m_GameSettings.NextOpponent;
            m_HomeTeamLogo.sprite =
                GameManager.s_GameManger.GetTeamLogoByName(GameManager.s_GameManger.m_GameSettings.NextOpponent);
        }
    }

    public void OnGoToMatchClick()
    {
        StartCoroutine(GameManager.s_GameManger.SyncClientDB("NewDesignMatchResult"));
    }
}
