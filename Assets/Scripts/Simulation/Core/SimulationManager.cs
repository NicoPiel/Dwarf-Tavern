using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Simulation.Modules.CustomerSimulation;
using UnityEngine;
using UnityEngine.Events;

namespace Simulation.Core
{
    public class SimulationManager : MonoBehaviour
    {
        // Public
        public static Dictionary<string, string[]> Names { get; set; }
        public static Dictionary<string, string[]> Orders { get; set; }
        public static List<string> Attributes { get; set; }
        public static Dictionary<string, string[]> AttributeCombinations { get; set; }

        // Private
        private static SimulationManager _instance;
        [SerializeField] private CustomerSimulation customerSimulation;
        private static readonly string PathToNameJson = Application.streamingAssetsPath + "/JSON/names.json";
        private static readonly string PathToOrdersJson = Application.streamingAssetsPath + "/JSON/orders.json";
        private static readonly string PathToAttributesJson = Application.streamingAssetsPath + "/JSON/attributes.json";

        // Events
        public static UnityEvent onSimulationStart;
        public static UnityEvent onSimulationPause;
        public static UnityEvent onSimulationUnpause;
        public static UnityEvent onSimulationTick;

        private bool _paused;

        // Start is called before the first frame update
        private void Awake()
        {
            // Reference static instance
            _instance = this;

            // Setup Events
            onSimulationStart = new UnityEvent();
            onSimulationPause = new UnityEvent();
            onSimulationUnpause = new UnityEvent();
            onSimulationTick  = new UnityEvent();
            
            // JSON files
            Names = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(PathToNameJson));
            Orders = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(PathToOrdersJson));
            AttributeCombinations = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(PathToAttributesJson));
            Attributes = new List<string>();

            foreach (var key in AttributeCombinations.Keys)
            {
                Attributes.Add(key);
            }
        }
    
        private void Start()
        {
            // Subscribe to own events for debugging
            onSimulationStart.AddListener(OnSimulationStart);
            onSimulationPause.AddListener(OnSimulationPause);
            onSimulationUnpause.AddListener(OnSimulationUnpause);
            onSimulationTick.AddListener(OnSimulationTick);
        }

        public void StartSimulation()
        {
            onSimulationStart.Invoke();
            StartCoroutine(SimulationTick());
        }

        public void PauseSimulation()
        {
            onSimulationPause.Invoke();
            StopCoroutine(SimulationTick());
        }
        
        public void UnpauseSimulation()
        {
            onSimulationUnpause.Invoke();
            StartCoroutine(SimulationTick());
        }

        // TODO
        private IEnumerator SimulationPreTick()
        {
            yield return null;
        }

        private IEnumerator SimulationTick()
        {
            for (;;)
            {
                if (_paused) break;
                
                onSimulationTick.Invoke();
                yield return new WaitForSeconds(1f);
            }
        }

        // TODO
        private IEnumerator SimulationPostTick()
        {
            yield return null;
        }

        private void OnSimulationStart()
        {
            _paused = false;
            Debug.Log("Simulation started.");
        }

        private void OnSimulationPause()
        {
            _paused = true;
            Debug.Log("Simulation paused.");
        }
        
        private void OnSimulationUnpause()
        {
            _paused = false;
            Debug.Log("Simulation unpaused.");
        }

        private void OnSimulationTick()
        {
            Debug.Log("Tick.");
        }
        
        public static SimulationManager GetInstance()
        {
            return _instance;
        }

        public static CustomerSimulation CustomerSimulation()
        {
            return _instance.customerSimulation;
        }
    }
}
