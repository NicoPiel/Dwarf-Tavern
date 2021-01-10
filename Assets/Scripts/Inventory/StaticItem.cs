using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/StaticItem", order = 1)]
    public class StaticItem : ScriptableObject, Item
    {
        public string id;
        public string displayName;
        public string lore;
        public bool stackable;
        public Item.Rarity rarity;
        public Sprite baseTexture;

        public virtual string GetId()
        {
            return id;
        }

        public virtual string GetDisplayName()
        {
            return displayName;
        }

        public virtual string GetLore()
        {
            return lore;
        }

        public virtual bool CanStackWith(Item other)
        {
            switch (other)
            {
                case null:
                    return false;
                case StaticItem item when (item == this || item.GetId().Equals(this.GetId())):
                    return true;
                default:
                    return false;
            }
        }
        
        public override bool Equals(object obj)
        {
            return obj != null && obj is StaticItem item && this.CanStackWith(item);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (id != null ? id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (displayName != null ? displayName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (lore != null ? lore.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ stackable.GetHashCode();
                hashCode = (hashCode * 397) ^ (baseTexture != null ? baseTexture.GetHashCode() : 0);
                return hashCode;
            }
        }

        public virtual bool IsComplex()
        {
            return false;
        }

        public virtual Sprite GetSprite()
        {
            return baseTexture;
        }

        public virtual bool IsIngredient()
        {
            return false;
        }

        public virtual Dictionary<Item.Slot, Dictionary<string, int>> GetModifiers()
        {
            return new Dictionary<Item.Slot, Dictionary<string, int>>();
        }

        public virtual Item.Rarity GetRarity()
        {
            return rarity;
        }
    }
}