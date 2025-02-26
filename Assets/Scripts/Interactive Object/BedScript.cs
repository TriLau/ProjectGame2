using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private bool _isBeingUsed = false;
    public bool IsBeingUsed
    {
        get { return _isBeingUsed; }
        private set
        {
            _isBeingUsed = value;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSleep(bool toUse)
    {
        IsBeingUsed = toUse;
        if (toUse)
        {
            playerController.transform.SetParent(transform);
            playerController.transform.position = transform.position;
            playerController.GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            playerController.transform.SetParent(null);
            playerController.GetComponent<Collider2D>().isTrigger = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("Player"))
        {
            if (IsBeingUsed) return;
            playerController = collision.collider.GetComponent<PlayerController>();
            playerController.SetCurrentBed(this);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerController.ClearBed();
            playerController = null;
        }
    }
}
