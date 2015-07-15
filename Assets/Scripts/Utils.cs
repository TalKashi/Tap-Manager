using System;
using System.Collections.Generic;
using UnityEngine;

public class MyUtils
{

    public static string AddOrdinal(int i_Num)
    {
        string oridnaledNum = i_Num.ToString();

        if (i_Num > 0)
        {
            int mod = i_Num % 100;

            if (mod >= 11 || mod <= 13)
            {
                oridnaledNum = i_Num + "th";
            }
            else
            {
                if (mod == 1)
                {
                    oridnaledNum = i_Num + "st";
                }
                else if (mod == 2)
                {
                    oridnaledNum = i_Num + "nd";
                }
                else if (mod == 3)
                {
                    oridnaledNum = i_Num + "rd";
                }
                else
                {
                    oridnaledNum = i_Num + "th";
                }
            }
        }

        return oridnaledNum;
    }

    public static int GetPercentage(ulong i_Num1, ulong i_Num2)
    {
        double percentage = (double)i_Num1 / (double)i_Num2;

        return (int)(percentage * 100);
    }

    #region Game Settings Section

    public static void LoadGameSettings(Dictionary<string, object> i_Json, ref GameSettings o_GameSettings)
    {
        object gameSettingsDict, timeTillNextMatchMs, nextMatchDict;

        if (o_GameSettings == null)
        {
            o_GameSettings = new GameSettings();
        }

        if (i_Json.TryGetValue("settings", out gameSettingsDict))
        {
            extractGameSettings((Dictionary<string, object>)gameSettingsDict, ref o_GameSettings);
        }
        else
        {
            Debug.Log("WARN: Failed to get GameSettings from json");
        }

        if (i_Json.TryGetValue("timeTillNextMatch", out timeTillNextMatchMs))
        {
            long timeLeftInMs = long.Parse(timeTillNextMatchMs.ToString());

            o_GameSettings.TimeTillNextMatch = TimeSpan.FromMilliseconds(timeLeftInMs);
            Debug.Log("!!!! " + o_GameSettings.TimeTillNextMatch);
            GameManager.s_GameManger.TimeTillNextMatchUpdated();
        }
        else
        {
            Debug.Log("WARN: Failed to get TimeTillNextMatch from json");
        }

        if (i_Json.TryGetValue("nextMatch", out nextMatchDict))
        {
            extractNextMatchData((Dictionary<string, object>) nextMatchDict, ref o_GameSettings);
        }
        else
        {
            Debug.Log("WARN: Failed to get NextMatch from json");
        }

        // TODO: Num of leagues
    }

