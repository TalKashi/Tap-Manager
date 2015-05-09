using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoarboard : MonoBehaviour
{

    public Text m_HomeTeamName;
    public Text m_HomeTeamGoals;
    public Text m_AwayTeamName;
    public Text m_AwayTeamGoals;
    public Text m_CrowdAttendence;

    void Start()
    {
        MatchInfo lastMatch = GameManager.s_GameManger.m_myTeam.GetLastMatchInfo();
        m_HomeTeamName.text = lastMatch.GetHomeTeamString();
        m_AwayTeamName.text = lastMatch.GetAwayTeamString();
        m_HomeTeamGoals.text = lastMatch.GetHomeGoals().ToString();
        m_AwayTeamGoals.text = lastMatch.GetAwayGoals().ToString();
        m_CrowdAttendence.text = lastMatch.GetTotalCrowd().ToString();
		playSound ();
    }

	private void playSound(){
		eResult lastMatch = GameManager.s_GameManger.m_myTeam.GetLastResult();
		switch (lastMatch) {

			case eResult.Draw:
				SoundManager.s_SoundManager.playDrawSound();
			break;

			case eResult.Won:
                SoundManager.s_SoundManager.playWinSound();
			break;

			case eResult.Lost:
                SoundManager.s_SoundManager.playLoseSound();
			break;

		}
	}

    public void ShareWithFriends()
    {
        //string linkCaption = string.Format("I've just {0} in Tap Manager!", getWinOrLoseStr());
        //string linkName = "Check out this game!";
        //string link = "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag" + (FB.IsLoggedIn ? FB.UserId : "guest");
        FB.Feed(linkCaption: string.Format("I've just {0} in Tap Manager!", getWinOrLoseStr()),
            linkName: "Check out this game!",
            link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag" + (FB.IsLoggedIn ? FB.UserId : "guest"
            ));
    }

    string getWinOrLoseStr()
    {
        MatchInfo lastGame = GameManager.s_GameManger.m_myTeam.GetLastMatchInfo();
        bool k_IsHomeMatch = GameManager.s_GameManger.m_myTeam.IsLastGameIsHomeGame;
        string vs = k_IsHomeMatch ? lastGame.GetAwayTeamString() : lastGame.GetHomeTeamString();
        switch (GameManager.s_GameManger.m_myTeam.GetLastResult())
        {
            case eResult.Draw:
                return string.Format("drawn {0}-{1} vs. {2}", lastGame.GetHomeGoals(), lastGame.GetAwayGoals(), vs);
            case eResult.Lost:
                return string.Format("lost {0}-{1} vs. {2}", lastGame.GetHomeGoals(), lastGame.GetAwayGoals(), vs);
            case eResult.Won:
                return string.Format("won {0}-{1} vs. {2}", lastGame.GetHomeGoals(), lastGame.GetAwayGoals(), vs);
        }

        // Not suppose to reach here!
        return "";
    }
}
