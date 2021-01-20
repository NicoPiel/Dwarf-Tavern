using System;
using UnityEngine;
using UnityEngine.UI;

public class BeerDisplay : BaseMenu
{
    public OrderProcessMenu orderProcessMenu;
    public BeerglasSlotManager slotManager;

    public GameObject beerSlot01;
    public GameObject beerSlot02;
    public GameObject beerSlot03;
    public GameObject beerSlot04;

    private void Start()
    {
        BeerDisplaySlot.onSlotClicked.AddListener(SetSlotOnOrderProcessMenu);
    }

    public override void ShowMenu()
    {
        gameObject.SetActive(true);
        GetSlots();
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, 200, 0), 0.4f);
    }

    public override void HideMenu()
    {
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, -200, 0), 0.4f).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void SetSlotOnOrderProcessMenu(GameObject obj, int slot)
    {
        orderProcessMenu.SetItem(obj, slotManager.itemBeerHolder.GetItemBeerFromSlot(slot));
    }

    private void GetSlots()
    {
        beerSlot01 = slotManager.BeerSlot1;
        beerSlot02 = slotManager.BeerSlot2;
        beerSlot03 = slotManager.BeerSlot3;
        beerSlot04 = slotManager.BeerSlot4;
    }
}
