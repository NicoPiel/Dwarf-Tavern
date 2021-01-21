using System;
using UnityEngine;

public class BeerDisplay : BaseMenu
{
    public OrderProcessMenu orderProcessMenu;

    public override void ShowMenu()
    {
        gameObject.SetActive(true);
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, 200, 0), 0.4f);
    }

    public override void HideMenu()
    {
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, -200, 0), 0.4f).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void SetSlotOnOrderProcessMenu(BeerSlot slot)
    {
        orderProcessMenu.SetItem(slot, ItemBeerHolder.GetInstance().GetItemBeerFromSlot(slot.slotNumber));
    }
}
