using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StaticItem", order = 1)]
    public class StaticItem : ScriptableObject, Item
    {
        public string id;
        public string displayName;
        public string lore;
        public bool stackable;
        public Sprite baseTexture;
        
        public string GetId()
        {
            return id;
        }

        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetLore()
        {
            return lore;
        }

        public bool CanStackWith(Item other)
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

        public bool IsComplex()
        {
            return false;
        }

        public Sprite GetSprite()
        {
            return baseTexture;
        }
    }
}