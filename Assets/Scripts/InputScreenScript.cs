using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputScreenScript : MonoBehaviour {

	public InputField m_teamName;
	public InputField m_stadiumName;
	public Text m_error;
	private bool m_isValid;
	public string m_nextScene;
	// Use this for initialization
	void Start () {
		m_error.text = "";
	}


	public void OnContinueClick(){
		m_isValid = true;
        SoundManager.s_SoundManager.playClickSound();
		if (m_teamName.textComponent.text == null || m_teamName.textComponent.text.Length == 0) {
			m_error.text = "Team name cant be empty String";
			m_isValid = false;
		}

		if (m_stadiumName.textComponent.text == null || m_stadiumName.textComponent.text.Length == 0) {
			m_error.text = "Stadium name cant be String";
			m_isValid = false;
		}

		if (m_isValid) {
			GameManager.s_GameManger.m_myTeam.SetName(m_teamName.textComponent.text);
			GameManager.s_GameManger.m_myTeam.SetStadiumName(m_stadiumName.textComponent.text);
			Debug.Log("Stadium name is "+m_stadiumName.textComponent.text.Length);
			Debug.Log("Team name is "+m_teamName.textComponent.text);
			Application.LoadLevel(m_nextScene);

		}

	}
}
