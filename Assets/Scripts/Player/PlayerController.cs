using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float runSpeed = 1f;
    private float currentSpeed;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized;

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", Mathf.Abs(movement.x));
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.magnitude);
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.LeftShift));

        if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
