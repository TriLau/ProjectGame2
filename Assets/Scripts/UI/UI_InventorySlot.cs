using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Sprite selectedColor, notSelectedColor;

    public int slotIndex;


    private void Awake()
    {
        Deselect();
    }

    // Select slot
    public void Select()
    {
        image.sprite = selectedColor;
    }

    public void Deselect()
    {
        image.sprite = notSelectedColor;
    }

    // Drop item into slot
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            UI_InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<UI_InventoryItem>();
            if (inventoryItem != null )
            {
                inventoryItem.parentAfterDrag = transform;
            }
        }
    }
}
