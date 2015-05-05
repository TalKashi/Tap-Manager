using System;
using System.Collections.Generic;
using UnityEngine;

public class MyUtils
{
    public static void LoadLeagueData(Dictionary<string, object> i_Json, ref TeamScript[] io_AllTeams)
    {
        object leagueArr;

        if (i_Json.TryGetValue("league", out leagueArr))
        {
            extractLeagueTeams((List<object>) leagueArr, ref io_AllTeams);
        }
        else
        {
            Debug.LogError("Failed to get League from json");
        }
    }

    private static void extractLeagueTeams(List<object> leagueArr, ref TeamScript[] io_AllTeams)
    {
        io_AllTeams = new TeamScript[leagueArr.Count];

        int i = 0;
        foreach (object team in leagueArr)
        {
            extractTeamData((Dictionary<string, object>)team, ref io_AllTeams[i]);
            i++;
        }
    }

    public static void LoadBucketData(Dictionary<string, object> i_BucketDict, ref Bucket io_Bucket)
    {
        object valueForSecond, maxAmount, lastFlush, level;

        if (io_Bucket == null)
        {
            io_Bucket = new Bucket();
        }

        if (i_BucketDict.TryGetValue("valueForSecond", out valueForSecond))
        {
            io_Bucket.SetValuePerSecond(float.Parse(valueForSecond.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get ValuePerSecond from json");
        }

        if (i_BucketDict.TryGetValue("maxAmount", out maxAmount))
        {
            io_Bucket.SetMaxAmount(int.Parse(maxAmount.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get MaxAmount from json");
        }

        if (i_BucketDict.TryGetValue("lastFlush", out lastFlush))
        {
            io_Bucket.LastFlush = (DateTime)lastFlush;
        }
        else
        {
            Debug.LogError("Failed to get LastFlush from json");
        }

        if (i_BucketDict.TryGetValue("level", out level))
        {
            io_Bucket.SetLevel(int.Parse(level.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get Level from json");
        }
    }

    public static void LoadSquadData(Dictionary<string, object> i_Json, ref SquadScript io_Squad)
    {
        object squadDict;

        if (io_Squad == null)
        {
            io_Squad = new SquadScript();
        }

        if (i_Json.TryGetValue("squad", out squadDict))
        {
            extractSquadData((Dictionary<string, object>)squadDict, ref io_Squad);
        }
    }

    private static void extractSquadData(Dictionary<string, object> i_SquadDict, ref SquadScript io_Squad)
    {
        object playerArr;

        if (i_SquadDict.TryGetValue("players", out playerArr))
        {
            extractPlayersData((List<object>)playerArr, io_Squad);
        }
        else
        {
            Debug.LogError("Failed to get Players from json");
        }
    }

    private static void extractPlayersData(List<object> i_PlayersList, SquadScript io_Squad)
    {
        PlayerScript[] players = new PlayerScript[i_PlayersList.Count];
        int i = 0;
        foreach (object player in i_PlayersList)
        {
            players[i] = extractOnePlayerData((Dictionary<string, object>)player);
            i++;
        }

        io_Squad.Players = players;
    }

    private static PlayerScript extractOnePlayerData(Dictionary<string, object> i_PlayerDict)
    {
        object pos, firstName, lastName, salary, isInjured, age, gamesPlayed, priceToBoost;
        object goalsScored, level, price, boost, isPlaying, yearJoinedTheClub, playerImage;
        PlayerScript player = new PlayerScript();

        if (i_PlayerDict.TryGetValue("position", out pos))
        {
            player.SetPosition((ePosition)pos);
        }
        else
        {
            Debug.LogError("Failed to get Position from json");
        }

        if (i_PlayerDict.TryGetValue("firstName", out firstName))
        {
            player.SetFirstName((string)firstName);
        }
        else
        {
            Debug.LogError("Failed to get FirstName from json");
        }

        if (i_PlayerDict.TryGetValue("lastName", out lastName))
        {
            player.SetLastName((string)lastName);
        }
        else
        {
            Debug.LogError("Failed to get LastName from json");
        }

        if (i_PlayerDict.TryGetValue("salary", out salary))
        {
            player.SetSalary(int.Parse(salary.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get Salary from json");
        }

        if (i_PlayerDict.TryGetValue("isInjured", out isInjured))
        {
            player.SetIsInjured((bool)isInjured);
        }
        else
        {
            Debug.LogError("Failed to get IsInjured from json");
        }

        if (i_PlayerDict.TryGetValue("age", out age))
        {
            player.SetAge(int.Parse(age.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get Age from json");
        }

        if (i_PlayerDict.TryGetValue("gamesPlayed", out gamesPlayed))
        {
            player.SetGamesPlayed(int.Parse(gamesPlayed.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get GamesPlayed from json");
        }

        if (i_PlayerDict.TryGetValue("goalsScored", out goalsScored))
        {
            player.SetGoalsScored(int.Parse(goalsScored.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get GoalScores from json");
        }

        if (i_PlayerDict.TryGetValue("level", out level))
        {
            player.SetPlayerLevel(int.Parse(level.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get Level from json");
        }

        if (i_PlayerDict.TryGetValue("price", out price))
        {
            player.SetPlayerPrice(int.Parse(price.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get Price from json");
        }

        if (i_PlayerDict.TryGetValue("priceToBoost", out priceToBoost))
        {
            player.SetPriceToBoostPlayer(int.Parse(priceToBoost.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get PriceToBoost from json");
        }

        if (i_PlayerDict.TryGetValue("boost", out boost))
        {
            player.SetBoostLevel(int.Parse(boost.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get Boost from json");
        }

        if (i_PlayerDict.TryGetValue("isPlaying", out isPlaying))
        {
            player.SetIsPlaying((bool)isPlaying);
        }
        else
        {
            Debug.LogError("Failed to get IsPlaying from json");
        }

        if (i_PlayerDict.TryGetValue("yearJoinedTheClub", out yearJoinedTheClub))
        {
            player.SetYearJoinedTheClub(int.Parse(yearJoinedTheClub.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get YearJoinedTheClub from json");
        }

        if (i_PlayerDict.TryGetValue("playerImage", out playerImage))
        {
            player.PlayerSpriteIndex = int.Parse(playerImage.ToString());
        }
        else
        {
            Debug.LogError("Failed to get PlayerImage from json");
        }

        return player;
    }

    public static void LoadTeamData(Dictionary<string, object> i_Json, ref TeamScript io_Team)
    {
        object teamDict;

        if (io_Team == null)
        {
            io_Team = new TeamScript();
        }

        if (i_Json.TryGetValue("team", out teamDict))
        {
            extractTeamData((Dictionary<string, object>)teamDict, ref io_Team);
        }
        else
        {
            Debug.LogError("Failed to get Team from json");
        }
    }

    private static void extractTeamData(Dictionary<string, object> i_TeamJson, ref TeamScript io_Team)
    {
        object id, shopDict, gamesHistoryDict, additionalFans, lastGameInfoDict;
        object lastResultEnum, isLastGameIsHomeGameBool, statisticsDict, teamName;

        if (io_Team == null)
        {
            io_Team = new TeamScript();
        }

        if (i_TeamJson.TryGetValue("_id", out id))
        {
            io_Team.ID = id.ToString();
        }
        else
        {
            Debug.LogError("Failed to get ID from json");
        }

        if (i_TeamJson.TryGetValue("teamName", out teamName))
        {
            io_Team.Name = teamName.ToString();
        }
        else
        {
            Debug.LogError("Failed to get TeamName from json");
        }

        if (i_TeamJson.TryGetValue("lastResult", out lastResultEnum))
        {
            io_Team.LastResult = (eResult) int.Parse(lastResultEnum.ToString());
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

        if (i_TeamJson.TryGetValue("additionalFans", out additionalFans))
        {
            io_Team.SetAdditionalFans(int.Parse(additionalFans.ToString()));
        }
        else
        {
            Debug.LogError("Failed to get AdditionalFans from json");
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

    private static void extractStatisticsData(Dictionary<string, object> i_StatsDict, ref TeamScript io_Team)
    {
        object longestWinStreak, longestLoseStreak, longestWinlessStreak;
        object longestUndefeatedStreak, biggestWinRecord, biggestLoseRecord;
        object currentWinStreak, currentLoseStreak, currentUndefeatedStreak, currentWinlessStreak;
        RecordsStatistics records = new RecordsStatistics();

        if (!i_StatsDict.TryGetValue("longestWinStreak", out longestWinStreak))
        {
            return;
        }

        if (!i_StatsDict.TryGetValue("currentWinlessStreak", out currentWinlessStreak))
        {
            return;
        }

        if (!i_StatsDict.TryGetValue("longestLoseStreak", out longestLoseStreak))
        {
            return;
        }

        if (!i_StatsDict.TryGetValue("longestUndefeatedStreak", out longestUndefeatedStreak))
        {
            return;
        }
        if (!i_StatsDict.TryGetValue("currentWinStreak", out currentWinStreak))
        {
            return;
        }
        if (!i_StatsDict.TryGetValue("biggestLoseRecord", out biggestLoseRecord))
        {
            return;
        }
        if (!i_StatsDict.TryGetValue("longestWinlessStreak", out longestWinlessStreak))
        {
            return;
        }
        if (!i_StatsDict.TryGetValue("currentLoseStreak", out currentLoseStreak))
        {
            return;
        }
        if (!i_StatsDict.TryGetValue("currentUndefeatedStreak", out currentUndefeatedStreak))
        {
            return;
        }
        if (!i_StatsDict.TryGetValue("longestWinStreak", out longestWinStreak))
        {
            return;
        }
        if (!i_StatsDict.TryGetValue("biggestWinRecord", out biggestWinRecord))
        {
            return;
        }

        records.longestWinStreak = int.Parse(longestWinStreak.ToString());
        records.longestLoseStreak = int.Parse(longestLoseStreak.ToString());
        records.longestWinlessStreak = int.Parse(longestWinlessStreak.ToString());
        records.longestUndefeatedStreak = int.Parse(longestUndefeatedStreak.ToString());

        records.biggestWinRecord = int.Parse(biggestWinRecord.ToString());
        records.biggestLoseRecord = int.Parse(biggestLoseRecord.ToString());

        records.currentWinStreak = int.Parse(currentWinStreak.ToString());
        records.currentLoseStreak = int.Parse(currentLoseStreak.ToString());
        records.currentWinlessStreak = int.Parse(currentWinlessStreak.ToString()) ;
        records.currentUndefeatedStreak = int.Parse(currentUndefeatedStreak.ToString());

    }

    private static void extractLastGameInfoData(Dictionary<string, object> i_LastGameInfoDict, ref TeamScript io_Team)
    {
        object homeTeam, awayTeam, homeTeamGoals, awayTeamGoals, crowdAtMatch;


        if (!i_LastGameInfoDict.TryGetValue("homeTeam", out homeTeam))
        {
            return;
        }

        if (!i_LastGameInfoDict.TryGetValue("awayTeam", out awayTeam))
        {
            return;
        }

        if (!i_LastGameInfoDict.TryGetValue("homeTeamGoals", out homeTeamGoals))
        {
            return;
        }

        if (!i_LastGameInfoDict.TryGetValue("awayTeamGoals", out awayTeamGoals))
        {
            return;
        }

        if (!i_LastGameInfoDict.TryGetValue("crowdAtMatch", out crowdAtMatch))
        {
            return;
        }

        io_Team.SetLastGameInfo(new MatchInfo((string)homeTeam, (string)awayTeam,int.Parse( homeTeamGoals.ToString()),int.Parse( awayTeamGoals.ToString())
                            ,int.Parse( crowdAtMatch.ToString())));
    }

    private static void extractShopData(Dictionary<string, object> i_ShopDict, ref TeamScript io_Team)
    {
        object fansLevel, facilitiesLevel, stadiumLevel;

        if (i_ShopDict.TryGetValue("fansLevel", out fansLevel))
        {
            io_Team.Fans = float.Parse(fansLevel.ToString());
        }
        else
        {
            Debug.LogError("Failed to get FansLevel data from json");
        }

        if (i_ShopDict.TryGetValue("facilitiesLevel", out facilitiesLevel))
        {
            io_Team.Facilities = float.Parse(facilitiesLevel.ToString());
        }
        else
        {
            Debug.LogError("Failed to get FacilitiesLevel data from json");
        }

        if (i_ShopDict.TryGetValue("stadiumLevel", out stadiumLevel))
        {
            io_Team.Stadium = float.Parse(stadiumLevel.ToString());
        }
        else
        {
            Debug.LogError("Failed to get StadiumLevel data from json");
        }
    }

    private static void extractGamesHistoryData(Dictionary<string, object> i_GamesHistoryDict, ref TeamScript io_Team)
    {
        object thisSeason, allTime;
        bool k_IsThisSeasonStats = true;

        if (i_GamesHistoryDict.TryGetValue("thisSeason", out thisSeason))
        {
            extractGamesStats((Dictionary<string, object>)thisSeason, io_Team, k_IsThisSeasonStats);
        }
        else
        {
            Debug.LogError("Failed to get ThisSeasonStats data from json");
        }

        if (i_GamesHistoryDict.TryGetValue("allTime", out allTime))
        {
            extractGamesStats((Dictionary<string, object>)allTime, io_Team, !k_IsThisSeasonStats);
        }
        else
        {
            Debug.LogError("Failed to get AllTimeStats data from json");
        }
    }

    private static void extractGamesStats(Dictionary<string, object> gameHistoryDict, TeamScript io_Team, bool i_IsThisSeasonStats)
    {
        object wins, losts, draws, goalsFor, goalsAgainst, homeGames;
        GamesStatistics stats = new GamesStatistics();

        if (gameHistoryDict.TryGetValue("wins", out wins))
        {
            stats.wins = int.Parse(wins.ToString());
        }
        else
        {
            Debug.LogError("Failed to get Wins data from json");
        }

        if (gameHistoryDict.TryGetValue("losts", out losts))
        {
            stats.losts = int.Parse(losts.ToString());
        }
        else
        {
            Debug.LogError("Failed to get Losts data from json");
        }

        if (gameHistoryDict.TryGetValue("draws", out draws))
        {
            stats.draws = int.Parse(draws.ToString());
        }
        else
        {
            Debug.LogError("Failed to get Draws data from json");
        }

        if (gameHistoryDict.TryGetValue("goalsFor", out goalsFor))
        {
            stats.goalsFor = int.Parse(goalsFor.ToString());
        }
        else
        {
            Debug.LogError("Failed to get GoalsFor data from json");
        }

        if (gameHistoryDict.TryGetValue("goalsAgainst", out goalsAgainst))
        {
            stats.goalsAgainst = int.Parse(goalsAgainst.ToString());
        }
        else
        {
            Debug.LogError("Failed to get GoalsAgainst data from json");
        }

        if (gameHistoryDict.TryGetValue("homeGames", out homeGames))
        {
            stats.homeGames = int.Parse(homeGames.ToString());
        }
        else
        {
            Debug.LogError("Failed to get HomeGames data from json");
        }

        io_Team.SetGamesStatistics(stats, i_IsThisSeasonStats);
    }
}