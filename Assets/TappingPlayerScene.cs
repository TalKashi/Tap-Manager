using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TappingPlayerScene : MonoBehaviour
{
    public Text m_PlayerPosition;
    public Text m_PlayerLevel;
    //public Text m_ClickValue;
    //public Slider m_PlayerXPSlider;
    public Text m_PlayerXPSliderText;
    public Image m_PlayerPicture;
    public Image m_PlayerShirt;
    public Image m_FillImage;
    public GameObject m_LoadingDataImage;

    private int m_PlayerID;
    private PlayerScript m_Player;


	// Use this for initialization
	void Start ()
	{
	    Color myColor = MyUtils.GetColorByTeamLogo(GameManager.s_GameManger.m_myTeam.LogoIdx);
        m_PlayerID = PlayerPrefs.GetInt("SelectedPlayer", -1);
	    if (m_PlayerID == -1)
	    {
            Debug.Log("ERROR: SelectedPlayer was not found in PlayerPrefs!!!");
	    }

	    m_PlayerShirt.color = myColor;
	    m_Player = GameManager.s_GameManger.m_MySquad.GetPlayerInIndex(m_PlayerID);
	    m_PlayerPicture.sprite = GameManager.s_GameManger.GetPlayerSpriteByImgIndx(m_Player.PlayerSpriteIndex);
	    GameManager.s_GameManger.CurrentScene = GameManager.k_Player;

	    updatePlayerData();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (GameManager.s_GameManger.IsLoadingData)
	    {
	        m_LoadingDataImage.SetActive(true);
	    }
	    else
	    {
            m_LoadingDataImage.SetActive(false);
            updatePlayerData();
	    }
	}

    private void updatePlayerData()
    {
        GameManager.s_GameManger.CurrentSceneHeaderName = m_Player.GetFullName();
        m_PlayerPosition.text = m_Player.GetFullPosition();
        //m_ClickValue.text = string.Format("XP Boost: {0}", m_Player.BoostAmount);
        m_PlayerLevel.text = string.Format("LEVEL {0}", m_Player.GetLevel());
        //m_PlayerXPSlider.maxValue = m_Player.NextBoostCap;
        //m_PlayerXPSlider.minValue = 0;
        //m_PlayerXPSlider.value = m_Player.CurrentBoost;
        double percentage = MyUtils.GetPercentage(m_Player.CurrentBoost, m_Player.NextBoostCap);
        m_PlayerXPSliderText.text = string.Format("{0:P2}", percentage);
        m_FillImage.fillAmount = (float)percentage;
    }

    public void OnTrainClick()
    {
        StartCoroutine(sendBoostClickToServer());
        m_Player.BoostPlayer();
    }

    private IEnumerator sendBoostClickToServer()
    {
        //m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);
        form.AddField("playerId", m_Player.ID);
        Debug.Log("player_ID=" + m_Player.ID);


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
                    //m_Player.BoostPlayer();
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
