using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;


public class User
{
    public string Email { get; set; }
    public string ID { get; set; }
    public string FBId { get; set; }
    public string Name { get; set; }
    public string ManagerName { get; set; }
    public int Money { get; set; }
    public string Birthday { get; set; }
    public int Age { get; set; }
    public int CoinValue { get; set; }
    public Sprite ProfilePic { get; set; }
    //public List<Message> Messages { get; private set; }
    public Inbox Inbox { get; set; }

    public User()
    {
        //Messages = Message.LoadMessagesData();
        Inbox = Inbox.LoadMessagesData();
    }
}

public class GameSettings
{
    private int m_FansInitCost = 1000;
    private float m_FansCostMulti = 1.8f;

    private int m_FacilitiesInitCost = 4000;
    private float m_FacilitiesCostMulti = 2.25f;

    private int m_StadiumInitCost = 8000;
    private float m_StadiumCostMulti = 4f;

    private float m_PlayerBoostCostMultiplier = 1.5f;

    private TimeSpan m_TimeTillNextMatch = TimeSpan.MaxValue;

    private string m_NextOpponent;
    private bool m_IsNextMatchAtHome;

    public string NextOpponent
    { 
        get { return m_NextOpponent; }
        set { m_NextOpponent = value; } 
    }

    public int NumOfLeagues { get; set; }

    public bool IsNextMatchAtHome
    {
        get { return m_IsNextMatchAtHome; }
        set { m_IsNextMatchAtHome = value; }
    }

    public float PlayerBoostCostMultiplier
    {
        get { return m_PlayerBoostCostMultiplier; }
        set { m_PlayerBoostCostMultiplier = value; }
    }

    public int FansIntialCost
    {
        get { return m_FansInitCost; }
        set { m_FansInitCost = value; }
    }

    public float FansCostMultiplier
    {
        get { return m_FansCostMulti; }
        set { m_FansCostMulti = value; }
    }

    // Returns the i_Level cost (if level=0 then returns m_FansInitCost,
    // if level=1 then return m_FansInitCost*m_FansCostMulti)
    public int GetFansCostForLevel(int i_Level)
    {
        Debug.Log("m_FansInitCost=" + m_FansInitCost + ";m_FansCostMulti=" + m_FansCostMulti + "i_Level=" + i_Level);
        return (int) (m_FansInitCost * Math.Pow(m_FansCostMulti, i_Level));
    }

    public int FacilitiesIntitalCost
    {
        get { return m_FacilitiesInitCost; }
        set { m_FacilitiesInitCost = value; }
    }

    public float FacilitiesCostMultiplier
    {
        get { return m_FacilitiesCostMulti; }
        set { m_FacilitiesCostMulti = value; }
    }

    public int GetFacilitiesCostForLevel(int i_Level)
    {
        return (int)(m_FacilitiesInitCost * Math.Pow(m_FacilitiesCostMulti, i_Level));
    }

    public int StadiumIntitalCost
    {
        get { return m_StadiumInitCost; }
        set { m_StadiumInitCost = value; }
    }

    public float StadiumCostMultiplier
    {
        get{return m_StadiumCostMulti;}
        set { m_StadiumCostMulti = value; }
    }

    public int GetStadiumCostForLevel(int i_Level)
    {
        return (int)(m_StadiumInitCost * Math.Pow(m_StadiumCostMulti, i_Level));
    }

    public TimeSpan TimeTillNextMatch
    {
        get { return m_TimeTillNextMatch; }
        set { m_TimeTillNextMatch = value; }
    }
}

public class GameManager : MonoBehaviour 
{

	public static GameManager s_GameManger;

