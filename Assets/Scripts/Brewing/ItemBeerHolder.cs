using UnityEngine;

public class ItemBeerHolder : MonoBehaviour
{
    public bool initialized = false;
    
    private static ItemBeerHolder _instance;
    
    public ItemBeer _itemBeer1;
    public ItemBeer _itemBeer2;
    public ItemBeer _itemBeer3;
    public ItemBeer _itemBeer4;

    private void Awake()
    {
        _instance = this;
        initialized = true;
    }

    public static ItemBeerHolder GetInstance()
    {
        return _instance;
    }
    
    public bool Add(ItemBeer itemBeer)
    {
        if (_itemBeer1 == null)
        {
            _itemBeer1 = itemBeer;
        }else if (_itemBeer2 == null)
        {
            _itemBeer2 = itemBeer;
        }else if (_itemBeer3 == null)
        {
            _itemBeer3 = itemBeer;
            
        }else if (_itemBeer4 == null)
        {
            _itemBeer4 = itemBeer;
        }
        else
        {
            return false;   
        }
        GameManager.GetEventHandler().onItemBeerHolderChanged.Invoke();
        return true;

    }

    public void Remove(int slot)
    {
        switch (slot)
        {
            case 1:
                _itemBeer1 = null;
                break;
            case 2:
                _itemBeer2 = null;
                break;
            case 3:
                _itemBeer3 = null;
                break;
            case 4:
                _itemBeer4 = null;
                break;
        }
        GameManager.GetEventHandler().onItemBeerHolderChanged.Invoke();
    }

    public bool IsFull()
    {
        return _itemBeer1 != null && _itemBeer2 != null && _itemBeer3 != null && _itemBeer4 != null;
    }

    public ItemBeer GetItemBeerFromSlot(int slot)
    {
        switch (slot)
        {
            case 1:
                return _itemBeer1; ;
            case 2:
                return _itemBeer2;
            case 3:
                return _itemBeer3;;
            case 4:
                return _itemBeer4;
        }
        return null;
    }

    public static bool IsInitialized()
    {
        return _instance.initialized;
    }
}
