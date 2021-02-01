using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Expeditions.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Event = Expeditions.Events.Event;

public class CurrentEventUI : MonoBehaviour
{
    public GameObject _choice1;
    public GameObject _choice2;
    public GameObject _choice3;

    public TMP_Text description;

    private Expeditions.Events.Event curretevents;

    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateUI();
        EventHandler.onTriggerExpeditionEvent.AddListener((events) => UpdateUI());
    }

    // Update is called once per frame
    private void UpdateUI()
    {
        if (ExpeditionHolder.GetInstance().GetSelectedExpedition() != null)
        {
            if (ExpeditionHolder.GetInstance().GetSelectedExpedition().IsStarted())
            {
                if (ExpeditionHolder.GetInstance().GetCurrentEvent() != null)
                {
                    curretevents = ExpeditionHolder.GetInstance().GetCurrentEvent();
                    gameObject.transform.localScale = Vector3.one;

                    description.text = curretevents.description;
                    
                    SetupButtons(_choice1, 0);
                    SetupButtons(_choice2, 1);
                    SetupButtons(_choice3, 2);
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
        else
        {
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    private void SetupButtons(GameObject button, int choice)
    {
        button.transform.Find("Choice").GetComponent<TMP_Text>().text = curretevents.choices[choice].text;

        string text = "";
        string br = "\n";
        float bonus = 0;
        if (curretevents.choices[choice].requirementType == Event.EventChoice.RequirementType.Hard)
        {
            text += $"Benötigt: {curretevents.choices[choice].requiredClass}" + br + br;
            if (ExpeditionHolder.GetInstance().GetSelectedExpedition().GetTeam()
                .ContainsRole(curretevents.choices[choice].requiredClass))
            {
                button.GetComponent<Button>().enabled = false;
            }
            else
            {
                button.GetComponent<Button>().enabled = true;
            }
        }
        else if (curretevents.choices[choice].requirementType == Event.EventChoice.RequirementType.Soft)
        {
            button.GetComponent<Button>().enabled = true;
            text +=
                $"Unterstützend: {curretevents.choices[choice].requiredClass} um {curretevents.choices[choice].reqBonus * 100}%" +
                br + br;
            bonus = curretevents.choices[choice].reqBonus;
        }
        else
        {
            button.GetComponent<Button>().enabled = true;
        }

        text += $"Chance auf Erfolg: {(curretevents.choices[choice].chance + bonus) * 100}";


        button.transform.Find("Headline").GetComponent<TMP_Text>().text = text;
    }

    public void OnChoiceButtonPressed(int i)
    {
        EventPoolManager.Instance.HandleChoice(i);
    }
}