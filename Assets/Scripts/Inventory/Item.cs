using System;
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
        
        
        
    }
}