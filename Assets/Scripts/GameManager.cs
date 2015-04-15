using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager s_GameManger;

    private TeamScript[] m_AllTeams;
	TeamScript m_myTeam;

	void Awake () {
		if (s_GameManger == null) {
			s_GameManger = this;
			DontDestroyOnLoad (gameObject);

		}
		else 
		{
			Destroy(gameObject);
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
    }

	public void FansUpdate(float i_Value)
	{
        m_myTeam.UpdateFansLevel(i_Value);
	}

	public void StadiumUpdate(float i_Value)
	{
        m_myTeam.UpdateStadiumLevel(i_Value);
	}


	public void FacilitiesUpdate(float i_Value)
	{
        m_myTeam.UpdateFacilitiesLevel(i_Value);
	}

    private void initTeams(int i_NumOfTeams)
    {
        m_AllTeams = new TeamScript[i_NumOfTeams];
        for (int i = 0; i < i_NumOfTeams; i++)
        {
            m_AllTeams[i] = new TeamScript();
            m_AllTeams[i].SetName("Team " + (i + 1));
        }
    }
}
