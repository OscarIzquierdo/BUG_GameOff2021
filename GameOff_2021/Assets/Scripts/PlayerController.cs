using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Float")]
    [SerializeField] float movSpeed;
    [SerializeField] float jumpForce;
    //[SerializeField] float gravity;
    [Header("Int")]
    [SerializeField] int jumpMax;
    [SerializeField] int maxHP;
    [Header("GameObject")]
    [SerializeField] GameObject swordHitBoxPrefab;
    //[SerializeField] GameObject swordHitBoxSpawn;
    [SerializeField] GameObject lastCheckpoint;
    [Header("Interface")]
    [SerializeField] GameObject[] hP_Points;
    [Header("AudioSource")]
    [SerializeField] AudioSource OverworldSFX;
    [SerializeField] AudioSource CaveSFX;
    [Header("AudioClip")]
    [SerializeField] AudioClip[] steps;
    [SerializeField] AudioClip[] sword;


    private Rigidbody2D rb2D;
    private GameObject swordHitBox;

    private int currentHP;
    private int jumpCount;
    private int hpArrayCount;

    private float initMovSpeed;

    private bool secondFragment = false;
    private bool isGrounded = true;
    private bool goInCave = false;

    public bool SecondFragment { get => secondFragment; set => secondFragment = value; }
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public bool GoInCave { get => goInCave; set => goInCave = value; }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("Die", false);
        jumpCount = jumpMax;
        initMovSpeed = movSpeed;
        CurrentHP = MaxHP;
        hpArrayCount = 0;
    }

    void FixedUpdate()
    {
        //Reduce HP in GUI

        if (secondFragment && CurrentHP == MaxHP)
        {
            hP_Points[0].SetActive(true);
            hP_Points[1].SetActive(true);
            hP_Points[2].SetActive(true);
        }

        if (secondFragment && CurrentHP == 2)
        {
            hP_Points[0].SetActive(false);
            hP_Points[1].SetActive(true);
            hP_Points[2].SetActive(true);
        }

        if (secondFragment && CurrentHP == 1)
        {
            hP_Points[0].SetActive(false);
            hP_Points[1].SetActive(false);
            hP_Points[2].SetActive(true);
        }

        if (secondFragment && CurrentHP == 0)
        {
            hP_Points[0].SetActive(false);
            hP_Points[1].SetActive(false);
            hP_Points[2].SetActive(false);
        }

        //Die

        if (CurrentHP <= 0)
        {
            GetComponent<Animator>().SetBool("isRevive", false);
            GetComponent<Animator>().SetBool("Die", true);
            movSpeed = 0;
            isGrounded = true;
            //Invoke("ReviveInCheckPoint", 5f);
        }
        else
        {
            GetComponent<Animator>().SetBool("Die", false);
        }
    }

    private void Update()
    {
        if (GoInCave)
        {
            CaveSFX.enabled = true;
            OverworldSFX.enabled = false;
        }
        else
        {
            OverworldSFX.enabled = true;
            CaveSFX.enabled = false;
        }


        //Move

        float inputX = Input.GetAxisRaw("Horizontal");

        rb2D.velocity = new Vector2(inputX * movSpeed, rb2D.velocity.y);

        //transform.Translate(new Vector3(inputX * movSpeed * Time.deltaTime, speedY * Time.deltaTime, 0f), Space.World);

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

        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
        {
            //Jump Start
            GetComponent<Animator>().SetBool("Jump", true);
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
            isGrounded = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Attack
            //swordHitBox = Instantiate(swordHitBoxPrefab, swordHitBoxSpawn.transform.position,Quaternion.identity);
            swordHitBoxPrefab.SetActive(true);
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
            isGrounded = true;
        }


    }

    public void EndAttackAnimation()
    {

        GetComponent<Animator>().SetBool("Attack", false);
        movSpeed = initMovSpeed;
        swordHitBoxPrefab.SetActive(false);
        //Destroy(swordHitBox);
    }

    void PlayerTakeDamage()
    {
        CurrentHP--;
        Debug.Log(CurrentHP + " HP left");
    }

    public void ReviveInCheckPoint()
    {
        GetComponent<Animator>().SetBool("isRevive", true);
        transform.position = lastCheckpoint.transform.position;
        CurrentHP = MaxHP;
        movSpeed = initMovSpeed;
    }

    public void PlayStepSound()
    {
        GetComponent<AudioSource>().clip = steps[Random.Range(0, steps.Length)];
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void PlaySwordSFX()
    {
        GetComponent<AudioSource>().clip = sword[Random.Range(0, sword.Length)];
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerTakeDamage" && CurrentHP > 0)
        {
            PlayerTakeDamage();
        }

        if (collision.tag == "CheckPoint")
        {
            lastCheckpoint = collision.gameObject;
            lastCheckpoint.GetComponentInChildren<Animator>().SetTrigger("Checked");
        }
    }
}
