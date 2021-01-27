using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Glossar : MonoBehaviour
{
    public GameObject ItemFramePrefab;
    // Start is called before the first frame update
    private void Start()
    {
        if(InventoryManager.GetInstance().AssetState == InventoryManager.State.Initialized)
            CreateGlossar(InventoryManager.State.Initialized);
        else
            EventHandler.onInventoryManagerInitialized.AddListener(CreateGlossar);
    }

    private void CreateGlossar(InventoryManager.State state)
    {
        InventoryManager inventoryManager = InventoryManager.GetInstance();
        if (state == InventoryManager.State.Initialized)
        {
            foreach (var item in inventoryManager.GetRegisteredIds())
            {
                StaticItem staticItem = inventoryManager.GetRegisteredItem(item);
                var gameObjectItem = Instantiate(ItemFramePrefab, transform);
                gameObjectItem.name = staticItem.id;
                gameObjectItem.transform.Find("ItemName").GetComponent<TMP_Text>().text = staticItem.displayName;
                gameObjectItem.transform.Find("ItemShortDescription").GetComponent<TMP_Text>().text = staticItem.lore;
                gameObjectItem.transform.Find("ItemIcon").GetComponent<Image>().sprite = staticItem.baseTexture;
            }
        }
    }
}
