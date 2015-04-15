using UnityEngine;
using System.Collections;

public class FinanceManager : MonoBehaviour {

    public static FinanceManager s_FinanceManager;

    public int m_FacilitiesMultiplier = 5000;
    public int m_StadiumMultiplier = 40000;

    void Awake()
    {
        if (s_FinanceManager == null)
        {
            s_FinanceManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int CalculateIncome(TeamScript i_Team)
    {
        int fanBase = i_Team.GetFanBase();
        int crowdAtLastMatch = i_Team.GetCrowdAtLastMatch();

        int incomeFromTickets = crowdAtLastMatch * i_Team.GetTicketPrice();
        int incomeFromMerchandise = (int) (fanBase * Random.Range(0f, 0.8f) * i_Team.GetMerchandisePrice());

        return incomeFromTickets + incomeFromMerchandise;
    }

    // Return as positive number!!
    public int CalculateOutcome(TeamScript i_Team)
    {
        int facilitiesLevel = (int) i_Team.GetFacilitiesLevel();
        int stadiumLevel = (int)i_Team.GetStadiumLevel();
        int salary = i_Team.GetSalary();

        int facilitiesCost = facilitiesLevel * m_FacilitiesMultiplier;
        int stadiumCost = stadiumLevel * m_StadiumMultiplier;

        return facilitiesCost + stadiumCost + salary;
    }
}
