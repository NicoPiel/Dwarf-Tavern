using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerSlot : MonoBehaviour
{
    public int slotNumber;
    
    public Sprite glassFull;
    public Sprite glassEmpty;
    public Sprite fill;
    
    private ItemBeerHolder itemBeerHolder;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(WaitUntilBeerHolderInitialized());
    }
    
    public void UpdateSlot()
    {
        // Debug.Log("BeerSlot UI Updating");
        if (itemBeerHolder.GetItemBeerFromSlot(slotNumber) != null)
        {
            // Debug.Log("Slot Filled:");
            ItemBeer item = itemBeerHolder.GetItemBeerFromSlot(slotNumber);
            gameObject.GetComponent<Image>().enabled = true;
            gameObject.GetComponent<Image>().color = item.GetColorModifier();
            gameObject.transform.Find("Glass Image").GetComponent<Image>().sprite = glassFull;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
            gameObject.transform.Find("Glass Image").GetComponent<Image>().sprite = glassEmpty;
        }
    }

    public void EmptySlot()
    {
        itemBeerHolder.Remove(slotNumber);
    }

    private IEnumerator WaitUntilBeerHolderInitialized()
    {
        yield return new WaitUntil(() => ItemBeerHolder.GetInstance());
        yield return new WaitUntil(ItemBeerHolder.IsInitialized);
        
        if (!itemBeerHolder)
        {
            itemBeerHolder = ItemBeerHolder.GetInstance();
            UpdateSlot();
        }

        GameManager.GetEventHandler().onItemBeerHolderChanged.AddListener(UpdateSlot);
    }
}
