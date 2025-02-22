using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ERType
{
    Tree,
    Stone
}

public class EnvironmentalResource : MonoBehaviour
{
    private Animator animator;
    private Damageable damageable;

    [SerializeField]
    private ERType eRType;

    void Start()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        ChangeState();
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        animator.SetTrigger("Hit");
    }

    public void ChangeState()
    {
        if (damageable.Health == 10)
        {
            animator.Play("Root_Idle");
        }
    }
}
