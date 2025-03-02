using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public Text countText;

    public Transform parentAfterDrag;
    private InventoryItem _inventoryItem;

    public InventoryItem InventoryItem
    { get { return _inventoryItem; } }

    public void InitialiseItem(InventoryItem newItem)
    {
        _inventoryItem = newItem;
        image.sprite = newItem.Item.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = _inventoryItem.Quantity.ToString();
        bool textActive = _inventoryItem.Quantity > 1;
        countText.gameObject.SetActive(textActive);
    }

    // Drop & Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        _inventoryItem.UpdateSlotIndex(parentAfterDrag.GetComponent<UI_InventorySlot>().slotIndex);
    }
}
