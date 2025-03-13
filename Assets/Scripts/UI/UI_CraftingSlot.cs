using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EGrid
{
    Grid2x2,
    Grid3x3
}

public class UI_CraftingSlot : MonoBehaviour, IDropHandler
{
    public int i, j;

    public static event Action<int, int, Item> OnCraftingSlotAdded;

    public void OnDrop(PointerEventData eventData)
    {
        UI_InventoryItem draggedItem = eventData.pointerDrag.GetComponent<UI_InventoryItem>();

        if (draggedItem == null) return;

        if (transform.childCount > 0)
        {
            UI_InventoryItem existingItem = transform.GetChild(0).GetComponent<UI_InventoryItem>();

            if (existingItem.InventoryItem.Item.itemName == draggedItem.InventoryItem.Item.itemName &&
                existingItem.InventoryItem.Item.stackable &&
                existingItem.InventoryItem.Quantity < existingItem.InventoryItem.MaxStack)
            {
                existingItem.InventoryItem.IncreaseQuantity(draggedItem.InventoryItem.Quantity);
                existingItem.RefreshCount();

                Destroy(draggedItem.gameObject);
                return;
            }
        }

        draggedItem.parentAfterDrag = transform;

        OnCraftingSlotAdded?.Invoke(i, j, draggedItem.InventoryItem.Item);
    }

    public Item GetItem()
    {
        UI_InventoryItem existingItem = transform.GetComponentInChildren<UI_InventoryItem>();
        return existingItem != null ? existingItem.InventoryItem.Item : null;
    }
}
