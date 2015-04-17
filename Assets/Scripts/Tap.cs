using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tap : MonoBehaviour {

    public int m_Value = 1;
    public Text m_CashText;

    void Update()
    {
        m_CashText.text = "$" + GameManager.s_GameManger.GetCash();
    }

    public void OnTap()
    {
        GameManager.s_GameManger.AddCash(m_Value);
    }


}
