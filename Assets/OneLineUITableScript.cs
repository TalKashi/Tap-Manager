using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OneLineUITableScript : MonoBehaviour {

	public int place;
	public Text m_place;
	public Text m_team;
	public Text m_played;
	public Text m_won;
	public Text m_lost;
	public Text m_drawn;
	public Text m_for;
	public Text m_against;
	public Text m_points;

	void Start()
	{
		m_place.text =""+ place;

	}
}
