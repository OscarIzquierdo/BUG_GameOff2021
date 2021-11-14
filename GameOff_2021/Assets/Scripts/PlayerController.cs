using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Float")]
    [SerializeField] float movSpeed;
    [SerializeField] float jumpForce;
    [Header("Int")]
    [SerializeField] int jumpMax;
    [SerializeField] int maxHP;
    [Header("GameObject")]
    [SerializeField] GameObject swordHitBoxPrefab;
    [SerializeField] GameObject swordHitBoxSpawn;
    [SerializeField] GameObject lastCheckpoint;
    [Header("Interface")]
    [SerializeField] GameObject[] hP_Points;

    private Rigidbody2D rb2D;
    private GameObject swordHitBox;

    private int currentHP;
    private int jumpCount;
    private int hpArrayCount;
    
    private float initMovSpeed;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("Die", false);
        jumpCount = jumpMax;
        initMovSpeed = movSpeed;
        currentHP = maxHP;
        hpArrayCount = 0;
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

        //Reduce HP in GUI

        if (currentHP == 2)
        {
            hP_Points[0].SetActive(false);
        }

        if (currentHP == 1)
        {
            hP_Points[1].SetActive(false);
        }

        if (currentHP == 0)
        {
            hP_Points[2].SetActive(false);
        }

        //Die

        if (currentHP <= 0)
        {
            GetComponent<Animator>().SetBool("isRevive", false);
            GetComponent<Animator>().SetBool("Die", true);
            movSpeed = 0;
            Invoke("ReviveInCheckPoint", 5f);
        }
        else
        {
            GetComponent<Animator>().SetBool("Die", false);
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
            swordHitBox = Instantiate(swordHitBoxPrefab, swordHitBoxSpawn.transform.position,Quaternion.identity);
            GetComponent<Animator>().SetBool("Attack", true);
            movSpeed = 0;
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
        
        GetComponent<Animator>().SetBool("Attack", false);
        movSpeed = initMovSpeed;
        Destroy(swordHitBox);
    }

    void PlayerTakeDamage()
    {
        currentHP--;
        Debug.Log(currentHP + " HP left");
    }

    void ReviveInCheckPoint()
    {
        GetComponent<Animator>().SetBool("isRevive", true);
        transform.position = lastCheckpoint.transform.position;
        currentHP = maxHP;
        movSpeed = initMovSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerTakeDamage" && currentHP > 0)
        {
            PlayerTakeDamage();
        }
    }
}
