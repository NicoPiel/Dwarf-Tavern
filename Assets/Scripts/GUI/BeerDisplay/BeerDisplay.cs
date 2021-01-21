using UnityEngine;

public class BeerDisplay : BaseMenu
{
    public OrderProcessMenu orderProcessMenu;

    public override void ShowMenu()
    {
        isShown = true;
        
        gameObject.SetActive(true);
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, 200, 0), 0.4f);
    }

    public override void HideMenu()
    {
        isShown = false;
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, -200, 0), 0.4f).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void SetSlotOnOrderProcessMenu(BeerSlot slot)
    {
        if (orderProcessMenu.IsShown()) orderProcessMenu.SetItem(slot, ItemBeerHolder.GetInstance().GetItemBeerFromSlot(slot.slotNumber));
    }
}
