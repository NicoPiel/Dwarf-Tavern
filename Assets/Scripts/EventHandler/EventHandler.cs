using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventHandler : MonoBehaviour
{

    public UnityEvent onGameStarted;
    public UnityEvent onGameLoaded;
    public UnityEvent<Interactable.InteractionEventPayload> onInteraction;
    public UnityEvent onAfterHourSceneLoaded;
    
    private void Awake()
    {
        onGameStarted = new UnityEvent();
        onGameLoaded = new UnityEvent();
        onAfterHourSceneLoaded = new UnityEvent();
        onInteraction = new UnityEvent<Interactable.InteractionEventPayload>();
    }
}
