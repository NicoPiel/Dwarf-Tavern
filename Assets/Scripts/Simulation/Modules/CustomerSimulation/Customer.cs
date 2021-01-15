using System;
using System.Collections;
using Interactions;
using Pathfinding;
using Simulation.Core;
using Simulation.Exceptions;
using TMPro;
using UnityEngine;
using Utility.Tooltip;
using Random = UnityEngine.Random;

namespace Simulation.Modules.CustomerSimulation
{
    /**
     * Customer class with state-machine-like behaviour
     */
    public class Customer : PlayerInteractable
    {
        public string Name;
        public TMP_Text namePlate;
        public CustomerPlace assignedPlace;
        public Tooltip tooltip;
        public float patience;
        
        private Seeker _seeker;
        private AIPath _pathfinder;
        
        [SerializeField] private bool _blocked;
        [SerializeField] private State _currentState;
        [SerializeField] private bool _canBeInteractedWith = false;
        [SerializeField] private bool _isInteractedWith = false;
        
        private Order _currentOrder;
        private bool _hadOrderTaken;
        private bool _hasBeenServed;
        private bool _wantsToLeave;
        
        /**
         * State enum
         */
        public enum State : int
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
            _seeker = GetComponent<Seeker>();
            _pathfinder = GetComponent<AIPath>();
            Init();
        }

        /**
         * Initialise customer as InQueue
         */
        private void Init()
        {
            _currentState = State.InQueue;
            Name = CustomerSimulation.GetRandomName();
            namePlate.text = Name;

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
            if (_blocked)
            {
                // Debug.LogWarning($"{Name}: Blocked");
                yield return new WaitUntil(() => !_blocked);
            }

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
                    StartCoroutine(Idle());
                    _currentState = State.Ordering;
                    break;
                case State.Ordering:
                    StartCoroutine(Order());
                    break;
                case State.Waiting:
                    StartCoroutine(Wait());
                    break;
                case State.Consuming:
                    StartCoroutine(Consume());
                    break;
                case State.Paying:
                    break;
                case State.Leaving:
                    StartCoroutine(Leave());
                    break;
                case State.Idle:
                    break;
                default:
                    throw new StateMachineException($"State Machine of {Name} is broken and/or in unrecognisable state.");
            }
        }

        private IEnumerator Arrive()
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
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

            _pathfinder.destination = target;
            _pathfinder.SearchPath();

            yield return new WaitUntil(() => _pathfinder.reachedDestination);
            
            ArriveAtTable();
        }

        /**
         * Extra state if the customer has just arrived at their assigned table; call if you need to use delegates.
         */
        private void ArriveAtTable()
        {
            _currentState = State.ArrivedAtTable;
            Unblock();
        }

        /**
         * Forces the customer to do nothing.
         */
        private IEnumerator Idle()
        {
            if (_currentState != State.ArrivedAtTable) _currentState = State.Idle;

            yield return new WaitForSeconds(Random.Range(0.5f, 4f));
            Unblock();
        }

        private IEnumerator Order()
        {
            _currentState = State.Waiting;

            _currentOrder = new Order(this);
            Tooltip.ShowTooltip_Static(tooltip, _currentOrder.Name);
            _canBeInteractedWith = true;

            Unblock();

            yield return null;
        }

        /**
         * The customer waits to have his order taken or to get served. Times are hardcoded.
         */
        private IEnumerator Wait()
        {
            StartCoroutine(CountdownPatience());

            // Wait for a certain period of time, if customer hasn't had his order taken, otherwise wait to get served.
            if (!_hadOrderTaken)
            {
                patience = 15f;
                yield return new WaitUntil(() => _isInteractedWith || patience == 0);
                _isInteractedWith = false;
                _hadOrderTaken = true;
            }
            else
            {
                patience = 90f;
                yield return new WaitUntil(() => _isInteractedWith || patience == 0);
                _isInteractedWith = false;
                _hasBeenServed = true;
                _currentState = State.Consuming;
            }

            // Leave if patience reaches 0 while waiting.
            if (patience == 0)
            {
                _wantsToLeave = true;
                _currentState = State.Leaving;
            }
            
            Tooltip.HideTooltip_Static(tooltip);
            Unblock();
        }

        /**
         * The customers consumes his order.
         */
        private IEnumerator Consume()
        {
            if (!_hasBeenServed) throw new StateException(State.Consuming, "Customer has not been served yet. _hasBeenServed has to be true.");
            
            Tooltip.ShowTooltip_Static(tooltip, "Nom nom nom...");
            yield return new WaitForSeconds(20f);
        }

        private IEnumerator Leave()
        {
            if (!_wantsToLeave) throw new StateException(State.Leaving, "Customer does not want to leave. _wantsToLeave has to be true.");
            
            Vector3 target = GameObject.FindWithTag("Spawner").transform.position;

            _pathfinder.destination = target;
            _pathfinder.SearchPath();

            yield return new WaitUntil(() => _pathfinder.reachedDestination);

            Unblock();
            Destroy(gameObject);
        }

        /**
         * Blocks the state machine from changing states.
         */
        private void Block(bool blocked = true)
        {
            _blocked = blocked;
        }

        /**
         * Unblocks the state machine.
         */
        private void Unblock()
        {
            _blocked = false;
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

        protected override void OnInteract(GameObject source)
        {
            if (!_canBeInteractedWith || _isInteractedWith) return;

            CustomerSimulation customerSimulation = SimulationManager.GetCustomerSimulation();
            
            _isInteractedWith = true;
            customerSimulation.SetOrderOnMenu(_currentOrder);
            customerSimulation.ShowOrderMenu();
        }

        private IEnumerator CountdownPatience()
        {
            while (!_isInteractedWith)
            {
                yield return new WaitForSeconds(1.0f);
                patience -= 1;
            }
        }
    }

    public class StateException : UnityException
    {
        public StateException() : base()
        {
            Debug.LogException(this);
        }
        
        public StateException(Customer.State state)
        {
            Debug.LogError($"Illegal state: {state}.");
            Debug.LogException(this);
        }
        
        public StateException(string message) : base(message)
        {
            Debug.LogError(message);
            Debug.LogException(this);
        }

        public StateException(Customer.State state, string message) : base(message)
        {
            Debug.LogError(message);
            Debug.LogException(this);
        }

        public StateException(string message, Exception inner) : base(message, inner)
        {
            Debug.LogError(message);
            Debug.LogException(this);
            Debug.LogException(inner);
        }
    }
}