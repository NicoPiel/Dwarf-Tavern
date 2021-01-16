using System;
using System.Collections;
using System.Collections.Generic;
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
                level1.transform.Find("Button").GetComponent<Button>().enabled = false;
                level3.transform.Find("Button").GetComponent<Button>().enabled = false;
                level2.transform.Find("Price").GetComponent<TMP_Text>().text = ""+priceLevel2;
                level3.transform.Find("Price").GetComponent<TMP_Text>().text = "Gesperrt";
                break;
            case 2:
                level3.transform.Find("Button").GetComponent<Button>().enabled = true;
                level2.transform.Find("Button").GetComponent<Button>().enabled = false;
                level1.transform.Find("Button").GetComponent<Button>().enabled = false;
                level2.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                level3.transform.Find("Price").GetComponent<TMP_Text>().text = ""+priceLevel3;
                
                break;
            case 3:
                level3.transform.Find("Button").GetComponent<Button>().enabled = false;
                level2.transform.Find("Button").GetComponent<Button>().enabled = false;
                level1.transform.Find("Button").GetComponent<Button>().enabled = false;
                level2.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                level3.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                break;
        }
        
    }

    public void Upgrade(int button)
    {
        Debug.Log($"{button} pressed");
        if (button == 1)
        {
            //TODO Money Check
        }
        
        
        if (_upgradeStateHolder.GetUpgradeState(id) < 3)
        {
            _upgradeStateHolder.Upgrade(id);
            Setup();
        }

    }
  
}
