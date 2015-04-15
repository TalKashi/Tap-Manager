using UnityEngine;
using System.Collections;
using System.Text;

public class FixturesManager : MonoBehaviour
{
    public static FixturesManager s_FixturesManager;

    public int m_CurrentFixture = 1;
    // First array - for fixture number - for 20 teams should be 19
    // Second array - for match in fixture - for 20 teams should be 10
    // Third array - for pointer to teams - always should be 2
    private TeamScript[, ,] m_FixturesList;

    void Awake()
    {
        if (s_FixturesManager == null)
        {
            s_FixturesManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GenerateFixtures(TeamScript[] i_AllTeams)
    {
        m_CurrentFixture = 0;
        // Reference for this algorithm: http://bluebones.net/2005/05/generating-fixture-lists/
        int totalTeams = i_AllTeams.Length;
        int totalFixtures = totalTeams - 1;
        int matchesPerFixture = totalTeams / 2;
        TeamScript[, ,] tempFixturesList = new TeamScript[totalFixtures, matchesPerFixture, 2];

        for (int fixture = 0; fixture < totalFixtures; fixture++)
        {
            for (int match = 0; match < matchesPerFixture; match++)
            {
                int homeTeamIndex = (fixture + match) % (totalTeams - 1);
                int awayTeamIndex = (totalTeams - 1 - match + fixture)%(totalTeams - 1);

                // Last team stays in the same place while the others
                // rotate around it.
                if (match == 0)
                {
                    awayTeamIndex = totalTeams - 1;
                }
                tempFixturesList[fixture, match, 0] = i_AllTeams[homeTeamIndex];
                tempFixturesList[fixture, match, 1] = i_AllTeams[awayTeamIndex];
            }
        }

        // Interleave so that home and away games are fairly evenly dispersed.
        m_FixturesList = new TeamScript[totalFixtures, matchesPerFixture, 2];
        int even = 0;
        int odd = totalTeams / 2;
        for (int i = 0; i < totalFixtures; i++)
        {
            if (i % 2 == 0)
            {
                //m_FixturesList.SetValue(tempFixturesList.GetValue(even), i);
                for (int j = 0; j < matchesPerFixture; j++)
                {
                    m_FixturesList[i, j, 0] = tempFixturesList[even, j, 0];
                    m_FixturesList[i, j, 1] = tempFixturesList[even, j, 1];
                }
                even++;
            }
            else
            {
                //m_FixturesList.SetValue(tempFixturesList.GetValue(odd), i);
                for (int j = 0; j < matchesPerFixture; j++)
                {
                    m_FixturesList[i, j, 0] = tempFixturesList[odd, j, 0];
                    m_FixturesList[i, j, 1] = tempFixturesList[odd, j, 1];
                }
                odd++;
            }
        }

        // Last team can't be away for every game so flip them
        // to home on odd rounds.
        for (int fixture = 0; fixture < totalFixtures; fixture++)
        {
            if (fixture % 2 == 1)
            {
                TeamScript tempTeam = m_FixturesList[fixture, 0, 0];
                m_FixturesList[fixture, 0, 0] = m_FixturesList[fixture, 0, 1];
                m_FixturesList[fixture, 0, 1] = tempTeam;
            }
        }

        // FOR TEST
        print(PrintFullFixtures());
    }

    public void ExecuteNextFixture()
    {
        // Needed for next round to switch home and away teams
        int currentFixture = m_CurrentFixture % getFixturesPerRound();
        for (int i = 0; i < getMatchesPerFixture(); i++)
        {
            if (m_CurrentFixture < getFixturesPerRound())
            {
                MatchManager.s_MatchManager.CalcResult(m_FixturesList[currentFixture, i, 0],
                                                         m_FixturesList[currentFixture, i, 1]);
            }
            else
            {
                MatchManager.s_MatchManager.CalcResult(m_FixturesList[currentFixture, i, 1],
                                                         m_FixturesList[currentFixture, i, 0]);
            }
        }
        m_CurrentFixture++;
    }

    public string PrintFullFixtures()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < getFixturesPerRound() * 2; i++)
        {
            stringBuilder.AppendLine("Fixture " + (i + 1));
            for (int j = 0; j < getMatchesPerFixture(); j++)
            {
                if (i < getFixturesPerRound())
                {
                    stringBuilder.AppendLine(m_FixturesList[i, j, 0].GetName() + " v " + m_FixturesList[i, j, 1].GetName());
                }
                else
                {
                    stringBuilder.AppendLine(m_FixturesList[i % getFixturesPerRound(), j, 1].GetName() + " v " + m_FixturesList[i % getFixturesPerRound(), j, 0].GetName());
                }
            }
            stringBuilder.AppendLine();
        }


        return stringBuilder.ToString();
    }

    public string PrintLastFixturesResults()
    {
        if (m_CurrentFixture == 0)
        {
            return "No Matches Yet";
        }
        StringBuilder stringBuilder = new StringBuilder();
        // TODO: Support modulu here!
        for (int i = 0; i < getMatchesPerFixture(); i++)
        {
            MatchInfo lastMatch = m_FixturesList[m_CurrentFixture - 1, i, 0].GetLastMatchInfo();
            stringBuilder.AppendLine(lastMatch.GetHomeTeamString() + ": " + lastMatch.GetHomeGoals() + "\n" +
                                     lastMatch.GetAwayTeamString() + ": " + lastMatch.GetAwayGoals() + "\n" +
                                     "Crowd: " + lastMatch.GetTotalCrowd());
            stringBuilder.AppendLine("------------------------------");
        }

        return stringBuilder.ToString();
    }

    private int getFixturesPerRound()
    {
        return m_FixturesList.GetLength(0);
    }

    private int getMatchesPerFixture()
    {
        return m_FixturesList.GetLength(1);
    }

    // If Given wrong parameters, returns NULL
    private TeamScript getTeamByFixtureAndMatch(int i_Fixture, int i_Match, bool i_IsHomeTeam)
    {
        int fixturesPerRound = m_FixturesList.GetLength(0);
        int matchesPerFixture = m_FixturesList.GetLength(1);
        if (i_Fixture + 1 > fixturesPerRound*2 || i_Match + 1 > matchesPerFixture)
        {
            return null;
        }
        bool isSecondRound = i_Fixture >= fixturesPerRound;
        int teamIndex;
        if (isSecondRound)
        {
            teamIndex = i_IsHomeTeam ? 1 : 0;
            return m_FixturesList[i_Fixture%fixturesPerRound, i_Match, teamIndex];
        }
        teamIndex = i_IsHomeTeam ? 0 : 1;
        return m_FixturesList[i_Fixture % fixturesPerRound, i_Match, teamIndex];
    }
}
