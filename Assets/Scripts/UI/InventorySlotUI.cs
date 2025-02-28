using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    private InventorySlot _slot;

    public Image image;
    public Sprite selectedColor, notSelectedColor;

    public InventorySlot Slot
    { get { return _slot; } }

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.sprite = selectedColor;
    }

    public void Deselect()
    {
        image.sprite = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItemUI inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

    public InventorySlot GetSelectedSlot()
    {
        return _slot;
    }
}
