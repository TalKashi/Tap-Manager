using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class ClubHistoryScreen : MonoBehaviour
{
    public Text m_Text;

    void Start()
    {
        GamesStatistics allTimeStatistics = GameManager.s_GameManger.m_myTeam.AllTimeStatistics;
        RecordsStatistics teamRecords = GameManager.s_GameManger.m_myTeam.TeamRecords;

        m_Text.text = string.Format(
            @"Longest Winning Streak: {0}
Longest Losing Streak: {1}
Longest Undefeated Streak: {2}
Longest Winning-less streak: {3}

Total Wins: {4}
Total Lost: {5}
Total Draws: {6}

Total Goals Scored: {7}
Total Goals Conceded: {8}
Total Crowd: {9}",
            teamRecords.longestWinStreak, teamRecords.longestLoseStreak, teamRecords.longestUndefeatedStreak,
            teamRecords.longestWinlessStreak,
            allTimeStatistics.wins, allTimeStatistics.losts, allTimeStatistics.draws,
            allTimeStatistics.goalsFor, allTimeStatistics.goalsAgainst, allTimeStatistics.crowd);
    }

    public void OnSoomlaTestClick()
    {
        StoreInventory.BuyItem(Store.INSTANT_TRAIN.ItemId);
    }
}
