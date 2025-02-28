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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItemToTnventorySlot(item);
            Destroy(gameObject);
        }
    }
}
