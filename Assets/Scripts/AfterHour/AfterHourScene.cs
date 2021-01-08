using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterHourScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AfterHour Scene Loaded");
        GameManager.GetEventHandler().onAfterHourSceneLoaded.Invoke();
    }
}
