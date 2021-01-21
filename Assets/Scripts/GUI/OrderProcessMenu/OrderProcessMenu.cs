using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrderProcessMenu : BaseMenu
{
    public Button processButton;
    public Button closeButton;
    public BeerDisplay beerDisplay;

    private ItemBeer itemHeld;

    public static OrderProcessEvent onOrderProcess;
    public static UnityEvent onOrderProcessCancel;

    private void Awake()
    {
        onOrderProcess = new OrderProcessEvent();
        onOrderProcessCancel = new UnityEvent();
    }

    public void OrderProcess()
    {
        if (itemHeld == null) return;
        
        onOrderProcess.Invoke(itemHeld);
        HideMenu();
    }

    public void OrderProcessCancel()
    {
        onOrderProcessCancel.Invoke();
        
        RemoveItem();
        HideMenu();
    }
    
    public void SetItem(BeerSlot item, ItemBeer itemBeer)
    {
        itemHeld = itemBeer;
        ItemBeerHolder.GetInstance().Set(itemBeer, 5);
        ItemBeerHolder.GetInstance().Remove(item.slotNumber);
    }

    public void RemoveItem()
    {
        ItemBeerHolder.GetInstance().Add(itemHeld);
        ItemBeerHolder.GetInstance().Remove(5);
        itemHeld = null;
    }

    public override void ShowMenu()
    {
        isShown = true;
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(220, 295, 0), 0.4f);
        
        beerDisplay.ShowMenu();
    }

    public override void HideMenu()
    {
        isShown = false;
        
        beerDisplay.HideMenu();
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(-250, 295, 0), 0.4f);
    }
}

public class OrderProcessEvent : UnityEvent<ItemBeer>
{
    
}
