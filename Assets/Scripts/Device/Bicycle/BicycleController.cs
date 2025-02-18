using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleController : MonoBehaviour
{
    private float speed;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private GameObject player;
    private PlayerController playerController;
    private Collider2D col;

    private bool isRiding = false;
    private bool canRide = false;
    public bool CanRide
    {
        get { return canRide; }
        private set
        {
            canRide = value;
            playerController.CanRide = value;
        }
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set
        {
            if (_isFacingRight != value)
            {
                _isFacingRight = value;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canRide)
        {
            isRiding = !isRiding;

            if (isRiding)
            {
                col.isTrigger = true;
                player.transform.position = transform.position;
            }
            else
            {
                col.isTrigger= false;
                transform.position = player.transform.position; 
            }
        }

        if (isRiding)
        {
            speed = playerController.bicycleSpeed;
            IsFacingRight = playerController.IsFacingRight;

            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            movement = new Vector2(moveX, moveY).normalized;

            animator.SetFloat("Horizontal", Mathf.Abs(playerController.Movement.x));
            animator.SetFloat("Vertical", playerController.Movement.y);

            animator.SetFloat("Speed", movement.magnitude);

            if (movement.x > 0 && !IsFacingRight) IsFacingRight = true;
            else if (movement.x < 0 && IsFacingRight) IsFacingRight = false;
        }
    }

    private void FixedUpdate()
    {
        if (isRiding)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerController = collision.GetComponent<PlayerController>();
            col = collision;
            CanRide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanRide = false;
        }
    }
}
