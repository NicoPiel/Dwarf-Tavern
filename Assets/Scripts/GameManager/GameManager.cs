using Simulation.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gamePaused;
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
        if (SceneManager.GetActiveScene().name == "Tavern") SimulationManager.GetInstance().StartSimulation();
    }

    /**
     * Will stop time and set a boolean to let other scripts know.
     */
    public static void PauseGame()
    {
        if (GameIsPaused()) return;
        
        _instance._gamePaused = true;

        Time.timeScale = 0f;
    }

    /**
     * Will resume time and set a boolean to let other scripts know.
     */
    public static void UnpauseGame()
    {
        if (!GameIsPaused()) return;
        
        Time.timeScale = 1f;
        
        _instance._gamePaused = false;
    }

    /**
     * Returns if the game is paused right now.
     */
    public static bool GameIsPaused()
    {
        return _instance._gamePaused;
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
