using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] int jumpMax;
    [SerializeField] int maxHP;
    [SerializeField] GameObject swordHitBox;
    [SerializeField] GameObject lastCheckpoint;
    Rigidbody2D rb2D;
    private int currentHP;
    private int jumpCount;
    private float initMovSpeed;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        jumpCount = jumpMax;
        initMovSpeed = movSpeed;
        swordHitBox.SetActive(false);
        currentHP = maxHP;
    }

    void FixedUpdate()
    {
        //Move

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

        //Die

        if (currentHP <= 0)
        {
            GetComponent<Animator>().SetTrigger("Die");
            movSpeed = 0;
            Invoke("ReviveInCheckPoint", 5f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
        {
            //Jump Start
            GetComponent<Animator>().SetBool("Jump", true);
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Attack
            GetComponent<Animator>().SetBool("Attack", true);
            movSpeed = 0;
            swordHitBox.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            //End Jump
            GetComponent<Animator>().SetBool("Jump", false);
            jumpCount = jumpMax;
        }
    }

    public void EndAttackAnimation()
    {
        swordHitBox.SetActive(false);
        GetComponent<Animator>().SetBool("Attack", false);
        movSpeed = initMovSpeed;
    }

    void PlayerTakeDamage()
    {
        currentHP--;
    }

    void ReviveInCheckPoint()
    {
        transform.position = lastCheckpoint.transform.position;
        currentHP = maxHP;
        movSpeed = initMovSpeed;
    }
}
