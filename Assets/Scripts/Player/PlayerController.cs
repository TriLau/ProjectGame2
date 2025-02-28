using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float runSpeed = 1f;
    public float vehicleSpeed;
    private string currentState;

    [SerializeField]
    private bool _canMove = true;
    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }

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
    private Player player;
    
    [SerializeField]
    private VehicleController _currentVehicle;
    public VehicleController CurrentVehicle
    {
        get { return _currentVehicle; }
        private set { _currentVehicle = value; }
    }

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
                if (CurrentVehicle.tag == "Bicycle")
                    animator.SetBool("UseDevice", true);
                else if (CurrentVehicle.tag == "Horse")
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
    private bool _isHoldingItem = false;
    public bool IsHoldingItem
    {
        get { return _isHoldingItem; }
        private set
        {
            _isHoldingItem = value;
        }
    }

    [SerializeField]
    private bool _canAttack = true;
    public bool CanAttack
    {
        get { return _canAttack; }
        private set { _canAttack = value; }
    }

    [SerializeField]
    private bool _canSleep = false;
    public bool CanSleep
    {
        get { return _canSleep; }
        private set { _canSleep = value; }
    }

    [SerializeField]
    private bool _isSleeping = false;
    public bool IsSleeping
    {
        get { return _isSleeping; }
        private set { _isSleeping = value; }
    }

    [SerializeField]
    private BedScript _currentBed;
    public BedScript CurrentBed
    {
        get { return _currentBed; }
        private set { _currentBed = value; }
    }

    [SerializeField]
    private bool _hadTarget;
    public bool HadTarget
    {
        get { return _hadTarget; }
        private set { _hadTarget = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        player = new Player();
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
                CurrentVehicle.SetRiding(true);
                CurrentVehicle.transform.SetParent(transform);
            }
            else
            {
                CurrentVehicle.SetRiding(false);
                CurrentVehicle.transform.SetParent(null);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && CanSleep)
        {
            if (IsSleeping)
            {
                CanMove = true;
                IsSleeping = !IsSleeping;
                CurrentBed.SetSleep(IsSleeping);
                animator.SetBool(AnimationStrings.isSleep, false);
            }
            else
            {
                animator.SetBool(AnimationStrings.isSleep, true);
                CanMove = false;
                IsSleeping = !IsSleeping;
                CurrentBed.SetSleep(IsSleeping);
            }
        }

        if (IsHoldingItem && CanAttack && !IsSleeping)
        {
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(AttackRoutine());
            }
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

    // ============== Bed Stuff =============
    public void SetCurrentBed(BedScript bed)
    {
        if (HadTarget) return;
        HadTarget = true;
        CanSleep = true;
        CurrentBed = bed;
        CurrentBed.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void ClearBed()
    {
        HadTarget = false;
        CanSleep = false;
        CurrentBed.GetComponent<SpriteRenderer>().color = Color.white;
        CurrentBed = null;

    }

    // ============= Vehicle ================
    public void SetCurrentVehicle(VehicleController vehicle)
    {
        if (IsRidingVehicle || HadTarget) return;
        HadTarget = true;
        CanRide = true;
        CurrentVehicle = vehicle;
        CurrentVehicle.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void ClearVehicle()
    {
        HadTarget = false;
        CanRide = false;
        CurrentVehicle.GetComponent<SpriteRenderer>().color = Color.white;
        CurrentVehicle = null;
    }

    // ============== Movement ==================
    public void SetFacing()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void OnMove()
    {
        if (!CanMove) return;
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

    // =================== Item ======================
    public void PickupItem(Item item)
    {
        bool result = InventoryManager.Instance.AddItemToTnventorySlot(item);
        if (result == true)
        {
            Debug.Log("Item added");
            IsHoldingItem = true;
        }
        else
        {
            Debug.Log("Item not added");
        }
    }

    public Item GetSelectedItem()
    {
        Item receivedItem = InventoryManager.Instance.GetSelectedItem(false);
        if (receivedItem != null)
        {
            return receivedItem;
        }

        return null;
    }

    public void UseSelectedItem()
    {
        Item receivedItem = InventoryManager.Instance.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item: " + receivedItem);
        }
        else
        {
            Debug.Log("Not item used");
        }
    }

    // ===================== Animation =====================
    private void CheckAnimation()
    {
        Item item = GetSelectedItem();
        if (item != null)
        {
            IsHoldingItem = true;
        }
        else
        {
            IsHoldingItem = false;
        }

        if (IsHoldingItem)
        {
            ChangeAnimationState(item.name);
        }

        if (!IsHoldingItem || InventoryManager.Instance.GetSelectedItem(false) == null)
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

    private IEnumerator AttackRoutine()
    {
        CanAttack = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        CanAttack = true;
    }
}