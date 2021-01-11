using Simulation.Core;
using UnityEngine;

namespace Simulation.Modules.CustomerSimulation
{
    public class Order
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Order()
        {
            Name = RandomOrderName();
            Description = RandomOrderDescription();
        }
        
        private string RandomOrderName()
        {
            var beverages = SimulationManager.Orders["beverages"];
            var tastes = SimulationManager.Orders["tastes"];

            var beverageString = beverages[Random.Range(0, beverages.Length)].Split(' ');
            var article = beverageString[0];
            var beverage = beverageString[1];
            
            var taste = tastes[Random.Range(0, tastes.Length)];

            taste += article switch
            {
                "das" => "es",
                "der" => "er",
                "die" => "e",
                _ => throw new UnityException($"Wrong format in orders.json with {beverage}")
            };

            return $"{taste} {beverage}";
        }

        private string RandomOrderDescription()
        {
            return "placeholder";
        }
    }
}
