using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ExpeditionUI : MonoBehaviour
{
    public int expeditionSlot;

    public TMP_Text tmpName;
    public TMP_Text length;
    public Image icon;

    public Sprite jungle;
    public Sprite cave;
    public Sprite forrest;

    public void OnEnable()
    {
        UpdateUI();
        EventHandler.onExpeditionHolderChanged.AddListener(UpdateUI);
    }

    private void UpdateUI()
    {
        if (ExpeditionHolder.GetInstance().IsExpeditionSelected())
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        
        
        
        var expedition = ExpeditionHolder.GetInstance().GetExpedition(expeditionSlot);
        if (expedition != null)
        {
            gameObject.transform.localScale = Vector3.one;
            tmpName.text = expedition.GetName();
            length.text = $"Eventanzahl: {expedition.GetLength().ToString()}";
            Debug.Log("Type should be: " + expedition.GetThemeType());
            if (expedition.GetThemeType() == Expedition.ThemeType.Cave)
            {
                icon.sprite = cave;
            }else if (expedition.GetThemeType() == Expedition.ThemeType.Jungle)
            {
                icon.sprite = jungle;
            }else if (expedition.GetThemeType() == Expedition.ThemeType.Forest)
            {
                icon.sprite = forrest;
            }
        }
        else
        {
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    public void ButtonClick()
    {
        ExpeditionHolder.GetInstance().SelectExpedition(expeditionSlot);
    }
}
