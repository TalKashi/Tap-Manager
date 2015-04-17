using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLineGUIScript : MonoBehaviour {

	public GameObject m_playerPicture;
	public Text m_name;
	public Text m_position;
	public Text m_level;

	public void SetPicture(Sprite i_sprite)
	{
		m_playerPicture.GetComponent<SpriteRenderer> ().sprite = i_sprite;
	} 
	

}
