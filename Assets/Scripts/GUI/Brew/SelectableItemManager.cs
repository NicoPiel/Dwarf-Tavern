using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SelectableItemManager : MonoBehaviour
{
    public GameObject prefabItemFrame;

    private Inventory.Inventory _inventory;
    private SortParameter _sortParameter;
    [SerializeField] private TMP_InputField _input;

    public enum SortParameter : int
    {
        Basis,
        Taste,
        Bonus,
        All,
        Search
    }
    // Start is called before the first frame update
    
    
    
    private void OnEnable()
    {
        _sortParameter = SortParameter.All;
        UpdateUI();
        GameManager.GetEventHandler().onInventoryChanged.AddListener(() =>
        {
            DeleteEverything();
            UpdateUI();
        });
    }
    
    private void UpdateUI()
    {
        
        _inventory = InventoryManager.GetInstance().GetPlayerInventory();

        DeleteEverything();
        foreach(var t in _inventory.GetContents())
        {
            Item item = t.Key;
            if(Sort(item))
            {
                var gameObjectItem = Instantiate(prefabItemFrame, transform);
                gameObjectItem.name = item.GetId();
                gameObjectItem.transform.Find("ItemObject").GetComponent<Image>().sprite = item.GetSprite();
                gameObjectItem.transform.Find("ItemObject").GetComponent<ItemHolder>().SetItem(item);
                gameObjectItem.transform.Find("Amount").GetComponent<TMP_Text>().text = t.Value + "x";
            }
        }
    }

    private bool Sort(Item item)
    {
        switch (_sortParameter)
        {
            case SortParameter.All:
                return true;
            case SortParameter.Basis:
                if (item.GetModifiers().ContainsKey(Item.Slot.Basic))
                {
                    return true;
                }

                break;
            case SortParameter.Taste:
                if (item.GetModifiers().ContainsKey(Item.Slot.Taste1) ||
                    item.GetModifiers().ContainsKey(Item.Slot.Taste2))
                {
                    return true;
                }
                break;
            case SortParameter.Bonus:
                if (item.GetModifiers().ContainsKey(Item.Slot.Bonus))
                {
                    return true;
                }
                break;
            case SortParameter.Search:
                Debug.Log("Search");
                if (item.GetDisplayName().Contains(_input.text))
                {
                    return true;
                }
                break;
        }

        return false;
    }

    private void DeleteEverything()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ChangeSortParameter(int parameter)
    {
        switch(parameter)
        {
            case 0:
                _sortParameter = SortParameter.Basis;
                break;
            case 1:
                _sortParameter = SortParameter.Taste;
                break;
            case 2:
                _sortParameter = SortParameter.Bonus;
                break;
            case 3:
                _sortParameter = SortParameter.All;
                break;
            case 4:
                _sortParameter = SortParameter.Search;
                break;
        }
        UpdateUI();
    }
}
