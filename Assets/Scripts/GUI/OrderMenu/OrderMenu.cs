using System.Collections;
using Simulation.Modules.CustomerSimulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderMenu : BaseMenu
{
    private Order orderReference;
    
    [SerializeField] private TMP_Text orderNameText;
    [SerializeField] private TMP_Text orderDescriptionText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button closeButton;
    
    public void SetOrder(Order order)
    {
        orderReference = order;
        orderNameText.text = order.Name;
        orderDescriptionText.text = order.Description;
    }

    public void AcceptOrder()
    {
        orderReference?.Accept();
        HideMenu();
    }

    public void CancelAcceptingOrder()
    {
        orderReference?.onAcceptCancel.Invoke(orderReference);
        HideMenu();
    }

    public override void ShowMenu()
    {
        isShown = true;
        gameObject.SetActive(true);
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(220, 295, 0), 0.4f);
    }

    public override void HideMenu()
    {
        isShown = false;
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(-250, 295, 0), 0.4f).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}