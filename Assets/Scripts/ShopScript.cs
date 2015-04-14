using UnityEngine;
using System.Collections;

public class ShopScript : MonoBehaviour {



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
