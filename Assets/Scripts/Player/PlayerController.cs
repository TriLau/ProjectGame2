using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float runSpeed = 1f;
    public float vehicleSpeed;
    private string currentState;

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

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D col;
    [SerializeField]
    private VehicleController currentVehicle;

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
            if (value)
            {
                if (currentVehicle.tag == "Bicycle")
                    animator.SetBool("UseDevice", true);
                else if (currentVehicle.tag == "Horse")
                    animator.SetBool("UseHorse", true);
            }
            else
            {
                animator.SetBool("UseDevice", false);
                animator.SetBool("UseHorse", false);
            }

        }
    }

    [SerializeField]
    private bool _isPickingup = false;
    public bool IsPickingup
    {
        get { return _isPickingup; }
        private set 
        { 
            _isPickingup = value;
        }
    }

    void Start()
    {
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
                currentVehicle.SetRiding(true);
                currentVehicle.transform.SetParent(transform);
            }
            else
            {
                currentVehicle.SetRiding(false);
                currentVehicle.transform.SetParent(null);
            }
        }

        if (Input.GetMouseButtonDown(0) && IsPickingup)
        {
            animator.SetTrigger("Attack");
        }

        if (!IsRidingVehicle)
        {
            CheckAnimation();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * CurrentSpeed * Time.fixedDeltaTime);
    }

    public void SetCurrentVehicle(VehicleController vehicle)
    {
        if (IsRidingVehicle) return;

        CanRide = true;
        currentVehicle = vehicle;
    }

    public void ClearVehicle()
    {
        CanRide = false;
        currentVehicle = null;
    }

    public void SetFacing()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void OnMove()
    {
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
    }

    public void PickupItem(ItemData item)
    {
        bool result = InventoryManager.Instance.AddItemToTnventorySlot(item);
        if (result == true)
        {
            Debug.Log("Item added");
            IsPickingup = true;
        }
        else
        {
            Debug.Log("Item not added");
        }
    }

    public ItemData GetSelectedItem()
    {
        ItemData receivedItem = InventoryManager.Instance.GetSelectedItem(false);
        if (receivedItem != null)
        {
            return receivedItem;
        }

        return null;
    }

    public void UseSelectedItem()
    {
        ItemData receivedItem = InventoryManager.Instance.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item: " + receivedItem);
        }
        else
        {
            Debug.Log("Not item used");
        }
    }

    private void CheckAnimation()
    {
        ItemData item = GetSelectedItem();
        if (item != null)
        {
            IsPickingup = true;
        }
        else
        {
            IsPickingup = false;
        }

        if (IsPickingup)
        {
            switch (item.name)
            {
                case "Axe":
                    ChangeAnimationState(AnimationStrings.axeIdle);
                    break;
                case "Sword":
                    ChangeAnimationState(AnimationStrings.swordIdle);
                    break;
            }
        }

        if (!IsPickingup || InventoryManager.Instance.GetSelectedItem(false) == null)
        {
            ChangeAnimationState(AnimationStrings.idle);
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}