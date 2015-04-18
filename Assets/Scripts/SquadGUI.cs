using UnityEngine;
using System.Collections;

public class SquadGUI : MonoBehaviour
{

    public GameObject m_PlayerRowGameObject;

    void Start()
    {
        PlayerScript[] allPlayers = GameManager.s_GameManger.m_MySquad.GetAllSquad();
        for (int i = 0; i < allPlayers.Length; i++)
        {
            GameObject playerRow = Instantiate(m_PlayerRowGameObject);
            playerRow.transform.SetParent(gameObject.transform);
            // get row script
            PlayerRowGUI rowScript = playerRow.GetComponent<PlayerRowGUI>();
            // pass player[i] data to it 
            rowScript.Init(allPlayers[i],i);
        }
        

    }
}
