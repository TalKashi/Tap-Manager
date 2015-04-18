using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip m_click;
	public AudioClip m_win;
	public AudioClip m_lose;
	public AudioClip m_draw;


	public void playWinSound()
	{
		AudioSource.PlayClipAtPoint (m_click, transform.position);
	}

	public void playLoseSound()
	{
		AudioSource.PlayClipAtPoint (m_lose, transform.position);
	}

	public void playDrawSound()
	{
		AudioSource.PlayClipAtPoint (m_draw, transform.position);
	}

	public void playClickSound()
	{
		AudioSource.PlayClipAtPoint (m_click, transform.position);
	}


}
