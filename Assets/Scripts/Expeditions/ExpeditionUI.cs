using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpeditionUI : MonoBehaviour
{
    public int ExpeditionSlot;

    public TMP_Text name;
    public TMP_Text length;

    public void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        Expedition expo = ExpeditionHolder.GetInstance().GetExpedition(ExpeditionSlot);
        if (expo != null)
        {
            name.text = expo.GetName();
            length.text = expo.GetLength().ToString();
        }
        else
        {
            
        }
    }

    public void ButtonClick()
    {
        
    }
}
