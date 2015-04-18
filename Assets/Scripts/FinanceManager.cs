using UnityEngine;
using System.Collections;

public class FinanceManager : MonoBehaviour {

    public static FinanceManager s_FinanceManager;

    public int m_FacilitiesMultiplier = 5000;
    public int m_StadiumMultiplier = 40000;
	private int m_incomeFromTickets;
	private int m_incomeFromMerchandise;
	private int m_facilitiesCost;
	private int m_stadiumCost;
	private int m_salary;

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

        m_incomeFromTickets = crowdAtLastMatch * i_Team.GetTicketPrice();
        m_incomeFromMerchandise = (int) (fanBase * Random.Range(0f, 0.8f) * i_Team.GetMerchandisePrice());
		Debug.Log ("fanBase = " + fanBase + "--" + "crowdAtLastMatch = " + crowdAtLastMatch + "m_incomeFromTickets= " +
			m_incomeFromTickets + " ,m_incomeFromMerchandise = " + m_incomeFromMerchandise);
        return m_incomeFromTickets + m_incomeFromMerchandise;
    }

    /*
     * Return as positive number!!
     */
    public int CalculateOutcome(TeamScript i_Team)
    {
        int facilitiesLevel = (int) i_Team.GetFacilitiesLevel();
        int stadiumLevel = (int)i_Team.GetStadiumLevel();
        m_salary = i_Team.GetSalary();

        m_facilitiesCost = (facilitiesLevel+1) * m_FacilitiesMultiplier;
        m_stadiumCost = (stadiumLevel+1) * m_StadiumMultiplier;

        return m_facilitiesCost + m_stadiumCost + m_salary;
    }

	public int GetStadiumCost()
	{
		return m_stadiumCost;
	}

	public int GetFacilitiesCost()
	{
		return m_facilitiesCost;
	}

	public int GetIncomeFromTickets()
	{
		return m_incomeFromTickets;
	}

	public int GetIncomeFromMerchandise(){
		return m_incomeFromMerchandise;
	}

	public int GetSalary()
	{
		return m_salary;
	}
}
