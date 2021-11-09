using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movSpeed;
    [SerializeField] float jumpForce;
    Rigidbody2D rb2D;
    private int jumpCount;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(inputX * movSpeed * Time.fixedDeltaTime, 0f, 0f), Space.World);

        if (inputX < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if (inputX > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }

        ContactFilter2D filter;
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.1f, filter, RaycastHit2D[]);
        {

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
        {
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }
    }
}
