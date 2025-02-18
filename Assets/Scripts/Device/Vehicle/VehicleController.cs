using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerController playerController;

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

    private bool isBeingRidden = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isBeingRidden)
        {
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
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
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
            playerController.vehicleSpeed = speed;
            playerController.transform.position = transform.position;
            playerController.GetComponent<Collider2D>().isTrigger = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            playerController.transform.position = transform.position;
            playerController.GetComponent<Collider2D>().isTrigger = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}