using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;

public class SelectedExpeditionUI : MonoBehaviour
{
    public TMP_Text tmpName;
    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateUI();
        GameManager.GetEventHandler().onExpeditionHolderChanged.AddListener(UpdateUI);
    }

    private void UpdateUI()
    {
        if (ExpeditionHolder.GetInstance().IsSomethingSelected())
        {
            var expo = ExpeditionHolder.GetInstance().GetSelectedExpedition();
            gameObject.transform.localScale = Vector3.one;
            tmpName.text = expo.GetName();
        }
        else
            gameObject.transform.localScale = Vector3.zero;
            
    }
}
