using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;

public class MercenaryUI : MonoBehaviour
{
    private Mercenary _mercenary;
    public TMP_Text role;
    public TMP_Text level;
    public TMP_Text hp;
    public TMP_Text price;

    private void OnEnable()
    {
        _mercenary = new Mercenary(5);
        UpdateUI();
    }

    private void UpdateUI()
    {
        role.text = $"{_mercenary.GetRole().ToString()}";
        level.text = $"Level: {_mercenary.GetLevel().ToString()}";
        hp.text = $"HP: {_mercenary.GetMaxHealthPoints().ToString()}";
        price.text = _mercenary.GetPrice().ToString();
    }

    public void ButtonClicked()
    {
        if (InventoryManager.GetInstance().GetPlayerInventory().TryCharge(_mercenary.GetPrice(), false))
        {
            ExpeditionHolder.GetInstance().GetSelectedExpedition().GetTeam().AddToTeam(_mercenary);
            Destroy(gameObject);
        }
    }
}
