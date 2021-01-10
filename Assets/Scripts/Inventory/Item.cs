using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public interface Item
    { 
        string GetId();
        string GetDisplayName();
        string GetLore();
        
        /**
         * <summary>Checks if the Item would stack with another item</summary>
         */
        bool CanStackWith(Item other);
        bool IsComplex();
        Sprite GetSprite();
        
        bool IsIngredient();
        Dictionary<Slot, Dictionary<string, int>> GetModifiers();
        Rarity GetRarity();

        public enum Rarity
        {
            //Useless items or ingredients with negative effects
            Junk,
            //Useful but common items with neutral effects
            Common,
            //Less common and positive effects
            Uncommon,
            //Rare/valuable items with very positive effects
            Rare,
            //Extremely rare items with the best effects
            Epic,
            //Unique items that can only be obtained a limited amount of times (e.g. story items)
            Legendary
        }

        public enum Slot
        {
            Basic,
            Taste1,
            Taste2,
            Bonus
        }

    }
}