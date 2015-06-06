using UnityEngine;
using System.Collections;

public class SetActiveButton : MonoBehaviour 
{

    public void OnClick(GameObject i_ObjectSwitchActiveState)
    {
        i_ObjectSwitchActiveState.SetActive(!i_ObjectSwitchActiveState.activeSelf);
    }

    public void OnClickDestroy(GameObject i_ObjectToDestory)
    {
        Destroy(i_ObjectToDestory);
    }
}
