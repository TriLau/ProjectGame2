using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : PlayerController
{
    public float vehicleSpeed = 1f;
    public Vector2 movement;
    private Animator animator;
    private Animator playerAnimator;
    private PlayerController playerController;

    [SerializeField]
    private bool _isBeingRidden = false;
    public bool IsBeingRidden
    {
<<<<<<< HEAD
        get { return _isFacingRight; }
        set 
=======
        get { return _isBeingRidden; }
        private set
>>>>>>> origin/dev
        {
            _isBeingRidden = value;
            animator.SetBool("IsRiding", value);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Horizontal", Mathf.Abs(movement.x));
        animator.SetFloat("Vertical", movement.y);
    }

    void Update()
    {
        if (IsBeingRidden)
        {
            if (playerController.movement != Vector2.zero)
            {                 
                animator.SetFloat("Horizontal", Mathf.Abs(playerController.movement.x));
                animator.SetFloat("Vertical", playerController.movement.y);

                playerAnimator.SetFloat("Horizontal", Mathf.Abs(playerController.movement.x));
                playerAnimator.SetFloat("Vertical", playerController.movement.y);
            }
            
            animator.SetFloat("Speed", playerController.movement.magnitude);
            playerAnimator.SetFloat("Speed", playerController.movement.magnitude);
        }
    }

    private void FixedUpdate()
    {
        
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
        IsBeingRidden = riding;

        if (riding)
        {
            playerController.transform.position = transform.position;
            playerController.GetComponent<Collider2D>().isTrigger = true;
            playerAnimator = playerController.GetComponent<Animator>();
            transform.localScale = playerController.transform.localScale;
            animator.SetFloat("Horizontal", Mathf.Abs(playerController.LastMovement.x));
            animator.SetFloat("Vertical", playerController.LastMovement.y);
<<<<<<< HEAD
            playerController.IsFacingRight = IsFacingRight;
            rb.bodyType = RigidbodyType2D.Dynamic;
=======
            playerController.vehicleSpeed = vehicleSpeed;
>>>>>>> origin/dev
        }
        else
        {
            playerController.transform.position = transform.position;
            playerController.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}