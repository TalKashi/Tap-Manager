using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinanceGUI : MonoBehaviour {

    public Text m_Outcome;
    public Text m_Income;
    public Text m_Total;

    void Start()
    {
        int income = FinanceManager.s_FinanceManager.CalculateIncome(GameManager.s_GameManger.m_myTeam);
        int outcome = FinanceManager.s_FinanceManager.CalculateOutcome(GameManager.s_GameManger.m_myTeam);

        m_Outcome.text = "Total outcome: " + outcome;
        m_Income.text = "Total income: " + income;

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
