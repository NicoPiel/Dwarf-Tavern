using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public abstract class ComplexItem : Item
    {
        private readonly StaticItem reference;

        protected ComplexItem(StaticItem reference)
        {
            this.reference = reference;
        }

        public virtual string GetId()
        {
            return reference.GetId();
        }

        public virtual string GetDisplayName()
        {
            return reference.GetDisplayName();
        }

        public virtual string GetLore()
        {
            return reference.GetLore();
        }

        public virtual bool CanStackWith(Item other)
        {
            return reference.CanStackWith(other);
        }

        public virtual bool IsComplex()
        {
            return true;
        }

        public virtual Sprite GetSprite()
        {
            return reference.GetSprite();
        }

        public virtual bool IsIngredient()
        {
            return reference.IsIngredient();
        }

        public virtual Dictionary<Item.Slot, Dictionary<string, int>> GetModifiers()
        {
            return reference.GetModifiers();
        }

        public virtual Item.Rarity GetRarity()
        {
            return reference.GetRarity();
        }

        public override bool Equals(object other)
        {
            return other is ComplexItem && reference.Equals(((ComplexItem) other).reference);
        }

        public override int GetHashCode()
        {
            return reference.GetHashCode();
        }
    }
}