using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using Inventory;
using Messages;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventHandler : MonoBehaviour
{

    public static UnityEvent onGameStarted;
    public static UnityEvent onGameLoaded;
    public static UnityEvent onGamePaused;
    public static UnityEvent onGameUnpaused;
    
    public static UnityEvent<Interactable.InteractionEventPayload> onInteraction;
    public static UnityEvent onAfterHourSceneLoaded;
    public static UnityEvent onDayChanged;
    public static UnityEvent<InventoryManager.State> onInventoryManagerInitialized;

    public static UnityEvent onInventoryChanged;
    public static UnityEvent<int> onFundsChangedFrom;
    public static UnityEvent onFundsChanged;

    public static UnityEvent onExpeditionHolderChanged;
    public static UnityEvent onTeamChanged;
    public static UnityEvent onExpeditionStarted;

    public static UnityEvent onItemBeerHolderChanged;
    public static UnityEvent<ItemBeer> onBrewed;

    public static UnityEvent<Expeditions.Events.Event> onTriggerExpeditionEvent;
    public static UnityEvent onExpeditionEventFinished;

    public static UnityEvent<KeyValuePair<LetterMessage, LetterMessage.ResponseOption>> onLetterResponse;

    private void Awake()
    {
        onItemBeerHolderChanged = new UnityEvent();
        onInventoryChanged = new UnityEvent();
        onGameStarted = new UnityEvent();
        onGameLoaded = new UnityEvent();
        onGamePaused = new UnityEvent();
        onGameUnpaused = new UnityEvent();
        onAfterHourSceneLoaded = new UnityEvent();
        onBrewed = new UnityEvent<ItemBeer>();
        onInteraction = new UnityEvent<Interactable.InteractionEventPayload>();
        onInventoryManagerInitialized = new UnityEvent<InventoryManager.State>();
        onFundsChangedFrom = new UnityEvent<int>();
        onFundsChanged = new UnityEvent();
        onDayChanged = new UnityEvent();
        onExpeditionHolderChanged = new UnityEvent();
        onExpeditionStarted = new UnityEvent();
        onTeamChanged = new UnityEvent();
        onTriggerExpeditionEvent = new UnityEvent<Expeditions.Events.Event>();
        onExpeditionEventFinished = new UnityEvent();
        onLetterResponse = new UnityEvent<KeyValuePair<LetterMessage, LetterMessage.ResponseOption>>();
    }
}
