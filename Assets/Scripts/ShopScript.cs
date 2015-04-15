using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShopScript : MonoBehaviour {

	public Text m_fansLevel;
	public Text m_facilitiesLevel;
	public Text m_stadiumLevel;

	void Start()
	{
		m_fansLevel.text = "Fans Level" + (GameManager.s_GameManger.m_myTeam.GetFansLevel() + 1);
		m_facilitiesLevel.text = "Facilities Level: " + (GameManager.s_GameManger.m_myTeam.GetFacilitiesLevel() + 1);
		m_stadiumLevel.text = "Stadium Level: "+(GameManager.s_GameManger.m_myTeam.GetStadiumLevel() + 1);
	}

	public void OnFansClick()
	{
		GameManager.s_GameManger.FansUpdate (1);
	}

	public void OnFacilitiesClick()
	{
		GameManager.s_GameManger.FacilitiesUpdate (1);
	}

	public void OnStadiumClick()
	{
		GameManager.s_GameManger.StadiumUpdate (1);
	}

}
