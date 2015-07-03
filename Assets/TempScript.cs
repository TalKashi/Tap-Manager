using UnityEngine;
using System.Collections;

public class TempScript : MonoBehaviour 
{

    public void OnLoadDataClick(string i_SceneName)
    {
        StartCoroutine(GameManager.s_GameManger.SyncClientDB(i_SceneName));
    }
}
