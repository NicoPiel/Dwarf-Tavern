using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionHolder : MonoBehaviour
{
    private List<Expedition> _todaysExpeditions;

    private Expedition _selectedExpedition;

    private static ExpeditionHolder _instance;

    public static ExpeditionHolder GetInstance()
    {
        return _instance;
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(WaitUntilDayCounterIsInitialized());
    }

    public Expedition GetExpedition(int id)
    {
        Debug.Log(id);
        if (id > 0 && id < 4)
            return _todaysExpeditions[id - 1];
        else
            throw new ArgumentException("ID not in Range");
    }

    public void SelectExpedition(int id)
    {
        if (id > 0 && id < 4 && _todaysExpeditions[id - 1] != null)
        {
            _selectedExpedition = _todaysExpeditions[id - 1];
            _todaysExpeditions[id - 1] = null;
            GameManager.GetEventHandler().onExpeditionHolderChanged.Invoke();
        }
        else
            throw new ArgumentException("ID not in Range");
    }

    private IEnumerator WaitUntilDayCounterIsInitialized()
    {
        yield return new WaitUntil(() => DayCounter.GetInstance());
        yield return new WaitUntil(DayCounter.IsInitialized);

        
        _instance = this;
        _todaysExpeditions = new List<Expedition>();
        int difficulty;

        if (DayCounter.GetInstance().GetDayCount() < 3)
        {
            difficulty = 1;
        }
        else if (DayCounter.GetInstance().GetDayCount() < 6)
        {
            difficulty = 2;
        }
        else
        {
            difficulty = 3;
        }

        _todaysExpeditions.Add(new Expedition(difficulty));
        _todaysExpeditions.Add(new Expedition(difficulty));
        _todaysExpeditions.Add(new Expedition(difficulty));
    }

    public bool IsSomethingSelected()
    {
        return _selectedExpedition != null;
    }

    public Expedition GetSelectedExpedition()
    {
        return _selectedExpedition;
    }
}