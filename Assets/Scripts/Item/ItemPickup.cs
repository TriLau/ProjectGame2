using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;

    private void Awake() // xai tam cai nay de test spawn item
    {
        InitialItem(item);
    }
    public void InitialItem(ItemData item)
    {
        GetComponent<SpriteRenderer>().sprite = item.image;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItemToTnventorySlot(item);
            Destroy(gameObject);
        }
    }
}
