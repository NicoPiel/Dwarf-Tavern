using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class ExpeditionUI : MonoBehaviour
{
    public int expeditionSlot;

    public TMP_Text tmpName;
    public TMP_Text length;

    public void OnEnable()
    {
        UpdateUI();
        GameManager.GetEventHandler().onExpeditionHolderChanged.AddListener(UpdateUI);
    }

    private void UpdateUI()
    {
        if (ExpeditionHolder.GetInstance().IsExpeditionSelected())
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        
        
        
        var expo = ExpeditionHolder.GetInstance().GetExpedition(expeditionSlot);
        if (expo != null)
        {
            gameObject.transform.localScale = Vector3.one;
            tmpName.text = expo?.GetName();
            length.text = expo?.GetLength().ToString();
        }
        else
        {
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    public void ButtonClick()
    {
        ExpeditionHolder.GetInstance().SelectExpedition(expeditionSlot);
    }
}
