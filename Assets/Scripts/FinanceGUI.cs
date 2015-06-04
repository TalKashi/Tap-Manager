using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinanceGUI : MonoBehaviour
{

    public Text m_Score;
	public Text m_incomeFromTickets;
	public Text m_incomeFromMerchandise;
	public Text m_facilitiesCost;
	public Text m_stadiumCost;
	public Text m_salary;
    public Text m_Total;
    public Text m_HomeTeamName;
    public Image m_HomeTeamLogo;
    public Text m_AwayTeamName;
    public Image m_AwayTeamLogo;

    void Start()
    {
        int income = FinanceManager.s_FinanceManager.CalculateIncome(GameManager.s_GameManger.m_myTeam);
        int outcome = FinanceManager.s_FinanceManager.CalculateOutcome(GameManager.s_GameManger.m_myTeam);

        m_Score.text = string.Format("{0} - {1}", GameManager.s_GameManger.m_myTeam.GetLastMatchInfo().GetHomeGoals(),
            GameManager.s_GameManger.m_myTeam.GetLastMatchInfo().GetHomeGoals());
		m_facilitiesCost.text = string.Format("{0:C0}", FinanceManager.s_FinanceManager.GetFacilitiesCost());
		m_stadiumCost.text = string.Format("{0:C0}", FinanceManager.s_FinanceManager.GetStadiumCost());
		m_salary.text = string.Format("{0:C0}", FinanceManager.s_FinanceManager.GetSalary());
		m_incomeFromTickets.text = string.Format("{0:C0}", FinanceManager.s_FinanceManager.GetIncomeFromTickets ());
		m_incomeFromMerchandise.text = string.Format("{0:C0}", FinanceManager.s_FinanceManager.GetIncomeFromMerchandise ());
        MatchInfo lastMatch = GameManager.s_GameManger.m_myTeam.GetLastMatchInfo();
        m_HomeTeamName.text = lastMatch.GetHomeTeamString();
        m_AwayTeamName.text = lastMatch.GetAwayTeamString();

        int revenue = income - outcome;
        m_Total.text = string.Format("{0:C0}", revenue < 0 ? -revenue : revenue);
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
