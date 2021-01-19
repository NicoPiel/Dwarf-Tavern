using Simulation.Core;
using UnityEngine;

namespace Simulation.Modules.CustomerSimulation
{
    public class Order
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Customer customerReference;

        public ItemBeer.Attribute[] requiredAttributes;
        
        private string article;
        private string beverage;
        private string taste;
        private string attributeA;
        private string attributeB;
        
        public Order(Customer customerReference)
        {
            this.customerReference = customerReference;
            Name = RandomOrderName();
            Description = RandomOrderAttributesAndDescription();
            requiredAttributes = MapAttributes(attributeA, attributeB);
        }

        public Order Accept()
        {
            CustomerSimulation.OpenOrders?.Add(this);
            CustomerSimulation.onOrderAccept.Invoke();
            return this;
        }

        public void Process()
        {
            CustomerSimulation.OpenOrders?.Remove(this);
            CustomerSimulation.onOrderProcess.Invoke();
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

        private string RandomOrderAttributesAndDescription()
        {
            string attribute;   
            var attributes = SimulationManager.AttributeCombinations;

            // Get collection of keys
            var keys = new string[attributes.Count];
            // Copy key collection to new array 
            attributes.Keys.CopyTo(keys, 0);
            // Get random attribute
            attributeA = keys[Random.Range(0, keys.Length)];
            // Get combinations with that attribute
            var attributeCombinations = attributes[attributeA];
            // Get random combination
            if (Random.value < 0.5)
            {
                attribute = attributeCombinations[0];
                attributeB = attributeA;
            }
            else
            {
                var randomInt = Random.Range(1, attributeCombinations.Length);
                attribute = attributeCombinations[randomInt];

                attributeB = keys[randomInt - 1];
            }
            
            
            // Generic output
            return $"Ich hätte gerne ein{GetArticleAccusativePostfix()} <b>{taste.Substring(0, taste.Length - 2).ToLower()}{GetArticleAccusativePostfix()} {beverage}</b>, {article} mich <b>{attribute.ToLower()}er</b> macht.";
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

        private ItemBeer.Attribute[] MapAttributes(string inputAttributeA, string inputAttributeB)
        {
            if (string.IsNullOrWhiteSpace(inputAttributeA) || string.IsNullOrWhiteSpace(inputAttributeB)) throw new UnityException("One or both of the attribute strings was empty or null.");
            
            return new[] {MapAttribute(inputAttributeA), MapAttribute(inputAttributeB)};
        }

        private ItemBeer.Attribute MapAttribute(string attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute)) throw new UnityException("Attribute string was empty or null.");
            
            return attribute switch
            {
                "Stärke" => ItemBeer.Attribute.Strength,
                "Geschick" => ItemBeer.Attribute.Dexterity,
                "Intelligenz" => ItemBeer.Attribute.Intelligence,
                "Vitalität" => ItemBeer.Attribute.Vitality,
                "Wille" => ItemBeer.Attribute.Will,
                "Mut" => ItemBeer.Attribute.Courage,
                _ => throw new UnityException("Something went wrong.")
            };
        }
    }
}
