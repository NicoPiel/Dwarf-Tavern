using Inventory;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    private Inventory.Inventory _inventory;
    private Item _currentItem;
    private UnityEvent _onSlotChanged;
    private GameObject _imageObject;
    private Image _image;
    private GameObject _textObject;
    public ItemSlot slot;

    
    public enum ItemSlot
    {
        Base,
        Taste,
        Bonus
    }
    private void Start()
    {
        _onSlotChanged = new UnityEvent();
        _inventory = InventoryManager.GetInstance().GetPlayerInventory();

        _imageObject = transform.Find("Image").gameObject;
        _image = _imageObject.GetComponent<Image>();

        _textObject = transform.Find("Text").gameObject;

        _onSlotChanged.AddListener(UpdateUI);
    }

    private void OnEnable()
    {
        transform.Find("Image").gameObject.SetActive(false);
        _currentItem = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RemoveItemFromSlot();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Item item = eventData.pointerDrag.GetComponent<ItemHolder>()?.GetItem();
            if (item != null)
            {
                if(IsCompatible(item)) 
                    AddItemToSlot(item);
                //else
                    //Debug.Log("Item not compatible" + item.GetModifiers());
            }
        }
    }

    private void AddItemToSlot(Item item)
    {
        if (item != null)
        {
            RemoveItemFromSlot();
        }

        _currentItem = item;
        _inventory.RemoveItem(item, 1);
        _onSlotChanged.Invoke();
    }

    public Item GetItem()
    {
        return _currentItem;
    }

    public bool IsItemSet()
    {
        return _currentItem != null;
    }

    private void RemoveItemFromSlot()
    {
        if (_currentItem != null)
        {
            _inventory.AddItem(_currentItem, 1);
            _currentItem = null;
            _onSlotChanged.Invoke();
        }
    }

    public void TakeItem()
    {
        if (_currentItem != null)
        {
            _currentItem = null;
            _onSlotChanged.Invoke();
        }
    }

    private void UpdateUI()
    {
        if (_currentItem != null)
        {
            _imageObject.SetActive(true);
            _textObject.SetActive(false);
            var tempColor = _image.color;
            tempColor.a = 255f;
            _image.color = tempColor;
            _image.sprite = _currentItem.GetSprite();
        }
        else if (_currentItem == null)
        {
            _image.sprite = null;
            _textObject.SetActive(true);
            var tempColor = _image.color;
            tempColor.a = 0f;
            _image.color = tempColor;
        }
    }

    private void OnDisable()
    {
        if (_currentItem != null)
        {
            _inventory.AddItem(_currentItem, 1);
        }
    }

    private bool IsCompatible(Item item)
    {
        switch (slot)
        {
            case ItemSlot.Base:
                if (item.GetModifiers().ContainsKey(Item.Slot.Basic))
                {
                    return true;
                }

                break;
            case ItemSlot.Taste:
                if (item.GetModifiers().ContainsKey(Item.Slot.Taste))
                {
                    return true;
                }

                break;
            case ItemSlot.Bonus:
                if (item.GetModifiers().ContainsKey(Item.Slot.Bonus))
                {
                    return true;
                }

                break;
            default:
                return false;
        }
        return false;
    }
}