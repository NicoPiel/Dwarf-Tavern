using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Simulation.Core;
using UnityEngine;

namespace Simulation.Modules.CustomerSimulation
{
    public class CustomerSimulation : MonoBehaviour
    {
        public static Dictionary<string, string[]> Names { get; set; }
        public static Dictionary<string, string[]> Orders { get; set; }
        public int CustomerLimit = 20;

        private static readonly string PathToNameJson = Application.streamingAssetsPath + "/JSON/names.json";
        private static readonly string PathToOrdersJson = Application.streamingAssetsPath + "/JSON/orders.json";

        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private List<Customer> customers;
        [SerializeField] private List<CustomerPlace> customerPlaces;
        [SerializeField] private List<CustomerPlace> unassignedCustomerPlaces;
        [SerializeField] private List<CustomerPlace> assignedCustomerPlaces;

        private int time;

        private void Awake()
        {
            Names = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(PathToNameJson));
            Orders = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(PathToOrdersJson));
        }

        // Start is called before the first frame update
        private void Start()
        {
            SimulationManager.onSimulationStart.AddListener(OnSimulationStart);
            SimulationManager.onSimulationPause.AddListener(OnSimulationPause);
            SimulationManager.onSimulationTick.AddListener(OnSimulationTick);

            customers = new List<Customer>();
            
            customerPlaces = new List<CustomerPlace>();
            foreach (var obj in GameObject.FindGameObjectsWithTag("CustomerPlace"))
            {
                customerPlaces.Add(obj.GetComponent<CustomerPlace>());
            }

            unassignedCustomerPlaces = customerPlaces;
            assignedCustomerPlaces = new List<CustomerPlace>();
        }

        private void OnSimulationStart()
        {
            time = 0;
        }

        private void OnSimulationPause()
        {
            
        }

        private void OnSimulationTick()
        {
            if (customers.Count <= CustomerLimit)
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

            foreach (var customer in customers)
            {
                customer.UpdateState();
            }
        }

        private Customer AddCustomer()
        {
            var newCustomer = Instantiate(customerPrefab, GameObject.FindWithTag("Spawner").transform);
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
                Destroy(customer.gameObject);
            }
            
            Debug.Log($"{customer.Name} hat die Taverne verlassen.");
        }

        private float CustomerProbability(int t)
        {
            var p = Mathf.Pow(1.6f, t) / 100;

            return p <= 1f ? p : 1f;
        }

        public static string GetRandomName()
        {
            var firstNameList = Names["dwarf_firstname"];
            var lastNameList = Names["dwarf_lastname"];

            var firstname = firstNameList[Random.Range(0, firstNameList.Length)];
            var lastname = lastNameList[Random.Range(0, lastNameList.Length)];

            return $"{firstname} {lastname}";
        }

        /**
         * Assigns a random unassigned place to a specific customer.
         */
        public CustomerPlace AssignCustomerToRandomPlace(Customer customer)
        {
            // Get random unassigned place.
            var customerPlace = unassignedCustomerPlaces[Random.Range(0, unassignedCustomerPlaces.Count)];
            // Remove it from the original list
            unassignedCustomerPlaces.Remove(customerPlace);
            // Add it to a different list
            assignedCustomerPlaces.Add(customerPlace);
            // Assign it to the customer
            customer.Assign(customerPlace);

            return customerPlace;
        }
    }
}