using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MercenaryUI : MonoBehaviour
{
    private Mercenary _mercenary;
    public TMP_Text role;
    public TMP_Text level;
    public TMP_Text hp;
    public TMP_Text price;

    private void OnEnable()
    {
        _mercenary = new Mercenary(5);
        role.text = $"{_mercenary.GetRole().ToString()}";
        level.text = $"Level: {_mercenary.GetLevel().ToString()}";
        hp.text = $"HP: {_mercenary.GetMaxHealthPoints().ToString()}";
        price.text = _mercenary.GetPrice().ToString();
    }
}
