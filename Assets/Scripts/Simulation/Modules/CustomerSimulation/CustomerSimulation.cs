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

        private static readonly string PathToNameJson = Application.streamingAssetsPath + "/JSON/names.json";
        private SimulationManager simulationManager;
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private List<Customer> customers; 

        private void Awake()
        {
            Names = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(File.ReadAllText(PathToNameJson));
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            simulationManager = GameManager.GetSimulationManager();
            
            simulationManager.onSimulationStart.AddListener(OnSimulationStart);
            simulationManager.onSimulationPause.AddListener(OnSimulationPause);
            simulationManager.onSimulationTick.AddListener(OnSimulationTick);
            
            customers = new List<Customer>();
        }

        private void OnSimulationStart()
        {
            // DEBUG: Add customers
            for (var i = 0; i < 20; i++)
            {
                GameObject newCustomer = Instantiate(customerPrefab, transform);
                var customerScript = newCustomer.GetComponent<Customer>();
                newCustomer.name = customerScript.Name;
                
                customers.Add(customerScript);
            }
        }
        
        private void OnSimulationPause()
        {
            
        }
        
        private void OnSimulationTick()
        {
            
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
