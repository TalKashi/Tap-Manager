using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinanceGUI : MonoBehaviour {

	public Text m_incomeFromTickets;
	public Text m_incomeFromMerchandise;
	public Text m_facilitiesCost;
	public Text m_stadiumCost;
	public Text m_salary;
    public Text m_Total;

    void Start()
    {
        int income = FinanceManager.s_FinanceManager.CalculateIncome(GameManager.s_GameManger.m_myTeam);
        int outcome = FinanceManager.s_FinanceManager.CalculateOutcome(GameManager.s_GameManger.m_myTeam);

		m_facilitiesCost.text = "Facilities cost: " +  FinanceManager.s_FinanceManager.GetFacilitiesCost();
		m_stadiumCost.text = "Stadium cost: " +  FinanceManager.s_FinanceManager.GetStadiumCost();
		m_salary.text = "Team salary: " + FinanceManager.s_FinanceManager.GetSalary();
		m_incomeFromTickets.text = "Income from tickets: " + FinanceManager.s_FinanceManager.GetIncomeFromTickets ();
		m_incomeFromMerchandise.text = "Income from merchandise: " + FinanceManager.s_FinanceManager.GetIncomeFromMerchandise ();

        m_Total.text = "Total: " + (income - outcome);
        if (income < outcome)
        {
            m_Total.color = Color.red;
        }
        else
        {
            m_Total.color = Color.green;
        }

    }
}
