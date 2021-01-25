using Simulation.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gamePaused;
    private static GameManager _instance;

    [SerializeField] private EventHandler eventHandler;
    
    //Cursors
    [SerializeField] private Texture2D defaultCursor;

    // Audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mouseClick;
    [SerializeField] private AudioClip mouseDrop;
    
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
        
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.clip = mouseClick;
            audioSource.Play();
        }
    }

    /**
     * Will stop time and set a boolean to let other scripts know.
     */
    public static void PauseGame()
    {
        if (GameIsPaused()) return;
        
        // Do stuff
        EventHandler.onGamePaused.Invoke();
        _instance._gamePaused = true;

        // This needs to come last
        Time.timeScale = 0f;
    }

    /**
     * Will resume time and set a boolean to let other scripts know.
     */
    public static void UnpauseGame()
    {
        if (!GameIsPaused()) return;
        
        // This needs to come first
        Time.timeScale = 1f;
        
        // Do stuff
        EventHandler.onGameUnpaused.Invoke();
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
}
