using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemManager : Singleton<CraftingSystemManager>
{
    public GameObject outputSlot;
    public List<Recipe> recipes;
    public GameObject itemPrefab;

    private Item[,] grid = new Item[3, 3];

    private void OnEnable()
    {
        UI_CraftingSlot.OnCraftingSlotAdded += AddItemToGrid;
        UI_InventoryItem.ItemOnDrag += RemoveItemToGrid;
    }

    private void OnDisable()
    {
        UI_CraftingSlot.OnCraftingSlotAdded -= AddItemToGrid;
        UI_InventoryItem.ItemOnDrag -= RemoveItemToGrid;
    }

    public void AddItemToGrid(int i, int j, Item item)
    {
        grid[i, j] = item;
        CheckRecipe();
    }

    public void RemoveItemToGrid(int i, int j, Item item)
    {
        UI_InventoryItem uI_InventoryItem = outputSlot.GetComponentInChildren<UI_InventoryItem>();
        if (uI_InventoryItem != null)
        {
            grid[i, j] = null;
            Destroy(uI_InventoryItem.gameObject);
        }
        else return;    
    }

    public void CheckRecipe()
    {
        if (grid == null) return;

        foreach (Recipe recipe in recipes)
        {
            bool completeRecipe = true;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var slotItem = grid[i, j];
                    if (slotItem == null) Debug.Log($"{i}{j} Ko co slotitem");
                    else Debug.Log($"{i}{j}co slot slotitem");

                    var recipeItem = recipe.GetItem(i, j);
                    if (recipeItem == null) Debug.Log($"{i}{j} Ko co recipeitem");
                    else Debug.Log($"{i}{j}co slot recipeitem");

                    if ((slotItem == null && recipeItem != null) ||
                        (slotItem != null && recipeItem == null) ||
                        (slotItem != null && recipeItem != null &&
                        slotItem.itemName != recipeItem.itemName))
                    {
                        completeRecipe = false;
                        Debug.Log("Sai cong thuc");
                        break;
                    }
                }

                if (!completeRecipe) break;
            }

            if (completeRecipe)
            {
                Debug.Log("Dung cong thuc");
                CreateItem(recipe.itemOutput);
                return;
            }
        }
    }

    public void CreateItem(Item item)
    {
        if (item == null) return;
       
        if (outputSlot.transform.childCount > 0) return;

        UI_InventorySlot slot = outputSlot.GetComponent<UI_InventorySlot>();
        InventoryItem inventoryItem = new InventoryItem(System.Guid.NewGuid().ToString(), item, slot.slotIndex);
        GameObject newItem = Instantiate(itemPrefab, outputSlot.transform);
        UI_InventoryItem inventoryItemUI = newItem.GetComponent<UI_InventoryItem>();
        inventoryItemUI.InitialiseItem(inventoryItem);
    }

    public void ChangeListToGrid(List<UI_CraftingSlot> list, UI_CraftingSlot[,] grid)
    {
        int t = 0;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (t < list.Count)
                {
                    grid[i, j] = list[t];
                    t++;
                }
            }
        }
    }
}
