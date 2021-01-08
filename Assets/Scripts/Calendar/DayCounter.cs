using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayCounter : MonoBehaviour
{
    private static DayCounter _instance;
    private int _dayCount;
    
    private void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        _instance = this;
        Debug.Log("DayCounterAwake");
        GameManager.GetEventHandler().onAfterHourSceneLoaded.AddListener(CountDay);
    }

    private void CountDay()
    {
        _dayCount++;
    }

    public int GetDayCount()
    {
        return _dayCount;
    }

  
}
