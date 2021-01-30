using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Simulation.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gamePaused;
    private static GameManager _instance;
    private static Dictionary<string, string> translations;
    private static Dictionary<string, string[]> places;
    
    private static readonly string PathToTranslationJson = Application.streamingAssetsPath + "/JSON/translations.json";
    private static readonly string PathToPlacesJson = Application.streamingAssetsPath + "/JSON/places.json";

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
        
        translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(PathToTranslationJson));
        places = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(PathToPlacesJson));
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

    public static string GetTranslation(string word)
    {
        if (translations.TryGetValue(word, out var output))
        {
            return output;
        }

        throw new UnityException($"No translation found for: {word}");
    }

    public static string GetRandomPlace(string category)
    {
        if (places.TryGetValue(category, out var place))
        {
            return place[Random.Range(0, place.Length)];
        }

        throw new UnityException($"No category \"{category}\" was found.");
    }

    public static GameManager GetGameManager()
    {
        return _instance;
    }
}
