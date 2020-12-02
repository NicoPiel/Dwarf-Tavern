using System.Collections;
using Simulation.Exceptions;
using UnityEngine;
using Pathfinding;

namespace Simulation.Modules.CustomerSimulation
{
    /**
     * Customer class with state-machine-like behaviour
     */
    public class Customer : MonoBehaviour
    {
        public string Name;
        public CustomerPlace assignedPlace;

        private Seeker seeker;
        private AIPath pathfinder;
        private bool _blocked;
        private State _currentState;

        /**
         * State enum
         */
        private enum State : int
        {
            InQueue,
            MovingToTable,
            ArrivedAtTable,
            Ordering,
            Waiting,
            Consuming,
            Paying,
            Leaving,
            Idle
        }

        private void Awake()
        {
            seeker = GetComponent<Seeker>();
            pathfinder = GetComponent<AIPath>();
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

        /**
         * Manually update a customer's state machine; you do not need to call this method often, as the state machine is designed to update itself.
         */
        public void UpdateState()
        {
            StartCoroutine(StateMachine());
        }


        private IEnumerator StateMachine()
        {
            if (_blocked) yield return new WaitUntil(() => !_blocked);

            Block();

            Debug.Log($"{Name}: {_currentState}");

            switch (_currentState)
            {
                case State.InQueue:
                    StartCoroutine(Arrive());
                    break;
                case State.MovingToTable:
                    StartCoroutine(MoveToTable());
                    break;
                case State.ArrivedAtTable:
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
            yield return new WaitForSeconds(2f);
            _currentState = State.MovingToTable;
            Unblock();
        }

        /**
         * Utilises Pathfinding to move the customer to their assigned place.
         */
        private IEnumerator MoveToTable()
        {
            if (assignedPlace == null) yield return null;

            Vector3 target = assignedPlace.transform.position;

            pathfinder.destination = target;
            pathfinder.SearchPath();

            while (!pathfinder.reachedDestination)
            {
                yield return null;
            }
            
            ArriveAtTable();
        }

        //TODO: Implement state changing

        /**
         * Extra state if the customer has just arrived at their assigned table; call if you need to use delegates.
         */
        private void ArriveAtTable()
        {
            _currentState = State.ArrivedAtTable;
            StartCoroutine(Idle());
        }

        /**
         * Forces the customer to do nothing.
         */
        private IEnumerator Idle()
        {
            if (_currentState != State.ArrivedAtTable) _currentState = State.Idle;

            yield return new WaitForSeconds(3f);
            Unblock();
        }

        /**
         * Blocks the state machine from changing states.
         */
        private void Block()
        {
            this._blocked = true;
        }

        /**
         * Unblocks the state machine.
         */
        private void Unblock()
        {
            this._blocked = false;
        }

        /**
         * Unblocks the state machine. To be called from the Seeker component.
         */
        private void Unblock(Path p)
        {
            this._blocked = false;
            Debug.Log($"{Name} has arrived at their table.");
        }

        public void Assign(CustomerPlace place)
        {
            assignedPlace = place;
        }

        public void Unassign()
        {
            assignedPlace = null;
        }
    }
}