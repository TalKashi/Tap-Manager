using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShopScript : MonoBehaviour {

	public Text m_fansLevel;
	public Text m_facilitiesLevel;
	public Text m_stadiumLevel;

    private bool m_WaitingForServer = false;

	void Update()
	{
		m_fansLevel.text = "FANS LEVEL: " + (GameManager.s_GameManger.m_myTeam.GetFansLevel() + 1);
        m_facilitiesLevel.text = "FACILITIES LEVEL: " + (GameManager.s_GameManger.m_myTeam.GetFacilitiesLevel() + 1);
        m_stadiumLevel.text = "STADIUM LEVEL: " + (GameManager.s_GameManger.m_myTeam.GetStadiumLevel() + 1);

	    if (m_WaitingForServer)
	    {
	        // TODO: Enable server send msg screen
	    }
	}

	public void OnFansClick()
	{
		//GameManager.s_GameManger.FansUpdate ();
        int upgradeCost = GameManager.s_GameManger.m_GameSettings.GetFansCostForLevel(GameManager.s_GameManger.m_myTeam.GetFansLevel());
        if (GameManager.s_GameManger.GetCash() >= upgradeCost)
        {
            StartCoroutine(sendFansClickToServer(upgradeCost));
        }
        else
        {
            // TODO: Notify player that he doesn't have money
        }
	}

    IEnumerator sendFansClickToServer(int i_UpgradeCost)
    {
        // TODO: Add 'loading' popup
        m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("email", GameManager.s_GameManger.m_User.Email);

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
            // Check ok response
            // if new user go to new team screen
            // else go to home page with team data
            switch (request.text)
            {
                case "ok":
                    GameManager.s_GameManger.FansUpdate(i_UpgradeCost);
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
            StartCoroutine(sendFacilitiesClickToServer(upgradeCost));
        }
        else
        {
            // TODO: Notify player that he doesn't have money
        }
	}

    IEnumerator sendFacilitiesClickToServer(int i_UpgradeCost)
    {
        // TODO: Add 'loading' popup
        m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("email", GameManager.s_GameManger.m_User.Email);

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
            // Check ok response
            // if new user go to new team screen
            // else go to home page with team data
            switch (request.text)
            {
                case "ok":
                    GameManager.s_GameManger.FacilitiesUpdate(i_UpgradeCost);
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
            StartCoroutine(sendStadiumClickToServer(upgradeCost));
        }
        else
        {
            // TODO: Notify player that he doesn't have money
        }
	}

    IEnumerator sendStadiumClickToServer(int i_UpgradeCost)
    {
        // TODO: Add 'loading' popup
        m_WaitingForServer = true;
        WWWForm form = new WWWForm();
        form.AddField("email", GameManager.s_GameManger.m_User.Email);

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
            // Check ok response
            switch (request.text)
            {
                case "ok":
                    GameManager.s_GameManger.StadiumUpdate(i_UpgradeCost);
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

    public void OnNextMatchClick()
    {
        FixturesManager.s_FixturesManager.ExecuteNextFixture();
    }
}
