using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSceneScript : MonoBehaviour {

	private PlayerScript m_playerScript;
	public Text m_position;
	public Text m_firstName;
	public Text m_lastName;
	public Text m_salary;
	private bool m_isInjured;
	public Text m_age;
	public Text m_gamePlayed;
	public Text m_goalScored ;
	public Text m_level;
	public Text m_price;
	public Text m_priceToBoost;
	public Text m_yearOfJoiningTheClub;
	public SpriteRenderer m_PlayerImage;

	public GameObject m_releasePlayerMenu;

	// Use this for initialization
	void Start () {
		int i = PlayerPrefs.GetInt ("SelectedPlayer");
		if (i != null) {
			m_playerScript = GameManager.s_GameManger.m_MySquad.GetPlayerInIndex (i);
		} else 
		{
			m_playerScript = GameManager.s_GameManger.m_MySquad.GetPlayerInIndex (0);
		}

		m_position.text ="Position"+ m_playerScript.getPlayerPosition();
		m_firstName.text = ""+m_playerScript.getPlayerFirstName();
		m_lastName.text = ""+m_playerScript.getPlayerLastName();
		m_salary.text = ""+m_playerScript.GetSalary();
		m_isInjured = m_playerScript.isInjered();
		m_age.text = ""+m_playerScript.GetAge();
		m_gamePlayed.text = ""+m_playerScript.GetGamePlayed();
		m_goalScored.text  = ""+m_playerScript.GetGoalScored();
		m_level.text = ""+m_playerScript.GetLevel();
		m_price.text = ""+m_playerScript.GetPlayerPrice();
		m_priceToBoost.text = ""+m_playerScript.GetPriceToBoostPlayer();
		m_yearOfJoiningTheClub.text = ""+m_playerScript.GetYearJoinindtheClub();
		//m_PlayerImage.sprite = m_playerScript.getPlayerImage().sprite;

	}
	
	public void OnClickReleasePlayer(){
		m_releasePlayerMenu.SetActive (true);
	}

	public void OnClickNoReleasePlayer(){
		m_releasePlayerMenu.SetActive (false);
	}

	public void OnClickYesReleasePlayer(){
		m_releasePlayerMenu.SetActive (false);
		m_playerScript.InitYoungPlayer ();
	}

	public void onClickBoost(){
		if (m_playerScript.GetPriceToBoostPlayer () <= GameManager.s_GameManger.GetCash ()) {
			m_playerScript.BoostPlayer(34);
			GameManager.s_GameManger.AddCash(-m_playerScript.GetPriceToBoostPlayer ());
		}
	}

	public void onClickDrugs(){
		//temp sol
		if (m_playerScript.GetPriceToBoostPlayer ()/2 <= GameManager.s_GameManger.GetCash ()) {
			m_playerScript.BoostPlayer(Random.Range(0,105));
			GameManager.s_GameManger.AddCash((int)-m_playerScript.GetPriceToBoostPlayer ()/2);
		}

	}


}
