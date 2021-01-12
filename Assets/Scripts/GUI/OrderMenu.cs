using Simulation.Modules.CustomerSimulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderMenu : MonoBehaviour
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
        SetInactive();
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}