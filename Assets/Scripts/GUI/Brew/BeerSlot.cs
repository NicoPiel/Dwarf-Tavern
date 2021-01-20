using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerSlot : MonoBehaviour
{
    public int SlotNumber;
    
    public Sprite GlassFull;
    public Sprite GlassEmpty;
    public Sprite Fill;
    
    private ItemBeerHolder itemBeerHolder;
    // Start is called before the first frame update
    void OnEnable()
    {
       itemBeerHolder = ItemBeerHolder.GetInstance();
       UpdateSlot();
       GameManager.GetEventHandler().onItemBeerHolderChanged.AddListener(UpdateSlot);
    }
    
    public void UpdateSlot()
    {
        // Debug.Log("BeerSlot UI Updating");
        if (itemBeerHolder.GetItemBeerFromSlot(SlotNumber) != null)
        {
            // Debug.Log("Slot Filled:");
            ItemBeer item = itemBeerHolder.GetItemBeerFromSlot(SlotNumber);
            gameObject.GetComponent<Image>().enabled = true;
            gameObject.GetComponent<Image>().color = item.GetColorModifier();
            gameObject.transform.Find("Glass Image").GetComponent<Image>().sprite = GlassFull;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
            gameObject.transform.Find("Glass Image").GetComponent<Image>().sprite = GlassEmpty;
        }
    }

    public void EmptySlot()
    {
        itemBeerHolder.Remove(SlotNumber);
    }
}
