using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BeerglasSlotManager : MonoBehaviour
{
    // Start is called before the first frame update
    private UnityEvent beerChanged  = new UnityEvent();

    
    public GameObject BeerSlot1;
    public GameObject BeerSlot2;
    public GameObject BeerSlot3;
    public GameObject BeerSlot4;

    public Sprite GlassFull;
    public Sprite GlassEmpty;
    public Sprite Fill;
    
    public ItemBeerHolder itemBeerHolder;
    
    void OnEnable()
    {
        if (!itemBeerHolder) itemBeerHolder = GameObject.FindWithTag("ItemBeerHolder").GetComponent<ItemBeerHolder>();
        GameManager.GetEventHandler().onBrewed.AddListener(AddBeer);
        beerChanged.AddListener(UpdateUI);
        UpdateUI();
    }

    private void AddBeer(ItemBeer itemBeer)
    {
        itemBeerHolder.Add(itemBeer);
        // Debug.Log("Beer Added");
        beerChanged.Invoke();
    }
    void UpdateUI()
    {
        UpdateSlot(1, BeerSlot1);
        UpdateSlot(2, BeerSlot2);
        UpdateSlot(3, BeerSlot3);
        UpdateSlot(4, BeerSlot4);
    }

    public void UpdateSlot(int slot, GameObject slotObject)
    {
        // Debug.Log("BeerSlot UI Updating");
        if (itemBeerHolder.GetItemBeerFromSlot(slot) != null)
        {
            // Debug.Log("Slot Filled:");
            ItemBeer item = itemBeerHolder.GetItemBeerFromSlot(slot);
            slotObject.GetComponent<Image>().enabled = true;
            slotObject.GetComponent<Image>().color = item.GetColorModifier();
            slotObject.transform.Find("Glass Image").GetComponent<Image>().sprite = GlassFull;
        }
        else
        {
            slotObject.GetComponent<Image>().enabled = false;
            slotObject.transform.Find("Glass Image").GetComponent<Image>().sprite = GlassEmpty;
        }
    }

    public void OnBeerPressed(int button)
    {
        itemBeerHolder.Remove(button);
        // Debug.Log("Beer removed");
        beerChanged.Invoke();
    }
    
}
