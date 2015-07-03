using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OneLinePlayerRow : MonoBehaviour
{

    public Text m_Position;
    public Text m_PlayerNameText;
    public Slider m_XPSlider;
    public Text m_XP;
    public Text m_Level;
    public Button m_TrainButton;
    //public Text m_Age;
    //public Text m_Wage;
    public PlayerScript m_MyPlayer;

    void Update()
    {
        m_TrainButton.interactable = m_MyPlayer.GetPriceToBoostPlayer() <= GameManager.s_GameManger.GetCash();
    }

    public void OnTrainClick()
    {
        if (m_MyPlayer.GetPriceToBoostPlayer() <= GameManager.s_GameManger.GetCash())
        {
            StartCoroutine(sendBoostClickToServer());
        }
    }

    private IEnumerator sendBoostClickToServer()
    {
        //m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("playerId", m_MyPlayer.ID);
        Debug.Log("player_ID=" + m_MyPlayer.ID);


        Debug.Log("Sending boostClick to server");
        WWW request = new WWW(GameManager.URL + "playerBoostClick", form);
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
                    GameManager.s_GameManger.AddCash(-m_MyPlayer.GetPriceToBoostPlayer());
                    m_MyPlayer.BoostPlayer();
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
