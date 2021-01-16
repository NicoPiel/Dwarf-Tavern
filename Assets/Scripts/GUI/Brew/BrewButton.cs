using System.Collections;
using System.Collections.Generic;
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
        if (!slotBase.IsItemSet() || !slotTaste1.IsItemSet() || !slotTaste2.IsItemSet())
        {
            Debug.Log("Slots not filled!");
            return;
        }

        Debug.Log(!slotBonus.IsItemSet()
            ? "BrewButton(slotBase.GetItem(),slotTaste1.GetItem(), slotTaste2.GetItem())"
            : "BrewButton(slotBase.GetItem(),slotTaste1.GetItem(), slotTaste2.GetItem()), slotBonus.GetItem()");
    }
}