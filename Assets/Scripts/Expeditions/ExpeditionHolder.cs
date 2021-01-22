using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionHolder : MonoBehaviour
{
    private List<Expedition> _todaysExpeditions;
    
    // Start is called before the first frame update
    void Start()
    {
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
}
