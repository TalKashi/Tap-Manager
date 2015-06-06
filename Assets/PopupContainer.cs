using UnityEngine;

public class PopupContainer : MonoBehaviour
{
    public static PopupContainer s_PopupContainer;

    public GameObject m_NextMactPopup;

    void Awake()
    {
        if (s_PopupContainer == null)
        {
            s_PopupContainer = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextMatchPopup()
    {
        m_NextMactPopup.SetActive(true);
    }
}
