using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSp = 5f;

    [SerializeField] Rigidbody2D rb2D;

    [SerializeField] Animator animator;

    Vector2 movement;

    // To register input
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    //Better to use when using physics
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * moveSp * Time.fixedDeltaTime);
    }
}
