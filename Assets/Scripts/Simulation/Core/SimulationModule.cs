using UnityEngine;

namespace Simulation.Core
{
    public abstract class SimulationModule : MonoBehaviour
    {
        private bool initialized = false;

        // TODO: Force derived classes to call this method
        protected virtual void InitModule()
        {
            SimulationManager.onSimulationStart.AddListener(OnSimulationStart);
            SimulationManager.onSimulationPause.AddListener(OnSimulationPause);
            SimulationManager.onSimulationUnpause.AddListener(OnSimulationUnpause);
            SimulationManager.onSimulationTick.AddListener(OnSimulationTick);

            initialized = true;
        }

        protected abstract void OnSimulationStart();
        protected abstract void OnSimulationPause();
        protected abstract void OnSimulationUnpause();
        protected abstract void OnSimulationTick();
    }
}
