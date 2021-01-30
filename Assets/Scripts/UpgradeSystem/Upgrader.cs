using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrader : MonoBehaviour
{
    private UpgradeStateHolder _upgradeStateHolder;
    public string id;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public int priceLevel2;
    public int priceLevel3;
   
    private void OnEnable()
    {
        _upgradeStateHolder = UpgradeStateHolder.GetInstance();
        Setup();
    }
    private void Setup()
    {
        Debug.Log("Setting Up tables:");
        int state = _upgradeStateHolder.GetUpgradeState(id);
        switch (state)
        {
            case 1:
                level1.GetComponent<Button>().enabled = false;
                level2.GetComponent<Button>().enabled = true;
                level3.GetComponent<Button>().enabled = false;
                
                level1.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                level2.transform.Find("Price").GetComponent<TMP_Text>().text = ""+priceLevel2;
                level3.transform.Find("Price").GetComponent<TMP_Text>().text = "Gesperrt";
                
                
                level1.transform.Find("Image").GetComponent<Image>().enabled = false;
                level2.transform.Find("Image").GetComponent<Image>().enabled = true;
                level3.transform.Find("Image").GetComponent<Image>().enabled = false;
                
                
                break;
            case 2:
                level1.GetComponent<Button>().enabled = false;
                level2.GetComponent<Button>().enabled = false;
                level3.GetComponent<Button>().enabled = true;
                
                level1.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                level2.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                level3.transform.Find("Price").GetComponent<TMP_Text>().text = ""+priceLevel3;
                
                level1.transform.Find("Image").GetComponent<Image>().enabled = false;
                level2.transform.Find("Image").GetComponent<Image>().enabled = false;
                level3.transform.Find("Image").GetComponent<Image>().enabled = true;
                
                break;
            case 3:
                level1.GetComponent<Button>().enabled = false;
                level2.GetComponent<Button>().enabled = false;
                level3.GetComponent<Button>().enabled = false;
                
                level1.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                level2.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                level3.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                
                level1.transform.Find("Image").GetComponent<Image>().enabled = false;
                level2.transform.Find("Image").GetComponent<Image>().enabled = false;
                level3.transform.Find("Image").GetComponent<Image>().enabled = false;
                break;
        }
        
    }

    public void Upgrade(int button)
    {
        switch (button)
        {
            case 1 when !InventoryManager.GetInstance().GetPlayerInventory().TryCharge(priceLevel2, false):
            case 2 when !InventoryManager.GetInstance().GetPlayerInventory().TryCharge(priceLevel3, false):
                return;
        }


        if (_upgradeStateHolder.GetUpgradeState(id) >= 3) return;
        _upgradeStateHolder.Upgrade(id);
        Setup();
    }
  
}
