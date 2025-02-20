using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    private Player player;
=======
    public float walkSpeed = 1f;
    public float runSpeed = 1f;
    public float vehicleSpeed;

    [SerializeField]
    private float _currentSpeed;
    public float CurrentSpeed
    {
        get 
        {
            return _currentSpeed = IsRidingVehicle ? vehicleSpeed : IsRuning ? runSpeed : walkSpeed;
        }
    }

    public Vector2 movement;
    private Vector2 lastMovement;
    public Vector2 LastMovement
    {
        get { return lastMovement; }
    }

>>>>>>> origin/dev
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D col;
    private VehicleController curentVehicle;

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

    [SerializeField]
    private bool _canRun = true;
    public bool CanRun
    {
        get { return _canRun; }
        private set { _canRun = value; }
    }

    [SerializeField]
    private bool _isRuning = false;
    public bool IsRuning
    {
        get { return _isRuning; }
        private set { _isRuning = value; }
    }

    [SerializeField]
    private bool _canRide = false;
    public bool CanRide
    {
        get { return _canRide; }
        private set { _canRide = value; }
    }

    [SerializeField]
    private bool _isRidingVehicle = false;
    public bool IsRidingVehicle
    {
        get { return _isRidingVehicle; }
        private set
        {
            _isRidingVehicle = value;
            animator.SetBool("UseDevice", value);
        }
    }

    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (CanRun && Input.GetKey(KeyCode.LeftShift)) IsRuning = true;
        else IsRuning = false;

        OnMove();

        if (Input.GetKeyDown(KeyCode.E) && CanRide)
        {
            IsRidingVehicle = !IsRidingVehicle;

            if (IsRidingVehicle)
            {
                curentVehicle.SetRiding(true);
                curentVehicle.transform.SetParent(transform);
            }
            else
            {
                curentVehicle.SetRiding(false);
                curentVehicle.transform.SetParent(null);
            }
        }
    }

    private void FixedUpdate()
    {
<<<<<<< HEAD
        if (!IsRidingVehicle)
        {
            rb.MovePosition(rb.position + player.Movement * player.CurrentSpeed * Time.fixedDeltaTime);
        }
=======
        rb.MovePosition(rb.position + movement * CurrentSpeed * Time.fixedDeltaTime);
>>>>>>> origin/dev
    }

    public void SetCurrentVehicle(VehicleController vehicle)
    {
        if (IsRidingVehicle) return;

        CanRide = true;
        curentVehicle = vehicle;
    }

    public void ClearVehicle()
    {
        if (!IsRidingVehicle)
        {
            CanRide = false;
            curentVehicle = null;
        }
    }

    public void SetFacing()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void OnMove()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        player.Movement = new Vector2(moveX, moveY).normalized;

        if (movement != Vector2.zero) lastMovement = movement;

        animator.SetFloat("Horizontal", Mathf.Abs(lastMovement.x));
        animator.SetFloat("Vertical", lastMovement.y);

        animator.SetFloat("Speed", movement.magnitude);
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.LeftShift));

        if (movement.x > 0 && !IsFacingRight) IsFacingRight = true;
        else if (movement.x < 0 && IsFacingRight) IsFacingRight = false;
    }
}