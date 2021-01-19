using UnityEngine;
using UnityEngine.UI;

public class OrderProcessMenu : BaseMenu
{
    public Button processButton;
    public Button closeButton;
    public Image itemSlot;
    public BeerDisplay beerDisplay;

    public void RemoveItem()
    {
        itemSlot.color = new Color(0,0,0,0);
        itemSlot = null;
    }
    
    public void SetItem(Image itemImage)
    {
        itemSlot = itemImage;
        itemSlot.color = new Color(0,0,0,255);
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
