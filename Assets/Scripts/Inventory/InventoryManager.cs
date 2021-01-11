using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private static InventoryManager _instance;
        
        private readonly ConcurrentDictionary<string, StaticItem> _registeredItems =
            new ConcurrentDictionary<string, StaticItem>();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }
            DontDestroyOnLoad(this);
            _instance = this;
            RegisterItems();
        }

        public static InventoryManager GetInstance()
        {
            return _instance;
        }
        

        /**
         * <summary>Registers all Addressable Assets of the type ScriptableItem</summary>
         */
        private void RegisterItems()
        {
            //Locate all Addressable Assets with the "item" tag
            AssetState = State.WaitingForAssets;
            AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync("items");
            handle.Completed += ResourceLocationHandle_OnComplete;
        }

        /**
         * <summary>Event Handler listening to the completion of the async function called in <c>RegisterItems</c></summary>
         */
        private async void ResourceLocationHandle_OnComplete(AsyncOperationHandle<IList<IResourceLocation>> handle)
        {
            //If retrieving the locations of Items failed
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                //Set state to Error and invoke an event with that state
                AssetState = State.Error;
                GameManager.GetEventHandler().onInventoryManagerInitialized.Invoke(AssetState);
                return;
            }

            //Load the actual Assets from each location and save all AsyncOperationHandles in an array
            IList<IResourceLocation> locations = handle.Result;
            AsyncOperationHandle<StaticItem>[] handles = locations.Select(location =>
            {
                AsyncOperationHandle<StaticItem> itemHandle = Addressables.LoadAssetAsync<StaticItem>(location);
                itemHandle.Completed += ItemHandle_OnComplete;
                return itemHandle;
            }).ToArray();

            //Wait for all asset loading tasks to be completed and check if every single one of the tasks succeeded
            await Task.WhenAny(Task.WhenAll(handles.Select(itemHandle => (Task) itemHandle.Task)),
                Task.Delay(TimeSpan.FromSeconds(30)));
            bool success = handles.All(itemHandle => itemHandle.Status == AsyncOperationStatus.Succeeded);

            //Set the state accordingly and invoke the InventoryManagerInitialized event
            AssetState = success ? State.Initialized : State.Error;
            GameManager.GetEventHandler().onInventoryManagerInitialized.Invoke(AssetState);
        }

        /**
         * <summary>Event Handler listening to the completion of the async function called in <c>ResourceLocationHandle_OnComplete</c></summary>
         */
        private void ItemHandle_OnComplete(AsyncOperationHandle<StaticItem> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                //Save the retrieved StaticItem reference in the registry dictionary
                StaticItem item = handle.Result;
                string id = item.GetId();
                _registeredItems.AddOrUpdate(id, item, (k, v) => item);
            }
        }

        /**
         * <summary>Gets a StaticItem from its ID. Returns null if nonexistent</summary>
         */
        public StaticItem GetRegisteredItem(string id)
        {
            StaticItem ret = null;
            _registeredItems.TryGetValue(id, out ret);
            return ret;
        }

        public string[] GetRegisteredIds()
        {
            string[] ret = new string [_registeredItems.Count];
            _registeredItems.Keys.CopyTo(ret, 0);
            return ret;
        }

        /**
         * <summary>Gets/Sets the current <c>State</c> of this InventoryManager</summary>
         */
        public State AssetState
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get;
            [MethodImpl(MethodImplOptions.Synchronized)]
            protected set;
        } = State.Uninitialized;

        public Inventory PlayerInventory { get; private set; }

        /**
         * <summary>Returns PlayerInventory. Creates a new Inventory and assigns it to PlayerInventory if PlayerInventory is null.</summary>
         */
        public Inventory GetPlayerInventory()
        {
            //TODO: Load Inventory from save file
            return PlayerInventory ?? (PlayerInventory = new Inventory(50, 1000)); //Slot size of 50 hardcoded until save/load system is done
        }

        /**
         * <summary>Enum to represent all four possible states the InventoryManager can be in</summary>
         */
        public enum State
        {
            //Initialization was not yet attempted (default on instantiation)
            Uninitialized,

            //Initialization has begun but assets have not yet been completely loaded
            WaitingForAssets,

            //Initialization was successful and all StaticItems are properly registered
            Initialized,

            //Initialization failed due to an error or timeout while loading the assets
            Error
        }
    }
}
