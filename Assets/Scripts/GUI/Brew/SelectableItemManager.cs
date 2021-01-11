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
        _inventory = InventoryManager.GetInstance().GetPlayerInventory();
        Debug.Log("Button Pressed" + _inventory.GetContents().Count);
        
        foreach(var t in _inventory.GetContents())
        {
            
            Item item = t.Key;
            var gameObjectItem = Instantiate(prefabItemFrame, transform);
            gameObjectItem.name = item.GetId();
            gameObjectItem.transform.Find("Icon").GetComponent<Image>().sprite = item.GetSprite();
            gameObjectItem.transform.Find("Amount").GetComponent<TMP_Text>().text = t.Value+"x";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
