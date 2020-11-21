using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventHandler : MonoBehaviour
{

    public UnityEvent onGameStarted;
    public UnityEvent onGameLoaded;
    public UnityEvent<Interactable> onPlayerInteract;
    public UnityEvent<InventoryManager.State> onInventoryManagerInitialized;
    
    private void Awake()
    {
        onGameStarted = new UnityEvent();
        onGameLoaded = new UnityEvent();
        onPlayerInteract = new UnityEvent<Interactable>();
        onInventoryManagerInitialized = new UnityEvent<InventoryManager.State>();
    }
}
