using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputScript : MonoBehaviour
{
    public InputField m_Input;
    public Image m_SceneImage;
    public Sprite[] m_SceneSpriteNumber;

    private int m_CurrentInputScene;

    public const string k_InputScene = "inputScene";

    private const int k_TeamNameScene = 0;
    private const int k_CoachNameScene = 1;
    private const int k_StadiumNameScene = 2;
	// Use this for initialization
	void Start ()
	{
        //PlayerPrefs.DeleteAll();
	    m_CurrentInputScene = PlayerPrefs.GetInt(k_InputScene, 0);
	    m_SceneImage.sprite = m_SceneSpriteNumber[m_CurrentInputScene];

	    switch (m_CurrentInputScene)
	    {
            case k_TeamNameScene:
	            m_Input.placeholder.GetComponent<Text>().text = "Team Name";
	            break;
            case k_CoachNameScene:
                 m_Input.placeholder.GetComponent<Text>().text = "Coach Name";
	            break;
            case k_StadiumNameScene:
                 m_Input.placeholder.GetComponent<Text>().text = "Stadium Name";
	            break;
	    }
	}

    public void OnNextClick()
    {
        string inputText = m_Input.textComponent.text.Trim();
        if (inputText.Length != 0)
        {
            PlayerPrefs.SetInt(k_InputScene, m_CurrentInputScene + 1);
            switch (m_CurrentInputScene)
            {
                case k_TeamNameScene:
                    PlayerPrefs.SetString("teamName", inputText);
                    break;
                case k_CoachNameScene:
                    PlayerPrefs.SetString("name", inputText);
                    break;
                case k_StadiumNameScene:
                    PlayerPrefs.SetString("stadiumName", inputText);
                    break;
            }

            if (m_CurrentInputScene != k_StadiumNameScene)
            {
                Application.LoadLevel("NewDesignInput");
            }
            else
            {
                PlayerPrefs.DeleteKey(k_InputScene);
                StartCoroutine(sendNewTeam());
            }
        }
    }

    private IEnumerator sendNewTeam()
    {
        WWWForm form = new WWWForm();

        form.AddField("id", PlayerPrefs.GetString("id", "69289a8d-ff32-4d75-ab5f-3e6014ea2c5c"));

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
                StartCoroutine(GameManager.s_GameManger.SyncClientDB("NewMainScene"));
            }
        }
        Debug.Log("End of addNewTeamUser()");
    }
}
