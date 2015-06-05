using UnityEngine;
using UnityEngine.UI;

public class GoalScorerGUI : MonoBehaviour
{

    public Text m_HomeGoalScorer;
    public Text m_HomeGoalMinute;
    public Text m_AwayGoalScorer;
    public Text m_AwayGoalMinute;

    public void MySetActive(bool i_IsHomeGoal, string i_Name, int i_Minute)
    {
        if (i_IsHomeGoal)
        {
            m_HomeGoalScorer.text = i_Name;
            m_HomeGoalScorer.gameObject.SetActive(true);
            m_HomeGoalMinute.text = string.Format("{0}'", i_Minute);
            m_HomeGoalMinute.gameObject.SetActive(true);
        }
        else
        {
            m_AwayGoalScorer.text = i_Name;
            m_AwayGoalScorer.gameObject.SetActive(true);
            m_AwayGoalMinute.text = string.Format("{0}'", i_Minute);
            m_AwayGoalMinute.gameObject.SetActive(true);
        }

        gameObject.SetActive(true);
    }
}
