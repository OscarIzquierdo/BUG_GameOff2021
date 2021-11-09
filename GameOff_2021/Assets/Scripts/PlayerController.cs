using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] int jumpMax;
    Rigidbody2D rb2D;
    private int jumpCount;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        jumpCount = jumpMax;
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

        if (inputX != 0)
        {
            GetComponent<Animator>().SetBool("Moving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Moving", false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
        {
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpCount = jumpMax;
        }
    }
}
