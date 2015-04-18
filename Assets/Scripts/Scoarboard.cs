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
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>().playDrawSound();
			break;

			case eResult.Won:
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>().playWinSound();
			break;

			case eResult.Lost:
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>().playLoseSound();
			break;

		}
	}
}
