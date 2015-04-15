using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputScreenScript : MonoBehaviour {

	public InputField m_teamName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 if(Input.GetKeyDown(KeyCode.Q)){
			GameManager.s_GameManger.m_myTeam.SetName(m_teamName.textComponent.text);
			Debug.Log("Team name is "+m_teamName.textComponent.text);


		}
	
	}
}
