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
    public GameObject LvL1;
    public GameObject LvL2;
    public GameObject LvL3;

    public int price_lvl2;
    public int price_lvl3;
   
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
                LvL1.transform.Find("Button").GetComponent<Button>().enabled = false;
                LvL3.transform.Find("Button").GetComponent<Button>().enabled = false;
                LvL2.transform.Find("Price").GetComponent<TMP_Text>().text = ""+price_lvl2;
                LvL3.transform.Find("Price").GetComponent<TMP_Text>().text = ""+price_lvl3;
                break;
            case 2:
                LvL3.transform.Find("Button").GetComponent<Button>().enabled = true;
                LvL2.transform.Find("Button").GetComponent<Button>().enabled = false;
                LvL1.transform.Find("Button").GetComponent<Button>().enabled = false;
                LvL2.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                LvL3.transform.Find("Price").GetComponent<TMP_Text>().text = ""+price_lvl3;
                
                break;
            case 3:
                LvL3.transform.Find("Button").GetComponent<Button>().enabled = false;
                LvL2.transform.Find("Button").GetComponent<Button>().enabled = false;
                LvL1.transform.Find("Button").GetComponent<Button>().enabled = false;
                LvL2.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                LvL3.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
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
