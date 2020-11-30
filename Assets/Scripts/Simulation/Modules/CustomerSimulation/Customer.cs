using System.Collections;
using Simulation.Exceptions;
using UnityEngine;

namespace Simulation.Modules.CustomerSimulation
{
    /**
     * Customer class with state-machine-like behaviour
     */
    public class Customer : MonoBehaviour
    {
        public string Name { get; set; }
        private bool _blocked;

        private State _currentState;

        /**
         * State enum
         */
        private enum State : int
        {
            InQueue,
            MovingToTable,
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
            _currentState = State.InQueue;
            Name = CustomerSimulation.GetRandomName();

            Debug.Log(Name + " klopft an.");
        }

        public void UpdateState()
        {
            StartCoroutine(StateMachine());
        }

        private IEnumerator StateMachine()
        {
            if (_blocked) yield return new WaitUntil(() => !_blocked);

            Block();
            
            switch (_currentState)
            {
                case State.InQueue:
                    StartCoroutine(Arrive());
                    break;
                case State.MovingToTable:
                    StartCoroutine(MoveToTable());
                    break;
                case State.Ordering:
                    break;
                case State.Waiting:
                    break;
                case State.Consuming:
                    break;
                case State.Paying:
                    break;
                case State.Leaving:
                    break;
                case State.Idle:
                    break;
                default:
                    throw new StateMachineException($"State Machine of {Name} is broken and/or in unrecognisable state.");
            }
        }

        private IEnumerator Arrive()
        {
            yield return new WaitForSeconds(3f);
            Unblock();
        }

        private IEnumerator MoveToTable()
        {
            yield return new WaitForSeconds(3f);
            Unblock();
        }

        private void Block()
        {
            this._blocked = true;
        }

        private void Unblock()
        {
            this._blocked = false;
        }
    }
}
