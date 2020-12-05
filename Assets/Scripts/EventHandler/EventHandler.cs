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
    public UnityEvent<InteractionEventPayload> onPlayerInteract;
    
    private void Awake()
    {
        onGameStarted = new UnityEvent();
        onGameLoaded = new UnityEvent();
        onPlayerInteract = new UnityEvent<InteractionEventPayload>();
    }
}
