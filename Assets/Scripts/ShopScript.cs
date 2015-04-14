using UnityEngine;
using System.Collections;

public class ShopScript : MonoBehaviour {



	public void OnFansClick()
	{
		GameManager.s_gameManger.FansUpdate (0f);
	}

	public void OnFacilitiesClick()
	{
		GameManager.s_gameManger.FacilitiesUpdate (0f);
	}

	public void OnStadiumClick()
	{
		GameManager.s_gameManger.StadiumUpdate (0f);
	}

}
