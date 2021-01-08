using System.Collections;
using Simulation.Modules.CustomerSimulation;
using UnityEngine;
using UnityEngine.Events;

namespace Simulation.Core
{
    public class SimulationManager : SimulationModule
    {
        // Public

        // Private
        private static SimulationManager _instance;
        [SerializeField] private CustomerSimulation customerSimulation;

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
        }
    
        private new void Start()
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
                
                yield return new WaitForEndOfFrame();
                onSimulationTick.Invoke();
                yield return new WaitForSeconds(1f);
                yield return new WaitForEndOfFrame();
            }
        }

        // TODO
        private IEnumerator SimulationPostTick()
        {
            yield return null;
        }

        protected override void OnSimulationStart()
        {
            _paused = false;
            Debug.Log("Simulation started.");
        }

        protected override void OnSimulationPause()
        {
            _paused = true;
            Debug.Log("Simulation paused.");
        }
        
        protected override void OnSimulationUnpause()
        {
            _paused = false;
            Debug.Log("Simulation unpaused.");
        }

        protected override void OnSimulationTick()
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
