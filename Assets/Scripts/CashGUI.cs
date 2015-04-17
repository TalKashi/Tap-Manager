using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CashGUI : MonoBehaviour {

    public Text m_CashText;

    void Awake()
    {
        if (m_CashText == null)
        {
            m_CashText = GetComponent<Text>();
        }
    }

    void Update()
    {
        m_CashText.text = "$" + GameManager.s_GameManger.GetCash();
    }
}
