﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            SetFunds(initialFunds);
            _contents = new ConcurrentDictionary<Item, int>();
        }
        
        public Inventory(int capacityPerSlot, int initialFunds, ConcurrentDictionary<Item, int> contents)
        {
            _capacityPerSlot = capacityPerSlot;
            SetFunds(initialFunds);
            _contents = contents;
        }

        public void Load()
        {
            if (ES3.KeyExists("inv_contents"))
            {
                Dictionary<string, int> savedItems = ES3.Load<Dictionary<string, int>>("inv_contents");
                _contents = new ConcurrentDictionary<Item, int>(savedItems.ToDictionary(
                    entry => (Item) InventoryManager.GetInstance().GetRegisteredItem(entry.Key), entry => entry.Value));
            }

            if (ES3.KeyExists("inv_funds"))
            {
                _funds = ES3.Load<int>("inv_funds");
            }
        }

        public void Save()
        {
            ES3.Save("inv_contents", _contents.ToDictionary(entry => entry.Key.GetId(), entry => entry.Value));
            ES3.Save("inv_funds", _funds);
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
            if (amount > 0)
            {
                _contents.AddOrUpdate(type, amount, (oldType, oldAmount) => oldAmount + amount);
                EventHandler.onInventoryChanged.Invoke();
            }

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
            Debug.Log("Item Removed");
            EventHandler.onInventoryChanged.Invoke();
            //Return the amount of items that could be removed
            return amount;
        }
        
        /**
         * <summary>Returns a copy of the content dictionary of this Inventory</summary>
         */
        public Dictionary<Item, int> GetContents()
        {
            return _contents.ToDictionary(entry => entry.Key, entry => entry.Value);
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
                SetFunds(_funds - amount);
                if (_funds < 0)
                    SetFunds(0);
                return true;
            }

            return false;
        }

        public void SetFunds(int funds)
        {
            int oldFunds = _funds;
            _funds = funds;
            EventHandler.onFundsChangedFrom.Invoke(oldFunds);
            EventHandler.onFundsChanged.Invoke();
        }

        public void AddFunds(int amount)
        {
            SetFunds(_funds + amount);
        }
        
    }
}
