using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrader : MonoBehaviour
{
    private UpgradeStateHolder _upgradeStateHolder;
    public GameObject Table_LvL1;
    public GameObject Table_LvL2;
    public GameObject Table_LvL3;
    public GameObject Chair_LvL1;
    public GameObject Chair_LvL2;
    public GameObject Chair_LvL3;
   
    private void OnEnable()
    {
        _upgradeStateHolder = UpgradeStateHolder.GetInstance();
        SetupTables();
    }
    private void SetupTables()
    {
        Debug.Log("Setting Up tables:");
        int state = _upgradeStateHolder.GetUpgradeState("table");
        switch (state)
        {
            case 1:
                Table_LvL1.transform.Find("Button").GetComponent<Button>().enabled = false;
                Table_LvL3.transform.Find("Button").GetComponent<Button>().enabled = false;
                Table_LvL2.transform.Find("Price").GetComponent<TMP_Text>().text = "5000";
                Table_LvL3.transform.Find("Price").GetComponent<TMP_Text>().text = "10000";
                break;
            case 2:
                Table_LvL3.transform.Find("Button").GetComponent<Button>().enabled = true;
                Table_LvL2.transform.Find("Button").GetComponent<Button>().enabled = false;
                Table_LvL1.transform.Find("Button").GetComponent<Button>().enabled = false;
                Table_LvL2.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                Table_LvL3.transform.Find("Price").GetComponent<TMP_Text>().text = "10000";
                
                break;
            case 3:
                Table_LvL3.transform.Find("Button").GetComponent<Button>().enabled = false;
                Table_LvL2.transform.Find("Button").GetComponent<Button>().enabled = false;
                Table_LvL1.transform.Find("Button").GetComponent<Button>().enabled = false;
                Table_LvL2.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                Table_LvL3.transform.Find("Price").GetComponent<TMP_Text>().text = "Gekauft";
                break;
        }
        
    }

    public void UpgradeTable(int button)
    {
        Debug.Log($"{button} pressed");
        if (button == 1)
        {
            //TODO Money Check
        }
        
        
        if (_upgradeStateHolder.GetUpgradeState("table") < 3)
        {
            _upgradeStateHolder.Upgrade("table");
            SetupTables();
        }

    }
  
}
