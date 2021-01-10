using System;
using System.Collections;
using System.ComponentModel;
using Interactions;
using Simulation.Exceptions;
using UnityEngine;
using Pathfinding;
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
        public CustomerPlace assignedPlace;
        public Tooltip tooltip;
        
        private Seeker seeker;
        private AIPath pathfinder;
        [SerializeField] private bool _blocked;
        [SerializeField] private State _currentState;
        [SerializeField] private bool _isInteractedWith;
        [SerializeField] private string _currentOrder;

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
                Debug.LogWarning($"{Name}: Blocked");
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
            
            _currentOrder = RandomOrder();
            Tooltip.ShowTooltip_Static(tooltip, _currentOrder);
            yield return null;
        }

        private IEnumerator Wait()
        {
            yield return new WaitUntil(() => _isInteractedWith);
            Tooltip.HideTooltip_Static(tooltip);
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

        // TODO: Create actual orders
        private string RandomOrder()
        {
            var beverages = CustomerSimulation.Orders["beverages"];
            var tastes = CustomerSimulation.Orders["tastes"];

            var beverageString = beverages[Random.Range(0, beverages.Length)].Split(' ');
            var article = beverageString[0];
            var beverage = beverageString[1];
            
            var taste = tastes[Random.Range(0, tastes.Length)];

            switch (article)
            {
                case "das":
                    taste += "es";
                    break;
                case "der":
                    taste += "er";
                    break;
                case "die":
                    taste += "e";
                    break;
                default:
                    throw new UnityException($"Wrong format in orders.json with {beverage}");
            }

            return $"{taste} {beverage}";
        }

        protected override void OnInteract(GameObject source)
        {
            _isInteractedWith = true;
        }
    }
}