using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private InventoryManager inventoryManager;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

    public static GameManager GetGameManager()
    {
        return _instance;
    }

    public static EventHandler GetEventHandler()
    {
        return _instance.eventHandler;
    }
}
