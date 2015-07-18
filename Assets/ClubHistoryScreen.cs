using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class ClubHistoryScreen : MonoBehaviour
{
    public GameObject m_LoadingDataImage;
    public GameObject m_GenericPopup;

	public Text	m_LongestWinningStreak;
	public Text m_LongestLosingStreak;
	public Text m_LongestUndefeatedStreak;
	public Text m_LongestWinninglessstreak;

	public Text m_TotalWins;
	public Text m_TotalLost;
	public Text m_TotalDraws;

	public Text m_TotalGoalsScored;
	public Text m_TotalGoalsConceded;
	public Text m_TotalCrowd;
	public Text m_TotalChampionships;

    void Start()
    {
        GameManager.s_GameManger.m_GenericPopup = m_GenericPopup;
        GameManager.s_GameManger.CurrentSceneHeaderName = GameManager.k_ClubInfo;
        GameManager.s_GameManger.CurrentScene = GameManager.k_ClubInfo;

        GamesStatistics allTimeStatistics = GameManager.s_GameManger.m_myTeam.AllTimeStatistics;
        RecordsStatistics teamRecords = GameManager.s_GameManger.m_myTeam.TeamRecords;
		m_LongestWinningStreak.text = teamRecords.longestWinStreak.ToString();  
		m_LongestLosingStreak.text = teamRecords.longestLoseStreak.ToString();
		m_LongestUndefeatedStreak.text = teamRecords.longestUndefeatedStreak.ToString();
		m_LongestWinninglessstreak.text = teamRecords.longestWinlessStreak.ToString();
		m_TotalWins.text =  allTimeStatistics.wins.ToString();
		m_TotalLost.text = allTimeStatistics.losts.ToString();
		m_TotalDraws.text = allTimeStatistics.draws.ToString();
		m_TotalGoalsScored.text = allTimeStatistics.goalsFor.ToString();
		m_TotalGoalsConceded.text = allTimeStatistics.goalsAgainst.ToString();
		m_TotalCrowd.text = allTimeStatistics.crowd.ToString();
        m_TotalChampionships.text = GameManager.s_GameManger.m_myTeam.TotalChampionships.ToString();
    }

    void Update()
    {
        m_LoadingDataImage.SetActive(GameManager.s_GameManger.IsLoadingData);
    }

}
