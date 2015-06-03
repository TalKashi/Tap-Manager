using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewShopGUI : MonoBehaviour
{

    public Text m_FansUpgradeText;
    public Text m_FansUpgradeTitleText;
    public Text m_FacilitiesUpgradeText;
    public Text m_FacilitiesUpgradeTitleText;
    public Text m_StadiumUpgradeText;
    public Text m_StadiumUpgradeTitleText;

    private bool m_WaitingForServer = false;

	// Use this for initialization
	void Start ()
	{
	    m_FansUpgradeTitleText.text = string.Format("Total Fans: {0}", GameManager.s_GameManger.m_myTeam.GetFanBase());
        m_FansUpgradeText.text = string.Format("Invest in your fans{0}{1:C0}",
	            System.Environment.NewLine,
	            GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(GameManager.s_GameManger.m_myTeam.Fans));

	    m_FacilitiesUpgradeTitleText.text = string.Format("Facilities Level: {0}",
	        GameManager.s_GameManger.m_myTeam.Facilities + 1);
	    m_FacilitiesUpgradeText.text = string.Format("Improve your training facilities{0}{1:C0}",
	            System.Environment.NewLine,
	            GameManager.s_GameManger.m_GameSettings.GetFacilitiesCostForLevel(
	                GameManager.s_GameManger.m_myTeam.Facilities));

	    m_StadiumUpgradeTitleText.text = string.Format("Stadium Capacity: {0}k",
	        GameManager.s_GameManger.m_myTeam.TotalSeats/1000);
        m_StadiumUpgradeText.text = string.Format("Increase your stadium capacity{0}{1:C0}",
            System.Environment.NewLine, GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(
                    GameManager.s_GameManger.m_myTeam.Stadium));
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_FansUpgradeTitleText.text = string.Format("Total Fans: {0}", GameManager.s_GameManger.m_myTeam.GetFanBase());
        m_FansUpgradeText.text = string.Format("Invest in your fans{0}{1:C0}",
                System.Environment.NewLine,
                GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(GameManager.s_GameManger.m_myTeam.Fans));

        m_FacilitiesUpgradeTitleText.text = string.Format("Facilities Level: {0}",
            GameManager.s_GameManger.m_myTeam.Facilities + 1);
        m_FacilitiesUpgradeText.text = string.Format("Improve your training facilities{0}{1:C0}",
                System.Environment.NewLine,
                GameManager.s_GameManger.m_GameSettings.GetFacilitiesCostForLevel(
                    GameManager.s_GameManger.m_myTeam.Facilities));

        m_StadiumUpgradeTitleText.text = string.Format("Stadium Capacity: {0}k",
            GameManager.s_GameManger.m_myTeam.TotalSeats / 1000);
        m_StadiumUpgradeText.text = string.Format("Increase your stadium capacity{0}{1:C0}",
            System.Environment.NewLine, GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(
                    GameManager.s_GameManger.m_myTeam.Stadium));
	}

    public void OnFansClick()
    {
        //GameManager.s_GameManger.FansUpdate ();
        int upgradeCost = GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(GameManager.s_GameManger.m_myTeam.GetFansLevel());
        if (GameManager.s_GameManger.GetCash() >= upgradeCost)
        {
            GameManager.s_GameManger.FansUpdate(upgradeCost);
            StartCoroutine(sendFansClickToServer());
        }
        else
        {
            // TODO: Notify player that he doesn't have money
        }
    }

    IEnumerator sendFansClickToServer()
    {
        // TODO: Add 'loading' popup
        m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        //form.AddField("email", GameManager.s_GameManger.m_User.Email);
        form.AddField("id", GameManager.s_GameManger.m_User.ID);

        Debug.Log("Sending fansClick request");
        WWW request = new WWW(GameManager.URL + "upgradeFans", form);
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
            // if new user go to new team screen
            // else go to home page with team data
            switch (request.text)
            {
                case "ok":
                    Debug.Log("Fans upgraded OK!");
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

    public void OnFacilitiesClick()
    {
        //GameManager.s_GameManger.FacilitiesUpdate ();
        int upgradeCost = GameManager.s_GameManger.m_GameSettings.GetFacilitiesCostForLevel(GameManager.s_GameManger.m_myTeam.GetFacilitiesLevel());
        if (GameManager.s_GameManger.GetCash() >= upgradeCost)
        {
            print("Price=" + upgradeCost);
            GameManager.s_GameManger.FacilitiesUpdate(upgradeCost);
            StartCoroutine(sendFacilitiesClickToServer());
        }
        else
        {
            // TODO: Notify player that he doesn't have money
        }
    }

    IEnumerator sendFacilitiesClickToServer()
    {
        // TODO: Add 'loading' popup
        m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);

        Debug.Log("Sending facilitiesClick request");
        WWW request = new WWW(GameManager.URL + "upgradeFacilities", form);
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            Debug.Log(request.text);
            
            switch (request.text)
            {
                case "ok":
                    Debug.Log("Facilities upgraded OK!");
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

    public void OnStadiumClick()
    {
        //GameManager.s_GameManger.StadiumUpdate ();

        int upgradeCost = GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(GameManager.s_GameManger.m_myTeam.GetStadiumLevel());
        if (GameManager.s_GameManger.GetCash() >= upgradeCost)
        {
            GameManager.s_GameManger.StadiumUpdate(upgradeCost);
            StartCoroutine(sendStadiumClickToServer());
        }
        else
        {
            // TODO: Notify player that he doesn't have money
        }
    }

    IEnumerator sendStadiumClickToServer()
    {
        // TODO: Add 'loading' popup
        m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.s_GameManger.m_User.ID);

        Debug.Log("Sending stadiumClick request");
        WWW request = new WWW(GameManager.URL + "upgradeStadium", form);
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
                    Debug.Log("Stadium upgraded OK!");
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
}
