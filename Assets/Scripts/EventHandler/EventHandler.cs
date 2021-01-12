using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using Inventory;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventHandler : MonoBehaviour
{

    public UnityEvent onGameStarted;
    public UnityEvent onGameLoaded;
    public UnityEvent<Interactable.InteractionEventPayload> onInteraction;
    public UnityEvent onAfterHourSceneLoaded;
    public UnityEvent<InventoryManager.State> onInventoryManagerInitialized;

    public UnityEvent onInventoryChanged;
    
    private void Awake()
    {
        onInventoryChanged = new UnityEvent();
        onGameStarted = new UnityEvent();
        onGameLoaded = new UnityEvent();
        onAfterHourSceneLoaded = new UnityEvent();
        onInteraction = new UnityEvent<Interactable.InteractionEventPayload>();
        onInventoryManagerInitialized = new UnityEvent<InventoryManager.State>();
    }
}
