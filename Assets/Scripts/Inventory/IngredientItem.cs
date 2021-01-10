using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "NewIngredientItem", menuName = "ScriptableObjects/IngredientItem", order = 2)]
    public class IngredientItem : StaticItem
    {
        public ModifierMapEntry[] modifiers;
        private Dictionary<Item.Slot, Dictionary<string, int>> _modifierDict = null;
        public override bool IsIngredient()
        {
            return true;
        }

        public override Dictionary<Item.Slot, Dictionary<string, int>> GetModifiers()
        {
            if (_modifierDict == null)
            {
                _modifierDict = modifiers.ToDictionary(entry => entry.slot, entry => entry.entries.ToDictionary(modifier => modifier.key, modifier => modifier.value));
            }
            return _modifierDict;
        }

        public bool CanFit(Item.Slot slot)
        {
            return GetModifiers().ContainsKey(slot);
        }

        public Dictionary<string, int> GetModifiers(Item.Slot slot)
        {
            GetModifiers().TryGetValue(slot, out var dict);
            return dict;
        }

        [Serializable]
        public struct ItemModifier
        {
            public string key;
            public int value;
        }

        [Serializable]
        public struct ModifierMapEntry
        {
            public Item.Slot slot;
            public ItemModifier[] entries;
        }
    }
}