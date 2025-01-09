using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public PlayerGroupState state;

    public int cargoCapacity;
    public List<InventoryItem> inventoryItems;
    public void AddInventory(ItemType type, int quantity)
    {
        int cargoMax = quantity;
        int quantityToAdd;
        foreach (InventoryItem item in inventoryItems)
        {
            cargoMax += item.quantity;
        }


        if (cargoMax > cargoCapacity)
            return;
        else if (cargoMax > cargoCapacity)
        {
            quantityToAdd = cargoMax - cargoCapacity;
        }
        else
            quantityToAdd = quantity;



        foreach (InventoryItem item in inventoryItems) {

            if (item.type == type) {
                item.quantity += quantityToAdd;
                return;
            }
        }

        InventoryItem newItem = new InventoryItem();
        newItem.type = type;
        newItem.quantity = quantity;
        inventoryItems.Add(newItem);
    }
}



[Serializable]
public class InventoryItem
{
    public ItemType type;
    public int quantity;
}
