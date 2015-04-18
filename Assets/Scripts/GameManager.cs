using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameManager : MonoBehaviour {

	public static GameManager s_GameManger;

    public TeamScript m_myTeam;
    public SquadScript m_MySquad;
    public int m_Cash = 100000;
    private TeamScript[] m_AllTeams; // !! Do not change positions for m_AllTeams
    private TeamScript[] m_TeamsForTable;
	private TableScript m_table;
    private Bucket m_Bucket;
	public int[] m_fansLevelPrice = {0,1000,2000,3000,4000,5000};
	public int[] m_facilitiesLevelPrice = {0,1000,2000,3000,4000,5000};
	public int[] m_stadiumLevelPrice = {0,1000,2000,3000,4000,5000};
	public float m_timeMoneyChangeAnimation;


	void Awake () {
		if (s_GameManger == null)
        {
			s_GameManger = this;
            loadData();
			DontDestroyOnLoad (gameObject);

		}
        else
        {
			Destroy (gameObject);
		}

	}

    void Start()
    {
        FixturesManager.s_FixturesManager.GenerateFixtures(m_AllTeams);
    }

    void Update()
    {
        m_Bucket.AddMoneyToBucket(Time.deltaTime);
    }

    private void loadData()
    {
        Debug.Log("LOADING DATA");
        DateTime disconnectionTime = DateTime.Now;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/team.dat"))
        {
            //FileStream file = File.OpenRead(Application.persistentDataPath + "/team.dat");
            //m_myTeam = (TeamScript)binaryFormatter.Deserialize(file);
            //Debug.Log("Last game home goals=" + m_myTeam.GetLastMatchInfo().GetHomeGoals());
            //file.Close();
            //Debug.Log("Loaded My Team");
        }
        else
        {
            m_myTeam = new TeamScript();
            Debug.Log("Created new instance of my team!");
        }

        if (File.Exists(Application.persistentDataPath + "/allteams.dat"))
        {
            FileStream file = File.OpenRead(Application.persistentDataPath + "/allteams.dat");
            m_AllTeams = (TeamScript[])binaryFormatter.Deserialize(file);
            file.Close();
            Debug.Log("Loaded All Teams");
        }
        else
        {
            initTeams(20);
        }

        if (File.Exists(Application.persistentDataPath + "/gamedata.dat"))
        {
            FileStream file = File.OpenRead(Application.persistentDataPath + "/gamedata.dat");
            GameData gameData = (GameData)binaryFormatter.Deserialize(file);
            m_Cash = gameData.m_Cash;
            m_myTeam = m_AllTeams[gameData.m_MyTeamIndex];
            disconnectionTime = gameData.m_DisconnectionTime;
            file.Close();
            Debug.Log("Loaded Game Data");
        }

        if (File.Exists(Application.persistentDataPath + "/myplayers.dat"))
        {
            FileStream file = File.OpenRead(Application.persistentDataPath + "/myplayers.dat");
            m_MySquad = (SquadScript)binaryFormatter.Deserialize(file);
            file.Close();
            Debug.Log("Loaded All Squad");
        }
        else
        {
            m_MySquad = new SquadScript();
            m_MySquad.Init();
            Debug.Log("Created new instance of my squad!");
        }

        if (File.Exists(Application.persistentDataPath + "/bucket.dat"))
        {
            FileStream file = File.OpenRead(Application.persistentDataPath + "/bucket.dat");
            m_Bucket = (Bucket)binaryFormatter.Deserialize(file);
            m_Bucket.AddMoneyToBucket((float)DateTime.Now.Subtract(disconnectionTime).TotalSeconds);
            file.Close();
            Debug.Log("Loaded Bucket");
        }
        else
        {
            m_Bucket = new Bucket(1000, 3600);
            Debug.Log("Created new instance of bucket!");
        }

    }

    private void saveData()
    {
        Debug.Log("SAVING FILES");
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //FileStream file = File.Create(Application.persistentDataPath + "/team.dat");
        //binaryFormatter.Serialize(file, m_myTeam);
        //file.Close();

        FileStream file = File.Create(Application.persistentDataPath + "/allteams.dat");
        binaryFormatter.Serialize(file, m_AllTeams);
        file.Close();

        file = File.Create(Application.persistentDataPath + "/gamedata.dat");
        GameData gameData = new GameData();
        for (int i = 0; i < m_AllTeams.Length; i++)
        {
            if (m_myTeam == m_AllTeams[i])
            {
                Debug.Log("Match on index=" + i);
                gameData.m_MyTeamIndex = i;
            }
        }
        gameData.m_Cash = m_Cash;
        gameData.m_DisconnectionTime = DateTime.Now;
        binaryFormatter.Serialize(file, gameData);
        file.Close();

        file = File.Create(Application.persistentDataPath + "/myplayers.dat");
        binaryFormatter.Serialize(file, m_MySquad);
        file.Close();

        file = File.Create(Application.persistentDataPath + "/bucket.dat");
        binaryFormatter.Serialize(file, m_Bucket);
        file.Close();
    }

    public void UpdateWeeklyFinance()
    {
        AddCash(FinanceManager.s_FinanceManager.CalculateIncome(m_myTeam) - FinanceManager.s_FinanceManager.CalculateOutcome(m_myTeam));
    }

    void OnDisable()
    {
        if (s_GameManger == this)
        {
            saveData();
        }
    }

    public void AddCash(int i_Value)
    {
       	m_Cash += i_Value;
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
		if (i_amount < 0) {
			deltaCoin = 1;
		} else {
			deltaCoin = -1;
		}
		
		while (i_amount != 0) {
			i_amount += deltaCoin;
			m_Cash +=-deltaCoin;
			yield return new WaitForSeconds (m_timeMoneyChangeAnimation);
			
		}
	}

	public void FansUpdate(float i_Value)
	{
		if ((m_myTeam.GetFansLevel () + 1) >= m_fansLevelPrice.Length)
		{
			return;
		}

		if (m_Cash >= m_fansLevelPrice [(int)m_myTeam.GetFansLevel () + 1]) {
			m_myTeam.UpdateFansLevel (i_Value);
			AddCash(-m_fansLevelPrice [(int)(m_myTeam.GetFansLevel ())]);
		}
	}

	public void StadiumUpdate(float i_Value)
	{
		if ((m_myTeam.GetStadiumLevel () + 1) >= m_stadiumLevelPrice.Length)
		{
			return;
		}
		if (m_Cash >= m_stadiumLevelPrice [(int)(m_myTeam.GetStadiumLevel () + 1)])
		{
			m_myTeam.UpdateStadiumLevel (i_Value);
			AddCash (-m_stadiumLevelPrice [(int)(m_myTeam.GetStadiumLevel ())]);
		}

	}


	public void FacilitiesUpdate(float i_Value)
	{
		if ((m_myTeam.GetFacilitiesLevel () + 1) >= m_facilitiesLevelPrice.Length)
		{
			return;
		}
		if (m_Cash >= m_facilitiesLevelPrice [(int)(m_myTeam.GetFacilitiesLevel () + 1)])
		{
			m_myTeam.UpdateFacilitiesLevel (i_Value);
			AddCash (-m_facilitiesLevelPrice [(int)(m_myTeam.GetFacilitiesLevel () )]);
		}
	}

    private void initTeams(int i_NumOfTeams)
    {
        m_AllTeams = new TeamScript[i_NumOfTeams];
		NamesUtilsScript teamNamesScript = new NamesUtilsScript ();

		//Temp 
		m_AllTeams [0] = m_myTeam;	
		m_AllTeams[0].SetName("Your Team ");

        for (int i = 1; i < i_NumOfTeams; i++)
        {
            m_AllTeams[i] = new TeamScript();
			m_AllTeams[i].SetName(teamNamesScript.GetTeamNameInIndex(i));
        }

    }

    public int GetTeamPosition(TeamScript i_Team)
    {
        if (m_TeamsForTable == null)
        {
            updateTableLeague();
        }
        for (int i = 0; i < m_TeamsForTable.Length; i++)
        {
            if (i_Team == m_TeamsForTable[i])
            {
                return m_TeamsForTable.Length - i;
            }
        }

        return 0;
    }

	//Team in the first place is the team in the last place in the array.
	public void updateTableLeague()
    {
	    if (m_TeamsForTable == null)
	    {
	        m_TeamsForTable = new TeamScript[m_AllTeams.Length];
            Array.Copy(m_AllTeams, m_TeamsForTable, m_AllTeams.Length);
	    }

        Array.Sort(m_TeamsForTable, delegate(TeamScript team1, TeamScript team2) 
        {
            if (team1.GetPoints() < team2.GetPoints()) 
            {
                return -2;
            }
            else if(team1.GetPoints() > team2.GetPoints())
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

	    GameObject tableGameObject = GameObject.FindGameObjectWithTag("Table");
        if (tableGameObject == null)
        {
            return;
        }
        m_table = tableGameObject.GetComponent<TableScript>();
		m_table.InitTable(m_TeamsForTable.Length);
	    
        for (int i = 0; i < m_TeamsForTable.Length; i++)
        {
            m_table.UpdateLine((m_TeamsForTable.Length - i - 1), (m_TeamsForTable.Length - i),
                               m_TeamsForTable[i].GetName(), m_TeamsForTable[i].GetMatchPlayed(), m_TeamsForTable[i].GetMatchWon(),
                               m_TeamsForTable[i].GetMatchLost(), m_TeamsForTable[i].GetMatchDrawn(), m_TeamsForTable[i].GetGoalsFor(),
                               m_TeamsForTable[i].GetGoalsAgainst(), m_TeamsForTable[i].GetPoints());

		}
		
	}

    public int GetCash()
    {
        return m_Cash;
    }

	public TeamScript GetTeamByName(string i_teamName)
    {
		return Array.Find(m_AllTeams,team=> team.GetName() == i_teamName);

	}
}

[Serializable]
class GameData
{
    public int m_Cash;
    public int m_MyTeamIndex;
    public DateTime m_DisconnectionTime;
}
