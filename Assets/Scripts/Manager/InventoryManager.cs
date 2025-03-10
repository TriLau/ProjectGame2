using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : Singleton<InventoryManager>, IDataPersistence
{
    private Inventory inventory;
    
    
    public UI_Inventory inventoryUI;

    private static int selectedSlot = -1;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number <= 9)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventoryUI.inventorySlotsUI[selectedSlot].Deselect();
        }

        inventoryUI.inventorySlotsUI[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItemToInventorySlot(ItemWorld item)
    {
        for (int i = 0; i < inventoryUI.inventorySlotsUI.Count; i++)
        {
            UI_InventorySlot slotUI = inventoryUI.inventorySlotsUI[i];
            UI_InventoryItem itemUI = slotUI.GetComponentInChildren<UI_InventoryItem>();

            if (itemUI != null &&
                itemUI.InventoryItem.Item == item.Item &&
                itemUI.InventoryItem.Quantity < itemUI.InventoryItem.MaxStack &&
                itemUI.InventoryItem.Item.stackable == true)
            {
                itemUI.InventoryItem.AddQuantity(1);
                itemUI.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventoryUI.inventorySlotsUI.Count; i++)
        {
            UI_InventorySlot slotUI = inventoryUI.inventorySlotsUI[i];
            UI_InventoryItem itemUI = slotUI.GetComponentInChildren<UI_InventoryItem>();

            if (itemUI == null && inventory.AddItemToInventory(item.Id, item.Item, slotUI.slotIndex))
            {
                InventoryItem inventoryItem = inventory.GetInventoryItemOfIndex(slotUI.slotIndex);
                inventoryUI.AddItemToInventoryUI(inventoryItem, slotUI.slotIndex);
                return true;
            }
        }

        return false;
    }

    public Item GetSelectedItem(bool use)
    {
        UI_InventorySlot slot = inventoryUI.inventorySlotsUI[selectedSlot];
        UI_InventoryItem itemInSlot = slot.GetComponentInChildren<UI_InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.InventoryItem.Item;
            if (use == true)
            {
                itemInSlot.InventoryItem.AddQuantity(-1);
                if (itemInSlot.InventoryItem.Quantity <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }

        return null;
    }

    public void LoadData(GameData gameData)
    {
        inventory = gameData.InventoryData;
        ItemDatabase.Instance.SetItem(inventory.InventoryItemList);
        inventoryUI.UpdateSlotUI(inventory);
        ChangeSelectedSlot(0);
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.SetInventoryData(inventory);
    }
}
