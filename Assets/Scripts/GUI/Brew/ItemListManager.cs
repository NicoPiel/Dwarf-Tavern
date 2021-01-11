using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListManager : MonoBehaviour
{
    public GameObject itemList1;
    public GameObject itemList2;

    public GameObject itemList3;
    // Start is called before the first frame update

    public void OnButtonPressed(int button)
    {
        DisableAll();
        switch (button)
        {
            case 0 :
                itemList1.SetActive(true);
                break;
            case 1:
                itemList2.SetActive(true);
                break;
            case 2:
                itemList3.SetActive(true);
                break;
            default:
                DisableAll();
                break;
        }
    }

    private void DisableAll()
    {
        itemList1.SetActive(false);
        itemList2.SetActive(false);
        itemList3.SetActive(false);
    }
}