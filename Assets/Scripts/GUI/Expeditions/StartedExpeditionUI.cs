using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartedExpeditionUI : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        UpdateUI();
        GameManager.GetEventHandler().onExpeditionStarted.AddListener(UpdateUI);
    }

    // Update is called once per frame
    private void UpdateUI()
    {
        if (ExpeditionHolder.GetInstance().IsExpeditionSelected())
        {
            if (ExpeditionHolder.GetInstance().GetSelectedExpedition().IsStarted())
            {
                gameObject.transform.localScale = Vector3.one;
            }
            else
            {
                gameObject.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            gameObject.transform.localScale = Vector3.zero;
        }
    }
}
