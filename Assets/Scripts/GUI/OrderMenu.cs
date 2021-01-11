using TMPro;
using UnityEngine;


public class OrderMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text orderNameText;
    [SerializeField] private TMP_Text orderDescriptionText;

    public void SetOrder(string orderName, string orderDescription)
    {
        orderNameText.text = orderName;
        orderDescriptionText.text = orderDescription;
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