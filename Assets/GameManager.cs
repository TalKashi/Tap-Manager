using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager  s_gameManger;

	TeamScript m_myTeamScript;

	void Awake () {
		if (s_gameManger == null) {
			s_gameManger = this;
			DontDestroyOnLoad (gameObject);

		}
		else 
		{
			Destroy(gameObject);
		}
	}

	public void FansUpdate()
	{
		m_myTeamScript.UpdateFansLevel ();
	}

	public void StadiumUpdate()
	{
		m_myTeamScript.UpdateStadiumLevel();
	}


	public void FacilitiesUpdate()
	{
		m_myTeamScript.UpdateFacilitiesLevel ();
	}



	
	// Update is called once per frame
	void Update () {
	
	}
}
