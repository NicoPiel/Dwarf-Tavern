using Inventory;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public IngredientItem item;
    
    public IngredientItem GetItem()
    {
        return this.item;
    }

    public void SetItem(IngredientItem item)
    {
        this.item = item;
    }
}
