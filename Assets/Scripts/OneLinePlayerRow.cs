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
        if (GameManager.s_GameManger.IsEditPlayerMode)
        {
            StartCoroutine(changePlayerName());
        }
        else
        {
            Application.LoadLevel("PlayerDevelopment");
        }
        
    }

    private IEnumerator changePlayerName()
    {
        //m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("indexPlayer", m_MyPlayer.ID);
        form.AddField("firstName", "");
        form.AddField("lastName", "Kashi");
        Debug.Log("indexPlayer=" + m_MyPlayer.ID);



        Debug.Log("Sending changePlayerName to server");
        WWW request = new WWW(GameManager.URL + "changePlayerName", form);
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            // Check ok response
            switch (request.text)
            {
                case "ok":
                    m_MyPlayer.SetFirstName("");
                    m_MyPlayer.SetLastName("Kashi");
                    break;
                case "null":
                    Debug.Log("WARN: DB out of sync!");
                    // Sync DB
                    break;

                default:
                    // Do nothing
                    break;
            }
        }

        //m_WaitingForServer = false;
    }

    private IEnumerator sendBoostLevelUpClickToServer()
    {
        //m_WaitingForServer = true;
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
        }
        else
        {
            // Check ok response
            Debug.Log(request.text);
            switch (request.text)
            {
                case "ok":
                    m_MyPlayer.LevelUpPlayer();
                    break;
                case "null":
                    Debug.Log("WARN: DB out of sync!");
                    // Sync DB
                    break;

                default:
                    // Do nothing
                    break;
            }
        }

        //m_WaitingForServer = false;
    }
}
