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
    public UnityEvent onDayChanged;
    public UnityEvent<InventoryManager.State> onInventoryManagerInitialized;

    public UnityEvent onInventoryChanged;
    public UnityEvent<int> onFundsChangedFrom;
    public UnityEvent onFundsChanged;

    public UnityEvent onExpeditionHolderChanged;
    public UnityEvent onTeamChanged;

    public UnityEvent onItemBeerHolderChanged;
    public UnityEvent<ItemBeer> onBrewed;
    
    private void Awake()
    {
        onItemBeerHolderChanged = new UnityEvent();
        onInventoryChanged = new UnityEvent();
        onGameStarted = new UnityEvent();
        onGameLoaded = new UnityEvent();
        onAfterHourSceneLoaded = new UnityEvent();
        onBrewed = new UnityEvent<ItemBeer>();
        onInteraction = new UnityEvent<Interactable.InteractionEventPayload>();
        onInventoryManagerInitialized = new UnityEvent<InventoryManager.State>();
        onFundsChangedFrom = new UnityEvent<int>();
        onFundsChanged = new UnityEvent();
        onDayChanged = new UnityEvent();
        onExpeditionHolderChanged = new UnityEvent();
    }
}
