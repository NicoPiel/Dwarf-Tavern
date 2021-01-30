using System.Text;
using Simulation.Core;
using Simulation.Modules.CustomerSimulation;
using UnityEngine;
using UnityEngine.Events;

namespace Simulation.Modules.CustomerSimulation
{
    public class Order
    {
        // public
        public string Name { get; set; }
        public string Description { get; set; }
        public readonly Customer customerReference;

        public readonly ItemBeer.Type requiredType;
        public readonly ItemBeer.Attribute[] requiredAttributes;

        // private
        private string article;
        private string beverage;
        private string taste;
        private string attributeCombination;
        private string attributeA;
        private string attributeB;
        private int value;
        private int customerSatisfaction;

        // Events
        public readonly OrderEvent onAccept;
        public readonly OrderEvent onProcess;
        public readonly OrderEvent onAcceptCancel;

        public Order(Customer customerReference)
        {
            this.customerReference = customerReference;
            requiredType = ItemBeer.GetRandomType();
            Name = RandomOrderName();
            Description = RandomOrderAttributesAndDescription();
            requiredAttributes = MapAttributes(attributeA, attributeB);

            onAccept = new OrderEvent();
            onProcess = new OrderEvent();
            onAcceptCancel = new OrderEvent();
        }

        public Order Accept()
        {
            CustomerSimulation.OpenOrders?.Add(this);
            CustomerSimulation.onAnyOrderAccept.Invoke(this);
            onAccept.Invoke(this);
            return this;
        }

        public int Process(ItemBeer itemBeer)
        {
            value = itemBeer.GetPrice();
            
            CustomerSimulation.OpenOrders?.Remove(this);
            CustomerSimulation.onAnyOrderProcess.Invoke(this);
            onProcess.Invoke(this);
            return CompareBeverageToOrder(itemBeer);
        }

        private string RandomOrderName()
        {
            var beverages = SimulationManager.Orders["beverages"];
            var tastes = SimulationManager.Orders["tastes"];

            foreach (var s in beverages)
            {
                var beverageString = s.Split(' ');
                
                if (beverageString[1] == ItemBeer.TypeToString(requiredType))
                {
                    article = beverageString[0];
                    beverage = beverageString[1];
                }
            }

            taste = tastes[Random.Range(0, tastes.Length)];

            taste += GetArticleNominativePostfix();

            return $"{taste} {beverage}";
        }

        private string RandomOrderAttributesAndDescription()
        {
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
                attributeCombination = attributeCombinations[0];
                attributeB = "Ungenutzt";
            }
            else
            {
                var randomInt = Random.Range(1, attributeCombinations.Length);
                attributeCombination = attributeCombinations[randomInt];

                attributeB = keys[randomInt - 1];
            }


            // Generic output
            return
                $"Ich hätte gerne ein{GetArticleAccusativePostfix()} <b>{taste.Substring(0, taste.Length - 2).ToLower()}{GetSubstantiveAccusativePostfix()} {beverage}</b>, {article} mich <b>{attributeCombination.ToLower()}er</b> macht.";
        }

        public string GetShortDescription()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"Ein{GetArticleAccusativePostfix()} {beverage}");
            stringBuilder.Append(" ");
            stringBuilder.Append($"für {customerReference.Name}");
            stringBuilder.Append($", {article} ihn {attributeCombination}er macht.");
            
            
            return stringBuilder.ToString();
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
        
        private string GetSubstantiveAccusativePostfix()
        {
            return article switch
            {
                "das" => "es",
                "der" => "en",
                "die" => "e",
                _ => throw new UnityException($"Wrong format in orders.json with {beverage}")
            };
        }

        private ItemBeer.Attribute[] MapAttributes(string inputAttributeA, string inputAttributeB)
        {
            if (string.IsNullOrWhiteSpace(inputAttributeA) || string.IsNullOrWhiteSpace(inputAttributeB))
                throw new UnityException("One or both of the attribute strings was empty or null.");

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
                "Ungenutzt" => ItemBeer.Attribute.Unused,
                _ => throw new UnityException("Something went wrong.")
            };
        }

        private int CompareBeverageToOrder(ItemBeer itemBeer)
        {
            if (itemBeer.GetDrinkType() != requiredType)
            {
                return customerSatisfaction = 0;
            } 
            
            var stringBuilder = new StringBuilder();
            var beverageAttributes = itemBeer.GetAttributes();

            stringBuilder.Append($"Attribute des Items: {ItemBeer.AttributeToString(beverageAttributes[0])} und {ItemBeer.AttributeToString(beverageAttributes[1])}\nErgibt: {attributeCombination}");
            stringBuilder.Append($"Attribute der Bestellung: {ItemBeer.AttributeToString(requiredAttributes[0])} und {ItemBeer.AttributeToString(requiredAttributes[1])}\n");
            
            // If both requirements are met
            if ((requiredAttributes[0] == beverageAttributes[0] && requiredAttributes[1] == beverageAttributes[1]) ||
                (requiredAttributes[0] == beverageAttributes[1] && requiredAttributes[1] == beverageAttributes[0]))
            {
                stringBuilder.Append("Beide stimmen.");
                customerSatisfaction = 2;
            }
            else if ((requiredAttributes[0] == beverageAttributes[0] ^ requiredAttributes[1] == beverageAttributes[1]) ||
                     (requiredAttributes[1] == beverageAttributes[0] ^ requiredAttributes[1] == beverageAttributes[0]))
            {
                stringBuilder.Append("Eins stimmt.");
                customerSatisfaction = 1;
            }
            else
            {
                stringBuilder.Append("Keins stimmt.");
                customerSatisfaction = 0;
            }
            
            Debug.LogWarning(stringBuilder.ToString());

            return customerSatisfaction;
        }

        public int GetValue()
        {
            return value;
        }

        public int GetCustomerSatisfaction()
        {
            return customerSatisfaction;
        }

        public override bool Equals(object obj)
        {
            if (obj is Order order) return order.customerReference == customerReference;


            return false;
        }
    }
}

public class OrderEvent : UnityEvent<Order>
{
}