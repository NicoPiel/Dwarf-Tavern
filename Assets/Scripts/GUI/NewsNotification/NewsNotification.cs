using System;
using System.Collections;
using System.Collections.Generic;
using Messages;
using Simulation.Core;
using UnityEngine;

public class NewsNotification : BaseMenu
{
    public AudioSource audioSource;
    private bool _hasBeenShown = false;

    private void Start()
    {
        SimulationManager.onSimulationStart.AddListener(Init);
        SimulationManager.onSimulationTick.AddListener(ShouldMenuBeShown);
    }

    private void Init()
    {
        _hasBeenShown = false;
    }

    private void ShouldMenuBeShown()
    {
        if (!_hasBeenShown && 
            MessageSystemHandler.Instance.GetMessagesForDay(DayCounter.GetInstance().GetDayCount()).Count > 0 && 
            SimulationManager.GetInstance().timeValue >= SimulationManager.GetInstance().endOfDay/2)
        {
            ShowAndHideMenu();
            _hasBeenShown = true;
        }
    }

    private void ShowAndHideMenu()
    {
        isShown = true;
        audioSource.Play();
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(-200, 150, 0), 0.4f).setEaseInBounce().setOnComplete(() =>
        {
            StartCoroutine(WaitThenHide());
        });
    }

    private IEnumerator WaitThenHide()
    {
        yield return new WaitForSeconds(3f);
        HideMenu();
    }
    
    public override void ShowMenu()
    {
        isShown = true;
        audioSource.Play();
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(-200, 150, 0), 0.4f).setEaseInBounce();
    }

    public override void HideMenu()
    {
        isShown = false;
        
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(230, 150, 0), 0.4f).setEaseOutBounce();
    }
}
