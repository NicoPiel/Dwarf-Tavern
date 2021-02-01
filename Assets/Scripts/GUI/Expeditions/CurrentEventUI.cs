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
    public GameObject choiceUI;
    public GameObject OutComeUI;
    
    public GameObject _choice1;
    public GameObject _choice2;
    public GameObject _choice3;

    public TMP_Text description;

    private Expeditions.Events.Event curretevents;

    private bool _showFinalText;
    private string _finalText;

    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateUI();
        EventHandler.onTriggerExpeditionEvent.AddListener((events) =>
        {
            _showFinalText = false;
            UpdateUI();
        });

    }

    // Update is called once per frame
    private void UpdateUI()
    {
        if (ExpeditionHolder.GetInstance().GetSelectedExpedition() == null)
        {
            gameObject.transform.localScale = Vector3.zero;
            return;
        }
        if (!ExpeditionHolder.GetInstance().GetSelectedExpedition().IsStarted())
        {
            gameObject.transform.localScale = Vector3.zero;
            return;
        }

        if (ExpeditionHolder.GetInstance().GetCurrentEvent() == null)
        {
            if (_showFinalText)
            {
                choiceUI.transform.localScale = Vector3.zero;
                OutComeUI.transform.localScale = Vector3.one;
                OutComeUI.GetComponentInChildren<TMP_Text>().text = _finalText;
            }
            else
            {
                gameObject.transform.localScale = Vector3.zero;
            }
            return;
        }
        
        choiceUI.transform.localScale = Vector3.one;
        OutComeUI.transform.localScale = Vector3.zero;
        curretevents = ExpeditionHolder.GetInstance().GetCurrentEvent();
        gameObject.transform.localScale = Vector3.one;

        description.text = curretevents.description;
                    
        SetupButtons(_choice1, 0);
        SetupButtons(_choice2, 1);
        SetupButtons(_choice3, 2);
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
                button.GetComponent<Button>().enabled = true;
            }
            else
            {
                button.GetComponent<Button>().enabled = false;
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
        Debug.Log($"Button {i} pressed");
        if (EventPoolManager.Instance.HandleChoice(i))
        {
            
            _finalText = curretevents.choices[i].positiveOutcome;
        }
        else
        {
            _finalText = curretevents.choices[i].negativeOutcome;
        }
        _showFinalText = true;
        ExpeditionHolder.GetInstance().RemoveCurrentEvent();
        UpdateUI();
    }
}