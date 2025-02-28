using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : Singleton<InventoryManager>
{
    private Inventory inventory;

    public int maxStackedItems = 5;
    public List<InventorySlotUI> inventorySlotsUI;
    public GameObject inventorySlotPrefab;
    public GameObject InventoryItemPrefab;

    public int selectedSlot = -1;

    private void Start()
    {
        inventory = new Inventory();
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 10)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    public void RefreshInventory()
    {
        inventory.Print();
    }

    // Inventory Slot
    public void SpawnNewSlot(int amount)
    {
        do
        {
            GameObject slot = Instantiate(inventorySlotPrefab, this.transform);
            InventorySlot iSlot = slot.GetComponent<InventorySlot>();
            amount--;
            inventory.AddSlotToInventory(iSlot);
        } while (amount > 0);
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlotsUI[selectedSlot].Deselect();
        }

        inventorySlotsUI[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItemToTnventorySlot(Item item)
    {
        return AddItem(item, inventorySlotsUI);
    }

    public bool AddItem(Item item, List<InventorySlotUI> slots)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlotUI slot = slots[i];
            InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackedItems &&
                itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlotUI slot = slots[i];
            InventoryItemUI itemInSlot = slot.GetComponentInChildren<InventoryItemUI>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlotUI slot)
    {
        GameObject newItemGO = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItemUI inventoryItem = newItemGO.GetComponent<InventoryItemUI>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlotsUI[selectedSlot].GetSelectedSlot();
        InventoryItemUI itemInSlot = inventorySlotsUI[selectedSlot].GetComponentInChildren<InventoryItemUI>();
        
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
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
}
