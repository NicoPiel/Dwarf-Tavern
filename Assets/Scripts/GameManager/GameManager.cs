using System.Collections;
using System.Collections.Generic;
using Simulation.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private EventHandler eventHandler;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        _instance = this;
    }

    private void Start()
    {
       
        if (SceneManager.GetActiveScene().name == "Tavern")
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

   
}
