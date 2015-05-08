using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tap : MonoBehaviour {

    public int m_Value = 1;
    public Text m_CashText;
    private int m_NumOfClicks;

    void Awake()
    {
        m_Value = GameManager.s_GameManger.m_User.CoinValue;
    }

    void Update()
    {
        m_CashText.text = "$" + GameManager.s_GameManger.GetCash();
    }

    public void OnTap()
    {
        GameManager.s_GameManger.AddCash(m_Value);
        GameManager.s_GameManger.NumOfClicksOnCoin++;
        SoundManager.s_SoundManager.playClickSound();
    }
}
