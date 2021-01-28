using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility.Tooltip;

public class DragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemHolder itemHolder;
    
    private CanvasGroup _canvasGroup;
    private Vector3 _startPosition;
    private Tooltip _tooltip;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _tooltip = Brewstand.GetTooltip();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
        _startPosition = transform.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        transform.position = _startPosition;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 pos = gameObject.transform.position;
        _tooltip.GetComponent<RectTransform>().position = new Vector2(pos.x, pos.y - 250);
        
        IngredientItem item = itemHolder.GetItem();
        var sb = new StringBuilder();
        
        sb.Append($"Name: {item.GetDisplayName()}");
        sb.Append("\n\n");

        foreach (Inventory.Item.Slot slot in item.GetModifiers().Keys)
        {
            switch (slot)
            {
                case Item.Slot.Basic:
                {
                    sb.Append("Als Grundzutat:");
                    sb.Append("\n\n");
                
                    foreach (var modifier in item.GetModifiers(slot).Keys)
                    {
                        sb.Append($"\t+ {GameManager.GetTranslation(modifier.ToLower())}");
                        sb.Append("\n");
                    }

                    break;
                }
                
                case Item.Slot.Taste:
                {
                    sb.Append("Als Geschmackszutat:");
                    sb.Append("\n\n");
                
                    foreach (var modifier in item.GetModifiers(slot).Keys)
                    {
                        sb.Append($"\t+ {GameManager.GetTranslation(modifier.ToLower())}");
                        sb.Append("\n");
                    }

                    break;
                }

                case Item.Slot.Bonus:
                {
                    sb.Append("Als Bonuszutat:");
                    sb.Append("\n\n");
                
                    foreach (var modifier in item.GetModifiers(slot).Keys)
                    {
                        sb.Append($"\t+ {GameManager.GetTranslation(modifier.ToLower())}");
                        sb.Append("\n");
                    }
                    
                    break;
                }

                default:
                    throw new UnityException("Something went terribly wrong.");
            }
            
            sb.Append("\n\n\n");
        }

        Tooltip.ShowTooltip_Static(_tooltip, sb.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static(_tooltip);
    }
}
