using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{

    public TMP_Text display;

    private void Start()
    {
        StartCoroutine(WaitUntilInitialized());
    }

    // Update is called once per frame
    void UpdateUI()
    {
        display.text = InventoryManager.GetInstance().GetPlayerInventory().GetFunds().ToString();
    }
    
    private IEnumerator WaitUntilInitialized()
    {
        yield return new WaitUntil(() => InventoryManager.GetInstance().AssetState == InventoryManager.State.Initialized);
        
        EventHandler.onFundsChanged.AddListener(UpdateUI);
        UpdateUI();
    }
}
