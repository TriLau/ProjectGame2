using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle
{
    public float vehicleSpeed = 1f;

    private Vector2 movement;
    private Vector2 lastMovement;

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

    private bool isBeingRidden = false;
}
