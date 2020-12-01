using Simulation.Core;
using UnityEngine;

namespace Simulation.Modules.CustomerSimulation
{
    public class CustomerPlace : MonoBehaviour
    {
        public bool occupied = false;
        public Customer occupiedBy;
        
        // Start is called before the first frame update
        private void Start()
        {
            SimulationManager.onSimulationStart.AddListener(OnSimulationStart);
        }

        private void OnSimulationStart()
        {
            occupied = false;
            occupiedBy = null;
        }
    }
}
