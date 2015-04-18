﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerRowGUI : MonoBehaviour
{

    public Text m_Name;
    public Text m_Raiting;
    public Text m_Age;
    public Text m_Salary;
    public Text m_Position;
    public Image m_IsInjured;
    public Image m_IsSuspended;

    public void Init(PlayerScript i_Player)
    {
        m_Name.text = i_Player.GetShortName();
        m_Raiting.text = i_Player.GetLevel().ToString();
        m_Age.text = i_Player.GetAge().ToString();
        m_Salary.text = i_Player.GetSalary().ToString();
        m_Position.text = i_Player.getPlayerPosition().ToString();
        switch (i_Player.getPlayerPosition())
        {
            case ePosition.GK:
                m_Position.color = Color.yellow;
                break;
                case ePosition.D:
                m_Position.color = Color.blue;
                break;
                case ePosition.MF:
                m_Position.color = Color.green;
                break;
                case ePosition.S:
                m_Position.color = Color.red;
                break;

        }
        if (i_Player.isInjered())
        {
            //TODO: Set injured image
        }
        // TODO: check and set if player is suspended
    }
}
