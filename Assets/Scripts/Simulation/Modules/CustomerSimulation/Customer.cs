using UnityEngine;

namespace Simulation.Modules.CustomerSimulation
{
    /**
     * Customer class with state-machine-like behaviour
     */
    public class Customer : MonoBehaviour
    {
        public string Name { get; set; }

        private State currentState;

        /**
         * State enum
         */
        private enum State : int
        {
            InQueue,
            GoingToTable,
            Ordering,
            Waiting,
            Consuming,
            Paying,
            Leaving,
            Idle
        }

        private void Awake()
        {
            Init();
        }

        /**
         * Initialise customer as InQueue
         */
        private void Init()
        {
            currentState = State.InQueue;
            Name = CustomerSimulation.GetRandomName();

            Debug.Log(Name + " klopft an.");
        }
    }
}
