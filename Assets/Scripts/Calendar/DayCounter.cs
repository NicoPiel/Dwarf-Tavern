using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayCounter : MonoBehaviour
{
    private static DayCounter _instance;
    [SerializeField]
    private int dayCount;

    private static bool _initialized;
    
    private void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);

        if (ES3.KeyExists("DayCount"))
        {
            dayCount = ES3.Load<int>("DayCount");
        }
        else
        {
            SetDayCount(1);
        }
        _instance = this;
        Debug.Log("DayCounterAwake");
        EventHandler.onDayChanged.AddListener(CountDay);

        _initialized = true;
    }

    private void CountDay()
    {
        dayCount++;
        Save();
    }

    public void SetDayCount(int i)
    {
        dayCount = i;
        Save();
    }

    public int GetDayCount()
    {
        return dayCount;
    }

    public void Save()
    {
        ES3.Save("DayCount", dayCount);
    }

    public static DayCounter GetInstance()
    {
        return _instance;
    }

    public static bool IsInitialized()
    {
        return _initialized;
    }
}
