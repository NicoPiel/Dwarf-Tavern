using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBeerHolder : MonoBehaviour
{
    private ItemBeer _itemBeer1;
    private ItemBeer _itemBeer2;
    private ItemBeer _itemBeer3;
    private ItemBeer _itemBeer4;
    

    public bool Add(ItemBeer itemBeer)
    {
        if (_itemBeer1 == null)
        {
            _itemBeer1 = itemBeer;
            return true;
        }
        if (_itemBeer2 == null)
        {
            _itemBeer2 = itemBeer;
            return true;
        }
        if (_itemBeer3 == null)
        {
            _itemBeer3 = itemBeer;
            return true;
        }
        if (_itemBeer4 == null)
        {
            _itemBeer4 = itemBeer;
            return true;
        }
        return false;
    }

    public void Remove(int slot)
    {
        switch (slot)
        {
            case 1:
                _itemBeer1 = null;
                break;
            case 2:
                _itemBeer2 = null;
                break;
            case 3:
                _itemBeer3 = null;
                break;
            case 4:
                _itemBeer4 = null;
                break;
        }
    }

    public bool IsFull()
    {
        return _itemBeer1 != null && _itemBeer2 != null && _itemBeer3 != null && _itemBeer4 != null;
    }

    public ItemBeer GetItemBeerFromSlot(int slot)
    {
        switch (slot)
        {
            case 1:
                return _itemBeer1; ;
            case 2:
                return _itemBeer2;
            case 3:
                return _itemBeer3;;
            case 4:
                return _itemBeer4;
        }
        return null;
    }
}
