using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    private Inventory.Inventory _inventory;
    private Item _currentItem;

    private void Start()
    {
        _inventory = InventoryManager.GetInstance().GetPlayerInventory();
    }

    public void OnEnable()
    {
        transform.Find("Image").gameObject.SetActive(false);
        _currentItem = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging == false)
        {
            transform.Find("Image").gameObject.GetComponent<Image>().sprite = null;
            var tempColor = transform.Find("Image").gameObject.GetComponent<Image>().color;
            tempColor.a = 0f;
            transform.Find("Image").gameObject.GetComponent<Image>().color = tempColor;
            
            if (_currentItem != null)
            {
                Debug.Log("Nothing here 2");
                _inventory.AddItem(_currentItem, 1);
                _currentItem = null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        if (eventData.pointerDrag != null)
        {
            Item item = eventData.pointerDrag.GetComponent<ItemHolder>()?.GetItem();
            if (item != null)
            {
                Debug.Log("Dropped item");
                _currentItem = item;
                GameObject image = transform.Find("Image").gameObject;

                image.SetActive(true);
                
                var tempColor = image.GetComponent<Image>().color;
                tempColor.a = 255f;
                image.GetComponent<Image>().color = tempColor;
                image.GetComponent<Image>().sprite = item.GetSprite();

                
                _inventory.RemoveItem(item, 1);
            }
        }
    }

    public void OnDisable()
    {
        if (_currentItem != null)
        {
            _inventory.AddItem(_currentItem, 1);
        }
    }
}
