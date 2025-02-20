using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : PlayerController
{
    public float vehicleSpeed = 1f;
    private Vector2 movement;
    private Vector2 lastMovement;
    private Rigidbody2D rb;
    private Animator animator;
    private Animator playerAnimator;
    private PlayerController playerController;

    [SerializeField]
    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set 
        {
            if (_isFacingRight != value)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            _isFacingRight = value;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            movement = new Vector2(moveX, moveY).normalized;

            if (movement != Vector2.zero)
            {
                lastMovement = movement;
                 
                animator.SetFloat("Horizontal", Mathf.Abs(movement.x));
                animator.SetFloat("Vertical", movement.y);

                playerAnimator.SetFloat("Horizontal", Mathf.Abs(movement.x));
                playerAnimator.SetFloat("Vertical", movement.y);
            }
            
            animator.SetFloat("Speed", movement.magnitude);
            playerAnimator.SetFloat("Speed", movement.magnitude);

            if (movement.x > 0 && !IsFacingRight) IsFacingRight = true;
            else if (movement.x < 0 && IsFacingRight) IsFacingRight = false;
        }
    }

    private void FixedUpdate()
    {
        if (isBeingRidden)
        {
            rb.MovePosition(rb.position + movement * vehicleSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player.IsRidingVehicle) return;

            playerController = player;
            playerController.SetCurrentVehicle(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player.IsRidingVehicle) return;

            playerController.ClearVehicle();
        }
    }

    public void SetRiding(bool riding)
    {
        isBeingRidden = riding;

        if (riding)
        {
            playerController.transform.position = transform.position;
            playerController.GetComponent<Collider2D>().isTrigger = true;
            playerAnimator = playerController.GetComponent<Animator>();
            animator.SetFloat("Horizontal", Mathf.Abs(playerController.LastMovement.x));
            animator.SetFloat("Vertical", playerController.LastMovement.y);
            playerController.IsFacingRight = IsFacingRight;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            playerController.transform.position = transform.position;
            playerController.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}