using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableItemManager : MonoBehaviour
{
    public GameObject prefabItemFrame;

    private Inventory.Inventory _inventory;
    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateUI();
        GameManager.GetEventHandler().onInventoryChanged.AddListener(() =>
        {
            DeleteEverything();
            UpdateUI();
        });
    }

    private void UpdateUI()
    {
        Debug.Log("UI should be updated now");
        _inventory = InventoryManager.GetInstance().GetPlayerInventory();
        Debug.Log("Button Pressed" + _inventory.GetContents().Count );
        
        DeleteEverything();
        foreach(var t in _inventory.GetContents())
        {
            Item item = t.Key;
            var gameObjectItem = Instantiate(prefabItemFrame, transform);
            gameObjectItem.name = item.GetId();
            gameObjectItem.transform.Find("ItemObject").GetComponent<Image>().sprite = item.GetSprite();
            gameObjectItem.transform.Find("ItemObject").GetComponent<ItemHolder>().SetItem(item); 
            gameObjectItem.transform.Find("Amount").GetComponent<TMP_Text>().text = t.Value+"x";
        }
    }

    private void DeleteEverything()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
