using System;
using UnityEngine;

namespace Simulation.Core
{
    public abstract class SimulationModule : MonoBehaviour
    {
        /**
         * This is set to true, if the module has been properly initialized.
         */
        protected bool initialized = false;

        private void Start()
        {
            SimulationManager.onSimulationStart.AddListener(ModuleStart);
        }

        /**
         * This method initializes UnityEvents for the deriving module.
         */
        protected void InitModule()
        {
            SimulationManager.onSimulationStart.AddListener(OnSimulationStart);
            SimulationManager.onSimulationPause.AddListener(OnSimulationPause);
            SimulationManager.onSimulationUnpause.AddListener(OnSimulationUnpause);
            SimulationManager.onSimulationTick.AddListener(OnSimulationTick);

            initialized = true;
        }
        
        /**
         * 
         */
        private void ModuleStart()
        {
            if (!initialized) throw new UnityException("Module was not initialized correctly. Don't forget to use the InitModule() method in your modules.");
        }

        protected abstract void OnSimulationStart();
        protected abstract void OnSimulationPause();
        protected abstract void OnSimulationUnpause();
        protected abstract void OnSimulationTick();
    }
}
