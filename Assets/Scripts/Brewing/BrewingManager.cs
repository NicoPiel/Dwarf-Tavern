using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;

namespace Brewing
{
    public class BrewingManager : MonoBehaviour
    {
        private Dictionary<string, ItemBeer.Attribute> _attributeMap;
        private Dictionary<string, ItemBeer.Type> _typeMap;
        private Dictionary<ItemBeer.Attribute, Color> _attributeColorMap;
        private Dictionary<ItemBeer.Type, Color> _typeColorMap;

        public ColorMapEntry[] attributeColors;
        public ColorMapEntry[] typeColors;
        
        
        private void OnEnable()
        {
            if(_attributeMap == null) GenerateLookupMaps();
        }

        public ItemBeer Brew(IngredientItem baseItem, IngredientItem taste1, IngredientItem taste2,
            IngredientItem bonus = null)
        {
            List<ItemBeer.Type> typeModifiers = GetTypeModifiers(Item.Slot.Basic, baseItem);
            List<ItemBeer.Attribute> attributeModifiers1 = GetAttributeModifiers(Item.Slot.Taste, taste1);
            List<ItemBeer.Attribute> attributeModifiers2 = GetAttributeModifiers(Item.Slot.Taste, taste2);
            if (typeModifiers.Count < 1) return null;
            ItemBeer.Type drinkType = typeModifiers[0];
            ItemBeer.Attribute attr1 = attributeModifiers1.Count >= 1
                ? attributeModifiers1[0]
                : ItemBeer.Attribute.Unused;
            ItemBeer.Attribute attr2 = attributeModifiers2.Count >= 1
                ? attributeModifiers2[0]
                : ItemBeer.Attribute.Unused;
            _typeColorMap.TryGetValue(drinkType, out var cBase);
            var b1 = _attributeColorMap.TryGetValue(attr1, out var cAttr1);
            var b2 = _attributeColorMap.TryGetValue(attr2, out var cAttr2);
            Color finalColor = b1
                ? b2 ? Color.Lerp(Color.Lerp(cBase, cAttr1, 0.5f), cAttr2, 0.3333f) :
                Color.Lerp(cBase, cAttr1, 0.5f)
                : cBase;
            return new ItemBeer(InventoryManager.GetInstance().GetRegisteredItem("beer"), attr1, attr2, drinkType, finalColor);
        }

        private List<ItemBeer.Type> GetTypeModifiers(Item.Slot slot, IngredientItem item)
        {
            if (item != null && item.GetModifiers().TryGetValue(slot,out var modifiers))
            {
                return modifiers.Select(entry => _typeMap.TryGetValue(entry.Key, out var value) ? value : ItemBeer.Type.Unused)
                    .Where(type => type != ItemBeer.Type.Unused).ToList();
            }

            return new List<ItemBeer.Type>();
        }

        private List<ItemBeer.Attribute> GetAttributeModifiers(Item.Slot slot, IngredientItem item)
        {
            if (item != null && item.GetModifiers().TryGetValue(slot,out var modifiers))
            {
                return modifiers.Select(entry => _attributeMap.TryGetValue(entry.Key, out var value) ? value : ItemBeer.Attribute.Unused)
                    .Where(type => type != ItemBeer.Attribute.Unused).ToList();
            }

            return new List<ItemBeer.Attribute>();
        }

        private void GenerateLookupMaps()
        {
            _attributeMap = new Dictionary<string, ItemBeer.Attribute>
            {
                {"strength", ItemBeer.Attribute.Strength},
                {"dexterity", ItemBeer.Attribute.Dexterity},
                {"intelligence", ItemBeer.Attribute.Intelligence},
                {"vitality", ItemBeer.Attribute.Vitality},
                {"will", ItemBeer.Attribute.Will},
                {"courage", ItemBeer.Attribute.Courage}
            };
            _typeMap = new Dictionary<string, ItemBeer.Type>
            {
                {"beer", ItemBeer.Type.Beer},
                {"schnapps", ItemBeer.Type.Schnapps},
                {"brandy", ItemBeer.Type.Brandy},
                {"whiskey", ItemBeer.Type.Whiskey},
                {"wine", ItemBeer.Type.Wine},
            };
            _attributeColorMap = attributeColors.ToDictionary(ent => _attributeMap.TryGetValue(ent.id, out var attr) ? attr : ItemBeer.Attribute.Unused, ent => ent.color);
            _typeColorMap = typeColors.ToDictionary(ent => _typeMap.TryGetValue(ent.id, out var type) ? type : ItemBeer.Type.Unused, ent => ent.color);
        }

        [Serializable]
        public struct ColorMapEntry
        {
            public string id;
            [ColorUsageAttribute(true,true)]
            public Color color;
        }
    }
}