    public TeamScript m_myTeam;
    public SquadScript m_MySquad;
    //public int m_Cash = 100000;
    public TeamScript[] m_AllTeams; // !! Do not change positions for m_AllTeams
    //private TeamScript[] m_TeamsForTable;
	private TableScript m_table;
    public Bucket m_Bucket;
    private bool k_ShouldGoToMainScene = false;
    private bool k_IsCoinClickCoroutineRunning = false;
	public int[] m_fansLevelPrice = {0,1000,2000,3000,4000,5000};
	public int[] m_facilitiesLevelPrice = {0,1000,2000,3000,4000,5000};
	public int[] m_stadiumLevelPrice = {0,1000,2000,3000,4000,5000};
	public float m_timeMoneyChangeAnimation;
    public User m_User;
    public GameSettings m_GameSettings;
    public int NumOfClicksOnCoin { get; set; }
    public bool HasWatchedMatch { get; set; }
    public bool IsLoadingData { get { return k_IsLoadingData;} }

    public Sprite[] m_TeamLogos;
    public Sprite[] m_TeamLogosSmall;

    public GameObject m_NextMatchPopup;

    private bool m_HasDisplayedNextMatchPopup = false;
    private bool k_IsLoadingData;
    

    public GoalEvent[] LastGameSimulation { get; set; }
    public bool IsEditPlayerMode { get; set; }
    public string CurrentSceneHeaderName { get; set; }
    public string CurrentScene { get; set; }

    public bool IsPromotionLeague
    {
        get { return m_myTeam.LeagueIndex < m_GameSettings.NumOfLeagues; }
    }

    public bool IsRelegationLeague
    {
        get
        {
            return m_myTeam.LeagueIndex > 1;
        }
        
    }

    public Sprite m_UnreadMailSprite;
    public Sprite m_ReadMailSprite;

    //public const string URL = "http://tapmanger.herokuapp.com/";
    public const string URL = "http://77.125.2.181:4000/";
    public const string k_Lobby = "LOBBY";
    public const string k_Match = "MATCH";
    public const string k_League = "LEAGUE";
    public const string k_Squad = "SQUAD";
    public const string k_Shop = "SHOP";
    public const string k_Inbox = "INBOX";
    public const string k_About = "ABOUT";
    public const string k_Player = "PLAYER";

	void Awake () 
    {
		SingletoneAwakeMethod();
	}

    void Start()
    {
        //StartCoroutine(SyncClientDB());
        //Instantiate(m_NextMatchPopup);
    }

