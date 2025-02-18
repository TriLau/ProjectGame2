using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float runSpeed = 1f;
    public float bicycleSpeed = 1f;
    private float currentSpeed;
    private Vector2 movement;
    private Vector2 lastMovement;
    public Vector2 Movement 
    {
        get { return lastMovement; }
    }
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
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

    [SerializeField]
    private bool _isUsingBicycle = false;
    public bool IsUsingBicycle
    {
        get { return _isUsingBicycle; }
        private set
        {
            _isUsingBicycle = value;
            animator.SetBool("UseDevice", value);
        }
    }

    [SerializeField]
    private bool canRide = false;
    public bool CanRide
    {
        get { return canRide;}
        set
        {
            canRide = value;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : IsUsingBicycle ? bicycleSpeed : walkSpeed;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized;

        if (movement != Vector2.zero) lastMovement = movement;

        animator.SetFloat("Horizontal", Mathf.Abs(lastMovement.x));
        animator.SetFloat("Vertical", lastMovement.y);

        animator.SetFloat("Speed", movement.magnitude);
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.LeftShift));

        if (movement.x > 0 && !IsFacingRight) IsFacingRight = true;
        else if (movement.x < 0 && IsFacingRight) IsFacingRight = false;
        
        if (Input.GetKeyDown(KeyCode.E) && CanRide)
        {
            IsUsingBicycle = !IsUsingBicycle;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
}
