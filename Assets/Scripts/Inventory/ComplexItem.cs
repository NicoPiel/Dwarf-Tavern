using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public abstract class ComplexItem : Item
    {
        private StaticItem reference;

        protected ComplexItem(StaticItem reference)
        {
            this.reference = reference;
        }

        public string GetId()
        {
            return reference.GetId();
        }

        public string GetDisplayName()
        {
            return reference.GetDisplayName();
        }

        public string GetLore()
        {
            return reference.GetLore();
        }

        public bool CanStackWith(Item other)
        {
            return reference.CanStackWith(other);
        }

        public bool IsComplex()
        {
            return true;
        }

        public Sprite GetSprite()
        {
            return reference.GetSprite();
        }

        public bool IsIngredient()
        {
            return reference.IsIngredient();
        }

        public Dictionary<Item.Slot, Dictionary<string, int>> GetModifiers()
        {
            return reference.GetModifiers();
        }

        public Item.Rarity GetRarity()
        {
            return reference.GetRarity();
        }
    }
}