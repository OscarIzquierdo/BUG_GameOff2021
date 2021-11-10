using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum enemies {slime, bee, spider, bat, skeleton_base, skeleton_mage, dead};
    [Header("Type of Enemy")]
    public enemies enemyType;
    [Header("EnemyStats")]
    [SerializeField] float enemyMaxHP;
    [Header("Float")]
    [SerializeField] float moveSpeed;
    [SerializeField] float castTimeStopFollow;
    [SerializeField] float maxCastTime;
    [Header("Bool")]
    [SerializeField] bool playerIsInRange;
    [Header("GameObject")]
    [SerializeField] GameObject magicSignalPrefab;
    [SerializeField] GameObject fireColumnPrefab;

    Rigidbody2D rb2D;
    BoxCollider2D boxCol;
    Animator animator;
    private GameObject mageObjective;
    private GameObject magicSignal;
    private GameObject fireColumn;
    private float initMoveSpeed;
    private float castTime;
    private float currentHP;
    private bool canAttack = true;
    private bool isCasting = false;
    private int layerMask;

    public bool PlayerIsInRange { get => playerIsInRange; set => playerIsInRange = value; }

    private void Start()
    {
        initMoveSpeed = moveSpeed;
        rb2D = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        PlayerIsInRange = false;
        mageObjective = GameObject.Find("MageObjective");
        layerMask = LayerMask.GetMask("Floor");
        currentHP = enemyMaxHP;
        
    }

    private void FixedUpdate()
    {
        if (enemyType == enemies.slime)
        {
            SlimeBehaviour();
        }
        else if (enemyType == enemies.bee)
        {
            BeeBehaviour();
        }
        else if (enemyType == enemies.spider)
        {
            SpiderBehaviour();
        }
        else if (enemyType == enemies.bat)
        {
            Debug.Log("Murcierlago");
        }
        else if (enemyType == enemies.skeleton_base)
        {
            Debug.Log("Esqueleto");
        }
        else if (enemyType == enemies.skeleton_mage)
        {
            SkeletonMageBehaviour();
        }
        else if (enemyType == enemies.dead)
        {
            moveSpeed = 0;
        }
    }

    void SlimeBehaviour()
    {
        if (isFacingRight())
        {
            //Move Right
            rb2D.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            //Move Left
            rb2D.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    void BeeBehaviour()
    {
        if (!PlayerIsInRange)
        {
            if (isFacingRight())
            {
                //Move Right
                rb2D.velocity = new Vector2(moveSpeed, 0f);
            }
            else
            {
                //Move Left
                rb2D.velocity = new Vector2(-moveSpeed, 0f);
            }
        }
        else
        {
            moveSpeed = 0f;
            animator.SetBool("Attack", true);
        }
    }

    void SpiderBehaviour()
    {
        if (!PlayerIsInRange)
        {
            if (isFacingRight())
            {
                //Move Right
                rb2D.velocity = new Vector2(moveSpeed, 0f);
            }
            else
            {
                //Move Left
                rb2D.velocity = new Vector2(-moveSpeed, 0f);
            }
        }
        else
        {
            moveSpeed = 0f;
            animator.SetBool("Attack", true);
        }
    }

    void SkeletonMageBehaviour()
    {
        if (PlayerIsInRange)
        {
           
            
            RaycastHit2D hit = Physics2D.Raycast(mageObjective.transform.position, -Vector2.up, Mathf.Infinity, layerMask, -Mathf.Infinity, Mathf.Infinity);

            if(magicSignal != null)
            {              
                castTime += Time.fixedDeltaTime;

                if(castTime <= castTimeStopFollow)
                {
                    magicSignal.transform.position = new Vector2(mageObjective.transform.position.x, magicSignal.transform.position.y);
                }
                if (castTime >= maxCastTime)
                {
                    castTime = 0.0f;
                    animator.SetBool("RangedAttack", false);
                    FireColumn();
                    magicSignal.transform.position = new Vector2(magicSignal.transform.position.x, magicSignal.transform.position.y);
                }
            }

            if (hit.collider != null && hit.collider.tag == "Floor" && !isCasting && fireColumn == null && canAttack)
            {
                animator.SetBool("RangedAttack", true);
                magicSignal = Instantiate(magicSignalPrefab, hit.point, Quaternion.identity);
                canAttack = false;
            }
            Debug.DrawRay(mageObjective.transform.position, -Vector2.up, Color.green);    
        }
    }

    public void AttackAnimationEnded()
    {
        animator.SetBool("Attack", false);
        moveSpeed = initMoveSpeed;
    }

    public void Casting()
    {
        isCasting = true;
    }

    public void StopCasting()
    {
        isCasting = false;
    }

    public void FireColumn()
    {
        fireColumn = Instantiate(fireColumnPrefab, magicSignal.transform.position, Quaternion.identity);
        Destroy(magicSignal.gameObject);
        Destroy(fireColumn, 3.0f);
        Invoke("NewAttack", 4.5f);
    }

    private void NewAttack()
    {
        canAttack = true;
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    public void TakeDamage()
    {
        currentHP--;
        if (currentHP <= 0)
        {
            enemyType = enemies.dead;
            animator.SetBool("Die", true);
            GetComponent<BoxCollider2D>().enabled = false;
            //GetComponentInChildren<CircleCollider2D>().enabled = false;
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Efficient
        transform.localScale = new Vector2(-(Mathf.Sign(rb2D.velocity.x)), transform.localScale.y);
    }
}
