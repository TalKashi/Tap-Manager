﻿using System;
using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

	public static GameManager s_GameManger;

    public TeamScript m_myTeam;
    public int m_Cash = 1000;
    private TeamScript[] m_AllTeams;
	private TableScript m_table;
	public int[] m_fansLevelPrice = {0,1000,2000,3000,4000,5000};
	public int[] m_facilitiesLevelPrice = {0,1000,2000,3000,4000,5000};
	public int[] m_stadiumLevelPrice = {0,1000,2000,3000,4000,5000};


	void Awake () {
		if (s_GameManger == null)
        {
			s_GameManger = this;
            m_myTeam = new TeamScript();
			DontDestroyOnLoad (gameObject);

		}
        else
        {
			Destroy (gameObject);
		}

	}

    void Start()
    {
        initTeams(20);
        FixturesManager.s_FixturesManager.GenerateFixtures(m_AllTeams);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            FixturesManager.s_FixturesManager.ExecuteNextFixture();
            print(FixturesManager.s_FixturesManager.PrintLastFixturesResults());
        }
		if (Input.GetKeyDown(KeyCode.S)){
			updateTableLeague();
		}

    }

    public void AddCash(int i_Value)
    {
        m_Cash += i_Value;
    }

	public void FansUpdate(float i_Value)
	{
		if ((m_myTeam.GetFansLevel () + 1) >= m_fansLevelPrice.Length)
		{
			return;
		}

		if (m_Cash >= m_fansLevelPrice [(int)m_myTeam.GetFansLevel () + 1]) {
			m_myTeam.UpdateFansLevel (i_Value);
			AddCash(-m_fansLevelPrice [(int)(m_myTeam.GetFansLevel () + 1)]);
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
			AddCash (-m_stadiumLevelPrice [(int)(m_myTeam.GetStadiumLevel () + 1)]);
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
			AddCash (-m_facilitiesLevelPrice [(int)(m_myTeam.GetFacilitiesLevel () + 1)]);
		}
	}

    private void initTeams(int i_NumOfTeams)
    {
        m_AllTeams = new TeamScript[i_NumOfTeams];

		//Temp 
		m_AllTeams [0] = m_myTeam;	
		m_AllTeams[0].SetName("Your Team ");

        for (int i = 1; i < i_NumOfTeams; i++)
        {
            m_AllTeams[i] = new TeamScript();
            m_AllTeams[i].SetName("Team " + (i + 1));
        }
    }

	//Team in the first place is the team in the last place in the array.
	public void updateTableLeague()
    {
		Array.Sort(m_AllTeams, delegate(TeamScript team1, TeamScript team2) 
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
		
		m_table = GameObject.FindGameObjectWithTag("Table").GetComponent<TableScript>();
		for(int i = 0; i< m_AllTeams.Length; i++){
			m_table.UpdateLine((m_AllTeams.Length - i - 1),(m_AllTeams.Length - i),
			                   m_AllTeams[i].GetName(),m_AllTeams[i].GetMatchPlayed(),m_AllTeams[i].GetMatchWon(),
			                   m_AllTeams[i].GetMatchLost(),m_AllTeams[i].GetMatchDrawn(),m_AllTeams[i].GetGoalsFor(),
			                   m_AllTeams[i].GetGoalsAgainst(),m_AllTeams[i].GetPoints());

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
