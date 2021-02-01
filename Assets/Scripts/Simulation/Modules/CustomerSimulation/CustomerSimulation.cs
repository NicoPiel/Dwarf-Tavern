using System;
using System.Collections.Generic;
using System.Text;
using Simulation.Core;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Simulation.Modules.CustomerSimulation
{
    public class CustomerSimulation : SimulationModule
    {
        public int CustomerLimit;
        public static List<Order> OpenOrders;
        public OrderMenu orderMenu;

        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private List<Customer> customers;
        [SerializeField] private List<CustomerPlace> customerPlaces;
        [SerializeField] private List<CustomerPlace> unassignedCustomerPlaces;
        [SerializeField] private List<CustomerPlace> assignedCustomerPlaces;

        // Events
        public static OrderEvent onAnyOrderAccept;
        public static OrderEvent onAnyOrderProcess;
        public static UnityEvent onOrderListChange;
        public static CustomerLeaveEvent onCustomerLeave;
        
        private int time;

        private void Awake()
        {
            onAnyOrderAccept = new OrderEvent();
            onAnyOrderProcess = new OrderEvent();
            onOrderListChange = new UnityEvent();
            onCustomerLeave = new CustomerLeaveEvent();
        }

        // Start is called before the first frame update
        private void Start()
        {
            InitModule();
            customers = new List<Customer>();

            customerPlaces = new List<CustomerPlace>();
            foreach (var obj in GameObject.FindGameObjectsWithTag("CustomerPlace"))
            {
                customerPlaces.Add(obj.GetComponent<CustomerPlace>());
            }

            CustomerLimit = customerPlaces.Count;
            unassignedCustomerPlaces = customerPlaces;
            assignedCustomerPlaces = new List<CustomerPlace>();
            OpenOrders = new List<Order>();
            
            onAnyOrderAccept.AddListener(OnOrderEvent);
            onAnyOrderProcess.AddListener(OnOrderEvent);
            onOrderListChange.AddListener(OnOrderListChange);
            onCustomerLeave.AddListener(RemoveCustomer);
        }

        protected override void OnSimulationStart()
        {
            time = 0;
        }

        protected override void OnSimulationPause()
        {
            //throw new NotImplementedException();
        }

        protected override void OnSimulationUnpause()
        {
            //throw new NotImplementedException();
        }

        protected override void OnSimulationTick()
        {
            // Do stuff for each customer
            foreach (Customer customer in customers)
            {
                // Debug.Log($"Updating state for {customer.Name}");
                if (customer) customer.UpdateState();
            }

            if (customers.Count < CustomerLimit)
            {
                // Add a new customer based on time passed since the last time one was added.
                if (Random.value <= CustomerProbability(time))
                {
                    time = 0;
                    AddCustomer();
                }
                else
                {
                    time++;
                }
            }
        }

        private Customer AddCustomer()
        {
            GameObject newCustomer = Instantiate(customerPrefab, GameObject.FindWithTag("Spawner").transform);
            var customerScript = newCustomer.GetComponent<Customer>();
            newCustomer.name = customerScript.Name;
            AssignCustomerToRandomPlace(customerScript);

            customers.Add(customerScript);

            return customerScript;
        }

        private void RemoveCustomer(Customer customer)
        {
            if (customers.Remove(customer))
            {
                unassignedCustomerPlaces.Add(customer.assignedPlace);
                assignedCustomerPlaces.Remove(customer.assignedPlace);
                Destroy(customer.gameObject);
            }

            Debug.Log($"{customer.Name} hat die Taverne verlassen.");
        }

        private float CustomerProbability(int t)
        {
            var p = Mathf.Pow(1.2f, t) / 100;

            return p <= 1f ? p : 1f;
        }

        public static string GetRandomName(Customer.Race race)
        {
            string firstname;
            string lastname;
            
            switch (race)
            {
                case Customer.Race.Zwerg:
                    var dwarfFirstNameList = SimulationManager.Names["dwarf_firstname"];
                    var dwarfLastNameList = SimulationManager.Names["dwarf_lastname"];

                    firstname = dwarfFirstNameList[Random.Range(0, dwarfFirstNameList.Length)];
                    lastname = dwarfLastNameList[Random.Range(0, dwarfLastNameList.Length)];
                    break;
                case Customer.Race.Elf:
                    var elfFirstNameList = SimulationManager.Names["elf_firstname"];
                    var elfLastNameList = SimulationManager.Names["elf_lastname"];

                    firstname = elfFirstNameList[Random.Range(0, elfFirstNameList.Length)];
                    lastname = elfLastNameList[Random.Range(0, elfLastNameList.Length)];
                    break;
                case Customer.Race.Mensch:
                    var humanFirstNameList = SimulationManager.Names["human_firstname"];
                    var humanLastNameList = SimulationManager.Names["human_lastname"];

                    firstname = humanFirstNameList[Random.Range(0, humanFirstNameList.Length)];
                    lastname = humanLastNameList[Random.Range(0, humanLastNameList.Length)];
                    break;
                default:
                    throw new UnityException("Something went wrong.");
            };

            return $"{firstname} {lastname}";
        }

        /**
         * Assigns a random unassigned place to a specific customer.
         */
        public CustomerPlace AssignCustomerToRandomPlace(Customer customer)
        {
            // Get random unassigned place.
            CustomerPlace customerPlace = unassignedCustomerPlaces[Random.Range(0, unassignedCustomerPlaces.Count)];
            // Remove it from the original list
            unassignedCustomerPlaces.Remove(customerPlace);
            // Add it to a different list
            assignedCustomerPlaces.Add(customerPlace);
            // Assign it to the customer
            customer.AssignPlace(customerPlace);

            return customerPlace;
        }

        public void SetOrderOnMenu(Order order)
        {
            orderMenu.SetOrder(order);
        }

        public void ShowOrderMenu()
        {
            orderMenu.ShowMenu();
        }

        private void OnOrderEvent(Order order)
        {
            onOrderListChange.Invoke();
        }
        
        private void OnOrderListChange()
        {
            var sb = new StringBuilder();

            foreach (Order order in OpenOrders)
            {
                sb.Append($"- {order.GetShortDescription()}\n");
            }
            
            SimulationManager.GetInstance().questDisplay.text = sb.ToString();
        }
    }
}