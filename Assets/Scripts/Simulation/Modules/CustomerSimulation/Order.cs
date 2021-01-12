using Inventory;
using Simulation.Core;
using UnityEngine;

namespace Simulation.Modules.CustomerSimulation
{
    public class Order
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private string article;
        private string beverage;
        private string taste;
        
        public Order()
        {
            Name = RandomOrderName();
            Description = RandomOrderDescription();
        }

        public Order Accept()
        {
            CustomerSimulation.OpenOrders?.Add(this);
            return this;
        }

        public void Process()
        {
            CustomerSimulation.OpenOrders?.Remove(this);
        }
        
        private string RandomOrderName()
        {
            var beverages = SimulationManager.Orders["beverages"];
            var tastes = SimulationManager.Orders["tastes"];

            var beverageString = beverages[Random.Range(0, beverages.Length)].Split(' ');
            article = beverageString[0];
            beverage = beverageString[1];
            
            taste = tastes[Random.Range(0, tastes.Length)];

            taste += GetArticleNominativePostfix();

            return $"{taste} {beverage}";
        }

        private string RandomOrderDescription()
        {
            string attribute;   
            var attributes = SimulationManager.AttributeCombinations;

            // Get collection of keys
            var keys = new string[attributes.Count];
            // Copy key collection to new array 
            attributes.Keys.CopyTo(keys, 0);
            // Get random attribute
            var attributeString = keys[Random.Range(0, keys.Length)];
            // Get combinations in with that attribute
            var attributeCombinations = attributes[attributeString];
            // Get random combination
            if (Random.value < 0.5)
            {
                attribute = attributeCombinations[0];
            }
            else
            {
                attribute = attributeCombinations[Random.Range(1, attributeCombinations.Length)];
            }
            
            
            // Generic output
            return $"Ich hätte gerne ein{GetArticleAccusativePostfix()} {taste.Substring(0, taste.Length - 2).ToLower()}{GetArticleAccusativePostfix()} {beverage}, {article} mich {attribute.ToLower()}er macht.";
        }

        private string GetArticleNominativePostfix()
        {
            return article switch
            {
                "das" => "es",
                "der" => "er",
                "die" => "e",
                _ => throw new UnityException($"Wrong format in orders.json with {beverage}")
            };
        }
        
        private string GetArticleAccusativePostfix()
        {
            return article switch
            {
                "das" => "",
                "der" => "en",
                "die" => "e",
                _ => throw new UnityException($"Wrong format in orders.json with {beverage}")
            };
        }
    }
}
