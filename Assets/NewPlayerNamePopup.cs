using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class NewPlayerNamePopup : MonoBehaviour 
{
    public InputField m_NewPlayerNameInputField;
    public Button m_SendButton;
    public GameObject m_HeaderGameObject;

    private PlayerScript m_MyPlayer;

    void Start()
    {
        HeaderGUI headerScript = m_HeaderGameObject.GetComponent<HeaderGUI>();
        headerScript.m_PopupGameObject = gameObject;
        m_MyPlayer = GameManager.s_GameManger.m_MySquad.GetPlayerInIndex(PlayerPrefs.GetInt("SelectedPlayer"));
        gameObject.SetActive(false);
    }

    void Update()
    {
        string newName = m_NewPlayerNameInputField.text.Trim();
        m_SendButton.interactable = !string.IsNullOrEmpty(newName);
    }

    public void OnSendClick()
    {
        string newName = m_NewPlayerNameInputField.text.Trim();

        StartCoroutine(changePlayerName(newName));
    }

    private IEnumerator changePlayerName(string i_NewName)
    {
        string[] splitArr = Regex.Split(i_NewName, @"\W+");
        string firstName = splitArr[0];
        string lastName = string.Empty;
        for (int i = 1; i < splitArr.Length; i++)
        {
            if (i + 1 < splitArr.Length)
            {
                lastName += splitArr[i] + " ";
            }
            else
            {
                lastName += splitArr[i];
            }            
        }

        //m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("indexPlayer", m_MyPlayer.ID);
        form.AddField("firstName", firstName);
        form.AddField("lastName", lastName);
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
            Debug.Log(request.text);
            // Check ok response
            switch (request.text)
            {
                case "ok":
                    m_MyPlayer.SetFirstName(firstName);
                    m_MyPlayer.SetLastName(lastName);
                    gameObject.SetActive(false);
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
