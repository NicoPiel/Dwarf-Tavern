using System;
using System.Collections.Generic;
using System.Linq;
using Expeditions.Timer;
using Inventory;
using UnityEngine;
using Random = System.Random;

namespace Expeditions.Events
{
    public class EventPoolManager : MonoBehaviour
    {
        public const float ChanceCommon = 0.35f;
        public const float ChanceUncommon = 0.3f;
        public const float ChanceRare = 0.2f;
        public const float ChanceEpic = 0.1f;
        public const float ChanceLegendary = 0.05f;
        
        public static EventPoolManager Instance
        {
            get;
            private set;
        }

        public EventPool eventPool;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public SimulationAdapter GetSimulationAdapter()
        {
            return GetComponent<SimulationAdapter>();
        }

        public Event GetRandomEvent(Expedition expedition)
        {
            List<Event> events = eventPool.events.Where(evt =>
                evt.difficulty <= expedition.GetDifficulty() && (expedition.GetKarma() < 0
                    ? evt.neededKarmaLevel >= expedition.GetKarma() && evt.neededKarmaLevel <= 0
                    : evt.neededKarmaLevel <= expedition.GetKarma() && evt.neededKarmaLevel >= 0) &&
                (evt.themeType == Expedition.ThemeType.Default || evt.themeType == expedition.GetThemeType())).ToList();
            Random rand = new Random();
            return events.Count > 0 ? events[rand.Next(events.Count)] : null;
        }

        public bool HandleChoice(int choiceId)
        {
            Event evt = ExpeditionHolder.GetInstance().GetCurrentEvent();
            Expedition exp = ExpeditionHolder.GetInstance().GetSelectedExpedition();
            Event.EventChoice choice = evt.choices[choiceId];
            float chance = choice.chance;
            if (choice.requirementType == Event.EventChoice.RequirementType.Soft && exp.GetTeam().ContainsRole(choice.requiredClass))
            {
                chance += choice.reqBonus;
            }
            
            exp.AddKarma(choice.karmaAdjustment);

            bool success = new Random().NextDouble() <= chance;
            if (success)
            {
                switch (choice.reward)
                {
                    case "lootDrop":
                        GiveRandomLoot();
                        break;
                    case "karmaPlus":
                        exp.AddKarma(1);
                        break;
                }
            }
            else
            {
                switch (choice.loss)
                {
                    case "damage":
                        exp.DamageRandom();
                        break;
                    case "death":
                        exp.KillRandom();
                        break;
                }
            }

            return success;
        }

        private void GiveRandomLoot()
        {
            float rand = (float) new Random().NextDouble();
            int category = 1;
            if (rand >= ChanceCommon) category++;
            rand -= ChanceCommon;
            if (rand >= ChanceUncommon) category++;
            rand -= ChanceUncommon;
            if (rand >= ChanceRare) category++;
            rand -= ChanceRare;
            if (rand >= ChanceEpic) category++;
            rand -= ChanceEpic;
            if (rand >= ChanceLegendary) category++;

            List<IngredientItem> items = InventoryManager.GetInstance().GetRegisteredIds()
                .Select(id => InventoryManager.GetInstance().GetRegisteredItem(id)).OfType<IngredientItem>()
                .Where(i => i.GetRarity() == (Item.Rarity) category).ToList();
            IngredientItem item = items[new Random().Next(items.Count)];
            InventoryManager.GetInstance().GetPlayerInventory().AddItem(item, new Random().Next(1, 6));
        }
        
    }
}