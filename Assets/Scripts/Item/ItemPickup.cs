using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItemToTnventorySlot(item);
            Destroy(gameObject);
        }
    }
}