    void Update()
    {
        m_Bucket.AddMoneyToBucket(Time.deltaTime);

        if (m_GameSettings != null && m_GameSettings.TimeTillNextMatch != TimeSpan.MaxValue)
        {
            m_GameSettings.TimeTillNextMatch = TimeSpan.FromSeconds(m_GameSettings.TimeTillNextMatch.TotalSeconds - Time.deltaTime);

            if (!m_HasDisplayedNextMatchPopup && GetNextMatchTimeSpan() <= TimeSpan.Zero)
            {
                Instantiate(m_NextMatchPopup);
                Debug.Log("NEXT MATCH READY");
                m_HasDisplayedNextMatchPopup = true;
                HasWatchedMatch = false;
            }
        }

        if (!k_IsCoinClickCoroutineRunning && NumOfClicksOnCoin > 0)
        {
            StartCoroutine(checkNumOfClicksOnCoin());
            k_IsCoinClickCoroutineRunning = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("HasUnreadMessages=" + m_User.Inbox.HasUnreadMessages);
            for(int i=0; i < m_User.Inbox.TotalMessages; i++)
            {
                Debug.Log("HasRead: " + m_User.Inbox[i].HasReadMessage);
                Debug.Log("Header: " + m_User.Inbox[i].Header);
                Debug.Log("Content: " + m_User.Inbox[i].Content);
                
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("HasUnreadMessages=" + m_User.Inbox.HasUnreadMessages);
            foreach (Message message in m_User.Inbox.Messages)
            {
                Debug.Log("HasRead: " + message.HasReadMessage);
                Debug.Log("Header: " + message.Header);
                Debug.Log("Content: " + message.Content);
            }
        }
#endif
    }

    public void TimeTillNextMatchUpdated()
    {
        m_HasDisplayedNextMatchPopup = false;
    }

    IEnumerator checkNumOfClicksOnCoin()
    {
        while (true)
        {
            if (NumOfClicksOnCoin > 0)
            {
                StartCoroutine(sendCoinClick());
            }
            yield return new WaitForSeconds(1f);
        }

        // Not suppose to reach this place
        //k_IsCoinClickCoroutineRunning = false;
    }

    IEnumerator sendCoinClick()
    {
        WWWForm form = new WWWForm();
        int clicks = NumOfClicksOnCoin;
        NumOfClicksOnCoin = 0;
        //form.AddField("email", m_User.Email);
        form.AddField("id", m_User.ID);
        form.AddField("clicks", clicks);
        Debug.Log("m_User.ID=" + m_User.ID);
        

        Debug.Log("Sending sendCoinClick to server (" + clicks + ")");
        WWW request = new WWW(URL + "coinClick", form);
        yield return request;
        Debug.Log("Recieved response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            // Check ok response
            Debug.Log(request.text);
            switch (request.text)
            {
                case "ok":
                    // All Good!
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
    }

    public Sprite GetRandomTeamLogo()
    {
        int randomNumber = m_TeamLogos.Length;
        Debug.Log("randomNumber=" + randomNumber);
        return m_TeamLogos[UnityEngine.Random.Range(0, randomNumber) % m_TeamLogos.Length];
    }

    public Sprite GetTeamLogoByName(string i_TeamName)
    {
        foreach (TeamScript team in m_AllTeams)
        {
            if (team.Name == i_TeamName)
            {
                return GetTeamLogoBig(team.LogoIdx);
            }
        }

        return GetRandomTeamLogo();
    }

    public Sprite GetTeamLogoBig(int i_Idx)
    {
        return m_TeamLogos[i_Idx];
    }

    public Sprite GetMyTeamLogoBig()
    {
        return m_TeamLogos[m_myTeam.LogoIdx];
    }

    public Sprite GetTeamLogoSmall(int i_Idx)
    {
        return m_TeamLogosSmall[i_Idx];
    }

    public Sprite GetMyTeamLogoSmall()
    {
        return m_TeamLogosSmall[m_myTeam.LogoIdx];
    }

    //private void loadData()
    //{
    //    Debug.Log("LOADING DATA");
    //    DateTime disconnectionTime = DateTime.Now;
    //    BinaryFormatter binaryFormatter = new BinaryFormatter();

    //    if (File.Exists(Application.persistentDataPath + "/allteams.dat"))
    //    {
    //        FileStream file = File.OpenRead(Application.persistentDataPath + "/allteams.dat");
    //        m_AllTeams = (TeamScript[])binaryFormatter.Deserialize(file);
    //        file.Close();
    //        Debug.Log("Loaded All Teams");
    //    }
    //    else
    //    {
    //        initTeams(20);
    //    }

    //    if (File.Exists(Application.persistentDataPath + "/gamedata.dat"))
    //    {
    //        FileStream file = File.OpenRead(Application.persistentDataPath + "/gamedata.dat");
    //        GameData gameData = (GameData)binaryFormatter.Deserialize(file);
    //        //m_Cash = gameData.m_Cash;
    //        m_myTeam = m_AllTeams[gameData.m_MyTeamIndex];
    //        disconnectionTime = gameData.m_DisconnectionTime;
    //        file.Close();
    //        k_ShouldGoToMainScene = true;
    //        Debug.Log("Loaded Game Data");
    //    }

    //    if (File.Exists(Application.persistentDataPath + "/myplayers.dat"))
    //    {
    //        FileStream file = File.OpenRead(Application.persistentDataPath + "/myplayers.dat");
    //        m_MySquad = (SquadScript)binaryFormatter.Deserialize(file);
    //        file.Close();
    //        Debug.Log("Loaded All Squad");
    //    }
    //    else
    //    {
    //        m_MySquad = new SquadScript();
    //        m_MySquad.Init();
    //        Debug.Log("Created new instance of my squad!");
    //    }

    //    if (File.Exists(Application.persistentDataPath + "/bucket.dat"))
    //    {
    //        FileStream file = File.OpenRead(Application.persistentDataPath + "/bucket.dat");
    //        m_Bucket = (Bucket)binaryFormatter.Deserialize(file);
    //        m_Bucket.AddMoneyToBucket((float)DateTime.Now.Subtract(disconnectionTime).TotalSeconds);
    //        file.Close();
    //        Debug.Log("Loaded Bucket");
    //    }
    //    else
    //    {
    //        m_Bucket = new Bucket(1000, 3600);
    //        Debug.Log("Created new instance of bucket!");
    //    }

    //}

    //private void saveData()
    //{
    //    Debug.Log("SAVING FILES");
    //    BinaryFormatter binaryFormatter = new BinaryFormatter();

    //    FileStream file = File.Create(Application.persistentDataPath + "/allteams.dat");
    //    binaryFormatter.Serialize(file, m_AllTeams);
    //    file.Close();

    //    file = File.Create(Application.persistentDataPath + "/gamedata.dat");
    //    GameData gameData = new GameData();
    //    for (int i = 0; i < m_AllTeams.Length; i++)
    //    {
    //        if (m_myTeam == m_AllTeams[i])
    //        {
    //            Debug.Log("Match on index=" + i);
    //            gameData.m_MyTeamIndex = i;
    //        }
    //    }
    //    //gameData.m_Cash = m_Cash;
    //    gameData.m_DisconnectionTime = DateTime.Now;
    //    binaryFormatter.Serialize(file, gameData);
    //    file.Close();

    //    file = File.Create(Application.persistentDataPath + "/myplayers.dat");
    //    binaryFormatter.Serialize(file, m_MySquad);
    //    file.Close();

    //    file = File.Create(Application.persistentDataPath + "/bucket.dat");
    //    binaryFormatter.Serialize(file, m_Bucket);
    //    file.Close();
    //}

    public void UpdateWeeklyFinance()
    {
        AddCash(FinanceManager.s_FinanceManager.CalculateIncome(m_myTeam) - FinanceManager.s_FinanceManager.CalculateOutcome(m_myTeam));
    }

    void OnDisable()
    {
        if (s_GameManger == this)
        {
            //saveData();
            if (m_User != null)
            {
                Inbox.SaveMessagesData(m_User.Inbox);
            }
        }
    }

    public void AddCash(int i_Value)
    {
       	m_User.Money += i_Value;
		//StartCoroutine(addMoneyAnimation(i_Value));
    }

    public void EmptyBucket()
    {
        AddCash(m_Bucket.EmptyBucket());
    }

    public bool IsBucketFull()
    {
        return m_Bucket.IsFull();
    }

    public TimeSpan GetNextEmptyTimeSpan()
    {
        return m_Bucket.GetTimeUntilBucketIsFull();
    }

    public int GetMoneyInBucket()
    {
        return m_Bucket.GetMoneyInBucket();
    }

	IEnumerator addMoneyAnimation(int i_amount)
	{
		int deltaCoin = 0;
		
        deltaCoin = (i_amount < 0) ? 1 : -1;
		
		while (i_amount != 0) {
			i_amount += deltaCoin;
			m_User.Money +=-deltaCoin;
			yield return new WaitForSeconds (m_timeMoneyChangeAnimation);
			
		}
	}

    public void FansUpdate(int i_UpgradeCost)
	{
        /*
		if ((m_myTeam.GetFansLevel () + 1) >= m_fansLevelPrice.Length)
		{
			return;
		}
        

		if (m_User.Money >= m_fansLevelPrice [(int)m_myTeam.GetFansLevel () + 1]) {
			m_myTeam.UpdateFansLevel (i_Value);
			AddCash(-m_fansLevelPrice [(int)(m_myTeam.GetFansLevel ())]);
		}
         */
        AddCash(-i_UpgradeCost);
        m_myTeam.UpdateFansLevel(1);
	}

    public void StadiumUpdate(int i_UpgradeCost)
	{
        /*
		if ((m_myTeam.GetStadiumLevel () + 1) >= m_stadiumLevelPrice.Length)
		{
			return;
		}
		if (m_User.Money >= m_stadiumLevelPrice [(int)(m_myTeam.GetStadiumLevel () + 1)])
		{
			m_myTeam.UpdateStadiumLevel (i_Value);
			AddCash (-m_stadiumLevelPrice [(int)(m_myTeam.GetStadiumLevel ())]);
		}
         */
        AddCash(-i_UpgradeCost);
        m_myTeam.UpdateStadiumLevel(1);
	}


	public void FacilitiesUpdate(int i_UpgradeCost)
	{
        /*
		if ((m_myTeam.GetFacilitiesLevel () + 1) >= m_facilitiesLevelPrice.Length)
		{
			return;
		}
        if (m_User.Money >= m_facilitiesLevelPrice[(int)(m_myTeam.GetFacilitiesLevel() + 1)])
		{
			m_myTeam.UpdateFacilitiesLevel (i_Value);
			AddCash (-m_facilitiesLevelPrice [(int)(m_myTeam.GetFacilitiesLevel () )]);
		}
         */
        AddCash(-i_UpgradeCost);
        m_myTeam.UpdateFacilitiesLevel(1);
	}

    //private void initTeams(int i_NumOfTeams)
    //{
    //    m_AllTeams = new TeamScript[i_NumOfTeams];
    //    NamesUtilsScript teamNamesScript = new NamesUtilsScript ();

    //    //Temp 
    //    //m_AllTeams [0] = m_myTeam;	
    //    //m_AllTeams[0].SetName("Your Team ");

    //    for (int i = 1; i < i_NumOfTeams; i++)
    //    {
    //        m_AllTeams[i] = new TeamScript();
    //        m_AllTeams[i].SetName(teamNamesScript.GetTeamNameInIndex(i));
    //    }

    //}

    public int GetMyPosition()
    {
        return GetTeamPosition(m_myTeam);
    }

    public int GetTeamPosition(TeamScript i_Team)
    {

        sortLeagueTable();
        
        for (int i = 0; i < m_AllTeams.Length; i++)
        {
            if (i_Team.ID == m_AllTeams[i].ID)
            {
                return m_AllTeams.Length - i;
            }
        }

        return 0;
    }

    private void sortLeagueTable()
    {
        Array.Sort(m_AllTeams, delegate(TeamScript team1, TeamScript team2)
        {
            if (team1.GetPoints() < team2.GetPoints())
            {
                return -2;
            }
            else if (team1.GetPoints() > team2.GetPoints())
            {
                return 2;
            }
            else if (team1.GetGoalDiff() < team2.GetGoalDiff())
            {
                return -1;
            }
            else if (team1.GetGoalDiff() > team2.GetGoalDiff())
            {
                return 1;
            }

            return team1.GetName().CompareTo(team2.GetName());
        });
    }

	//Team in the first place is the team in the last place in the array.
	public void updateTableLeague()
    {
	    //if (m_TeamsForTable == null)
	   // {
	    //    m_TeamsForTable = new TeamScript[m_AllTeams.Length];
        //    Array.Copy(m_AllTeams, m_TeamsForTable, m_AllTeams.Length);
	   // }

        sortLeagueTable();

	    GameObject tableGameObject = GameObject.FindGameObjectWithTag("Table");
        if (tableGameObject == null)
        {
            return;
        }
        m_table = tableGameObject.GetComponent<TableScript>();
        m_table.InitTable(m_AllTeams.Length);

        for (int i = 0; i < m_AllTeams.Length; i++)
        {
            m_table.UpdateLine((m_AllTeams.Length - i - 1), (m_AllTeams.Length - i),
                               m_AllTeams[i].GetName(), m_AllTeams[i].GetMatchPlayed(), m_AllTeams[i].GetMatchWon(),
                               m_AllTeams[i].GetMatchLost(), m_AllTeams[i].GetMatchDrawn(), m_AllTeams[i].GetGoalsFor(),
                               m_AllTeams[i].GetGoalsAgainst(), m_AllTeams[i].GetPoints(), m_AllTeams[i].ID == m_myTeam.ID);

		}
		
	}

    public void GoBack()
    {
        switch (CurrentScene)
        {
            case k_League:
            case k_Squad:
            case k_Shop:
            case k_Inbox:
            case k_About:
                Application.LoadLevel("LobbyDevelopment");
                break;
            case k_Player:
                Application.LoadLevel("SquadDevelopment");
                break;
            default:
                Application.Quit();
                break;
        }
    }

    public int GetCash()
    {
        return m_User.Money;
    }

	public TeamScript GetTeamByName(string i_teamName)
    {
		return Array.Find(m_AllTeams,team=> team.GetName() == i_teamName);

	}

    public TimeSpan GetNextMatchTimeSpan()
    {
        return m_GameSettings.TimeTillNextMatch;
    }

    public string GetNextOpponent()
    {
        string homeOrAway = m_GameSettings.IsNextMatchAtHome ? " (H)" : " (A)";
        return m_GameSettings.NextOpponent + homeOrAway;
    }

    public IEnumerator SyncClientDB(string i_NextScene = null)
    {
        k_IsLoadingData = true;
        WWWForm form = new WWWForm();
        Debug.Log("sending sync request for user: " + PlayerPrefs.GetString("id"));
        form.AddField("email", PlayerPrefs.GetString("email"));
        form.AddField("id", PlayerPrefs.GetString("id"));
        WWW request = new WWW(URL + "getInfoById", form);

        yield return request;

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            print(request.text);
            Dictionary<string, object> json = Facebook.MiniJSON.Json.Deserialize(request.text) as Dictionary<string, object>;
            MyUtils.LoadTeamData(json, ref m_myTeam);
            MyUtils.LoadLeagueData(json, ref m_AllTeams);
            MyUtils.LoadBucketData(json, ref m_Bucket);
            MyUtils.LoadSquadData(json, ref m_MySquad);
            MyUtils.LoadGameSettings(json, ref m_GameSettings);
            MyUtils.LoadUserData(json, ref m_User);
            if (i_NextScene != null)
            {
                Application.LoadLevel(i_NextScene);
            }
            //if (m_ProfilePic != null)
            //{
            //    GameManager.s_GameManger.m_User.ProfilePic = m_ProfilePic;
            //}
            //k_IsDataLoaded = true;
        }
        k_IsLoadingData = false;
        Debug.Log("End of SyncClientDB()");

    }

    public void SingletoneAwakeMethod()
    {
        if (s_GameManger == null)
        {
            s_GameManger = this;
            //SoomlaStore.Initialize(new Store());
            //m_TeamLogos = Resources.LoadAll<Sprite>("Match Sybmols");
            //m_TeamLogosSmall = Resources.LoadAll<Sprite>("Top Bar Symbols");
            //loadData();
            //StartCoroutine(loadDataFromServer());
            DontDestroyOnLoad(gameObject);
            if (k_ShouldGoToMainScene)
            {
                //Application.LoadLevel("MainScene");
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public interface ISingletone
{
    void SingletoneAwakeMethod();
}

[Serializable]
class GameData
{
    public int m_Cash;
    public int m_MyTeamIndex;
    public DateTime m_DisconnectionTime;
}
