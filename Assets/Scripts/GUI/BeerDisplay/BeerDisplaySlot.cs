using System;
using UnityEngine;
using UnityEngine.Events;

public class BeerDisplaySlot : MonoBehaviour
{
    public static SlotEvent onSlotClicked;
    public int slot;

    public void Awake()
    {
        onSlotClicked = new SlotEvent();
    }

    public void SlotClicked()
    {
        onSlotClicked.Invoke(gameObject, slot);
    }
}

public class SlotEvent : UnityEvent<GameObject, int>
{
    
}