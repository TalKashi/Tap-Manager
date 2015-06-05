using UnityEngine;
using UnityEngine.UI;

public class Scoarboard : MonoBehaviour
{

    public Text m_HomeTeamName;
    public Image m_HomeTeamLogo;
    public Text m_AwayTeamName;
    public Image m_AwayTeamLogo;
    public Text m_CrowdAttendence;
    public Text m_FinalScore;
    public Text m_CurrentMinute;
    public GameObject m_Popup;

    void Start()
    {
        MatchInfo lastMatch = GameManager.s_GameManger.m_myTeam.GetLastMatchInfo();
        m_HomeTeamName.text = lastMatch.GetHomeTeamString();
        m_AwayTeamName.text = lastMatch.GetAwayTeamString();
        m_FinalScore.text = string.Format("{0} - {1}", lastMatch.GetHomeGoals(), lastMatch.GetAwayGoals());
        m_CurrentMinute.text = "90:00";
        m_CrowdAttendence.text = string.Format("Crowd: {0}", lastMatch.GetTotalCrowd());

        // Init logos
        m_HomeTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(lastMatch.HomeTeamLogoIdx);
        m_AwayTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(lastMatch.AwayTeamLogoIdx);
        

		playSound ();
    }

    public void OnSkipClick()
    {
        m_Popup.SetActive(true);
    }

	private void playSound()
    {
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
