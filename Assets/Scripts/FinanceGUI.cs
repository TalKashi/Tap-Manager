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

		m_facilitiesCost.text = "$" +  FinanceManager.s_FinanceManager.GetFacilitiesCost();
		m_stadiumCost.text = "$" +  FinanceManager.s_FinanceManager.GetStadiumCost();
		m_salary.text = "$" + FinanceManager.s_FinanceManager.GetSalary();
		m_incomeFromTickets.text = "$" + FinanceManager.s_FinanceManager.GetIncomeFromTickets ();
		m_incomeFromMerchandise.text = "$" + FinanceManager.s_FinanceManager.GetIncomeFromMerchandise ();

        m_Total.text = "Total: $" + (income - outcome);
        if (income < outcome)
        {
            m_Total.color = Color.red;
        }
        else
        {
            m_Total.color = Color.green;
        }
        StartCoroutine(sendFinanceToServer(income - outcome));
    }

    public void UpdateTeamWeeklyFinance()
    {
        GameManager.s_GameManger.UpdateWeeklyFinance();
    }

    IEnumerator sendFinanceToServer(int i_Revenue)
    {
        Debug.Log("Sending this week revenue of: " + i_Revenue);
        WWWForm form = new WWWForm();
        form.AddField("email", GameManager.s_GameManger.m_User.Email);
        form.AddField("fbid", GameManager.s_GameManger.m_User.FBId);
        form.AddField("money", i_Revenue);

        WWW request = new WWW(GameManager.URL + "addMoney", form);
        Debug.Log("Sending finance report");
        yield return request;
        Debug.Log("Recieved finance report response");

        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("ERROR: " + request.error);
        }
        else
        {
            switch (request.text)
            {
                case "ok":
                    Debug.Log("Server updated team finance");
                    // TODO: enable next screen button
                    break;
                case "null":
                    Debug.Log("Sever had problem?");
                    break;
            }
        }
    }
}
