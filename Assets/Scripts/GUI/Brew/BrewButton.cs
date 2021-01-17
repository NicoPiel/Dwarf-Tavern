using System;
using System.Collections;
using System.Collections.Generic;
using Brewing;
using Inventory;
using UnityEngine;

public class BrewButton : MonoBehaviour
{
    public Slot slotTaste1;
    public Slot slotTaste2;
    public Slot slotBase;
    public Slot slotBonus;
    
    // Start is called before the first frame update
    

    public void OnBrewButtonClicked()
    {
        if (!slotBase.IsItemSet() || !slotTaste1.IsItemSet())
        {
            Debug.Log("Slots not filled!");
            return;
        }
        

        ItemBeer itemBeer = GetComponent<BrewingManager>().Brew((IngredientItem) slotBase.GetItem(),
            (IngredientItem) slotTaste1.GetItem(), (IngredientItem) slotTaste2.GetItem(),
            (IngredientItem) slotBonus.GetItem());
        
        slotTaste1.TakeItem();
        slotTaste2.TakeItem();
        slotBase.TakeItem();
        slotBonus.TakeItem();
        
        Debug.Log(itemBeer);
        
        GameManager.GetEventHandler().onBrewed.Invoke(itemBeer);

    }
}