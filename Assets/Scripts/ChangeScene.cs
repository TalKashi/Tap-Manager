using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public void OnClickChangeScene(string i_SceneName)
    {
        Application.LoadLevel(i_SceneName);
    }
}
