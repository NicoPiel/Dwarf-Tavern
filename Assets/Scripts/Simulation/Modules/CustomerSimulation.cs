using Simulation.Core;
using UnityEngine;

namespace Simulation.Modules
{
    public class CustomerSimulation : MonoBehaviour
    {
        private SimulationManager simulationManager;
        
        private void Awake()
        {
            
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            simulationManager = GameManager.GetSimulationManager();
            
            
        }

        // Update is called once per frame
        private void Update()
        {
            
        }
    }
}
