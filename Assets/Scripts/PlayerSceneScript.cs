using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSceneScript : MonoBehaviour {

	private PlayerScript m_playerScript;
	public Text m_position;
	public Text m_NameText;
	//public Text m_lastName;
	public Text m_salary;
	private bool m_isInjured;
	public Text m_age;
	public Text m_gamePlayed;
	public Text m_goalScored ;
	public Text m_level;
	public Text m_price;
	public Text m_yearOfJoiningTheClub;
	public Image m_PlayerImage;
    public Slider m_BoostSlider;
    public Text m_DrugText;
    public Text m_BoostText;

	public GameObject m_releasePlayerMenu;

    private bool m_WaitingForServer = false;

	// Use this for initialization
	void Start () {
		int i = PlayerPrefs.GetInt ("SelectedPlayer", 0);
		
		m_playerScript = GameManager.s_GameManger.m_MySquad.GetPlayerInIndex (i);

        setPositionText();
        m_NameText.text = m_playerScript.GetFullName();
        m_gamePlayed.text = m_playerScript.GetGamePlayed().ToString();
        m_yearOfJoiningTheClub.text = "" + m_playerScript.GetYearJoinedTheClub();
        m_age.text = m_playerScript.GetAge().ToString();
        m_goalScored.text = m_playerScript.GetGoalScored().ToString();
        m_salary.text = "$" + m_playerScript.GetSalary() + " p/w";
	    m_DrugText.text = "Drugs ($" + m_playerScript.GetPriceToBoostPlayer()/2 + ")";
        
	    if (m_playerScript.getPlayerImage() != null)
	    {
	        m_PlayerImage.sprite = m_playerScript.getPlayerImage();
	    }
	}

    void Update()
    {
        m_BoostSlider.value = m_playerScript.GetBoostLevel();
        m_BoostSlider.maxValue = m_playerScript.NextBoostCap;
        m_BoostText.text = "Boost ($" + m_playerScript.GetPriceToBoostPlayer() + ")";
        
        //m_lastName.text = ""+m_playerScript.getPlayerLastName();
        
        m_isInjured = m_playerScript.IsInjered();
        m_level.text = m_playerScript.GetLevel().ToString();
        m_price.text = "$" + m_playerScript.GetPlayerPrice();

        if (m_WaitingForServer)
        {
            // TODO: Enable syncing with server
        }
    }
	
	public void OnClickReleasePlayer(){
		m_releasePlayerMenu.SetActive (true);
	}

	public void OnClickNoReleasePlayer(){
		m_releasePlayerMenu.SetActive (false);
	}

	public void OnClickYesReleasePlayer(){
		m_releasePlayerMenu.SetActive (false);
		m_playerScript.InitYoungPlayer ();
		Application.LoadLevel ("New_Squad");
	}

	public void onClickBoost()
    {
		if (m_playerScript.GetPriceToBoostPlayer () <= GameManager.s_GameManger.GetCash ()) 
        {
            StartCoroutine(sendBoostClickToServer());
		}
	}

    IEnumerator sendBoostClickToServer()
    {
        m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("email", GameManager.s_GameManger.m_User.Email);
        form.AddField("id", m_playerScript.ID);
        Debug.Log("player_ID=" + m_playerScript.ID);


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
                    GameManager.s_GameManger.AddCash(-m_playerScript.GetPriceToBoostPlayer());
                    m_playerScript.BoostPlayer();
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

        m_WaitingForServer = false;
    }

	public void onClickDrugs(){
		//temp sol
		if (m_playerScript.GetPriceToBoostPlayer ()/2 <= GameManager.s_GameManger.GetCash ()) {
			//m_playerScript.BoostPlayer(Random.Range(0,105));
			GameManager.s_GameManger.AddCash((int)-m_playerScript.GetPriceToBoostPlayer ()/2);
		}

	}

    private string getPosByEnum(ePosition i_Position)
    {
        switch (i_Position)
        {
            case ePosition.GK:
                return "GoalKeeper";
            case ePosition.D:
                return "Defender";
            case ePosition.MF:
                return "Midfielder";
            case ePosition.S:
                return "Striker";
            default:
                Debug.LogError("GOT UNKNOWN POS");
                return "UNKNOWN!";
        }
    }

    private void setPositionText()
    {
        switch (m_playerScript.getPlayerPosition())
        {
            case ePosition.GK:
                m_position.text = "GoalKeeper";
                m_position.color = Color.yellow;
                break;
            case ePosition.D:
                m_position.text = "Defender";
                m_position.color = Color.blue;
                break;
            case ePosition.MF:
                m_position.text = "Midfielder";
                m_position.color = Color.green;
                break;
            case ePosition.S:
                m_position.text = "Striker";
                m_position.color = Color.red;
                break;
            default:
                Debug.LogError("GOT UNKNOWN POS");
                break;
        }
    }
}
