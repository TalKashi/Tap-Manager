using UnityEngine;
using System.Collections;

public class SquadTableUI : MonoBehaviour {

	public GameObject[] m_playersLines;


	// Use this for initialization
	void Start () {
	
	}

	public void UpdatePlayerLine(int i, Sprite i_sprite,string i_name,string i_level,string i_position)
	{
		PlayerLineGUIScript playerLineGUIScript = m_playersLines [i].GetComponent<PlayerLineGUIScript> ();
		playerLineGUIScript.SetPicture (i_sprite);
		playerLineGUIScript.m_level.text = i_level;
		playerLineGUIScript.m_name.text = i_name;
		playerLineGUIScript.m_position.text = i_position;

	}
	
}
