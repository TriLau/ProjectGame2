using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("UI")]
    public Image image;
    public Text countText;

    public Transform parentAfterDrag;
    private InventoryItem _inventoryItem;

    public InventoryItem InventoryItem => _inventoryItem;

    public static Action<int, int, Item> ItemOnDrag;

    public void InitialiseItem(InventoryItem newItem)
    {
        _inventoryItem = newItem;
        image.sprite = newItem.Item.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = _inventoryItem.Quantity.ToString();
        countText.gameObject.SetActive(_inventoryItem.Quantity > 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SplitItem();
        }
    }

    private void SplitItem()
    {
        if (_inventoryItem.Quantity > 1)
        {
            int halfQuantity = _inventoryItem.Quantity / 2;

            _inventoryItem.DecreaseQuantity(halfQuantity);
            RefreshCount();

            UI_InventorySlot availableSlot = InventoryManager.Instance.GetEmptySlot();
            if (availableSlot == null)
            {
                return;
            }

            string newId = System.Guid.NewGuid().ToString();
            InventoryItem newItem = new InventoryItem(newId, _inventoryItem.Item, availableSlot.slotIndex, halfQuantity);

            InventoryManager.Instance.AddItemToEmptySlot(newItem, availableSlot);
        }
    }

    // Drop & Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        UI_CraftingSlot uI_CraftingSlot = parentAfterDrag.GetComponent<UI_CraftingSlot>();
        if (uI_CraftingSlot != null)
        {
            ItemOnDrag?.Invoke(uI_CraftingSlot.i, uI_CraftingSlot.j, _inventoryItem.Item);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if (parentAfterDrag.GetComponent<UI_InventorySlot>() != null)
        {
            _inventoryItem.UpdateSlotIndex(parentAfterDrag.GetComponent<UI_InventorySlot>().slotIndex);
        }
    }
}
