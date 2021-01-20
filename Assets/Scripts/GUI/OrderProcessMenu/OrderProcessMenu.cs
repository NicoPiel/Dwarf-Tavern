using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrderProcessMenu : BaseMenu
{
    public Button processButton;
    public Button closeButton;
    public GameObject itemSlot;
    public BeerDisplay beerDisplay;

    private ItemBeer itemHeld;

    public static OrderProcessEvent onOrderProcess;

    private void Awake()
    {
        onOrderProcess = new OrderProcessEvent();
    }

    private void Start()
    {
        // gameObject.SetActive(false);
    }

    public void OrderProcess()
    {
        if (itemHeld == null) return;
        
        onOrderProcess.Invoke(itemHeld);
        HideMenu();
    }

    public void RemoveItem()
    {
        itemSlot.GetComponent<Image>().color = new Color(0,0,0,0);
        itemSlot = null;
    }
    
    public void SetItem(GameObject item, ItemBeer itemBeer)
    {
        itemHeld = itemBeer;
        itemSlot = item;
        var itemImage = item.GetComponent<Image>();
        Color itemColor = itemImage.color;
        
        itemSlot.GetComponent<Image>().color = new Color(itemColor.r, itemColor.g, itemColor.b,255);
    }

    public override void ShowMenu()
    {
        gameObject.SetActive(true);
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(220, 295, 0), 0.4f);
        beerDisplay.ShowMenu();
    }

    public override void HideMenu()
    {
        beerDisplay.HideMenu();
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(-250, 295, 0), 0.4f).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}

public class OrderProcessEvent : UnityEvent<ItemBeer>
{
    
}
