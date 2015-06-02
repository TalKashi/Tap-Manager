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
        m_incomeFromTickets = (int) i_Team.IncomeFromTickets;
        m_incomeFromMerchandise = (int) i_Team.IncomeFromMerchandise;
		
        return m_incomeFromTickets + m_incomeFromMerchandise;
    }

    /*
     * Return as positive number!!
     */
    public int CalculateOutcome(TeamScript i_Team)
    {
        
        m_salary = (int)i_Team.Salary;

        m_facilitiesCost = (int) i_Team.FacilitiesCost;
        m_stadiumCost = (int) i_Team.StadiumCost;

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
