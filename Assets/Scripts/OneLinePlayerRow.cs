using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OneLinePlayerRow : MonoBehaviour
{

    public Text m_Position;
    public Text m_PlayerNameText;
    //public Slider m_XPSlider;
    public Text m_XP;
    public Text m_Level;
    public Button m_TrainButton;
    //public Text m_Age;
    //public Text m_Wage;
    public PlayerScript m_MyPlayer;
    public GameObject m_GenericPopup;

    void Update()
    {
        m_TrainButton.interactable = GameManager.s_GameManger.m_myTeam.TotalInstantTrain > 0;
    }

    public void OnInstantTrainClick()
    {
        if (GameManager.s_GameManger.m_myTeam.TotalInstantTrain > 0)
        {
            GameManager.s_GameManger.m_myTeam.TotalInstantTrain--;
            StartCoroutine(sendBoostLevelUpClickToServer());
        }
    }

    public void OnPlayerRowClick()
    {
        PlayerPrefs.SetInt("SelectedPlayer", m_MyPlayer.ID);
        Application.LoadLevel("PlayerDevelopment");

        
    }

    private IEnumerator sendBoostLevelUpClickToServer()
    {
        GameManager.s_GameManger.IsLoadingData = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("playerId", m_MyPlayer.ID);
        Debug.Log("player_ID=" + m_MyPlayer.ID);


        Debug.Log("Sending boostClick to server");
        WWW request = new WWW(GameManager.URL + "boostPlayerLevelUp", form);
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
            MyUtils.DisplayErrorMessage(m_GenericPopup);
            GameManager.s_GameManger.IsLoadingData = false;
        }
        else
        {
            // Check ok response
            Debug.Log(request.text);
            switch (request.text)
            {
                case "ok":
                    m_MyPlayer.LevelUpPlayer();
                    GameManager.s_GameManger.IsLoadingData = false;
                    break;
                case "null":
                    Debug.Log("WARN: DB out of sync!");
                    // Sync DB
                    MyUtils.DisplayOutOfSyncErrorMessage(m_GenericPopup);
                    StartCoroutine(GameManager.s_GameManger.SyncClientDB());
                    break;

                default:
                    MyUtils.DisplayOutOfSyncErrorMessage(m_GenericPopup);
                    StartCoroutine(GameManager.s_GameManger.SyncClientDB());
                    break;
            }
        }

        //m_WaitingForServer = false;
    }
}
