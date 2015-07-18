using UnityEngine;
using UnityEngine.UI;

public class WonPopupScript : MonoBehaviour
{
    public Text m_Score;
    public Image m_HomeTeamLogo;
    public Text m_HomeTeamName;
    public Image m_AwayTeamLogo;
    public Text m_AwayTeamName;
    public Button m_ShareCheckbox;

    public Sprite m_Checked;
    public Sprite m_UnChecked;

    public GameObject m_FinancialReportPopup;
    public string m_OnContinueNextScene;

    private MatchInfo m_LastMatchInfo;
    private bool v_IsShare = true;

	// Use this for initialization
	void Start ()
	{
        m_LastMatchInfo = GameManager.s_GameManger.m_myTeam.GetLastMatchInfo();

        m_Score.text = string.Format("{0} - {1}", m_LastMatchInfo.GetHomeGoals(), m_LastMatchInfo.GetAwayGoals());
        m_HomeTeamName.text = m_LastMatchInfo.GetHomeTeamString();
        m_AwayTeamName.text = m_LastMatchInfo.GetAwayTeamString();
        m_HomeTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(m_LastMatchInfo.HomeTeamLogoIdx);
        m_AwayTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(m_LastMatchInfo.AwayTeamLogoIdx);
	}

    public void OnCloseClick()
    {
        v_IsShare = false;
        OnContinueClick();
    }

    public void OnCheckboxClick()
    {
        v_IsShare = !v_IsShare;
        m_ShareCheckbox.image.sprite = v_IsShare ? m_Checked : m_UnChecked;
    }

    public void OnContinueClick()
    {
        if (v_IsShare)
        {
            ShareWithFriends();
        }
        else
        {
            gameObject.SetActive(false);
            m_FinancialReportPopup.SetActive(true);
        }
    }

    public void ShareWithFriends()
    {
        //string linkCaption = string.Format("I've just {0} in Tap Manager!", getWinOrLoseStr());
        //string linkName = "Check out this game!";
        //string link = "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag" + (FB.IsLoggedIn ? FB.UserId : "guest");
        if (FB.IsLoggedIn)
        {
            FB.Feed(linkCaption: string.Format("I've just WON in Tap Manager!"),
                linkName: "Check out this game!",
                link: "https://play.google.com/store/apps/details?id=com.TeamVanilla.TKSO",
                callback: isOK);
        }
        else
        {
            FacebookManager.s_FBManger.Login(shareWithFriendsDelegate);
        }
        
    }

    private void isOK(FBResult i_Response)
    {
        if (!string.IsNullOrEmpty(i_Response.Error))
        {
            Debug.Log("ERROR" + i_Response.Error);
        }
        else
        {
            Debug.Log("Share succesfully");
            Debug.Log(i_Response.Text);
        }
        gameObject.SetActive(false);
        m_FinancialReportPopup.SetActive(true);
    }

    private void shareWithFriendsDelegate(FBResult i_Response)
    {
        if (!string.IsNullOrEmpty(i_Response.Error))
        {
            Debug.LogError("ERROR: " + i_Response.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                FB.Feed(linkCaption: string.Format("I've just WON in Tap Manager!"),
                    linkName: "Check out this game!",
                    link: "https://play.google.com/store/apps/details?id=com.TeamVanilla.TKSO",
                    callback: isOK);
            }
            else
            {
                gameObject.SetActive(false);
                m_FinancialReportPopup.SetActive(true);
            }
        }
    }
}
