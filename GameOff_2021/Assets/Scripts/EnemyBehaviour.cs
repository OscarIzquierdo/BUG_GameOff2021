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

    GameObject player;

    public bool PlayerIsInRange { get => playerIsInRange; set => playerIsInRange = value; }

    private void Start()
    {
        initMoveSpeed = moveSpeed;
        rb2D = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        PlayerIsInRange = false;
        player = GameObject.FindGameObjectWithTag("Player");
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
            animator.SetBool("RangedAttack", true);

            animator.SetBool("RangedAttack", false);

        }
    }

    public void AttackAnimationEnded()
    {
        animator.SetBool("Attack", false);
        moveSpeed = initMoveSpeed;
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
