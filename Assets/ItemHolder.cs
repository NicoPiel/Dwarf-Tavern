using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item;
    
    public Item GetItem()
    {
        return this.item;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
