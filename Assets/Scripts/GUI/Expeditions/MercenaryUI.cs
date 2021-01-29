using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryUI : MonoBehaviour
{
    private Mercenary _mercenary;
    public TMP_Text mercenaryName;
    public TMP_Text level;
    public TMP_Text hp;
    public TMP_Text price;

    public Image icon;

    public Sprite swordsman;
    public Sprite mage;
    public Sprite priest;
    public Sprite hunter;
    

    private void OnEnable()
    {
        _mercenary = new Mercenary(5);
        UpdateUI();
    }

    private void UpdateUI()
    {
        mercenaryName.text = $"{_mercenary.GetName()}";
        level.text = $"Level: {_mercenary.GetLevel().ToString()}";
        hp.text = $"HP: {_mercenary.GetMaxHealthPoints().ToString()}";
        price.text = _mercenary.GetPrice().ToString();
        
        if(_mercenary.GetRole() == Mercenary.MercenaryRole.Hunter)
            icon.sprite = hunter;
        else if (_mercenary.GetRole() == Mercenary.MercenaryRole.Mage)
            icon.sprite = mage;
        else if (_mercenary.GetRole() == Mercenary.MercenaryRole.Priest)
            icon.sprite = priest;
        else if (_mercenary.GetRole() == Mercenary.MercenaryRole.Swordsman)
            icon.sprite = swordsman;
            
    }

    public void ButtonClicked()
    {
        if (!ExpeditionHolder.GetInstance().GetSelectedExpedition().GetTeam().IsFull())
        {
            if (InventoryManager.GetInstance().GetPlayerInventory().TryCharge(_mercenary.GetPrice(), false))
            {
                ExpeditionHolder.GetInstance().GetSelectedExpedition().GetTeam().AddToTeam(_mercenary);
                Destroy(gameObject);
            }
        }
    }
}

