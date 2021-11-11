using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum enemies {slime, bee, spider, bat, skeleton_base, skeleton_mage};

    public enemies enemyType;

    [SerializeField] float moveSpeed;
    float initMoveSpeed;
    Rigidbody2D rb2D;
    BoxCollider2D boxCol;
    Animator animator;
    [SerializeField] bool playerIsInRange;
    [SerializeField] GameObject magicSignalPrefab;
    private GameObject magicSignal;
    GameObject mageObjective;
    [SerializeField] GameObject fireColumnPrefab;
    private GameObject fireColumn;

    float castTime;
    [SerializeField] float castTimeStopFollow;
    [SerializeField] float maxCastTime;

    bool canAttack = true;
    bool isCasting = false;
    int layerMask;

    [SerializeField] GameObject[] teleportPositions;
    GameObject chosenPosition;
    GameObject lastChosenPosition;

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
                    magicSignal.transform.position = new Vector2(mageObjective.transform.position.x, hit.point.y);
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
        TeleportAnim();
        Invoke("NewAttack", 4.5f);
    }

    private void TeleportAnim()
    {
        animator.SetBool("Teleport", true);
    }

    public void Teleport()
    {
        if(lastChosenPosition == null)
        {
            chosenPosition = teleportPositions[Random.Range(0, teleportPositions.Length)];
            lastChosenPosition = chosenPosition;
            transform.position = chosenPosition.transform.position;
            animator.SetBool("Teleport", false);
        }

        else
        {
            while(chosenPosition == lastChosenPosition)
            {
                chosenPosition = teleportPositions[Random.Range(0, teleportPositions.Length)];
            }
                lastChosenPosition = chosenPosition;
                transform.position = chosenPosition.transform.position;
                animator.SetBool("Teleport", false);   
        }
        
    }

    private void NewAttack()
    {
        canAttack = true;
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Efficient
        transform.localScale = new Vector2(-(Mathf.Sign(rb2D.velocity.x)), transform.localScale.y);
    }
}
