using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void Update()
    {
        transform.gameObject.GetComponent<Rigidbody2D>().velocity *= 0.8f;
    }

    private void Awake() // xai tam cai nay de test spawn item
    {
        InitialItem(item);
    }
    public void InitialItem(Item item)
    {
        GetComponent<SpriteRenderer>().sprite = item.image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItemToInventorySlot(item);
            ItemWorldManager.Instance.RemoveItemWorld(gameObject);
            Destroy(gameObject);
        }
    }
}
