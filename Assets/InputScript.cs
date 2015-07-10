using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputScript : MonoBehaviour
{
    public InputField m_Input;
    public Image m_SceneImage;
    public Button m_NextButton;
    public Text m_NextButtonText;
    public Sprite[] m_SceneSpriteNumber;

    private int m_CurrentInputScene;

    public const string k_InputScene = "inputScene";

    private const int k_TeamNameScene = 0;
    private const int k_CoachNameScene = 1;
    private const int k_StadiumNameScene = 2;

    private bool k_IsLoadingData;
	// Use this for initialization
	void Start ()
	{
	    m_CurrentInputScene = PlayerPrefs.GetInt(k_InputScene, 0);
	    m_SceneImage.sprite = m_SceneSpriteNumber[m_CurrentInputScene];

	    switch (m_CurrentInputScene)
	    {
            case k_TeamNameScene:
	            m_Input.placeholder.GetComponent<Text>().text = "Team Name";
                m_Input.characterValidation = InputField.CharacterValidation.None;
	            break;
            case k_CoachNameScene:
                m_Input.placeholder.GetComponent<Text>().text = "Coach Name";
	            m_Input.characterValidation = InputField.CharacterValidation.Name;
	            break;
            case k_StadiumNameScene:
                m_Input.placeholder.GetComponent<Text>().text = "Stadium Name";
                m_Input.characterValidation = InputField.CharacterValidation.Name;
                m_NextButtonText.text = "START";
	            break;
	    }
	}

    public void OnNextClick()
    {
        string inputText = m_Input.textComponent.text.Trim();

        if (inputText.Length != 0)
        {
            PlayerPrefs.SetString("teamName", inputText);
            //PlayerPrefs.SetInt(k_InputScene, m_CurrentInputScene + 1);
            //switch (m_CurrentInputScene)
            //{
            //    case k_TeamNameScene:
            //        PlayerPrefs.SetString("teamName", inputText);
            //        break;
            //    case k_CoachNameScene:
            //        PlayerPrefs.SetString("name", inputText);
            //        break;
            //    case k_StadiumNameScene:
            //        PlayerPrefs.SetString("stadiumName", inputText);
            //        break;
            //}

            //if (m_CurrentInputScene != k_StadiumNameScene)
            //{
            //    Application.LoadLevel("NewDesignInput");
            //}
            StartCoroutine(sendNewTeam());
            //else
            //{
            //    PlayerPrefs.DeleteKey(k_InputScene);
                
            //}
        }
    }

    public void OnInputFieldChange()
    {
        string inputText = m_Input.textComponent.text.Trim();

        m_NextButton.interactable = !string.IsNullOrEmpty(inputText);
    }

    private IEnumerator sendNewTeam()
    {
        k_IsLoadingData = true;
        WWWForm form = new WWWForm();

        form.AddField("id", PlayerPrefs.GetString("id"));

        form.AddField("teamName", PlayerPrefs.GetString("teamName"));
        form.AddField("stadiumName", PlayerPrefs.GetString("stadiumName"));
        form.AddField("name", PlayerPrefs.GetString("name"));

        Debug.Log(PlayerPrefs.GetString("id"));

        WWW request = new WWW(GameManager.URL + "newUser", form);

        Debug.Log("Sending team data");
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            print(request.text);
            if (request.text == "ok")
            {
                StartCoroutine(GameManager.s_GameManger.SyncClientDB("NewDesignMainScene"));
            }
        }
        k_IsLoadingData = false;
        Debug.Log("End of addNewTeamUser()");
    }
}
