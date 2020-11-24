using System.Collections;
using Simulation.Modules;
using UnityEngine;
using UnityEngine.Events;

namespace Simulation.Core
{
    public class SimulationManager : MonoBehaviour
    {
        private static SimulationManager _instance;
        [SerializeField] private CustomerSimulation customerSimulation;

        private UnityEvent onSimulationStart;
        private UnityEvent onSimulationPause;
        private UnityEvent onSimulationTick;

        private bool _paused;

        // Start is called before the first frame update
        private void Awake()
        {
            // Reference static instance
            _instance = this;

            // Setup Events
            onSimulationStart = new UnityEvent();
            onSimulationPause = new UnityEvent();
            onSimulationTick  = new UnityEvent();
        }
    
        private void Start()
        {
            // Subscribe to own events for debugging
            onSimulationStart.AddListener(OnSimulationStart);
            onSimulationPause.AddListener(OnSimulationPause);
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

        private IEnumerator SimulationTick()
        {
            for (;;)
            {
                if (_paused) break;
                
                onSimulationTick.Invoke();
                yield return new WaitForSeconds(1f);
                yield return new WaitForEndOfFrame();
            }
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

        private void OnSimulationTick()
        {
            Debug.Log("Tick.");
        }
        
        public static SimulationManager GetInstance()
        {
            return _instance;
        }

        public CustomerSimulation CustomerSimulation()
        {
            return customerSimulation;
        }
    }
}
