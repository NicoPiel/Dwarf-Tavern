using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Inventory
{
    public class Inventory
    {
        private ConcurrentDictionary<Item, int> _contents;
        private int _capacityPerSlot;
        private int _funds;

        public Inventory(int capacityPerSlot, int initialFunds)
        {
            _capacityPerSlot = capacityPerSlot;
            _funds = initialFunds;
        }
        
        public Inventory(int capacityPerSlot, int initialFunds, Dictionary<Item, int> contents)
        {
            _capacityPerSlot = capacityPerSlot;
            _funds = initialFunds;
            _contents = new ConcurrentDictionary<Item, int>();
        }
        
        /**
         * <summary>Adds a specified amount of an Item to the inventory</summary>
         * <returns>The amount of items that couldn't be added due to capacity restrictions (e.g. 0 if all items were added)</returns>
         */
        public int AddItem(Item type, int amount)
        {
            //Calculate remaining capacity for this type of item
            int capacityOnStack = _capacityPerSlot;
            int storedAmount = GetAmountOf(type);
            capacityOnStack -= storedAmount;
            
            
            //Calculate how many items of amount can't fit into the slot and update amount accordingly
            int overflow = 0;
            if (amount > capacityOnStack)
            {
                overflow = amount - capacityOnStack;
                amount = capacityOnStack - storedAmount;
            }
            
            //If any items can be added, update the content dictionary
            if(amount > 0)
                _contents.AddOrUpdate(type, amount, (oldType, oldAmount) => oldAmount + amount);
            
            return overflow;
        }
        
        /**
         * <summary>Removes the specified amount of an item from this inventory</summary>
         * <returns>The amount of items that were removed. (min = 0 (Item not in inventory), max = amount)</returns>
         */
        public int RemoveItem(Item type, int amount)
        {
            //Check how many items of type exist and return if the Inventory doesn't contain any.
            int currentAmount = GetAmountOf(type);
            if (currentAmount <= 0) return 0;
            
            //If more items than the inventory currently holds are to be removed, decrease the amount of items to remove to the currently stored amount
            if (amount > currentAmount) amount = currentAmount;
            
            //If not all items of a stack are removed, update the stack size, otherwise delete the entire stack
            if (_contents.ContainsKey(type) && amount > 0 && amount < currentAmount)
            {
                _contents.AddOrUpdate(type, currentAmount - amount, (tem, i) => i - amount);
            }else if (amount > 0 && amount >= currentAmount)
            {
                _contents.TryRemove(type, out _);
            }
            
            //Return the amount of items that could be removed
            return amount;
        }

        public int GetAmountOf(Item type)
        {
            _contents.TryGetValue(type, out var amount);
            return amount;
        }

        public int GetFunds()
        {
            return _funds;
        }

        public bool HasSufficientFunds(int needed)
        {
            return _funds >= needed;
        }
        
        /**
         * <summary>Tries to remove <c>amount</c> from the inventory's funds and returns true if enough funds were available. If <c>force</c> is true and <c>amount</c> is
         * greater than the available funds, the method will remove as much as possible and always return true.</summary>
         */
        public bool TryCharge(int amount, bool force)
        {
            if (HasSufficientFunds(amount) || force)
            {
                _funds -= amount;
                if (_funds < 0)
                    _funds = 0;
                return true;
            }

            return false;
        }

        public void SetFunds(int funds)
        {
            _funds = funds;
        }
        
    }
}
