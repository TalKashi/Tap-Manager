using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class NewShopGUI : MonoBehaviour
{
    public GameObject m_ImprovePanel;
    public GameObject m_BuyPanel;
    public GameObject m_LoadingDataImage;
    public GameObject m_GenericPopup;

    public Text m_StadiumLevel;
    public Text m_StadiumSeats;
    public Text m_StadiumUpgradeCost;
    public Text m_StadiumUpgradeBonus;
    public Image m_StadiumIconBackground;

    public Text m_FansLevel;
    public Text m_FansTotal;
    public Text m_FansUpgradeCost;
    public Text m_FansUpgradeBonus;
    public Image m_FansIconBackground;

    public Text m_FacilitesLevel;
    public Text m_FacilitesPower; // not used ATM
    public Text m_FacilitesUpgradeCost;
    public Text m_FacilitesUpgradeBonus; // Not used ATM
    public Image m_FacilitesIconBackground;

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

    public Button m_FansUpgradeButton;
    public Button m_FacilitiesUpgradeButton;
    public Button m_StadiumUpgradeButton;

    private bool m_WaitingForServer = false;
    private bool m_ShowImprove = true;
    private const int k_BasicBoostAmount = 10;

	// Use this for initialization
	void Start ()
	{
        GameManager.s_GameManger.m_GenericPopup = m_GenericPopup;
	    GameManager.s_GameManger.CurrentSceneHeaderName = GameManager.k_Shop;
        GameManager.s_GameManger.CurrentScene = GameManager.k_Shop;

	    Color myColor = MyUtils.GetColorByTeamLogo(GameManager.s_GameManger.m_myTeam.LogoIdx);
	    m_ImproveUnderline.color = myColor;
        m_BuyUnderline.color = myColor;
	    m_StadiumIconBackground.color = myColor;
        m_FansIconBackground.color = myColor;
        m_FacilitesIconBackground.color = myColor;

	    updateGUI();
	}

    private void updateGUI()
    {
        int stadiumLevel = GameManager.s_GameManger.m_myTeam.Stadium;
        long upgradeCost = GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(stadiumLevel);
        m_StadiumLevel.text = string.Format("LEVEL: {0}", stadiumLevel + 1);
        m_StadiumSeats.text = string.Format("SEATS: {0}", MyUtils.ConvertNumber(GameManager.s_GameManger.m_myTeam.TotalSeats));
	    m_StadiumUpgradeCost.text = string.Format("{0}",
	        MyUtils.ConvertNumber(upgradeCost));
	    int newCapacityDiff = GameManager.s_GameManger.m_myTeam.TotalSeatsForLevel(stadiumLevel + 1) -
	                      GameManager.s_GameManger.m_myTeam.TotalSeats;
	    m_StadiumUpgradeBonus.text = string.Format("+ {0} SEATS", MyUtils.ConvertNumber(newCapacityDiff));
        m_StadiumUpgradeButton.interactable = upgradeCost < GameManager.s_GameManger.GetCash();

	    int fansLevel = GameManager.s_GameManger.m_myTeam.Fans;
        upgradeCost = GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(fansLevel);
	    m_FansLevel.text = string.Format("LEVEL: {0}", fansLevel + 1);
	    m_FansTotal.text = string.Format("TOTAL: {0}", MyUtils.ConvertNumber(GameManager.s_GameManger.m_myTeam.GetFanBase()));
	    m_FansUpgradeCost.text = string.Format("{0}",MyUtils.ConvertNumber(upgradeCost));
	    int newFansTotalDiff = GameManager.s_GameManger.m_myTeam.GetFanBaseByLevel(fansLevel + 1) -
	                           GameManager.s_GameManger.m_myTeam.GetFanBase();
	    m_FansUpgradeBonus.text = string.Format("+ {0} FANS", newFansTotalDiff);
        m_FansUpgradeButton.interactable = upgradeCost < GameManager.s_GameManger.GetCash();

	    int facilitesLevel = GameManager.s_GameManger.m_myTeam.Facilities;
        upgradeCost = GameManager.s_GameManger.m_GameSettings.GetFacilitiesCostForLevel(facilitesLevel);
	    m_FacilitesLevel.text = string.Format("LEVEL: {0}", facilitesLevel + 1);
	    m_FacilitesUpgradeCost.text = string.Format("{0}",
	        MyUtils.ConvertNumber(GameManager.s_GameManger.m_GameSettings.GetFacilitiesCostForLevel(facilitesLevel)));
        m_FacilitiesUpgradeButton.interactable = upgradeCost < GameManager.s_GameManger.GetCash();
        float tapPowerDiff = ((k_BasicBoostAmount * (facilitesLevel + 2)) / ((float) k_BasicBoostAmount * (facilitesLevel + 1))) - 1f;
        m_FacilitesUpgradeBonus.text = string.Format("+ {0:P0} IMPROVE", tapPowerDiff);
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
            updateGUI();
	    }
	}

    public void OnFansClick()
    {
        //GameManager.s_GameManger.FansUpdate ();
        long upgradeCost = GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(GameManager.s_GameManger.m_myTeam.GetFansLevel());
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

    public void OnSingleInstantTrainClick()
    {
        StoreInventory.BuyItem(Store.INSTANT_TRAIN.ItemId);
    }

    public void OnPackInstantTrainClick()
    {
        StoreInventory.BuyItem(Store.INSTANT_TRAIN_PACK.ItemId);
    }

    public void On500KCoinsClick()
    {
        StoreInventory.BuyItem(Store.FIVE_K_COINS.ItemId);
    }

    public void On5MCoinsClick()
    {
        StoreInventory.BuyItem(Store.FIVE_M_COINS.ItemId);
    }

    public void On10MCoinsClick()
    {
        StoreInventory.BuyItem(Store.TEN_M_COINS.ItemId);
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
            MyUtils.DisplayErrorMessage(m_GenericPopup);
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
                    MyUtils.DisplayOutOfSyncErrorMessage(m_GenericPopup);
                    StartCoroutine(GameManager.s_GameManger.SyncClientDB());
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
        long upgradeCost = GameManager.s_GameManger.m_GameSettings.GetFacilitiesCostForLevel(GameManager.s_GameManger.m_myTeam.GetFacilitiesLevel());
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
            MyUtils.DisplayErrorMessage(m_GenericPopup);
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
                    MyUtils.DisplayOutOfSyncErrorMessage(m_GenericPopup);
                    StartCoroutine(GameManager.s_GameManger.SyncClientDB());
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

        long upgradeCost = GameManager.s_GameManger.m_GameSettings.GetStadiumCostForLevel(GameManager.s_GameManger.m_myTeam.GetStadiumLevel());
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
            MyUtils.DisplayErrorMessage(m_GenericPopup);
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
                    MyUtils.DisplayOutOfSyncErrorMessage(m_GenericPopup);
                    StartCoroutine(GameManager.s_GameManger.SyncClientDB());
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
