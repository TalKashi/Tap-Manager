using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class NewShopGUI : MonoBehaviour
{
    public GameObject m_ImprovePanel;
    public GameObject m_BuyPanel;

    public Text m_StadiumLevel;
    public Text m_StadiumSeats;
    public Text m_StadiumUpgradeCost;
    public Text m_StadiumUpgradeBonus;

    public Text m_FansLevel;
    public Text m_FansTotal;
    public Text m_FansUpgradeCost;
    public Text m_FansUpgradeBonus;

    public Text m_FacilitesLevel;
    public Text m_FacilitesPower; // not used ATM
    public Text m_FacilitesUpgradeCost;
    public Text m_FacilitesUpgradeBonus; // Not used ATM

    public GameObject m_ImproveUnderlineObject;
    public Image m_ImproveUnderline;
    public GameObject m_BuyUnderlineObject;
    public Image m_BuyUnderline;

    //public Text m_FansUpgradeText;
    //public Text m_FansUpgradeTitleText;
    //public Text m_FacilitiesUpgradeText;
    //public Text m_FacilitiesUpgradeTitleText;
    //public Text m_StadiumUpgradeText;
    //public Text m_StadiumUpgradeTitleText;

    //public Button m_FansUpgradeButton;
    //public Button m_FacilitiesUpgradeButton;
    //public Button m_StadiumUpgradeButton;

    private bool m_WaitingForServer = false;
    private bool m_ShowImprove = true;

	// Use this for initialization
	void Start ()
	{
	    GameManager.s_GameManger.CurrentSceneHeaderName = GameManager.k_Shop;
        GameManager.s_GameManger.CurrentScene = GameManager.k_Shop;

	    Color myColor = MyUtils.GetColorByTeamLogo(GameManager.s_GameManger.m_myTeam.LogoIdx);
	    m_ImproveUnderline.color = myColor;
        m_BuyUnderline.color = myColor;

	    int stadiumLevel = GameManager.s_GameManger.m_myTeam.Stadium;
        m_StadiumLevel.text = string.Format("LEVEL: {0}", stadiumLevel + 1);
        m_StadiumSeats.text = string.Format("SEATS: {0}", MyUtils.ConvertNumber(GameManager.s_GameManger.m_myTeam.TotalSeats));
	    m_StadiumUpgradeCost.text = string.Format("{0}",
	        MyUtils.ConvertNumber(
	            GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(stadiumLevel)));
	    int newCapacityDiff = GameManager.s_GameManger.m_myTeam.TotalSeatsForLevel(stadiumLevel + 1) -
	                      GameManager.s_GameManger.m_myTeam.TotalSeats;
	    m_StadiumUpgradeBonus.text = string.Format("+ {0} SEATS", MyUtils.ConvertNumber(newCapacityDiff));

	    int fansLevel = GameManager.s_GameManger.m_myTeam.Fans;
	    m_FansLevel.text = string.Format("LEVEL: {0}", fansLevel + 1);
	    m_FansTotal.text = string.Format("TOTAL: {0}", MyUtils.ConvertNumber(GameManager.s_GameManger.m_myTeam.GetFanBase()));
	    m_FansUpgradeCost.text = string.Format("{0}",
	        MyUtils.ConvertNumber(GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(fansLevel)));
	    int newFansTotalDiff = GameManager.s_GameManger.m_myTeam.GetFanBaseByLevel(fansLevel + 1) -
	                           GameManager.s_GameManger.m_myTeam.GetFanBase();
	    m_FansUpgradeBonus.text = string.Format("+ {0} FANS", newFansTotalDiff);

	    int facilitesLevel = GameManager.s_GameManger.m_myTeam.Facilities;
	    m_FacilitesLevel.text = string.Format("LEVEL: {0}", facilitesLevel + 1);
	    m_FacilitesUpgradeCost.text = string.Format("{0}",
	        MyUtils.ConvertNumber(GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(facilitesLevel)));

	    //m_FansUpgradeTitleText.text = string.Format("Total Fans: {0}", GameManager.s_GameManger.m_myTeam.GetFanBase());
	    //m_FansUpgradeText.text = string.Format("Invest in your fans{0}{1:C0}",
	    //        System.Environment.NewLine,
	    //        GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(GameManager.s_GameManger.m_myTeam.Fans));

	    //m_FacilitiesUpgradeTitleText.text = string.Format("Facilities Level: {0}",
	    //    GameManager.s_GameManger.m_myTeam.Facilities + 1);
	    //m_FacilitiesUpgradeText.text = string.Format("Improve your training facilities{0}{1:C0}",
	    //        System.Environment.NewLine,
	    //        GameManager.s_GameManger.m_GameSettings.GetFacilitiesCostForLevel(
	    //            GameManager.s_GameManger.m_myTeam.Facilities));

	    //m_StadiumUpgradeTitleText.text = string.Format("Stadium Capacity: {0}k",
	    //    GameManager.s_GameManger.m_myTeam.TotalSeats/1000);
	    //m_StadiumUpgradeText.text = string.Format("Increase your stadium capacity{0}{1:C0}",
	    //    System.Environment.NewLine, GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(
	    //            GameManager.s_GameManger.m_myTeam.Stadium));
	}
	
	// Update is called once per frame
	void Update ()
	{
        //int costForFans = GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(GameManager.s_GameManger.m_myTeam.Fans);
        //m_FansUpgradeTitleText.text = string.Format("Total Fans: {0}", GameManager.s_GameManger.m_myTeam.GetFanBase());
        //m_FansUpgradeText.text = string.Format("Invest in your fans{0}{1:C0}",
        //        System.Environment.NewLine,
        //        costForFans);
        //m_FansUpgradeButton.interactable = GameManager.s_GameManger.GetCash() >= costForFans;

        //int costForFacilities = GameManager.s_GameManger.m_GameSettings.GetFacilitiesCostForLevel(GameManager.s_GameManger.m_myTeam.Facilities);
        //m_FacilitiesUpgradeTitleText.text = string.Format("Facilities Level: {0}",
        //    GameManager.s_GameManger.m_myTeam.Facilities + 1);
        //m_FacilitiesUpgradeText.text = string.Format("Improve your training facilities{0}{1:C0}",
        //        System.Environment.NewLine,
        //        costForFacilities);
        //m_FacilitiesUpgradeButton.interactable = GameManager.s_GameManger.GetCash() >= costForFacilities;

        //int costForStadium = GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(GameManager.s_GameManger.m_myTeam.Stadium);
        //m_StadiumUpgradeTitleText.text = string.Format("Stadium Capacity: {0}k",
        //    GameManager.s_GameManger.m_myTeam.TotalSeats / 1000);
        //m_StadiumUpgradeText.text = string.Format("Increase your stadium capacity{0}{1:C0}",
        //    System.Environment.NewLine, costForStadium);
        //m_StadiumUpgradeButton.interactable = GameManager.s_GameManger.GetCash() >= costForStadium;
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

    public void OnImproveClick()
    {
        m_ImprovePanel.SetActive(true);
        m_BuyPanel.SetActive(false);
        m_ImproveUnderlineObject.SetActive(true);
        m_BuyUnderlineObject.SetActive(false);
    }

    public void OnBuyClick()
    {
        m_ImprovePanel.SetActive(false);
        m_BuyPanel.SetActive(true);
        m_ImproveUnderlineObject.SetActive(false);
        m_BuyUnderlineObject.SetActive(true);
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
