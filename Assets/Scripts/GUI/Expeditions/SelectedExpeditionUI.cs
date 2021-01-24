using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SelectedExpeditionUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateUI();
        GameManager.GetEventHandler().onExpeditionHolderChanged.AddListener(UpdateUI);
    }

    private void UpdateUI()
    {
        if(ExpeditionHolder.GetInstance().IsSomethingSelected())
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
