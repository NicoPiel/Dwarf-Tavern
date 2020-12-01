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
        public int CustomerLimit = 20;

        private static readonly string PathToNameJson = Application.streamingAssetsPath + "/JSON/names.json";

        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private List<Customer> customers;
        [SerializeField] private List<CustomerPlace> customerPlaces;

        private int time;

        private void Awake()
        {
            Names = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(PathToNameJson));
        }

        // Start is called before the first frame update
        private void Start()
        {
            SimulationManager.onSimulationStart.AddListener(OnSimulationStart);
            SimulationManager.onSimulationPause.AddListener(OnSimulationPause);
            SimulationManager.onSimulationTick.AddListener(OnSimulationTick);

            customers = new List<Customer>();
            
            customerPlaces = new List<CustomerPlace>();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("CustomerPlace"))
            {
                customerPlaces.Add(obj.GetComponent<CustomerPlace>());
            }
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

        private Customer AddCustomer()
        {
            var newCustomer = Instantiate(customerPrefab, transform);
            var customerScript = newCustomer.GetComponent<Customer>();
            newCustomer.name = customerScript.Name;
            newCustomer.ass

            customers.Add(customerScript);

            return customerScript;
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
    }
}