    private static void extractNextMatchData(Dictionary<string, object> i_NextMatchDict, ref GameSettings o_GameSettings)
    {
        object opponent, isHomeMatch;

        if (i_NextMatchDict.TryGetValue("opponent", out opponent))
        {
            o_GameSettings.NextOpponent = opponent.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get NextOpponent from json");
        }

        if (i_NextMatchDict.TryGetValue("isHomeMatch", out isHomeMatch))
        {
            o_GameSettings.IsHomeOrAway = bool.Parse(isHomeMatch.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get IsHomeOrAway from json");
        }
    }

    private static void extractGameSettings(Dictionary<string, object> i_GameSettingsDict, ref GameSettings o_GameSettings)
    {
        object initPriceOfFans, initPriceOfFacilities, initPriceOfStadium, multiplierBoost;
        object fansMultiplier, facilitiesMultiplier, stadiumMultiplier;

        if (i_GameSettingsDict.TryGetValue("initPriceOfFans", out initPriceOfFans))
        {
            o_GameSettings.FansIntialCost = int.Parse(initPriceOfFans.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get FansIntialCost from json");
        }

        if (i_GameSettingsDict.TryGetValue("initPriceOfFacilities", out initPriceOfFacilities))
        {
            o_GameSettings.FacilitiesIntitalCost = int.Parse(initPriceOfFacilities.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get FacilitiesIntitalCost from json");
        }

        if (i_GameSettingsDict.TryGetValue("initPriceOfStadium", out initPriceOfStadium))
        {
            o_GameSettings.StadiumIntitalCost = int.Parse(initPriceOfStadium.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get StadiumIntitalCost from json");
        }

        if (i_GameSettingsDict.TryGetValue("fansMultiplier", out fansMultiplier))
        {
            o_GameSettings.FansCostMultiplier = float.Parse(fansMultiplier.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get FansCostMultiplier from json");
        }

        if (i_GameSettingsDict.TryGetValue("facilitiesMultiplier", out facilitiesMultiplier))
        {
            o_GameSettings.FacilitiesCostMultiplier = float.Parse(facilitiesMultiplier.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get FacilitiesCostMultiplier from json");
        }

        if (i_GameSettingsDict.TryGetValue("stadiumMultiplier", out stadiumMultiplier))
        {
            o_GameSettings.StadiumCostMultiplier = float.Parse(stadiumMultiplier.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get StadiumCostMultiplier from json");
        }

        if (i_GameSettingsDict.TryGetValue("multiplierBoost", out multiplierBoost))
        {
            o_GameSettings.PlayerBoostCostMultiplier = float.Parse(multiplierBoost.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get PlayerBoostCostMultiplier from json");
        }
    }

    #endregion Shop Info Section

    #region User Loading Section

    public static void LoadUserData(Dictionary<string, object> i_Json, ref User o_User)
    {
        object userDict;

        if (o_User == null)
        {
            o_User = new User();
        }

        if (i_Json.TryGetValue("user", out userDict))
        {
            extractUserData((Dictionary<string, object>)userDict, ref o_User);
        }
        else
        {
            Debug.Log("WARN: Failed to get User from json");
        }
    }

    private static void extractUserData(Dictionary<string, object> i_UserDict, ref User o_User)
    {
        object id, email, age, money, name, managerName, birthday, coinValue, fbId, messagesArrayObj;

        if (i_UserDict.TryGetValue("id", out id))
        {
            o_User.ID = id.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get ID from json");
        }

        if (i_UserDict.TryGetValue("fbId", out fbId))
        {
            o_User.FBId = fbId.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get FBId from json");
        }

        if (i_UserDict.TryGetValue("email", out email))
        {
            o_User.Email = email.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get Email from json");
        }

        if (i_UserDict.TryGetValue("age", out age))
        {
            o_User.Age = int.Parse(age.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get Age from json");
        }

        if (i_UserDict.TryGetValue("money", out money))
        {
            o_User.Money = (int)float.Parse(money.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get Money from json");
        }

        if (i_UserDict.TryGetValue("name", out name))
        {
            o_User.Name = name.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get Name from json");
        }

        if (i_UserDict.TryGetValue("managerName", out managerName))
        {
            o_User.ManagerName = managerName.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get ManagerName from json");
        }

        if (i_UserDict.TryGetValue("birthday", out birthday))
        {
            o_User.Birthday = birthday.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get Birthday from json");
        }

        if (i_UserDict.TryGetValue("coinValue", out coinValue))
        {
            o_User.CoinValue = int.Parse(coinValue.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get Birthday from json");
        }

        
        if (i_UserDict.TryGetValue("message", out messagesArrayObj))
        {
            extractMessagesData((List<object>)messagesArrayObj, ref o_User);
        }
        else
        {
            Debug.Log("WARN: Failed to get messages array from json");
        }
        
    }

    private static void extractMessagesData(List<object> i_Messages, ref User o_User)
    {
        foreach (object message in i_Messages)
        {
            extractSingleMessage((Dictionary<string, object>) message, ref o_User);
        }
    }

    private static void extractSingleMessage(Dictionary<string, object> i_Message, ref User o_User)
    {
        object header, content;

        if (i_Message.TryGetValue("header", out header) && i_Message.TryGetValue("content", out content))
        {
            Message message = new Message(header.ToString(), content.ToString());
            o_User.Inbox.AddNewMessage(message);
        }
        else
        {
            Debug.Log("WARN: Failed to get header or content in message from json");
        }
    }

    #endregion User Loading Section

    #region League Loading Scetion

    public static void LoadLeagueData(Dictionary<string, object> i_Json, ref TeamScript[] o_AllTeams)
    {
        object leagueArr;

        if (i_Json.TryGetValue("league", out leagueArr))
        {
            extractLeagueTeams((List<object>) leagueArr, ref o_AllTeams);
        }
        else
        {
            Debug.Log("WARN: Failed to get League from json");
        }
    }

    private static void extractLeagueTeams(List<object> leagueArr, ref TeamScript[] o_AllTeams)
    {
        o_AllTeams = new TeamScript[leagueArr.Count];

        int i = 0;
        foreach (object team in leagueArr)
        {
            extractTeamData((Dictionary<string, object>)team, ref o_AllTeams[i]);
            i++;
        }
    }

    #endregion League Loading Scetion

    #region Bucket Loading Section
    public static void LoadBucketData(Dictionary<string, object> i_Json, ref Bucket o_Bucket)
    {
        object bucket, details, timeNow;

        if (i_Json.TryGetValue("bucket", out bucket))
        {
            Dictionary<string, object> bucketDict = (Dictionary<string, object>) bucket;

            if (bucketDict.TryGetValue("details", out details))
            {
                extractBucketData((Dictionary<string, object>)details, ref o_Bucket);
            }
            else
            {
                Debug.Log("WARN: Failed to get BucketDetails from json");
            }

            if (bucketDict.TryGetValue("timeNow", out timeNow))
            {
                //o_Bucket.LastFlush = (DateTime)lastFlush;
                long timeNowMs = long.Parse(timeNow.ToString());
                long lastFlushMs = o_Bucket.LastFlush;
                o_Bucket.SetMoneyToZero();
                o_Bucket.AddMoneyToBucket((float)TimeSpan.FromMilliseconds(timeNowMs - lastFlushMs).TotalSeconds);
            }
            else
            {
                Debug.Log("WARN: Failed to get TimeNow(server) from json");
            }
        }
        else
        {
            Debug.Log("WARN: Failed to get Bucket from json");
        }
    }

    private static void extractBucketData(Dictionary<string, object> i_BucketDict, ref Bucket o_Bucket)
    {
        object valueForSecond, maxAmount, lastFlush, level, id, dateNow;

        if (o_Bucket == null)
        {
            o_Bucket = new Bucket();
        }

        if (i_BucketDict.TryGetValue("_id", out id))
        {
            o_Bucket.ID = id.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get ID from json");
        }

        if (i_BucketDict.TryGetValue("valueForSecond", out valueForSecond))
        {
            o_Bucket.SetValuePerSecond(float.Parse(valueForSecond.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get ValuePerSecond from json");
        }

        if (i_BucketDict.TryGetValue("maxAmount", out maxAmount))
        {
            o_Bucket.SetMaxAmount(int.Parse(maxAmount.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get MaxAmount from json");
        }

        if (i_BucketDict.TryGetValue("lastFlush", out lastFlush))
        {
            //o_Bucket.LastFlush = (DateTime)lastFlush;
            o_Bucket.LastFlush = long.Parse(lastFlush.ToString());
            //DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(lastFlushMs);
            //DateTime date = new DateTime(lastFlushMs);
            //o_Bucket.LastFlush = date;
        }
        else
        {
            Debug.Log("WARN: Failed to get LastFlush from json");
        }

        if (i_BucketDict.TryGetValue("level", out level))
        {
            o_Bucket.SetLevel(int.Parse(level.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get Level from json");
        }

        //if (i_BucketDict.TryGetValue("timeNow", out dateNow))
        //{
        //    //o_Bucket.LastFlush = (DateTime)lastFlush;
        //    long dateNowMs = long.Parse(dateNow.ToString());
        //    DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(dateNowMs);

        //    long timeNowMs = long.Parse(dateNow.ToString());
        //    long lastFlushMs = long.Parse(lastFlush.ToString());


        //    o_Bucket.AddMoneyToBucket(TimeSpan.FromMilliseconds(timeNowMs - lastFlushMs).Seconds);
        //}
        //else
        //{
        //    Debug.Log("WARN: Failed to get TimeNow(server) from json");
        //}
    }
    #endregion Bucket Loading Section

    #region Squad Loading Section
    public static void LoadSquadData(Dictionary<string, object> i_Json, ref SquadScript o_Squad)
    {
        object squadDict;

        if (o_Squad == null)
        {
            o_Squad = new SquadScript();
        }

        if (i_Json.TryGetValue("squad", out squadDict))
        {
            extractSquadData((Dictionary<string, object>)squadDict, ref o_Squad);
        }
        else
        {
            Debug.Log("WARN: Failed to get Squad from json");
        }
    }

    private static void extractSquadData(Dictionary<string, object> i_SquadDict, ref SquadScript o_Squad)
    {
        object playerArr, id;

        if (i_SquadDict.TryGetValue("_id", out id))
        {
            o_Squad.ID = id.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get ID from json");
        }

        if (i_SquadDict.TryGetValue("players", out playerArr))
        {
            extractPlayersData((List<object>)playerArr, o_Squad);
        }
        else
        {
            Debug.Log("WARN: Failed to get Players from json");
        }
    }

    private static void extractPlayersData(List<object> i_PlayersList, SquadScript o_Squad)
    {
        PlayerScript[] players = new PlayerScript[i_PlayersList.Count];
        int i = 0;
        foreach (object player in i_PlayersList)
        {
            players[i] = extractOnePlayerData((Dictionary<string, object>)player);
            i++;
        }

        o_Squad.Players = players;
    }

    private static PlayerScript extractOnePlayerData(Dictionary<string, object> i_PlayerDict)
    {
        object pos, firstName, lastName, salary, isInjured, age, gamesPlayed, priceToBoost;
        object goalsScored, level, price, boost, isPlaying, yearJoinedTheClub, playerImage;
        object id, nextBoost, currentBoost;

        PlayerScript player = new PlayerScript();

        if (i_PlayerDict.TryGetValue("position", out pos))
        {
            player.SetPosition((ePosition)int.Parse(pos.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get Position from json");
        }

        if (i_PlayerDict.TryGetValue("firstName", out firstName))
        {
            player.SetFirstName((string)firstName);
        }
        else
        {
            Debug.Log("WARN: Failed to get FirstName from json");
        }

        if (i_PlayerDict.TryGetValue("lastName", out lastName))
        {
            player.SetLastName((string)lastName);
        }
        else
        {
            Debug.Log("WARN: Failed to get LastName from json");
        }

        if (i_PlayerDict.TryGetValue("salary", out salary))
        {
            player.SetSalary(float.Parse(salary.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get Salary from json");
        }

        if (i_PlayerDict.TryGetValue("isInjured", out isInjured))
        {
            player.SetIsInjured((bool)isInjured);
        }
        else
        {
            Debug.Log("WARN: Failed to get IsInjured from json");
        }

        if (i_PlayerDict.TryGetValue("age", out age))
        {
            player.SetAge(int.Parse(age.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get Age from json");
        }

        if (i_PlayerDict.TryGetValue("gamesPlayed", out gamesPlayed))
        {
            player.SetGamesPlayed(int.Parse(gamesPlayed.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get GamesPlayed from json");
        }

        if (i_PlayerDict.TryGetValue("goalsScored", out goalsScored))
        {
            player.SetGoalsScored(int.Parse(goalsScored.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get GoalScores from json");
        }

        if (i_PlayerDict.TryGetValue("level", out level))
        {
            player.SetPlayerLevel(int.Parse(level.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get Level from json");
        }

        if (i_PlayerDict.TryGetValue("price", out price))
        {
            player.SetPlayerPrice(float.Parse(price.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get Price from json");
        }

        if (i_PlayerDict.TryGetValue("priceToBoost", out priceToBoost))
        {
            player.SetPriceToBoostPlayer((int)float.Parse(priceToBoost.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get PriceToBoost from json");
        }

        if (i_PlayerDict.TryGetValue("boost", out boost))
        {
            player.SetBoostLevel(int.Parse(boost.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get Boost from json");
        }

        if (i_PlayerDict.TryGetValue("isPlaying", out isPlaying))
        {
            player.SetIsPlaying((bool)isPlaying);
        }
        else
        {
            Debug.Log("WARN: Failed to get IsPlaying from json");
        }

        if (i_PlayerDict.TryGetValue("yearJoinedTheClub", out yearJoinedTheClub))
        {
            player.SetYearJoinedTheClub(int.Parse(yearJoinedTheClub.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get YearJoinedTheClub from json");
        }

        if (i_PlayerDict.TryGetValue("playerImage", out playerImage))
        {
            player.PlayerSpriteIndex = int.Parse(playerImage.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get PlayerImage from json");
        }

        if (i_PlayerDict.TryGetValue("id", out id))
        {
            player.ID = int.Parse(id.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get ID from json");
        }

        double nextBoostCap;
        if (i_PlayerDict.TryGetValue("nextBoost", out nextBoost) && double.TryParse(nextBoost.ToString(), out nextBoostCap))
        {
            player.NextBoostCap = (uint)nextBoostCap;
        }
        else
        {
            Debug.Log("WARN: Failed to get NextBoostCap from json");
        }

        if (i_PlayerDict.TryGetValue("currentBoost", out currentBoost))
        {
            player.CurrentBoost = (uint)double.Parse(currentBoost.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get NextBoostCap from json");
        }

        return player;
    }

    #endregion Squad Loading Section

    #region Team Loading Section

    public static void LoadTeamData(Dictionary<string, object> i_Json, ref TeamScript o_Team)
    {
        object teamDict;

        if (o_Team == null)
        {
            o_Team = new TeamScript();
        }

        if (i_Json.TryGetValue("team", out teamDict))
        {
            extractTeamData((Dictionary<string, object>)teamDict, ref o_Team);
        }
        else
        {
            Debug.Log("WARN: Failed to get Team from json");
        }
    }

    private static void extractTeamData(Dictionary<string, object> i_TeamJson, ref TeamScript o_Team)
    {
        object id, shopDict, gamesHistoryDict, additionalFans, lastGameInfoDict, financeDict, totalInstantTrainObj;
        object lastResultEnum, isLastGameIsHomeGameBool, statisticsDict, teamName, logo;

        if (o_Team == null)
        {
            o_Team = new TeamScript();
        }

        if (i_TeamJson.TryGetValue("_id", out id))
        {
            o_Team.ID = id.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get ID from json");
        }

        if (i_TeamJson.TryGetValue("teamName", out teamName))
        {
            o_Team.Name = teamName.ToString();
        }
        else
        {
            Debug.Log("WARN: Failed to get TeamName from json");
        }

        if (i_TeamJson.TryGetValue("lastResult", out lastResultEnum))
        {
            o_Team.LastResult = (eResult) int.Parse(lastResultEnum.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get LastResult(Enum) from json");
        }

        if (i_TeamJson.TryGetValue("isLastGameIsHomeGame", out isLastGameIsHomeGameBool))
        {
            o_Team.IsLastGameIsHomeGame = (bool)isLastGameIsHomeGameBool;
        }
        else
        {
            Debug.Log("WARN: Failed to get IsLastGameIsHomeGame from json");
        }

        if (i_TeamJson.TryGetValue("additionalFans", out additionalFans))
        {
            o_Team.SetAdditionalFans(int.Parse(additionalFans.ToString()));
        }
        else
        {
            Debug.Log("WARN: Failed to get AdditionalFans from json");
        }

        if (i_TeamJson.TryGetValue("shop", out shopDict))
        {
            extractShopData((Dictionary<string, object>) shopDict, ref o_Team);
        }
        else
        {
            Debug.Log("WARN: Failed to get Shop data from json");
        }

        if (i_TeamJson.TryGetValue("gamesHistory", out gamesHistoryDict))
        {
            extractGamesHistoryData((Dictionary<string, object>)gamesHistoryDict, ref o_Team);
        }
        else
        {
            Debug.Log("WARN: Failed to get GamesHistory data from json");
        }

        int logoIdx;
        if (i_TeamJson.TryGetValue("logo", out logo) && int.TryParse(logo.ToString(), out logoIdx))
        {
            o_Team.LogoIdx = logoIdx;
        }
        else
        {
            Debug.Log("WARN: Failed to get LogoIdx data from json");
        }

        if (i_TeamJson.TryGetValue("lastGameInfo", out lastGameInfoDict))
        {
            extractLastGameInfoData((Dictionary<string, object>)lastGameInfoDict, ref o_Team);
        }
        else
        {
            Debug.Log("WARN: Failed to get LastGameInfo data from json");
        }

        if (i_TeamJson.TryGetValue("statistics", out statisticsDict))
        {
            extractStatisticsData((Dictionary<string, object>)statisticsDict, ref o_Team);
        }
        else
        {
            Debug.Log("WARN: Failed to get Statistics data from json");
        }

        if (i_TeamJson.TryGetValue("finance", out financeDict))
        {
            extractFinanceReport((Dictionary<string, object>) financeDict, ref o_Team);
        }
        else
        {
            Debug.Log("WARN: Failed to get finance data from json");
        }

        float totalInstantTrain;
        if (i_TeamJson.TryGetValue("totalInstantTrain", out totalInstantTrainObj) &&
            float.TryParse(totalInstantTrainObj.ToString(), out totalInstantTrain))
        {
            o_Team.TotalInstantTrain = (int) totalInstantTrain;
        }
        else
        {
            Debug.Log("WARN: Failed to get TotalInstantTrain data from json");
        }
    }

    private static void extractFinanceReport(Dictionary<string, object> i_FinanceDict, ref TeamScript o_Team)
    {
        object incomeFromTicketsObj, incomeFromMerchandiseObj, facilitiesCostObj, stadiumCostObj, salaryObj, instantTrainObj;
        float incomeFromTickests, incomeFromMerchandise, facilitiesCost, stadiumCost, salary, instantTrain;

        if (i_FinanceDict.TryGetValue("incomeFromTickets", out incomeFromTicketsObj) &&
            float.TryParse(incomeFromTicketsObj.ToString(), out incomeFromTickests))
        {
            o_Team.IncomeFromTickets = incomeFromTickests;
        }
        else
        {
            Debug.Log("WARN: Failed to get IncomeFromTickets data from json");
        }

        if (i_FinanceDict.TryGetValue("incomeFromMerchandise", out incomeFromMerchandiseObj) &&
            float.TryParse(incomeFromMerchandiseObj.ToString(), out incomeFromMerchandise))
        {
            o_Team.IncomeFromMerchandise = incomeFromMerchandise;
        }
        else
        {
            Debug.Log("WARN: Failed to get IncomeFromMerchandise data from json");
        }

        if (i_FinanceDict.TryGetValue("facilitiesCost", out facilitiesCostObj) &&
            float.TryParse(facilitiesCostObj.ToString(), out facilitiesCost))
        {
            o_Team.FacilitiesCost = facilitiesCost;
        }
        else
        {
            Debug.Log("WARN: Failed to get FacilitiesCost data from json");
        }

        if (i_FinanceDict.TryGetValue("stadiumCost", out stadiumCostObj) &&
            float.TryParse(stadiumCostObj.ToString(), out stadiumCost))
        {
            o_Team.StadiumCost = stadiumCost;
        }
        else
        {
            Debug.Log("WARN: Failed to get StadiumCost data from json");
        }

        if (i_FinanceDict.TryGetValue("salary", out salaryObj) &&
            float.TryParse(salaryObj.ToString(), out salary))
        {
            o_Team.Salary = salary;
        }
        else
        {
            Debug.Log("WARN: Failed to get Salary data from json");
        }

        if (i_FinanceDict.TryGetValue("instantTrain", out instantTrainObj) &&
            float.TryParse(instantTrainObj.ToString(), out instantTrain))
        {
            o_Team.LastMatchInstantTrain = (int) instantTrain;
        }
        else
        {
            Debug.Log("WARN: Failed to get LastMatchInstantTrain data from json");
        }
    }

    private static void extractStatisticsData(Dictionary<string, object> i_StatsDict, ref TeamScript o_Team)
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

        o_Team.TeamRecords = records;

    }

    private static void extractLastGameInfoData(Dictionary<string, object> i_LastGameInfoDict, ref TeamScript o_Team)
    {
        object homeTeam, awayTeam, homeTeamGoals, awayTeamGoals, crowdAtMatch, playersScoreGoal;
        object homeTeamLogo, awayTeamLogo;


        if (!i_LastGameInfoDict.TryGetValue("homeTeam", out homeTeam))
        {
            return;
        }
        Debug.Log(homeTeam );
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

        if (!i_LastGameInfoDict.TryGetValue("playersScoreGoal", out playersScoreGoal))
        {
            playersScoreGoal = "";
        }

        int homeTeamLogoInt;
        if (!i_LastGameInfoDict.TryGetValue("homeTeamLogo", out homeTeamLogo)|| !int.TryParse(homeTeamLogo.ToString(), out homeTeamLogoInt))
        {
            homeTeamLogoInt = GameManager.s_GameManger.m_myTeam.LogoIdx;
        }

        int awayTeamLogoInt;
        if (!i_LastGameInfoDict.TryGetValue("awayTeamLogo", out awayTeamLogo) || !int.TryParse(awayTeamLogo.ToString(), out awayTeamLogoInt))
        {
            awayTeamLogoInt = GameManager.s_GameManger.m_myTeam.LogoIdx;
        }

        o_Team.SetLastGameInfo(new MatchInfo((string) homeTeam, (string) awayTeam, int.Parse(homeTeamGoals.ToString()),
            int.Parse(awayTeamGoals.ToString()), (int) float.Parse(crowdAtMatch.ToString()), playersScoreGoal.ToString(),
            homeTeamLogoInt, awayTeamLogoInt));
    }

    private static void extractShopData(Dictionary<string, object> i_ShopDict, ref TeamScript o_Team)
    {
        object fansLevel, facilitiesLevel, stadiumLevel;

        if (i_ShopDict.TryGetValue("fansLevel", out fansLevel))
        {
            o_Team.Fans = int.Parse(fansLevel.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get FansLevel data from json");
        }

        if (i_ShopDict.TryGetValue("facilitiesLevel", out facilitiesLevel))
        {
            o_Team.Facilities = int.Parse(facilitiesLevel.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get FacilitiesLevel data from json");
        }

        if (i_ShopDict.TryGetValue("stadiumLevel", out stadiumLevel))
        {
            o_Team.Stadium = int.Parse(stadiumLevel.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get StadiumLevel data from json");
        }
    }

    private static void extractGamesHistoryData(Dictionary<string, object> i_GamesHistoryDict, ref TeamScript o_Team)
    {
        object thisSeason, allTime;
        const bool v_IsThisSeasonStats = true;

        if (i_GamesHistoryDict.TryGetValue("thisSeason", out thisSeason))
        {
            extractGamesStats((Dictionary<string, object>)thisSeason, o_Team, v_IsThisSeasonStats);
        }
        else
        {
            Debug.Log("WARN: Failed to get ThisSeasonStats data from json");
        }

        if (i_GamesHistoryDict.TryGetValue("allTime", out allTime))
        {
            extractGamesStats((Dictionary<string, object>)allTime, o_Team, !v_IsThisSeasonStats);
        }
        else
        {
            Debug.Log("WARN: Failed to get AllTimeStats data from json");
        }
    }

    private static void extractGamesStats(Dictionary<string, object> gameHistoryDict, TeamScript o_Team, bool i_IsThisSeasonStats)
    {
        object wins, losts, draws, goalsFor, goalsAgainst, homeGames, crowdObj;
        GamesStatistics stats = new GamesStatistics();

        if (gameHistoryDict.TryGetValue("wins", out wins))
        {
            stats.wins = int.Parse(wins.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get Wins data from json");
        }

        if (gameHistoryDict.TryGetValue("losts", out losts))
        {
            stats.losts = int.Parse(losts.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get Losts data from json");
        }

        if (gameHistoryDict.TryGetValue("draws", out draws))
        {
            stats.draws = int.Parse(draws.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get Draws data from json");
        }

        if (gameHistoryDict.TryGetValue("goalsFor", out goalsFor))
        {
            stats.goalsFor = int.Parse(goalsFor.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get GoalsFor data from json");
        }

        if (gameHistoryDict.TryGetValue("goalsAgainst", out goalsAgainst))
        {
            stats.goalsAgainst = int.Parse(goalsAgainst.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get GoalsAgainst data from json");
        }

        if (gameHistoryDict.TryGetValue("homeGames", out homeGames))
        {
            stats.homeGames = int.Parse(homeGames.ToString());
        }
        else
        {
            Debug.Log("WARN: Failed to get HomeGames data from json");
        }

        float crowd;
        if (gameHistoryDict.TryGetValue("crowd", out crowdObj) && float.TryParse(crowdObj.ToString(), out crowd))
        {
            stats.crowd = (int) crowd;
        }
        else
        {
            Debug.Log("WARN: Failed to get crowd data from json");
        }

        o_Team.SetGamesStatistics(stats, i_IsThisSeasonStats);
    }
    #endregion Team Loading Section
}