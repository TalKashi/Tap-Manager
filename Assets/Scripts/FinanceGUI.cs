using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinanceGUI : MonoBehaviour
{
    public Text m_Header;
    public Text m_Score;
	public Text m_incomeFromTickets;
	public Text m_incomeFromMerchandise;
	public Text m_facilitiesCost;
	public Text m_stadiumCost;
	public Text m_salary;
    public Text m_TotalText;
    public Text m_Total;
    public Text m_HomeTeamName;
    public Image m_HomeTeamLogo;
    public Text m_AwayTeamName;
    public Image m_AwayTeamLogo;

    private Color k_Green;

    void Awake()
    {
        k_Green.b = 5;
    }

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
        string headerText = "WON";
        switch (GameManager.s_GameManger.m_myTeam.LastResult)
        {
            case eResult.Lost:
                headerText = "LOST";
                break;
            case eResult.Draw:
                headerText = "DRAW";
                break;
        }
        m_Header.text = string.Format("YOU {0}!", headerText);
        m_HomeTeamName.text = lastMatch.GetHomeTeamString();
        m_AwayTeamName.text = lastMatch.GetAwayTeamString();

        m_HomeTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(lastMatch.HomeTeamLogoIdx);
        m_AwayTeamLogo.sprite = GameManager.s_GameManger.GetTeamLogoBig(lastMatch.AwayTeamLogoIdx);

        int revenue = income - outcome;
        m_Total.text = string.Format("{0:C0}", revenue < 0 ? -revenue : revenue);
        if (income < outcome)
        {
            m_Total.color = new Color(0.87058823529411764705882352941176f, 0.30588235294117647058823529411765f, 0.14901960784313725490196078431373f);
        }
        else
        {
            m_Total.color = new Color(0.18431372549019607843137254901961f, 0.76078431372549019607843137254902f, 0.02352941176470588235294117647059f);
        }
        m_TotalText.color = m_Total.color;
    }
}
