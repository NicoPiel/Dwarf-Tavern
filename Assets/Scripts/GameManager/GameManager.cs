using System.Collections;
using System.Collections.Generic;
using Simulation.Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private SimulationManager simulationManager;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

    private void Start()
    {
        SimulationManager.GetInstance().StartSimulation();
    }

    public static GameManager GetGameManager()
    {
        return _instance;
    }

    public static EventHandler GetEventHandler()
    {
        return _instance.eventHandler;
    }

    public static SimulationManager GetSimulationManager()
    {
        return _instance.simulationManager;
    }
}
