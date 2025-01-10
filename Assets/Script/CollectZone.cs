using System.Collections.Generic;
using UnityEngine;

public class CollectZone : SpecialZone
{
    public List<InventoryItem> inventoryItems;
    public TMPro.TextMeshProUGUI text;

    private void Start()
    {
        AddInventory(ItemType.Wood, 0);
        AddInventory(ItemType.Stone, 0);
        UpdateText();
    }
    public void AddInventory(ItemType type, int quantity)
    {
        foreach (InventoryItem item in inventoryItems)
        {
            if (item.type == type)
            {
                item.quantity += quantity;
                UpdateText();
                return;
            }
        }

        InventoryItem newItem = new InventoryItem();
        newItem.type = type;
        newItem.quantity = quantity;
        inventoryItems.Add(newItem);
    }
    public override void OnActive()
    {
        base.OnActive();
    }


    private void Update()
    {
        if (soldierAssign.Count > 0)
        {
            for (int i = 0; i < soldierAssign.Count; i++) {

                foreach (InventoryItem item in soldierAssign[i].inventoryItems)
                {
                    AddInventory(item.type, item.quantity);
                }
                soldierAssign[i].inventoryItems.Clear();
            }
        }
    }

    private void UpdateText()
    {
        text.text = $"Wood : {inventoryItems[0].quantity} \n Stone : {inventoryItems[1].quantity}";
    }
}
