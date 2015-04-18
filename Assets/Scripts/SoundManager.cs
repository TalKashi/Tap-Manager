using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager s_SoundManager;

	public AudioClip m_click;
	public AudioClip m_win;
	public AudioClip m_lose;
	public AudioClip m_draw;

    void Awake()
    {
        if (s_SoundManager == null)
        {
            s_SoundManager = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }

	public void playWinSound()
	{
        AudioSource.PlayClipAtPoint(m_win, transform.position);
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
        AudioSource.PlayClipAtPoint(m_click, transform.position);
	}


}
