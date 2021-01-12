using System.Collections;
using Interactions;
using Pathfinding;
using Simulation.Core;
using Simulation.Exceptions;
using UnityEngine;
using Utility.Tooltip;

namespace Simulation.Modules.CustomerSimulation
{
    /**
     * Customer class with state-machine-like behaviour
     */
    public class Customer : PlayerInteractable
    {
        public string Name;
        public CustomerPlace assignedPlace;
        public Tooltip tooltip;
        public float patience;
        
        private Seeker seeker;
        private AIPath pathfinder;
        
        [SerializeField] private bool _blocked;
        [SerializeField] private State _currentState;
        [SerializeField] private bool _canBeInteractedWith = false;
        [SerializeField] private bool _isInteractedWith = false;
        
        private Order _currentOrder;

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

            pathfinder.destination = target;
            pathfinder.SearchPath();

            yield return new WaitUntil(() => pathfinder.reachedDestination);
            
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

            _currentOrder = new Order();
            Tooltip.ShowTooltip_Static(tooltip, _currentOrder.Name);
            _canBeInteractedWith = true;

            Unblock();

            yield return null;
        }

        private IEnumerator Wait()
        {
            StartCoroutine(CountdownPatience());
            yield return new WaitUntil(() => _isInteractedWith || patience == 0);

            if (patience == 0)
            {
                _currentState = State.Leaving;
            }
            
            Tooltip.HideTooltip_Static(tooltip);
            Unblock();
        }

        private IEnumerator Leave()
        {
            Vector3 target = GameObject.FindWithTag("Spawner").transform.position;

            pathfinder.destination = target;
            pathfinder.SearchPath();

            yield return new WaitUntil(() => pathfinder.reachedDestination);

            Unblock();
            Destroy(gameObject);
        }

        /**
         * Blocks the state machine from changing states.
         */
        private void Block(bool blocked = true)
        {
            this._blocked = blocked;
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

        protected override void OnInteract(GameObject source)
        {
            if (!_canBeInteractedWith || _isInteractedWith) return;

            CustomerSimulation customerSimulation = SimulationManager.CustomerSimulation();
            
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
}