using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player
{
    public float walkSpeed = 1f;
    public float runSpeed = 1f;

    [SerializeField]
    private float _currentSpeed;
    public float CurrentSpeed
    {
        get
        {
            return _currentSpeed = IsRuning ? runSpeed : walkSpeed;
        }
    }

    [SerializeField]
    private Vector2 _movement;
    public Vector2 Movement 
    { 
        get { return _movement; } 
        private set { _movement = value; }
    }

    private Vector2 lastMovement;
    public Vector2 LastMovement
    {
        get { return lastMovement; }
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
        }
    }

    public void SetMovement(Vector2 movement)
    {
        Movement = movement;
    }
}
