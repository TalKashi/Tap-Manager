using System;
using System.Collections.Generic;
using UnityEngine;

public class MyUtils
{
    public static void LoadTeamData(Dictionary<string, object> i_Json, ref TeamScript io_Team)
    {
        object teamDict;

        if (io_Team == null)
        {
            io_Team = new TeamScript();
        }

        if (i_Json.TryGetValue("team", out teamDict))
        {
            extractTeamData((Dictionary<string, object>) teamDict, ref io_Team);
        }
    }

    private static void extractTeamData(Dictionary<string, object> i_TeamJson, ref TeamScript io_Team)
    {
        object id, shopDict, gamesHistoryDict, crowdDict, lastGameInfoDict;
        object lastResultEnum, isLastGameIsHomeGameBool, statisticsDict;

        if (i_TeamJson.TryGetValue("id", out id))
        {
            io_Team.ID = id.ToString();
        }
        else
        {
            Debug.LogError("Failed to get ID from json");
        }

        if (i_TeamJson.TryGetValue("lastResult", out lastResultEnum))
        {
            io_Team.LastResult = (eResult)lastResultEnum;
        }
        else
        {
            Debug.LogError("Failed to get LastResult(Enum) from json");
        }

        if (i_TeamJson.TryGetValue("isLastGameIsHomeGame", out isLastGameIsHomeGameBool))
        {
            io_Team.IsLastGameIsHomeGame = (bool)isLastGameIsHomeGameBool;
        }
        else
        {
            Debug.LogError("Failed to get LastResult(Enum) from json");
        }

        if (i_TeamJson.TryGetValue("shop", out shopDict))
        {
            extractShopData((Dictionary<string, object>) shopDict, ref io_Team);
        }
        else
        {
            Debug.LogError("Failed to get Shop data from json");
        }

        if (i_TeamJson.TryGetValue("gamesHistory", out gamesHistoryDict))
        {
            extractGamesHistoryData((Dictionary<string, object>)gamesHistoryDict, ref io_Team);
        }
        else
        {
            Debug.LogError("Failed to get GamesHistory data from json");
        }

        if (i_TeamJson.TryGetValue("crowd", out crowdDict))
        {
            extractCrowdData((Dictionary<string, object>)crowdDict, ref io_Team);
        }
        else
        {
            Debug.LogError("Failed to get Crowd data from json");
        }

        if (i_TeamJson.TryGetValue("lastGameInfo", out lastGameInfoDict))
        {
            extractLastGameInfoData((Dictionary<string, object>)lastGameInfoDict, ref io_Team);
        }
        else
        {
            Debug.LogError("Failed to get LastGameInfo data from json");
        }

        if (i_TeamJson.TryGetValue("statistics", out statisticsDict))
        {
            extractStatisticsData((Dictionary<string, object>)statisticsDict, ref io_Team);
        }
        else
        {
            Debug.LogError("Failed to get Statistics data from json");
        }
    }
}