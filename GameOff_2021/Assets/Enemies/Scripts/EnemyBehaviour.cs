using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    private float waitTime;
    [SerializeField] float startWaitTime;

    [SerializeField] Transform[] moveSpots;
    [SerializeField] Animator animator;

    private int randomSpot;
    private float horizontal = 0;
    private float vertical = 0;

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        CheckOrientation();

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void CheckOrientation()
    {
        if (moveSpots[randomSpot].position.x > transform.position.x)
        {
            horizontal = 1f;
        }
        else if (moveSpots[randomSpot].position.x < transform.position.x)
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 0f;
        }

        if (moveSpots[randomSpot].position.y > transform.position.y)
        {
            vertical = 1f;
        }
        else if (moveSpots[randomSpot].position.y < transform.position.y)
        {
            vertical = -1f;
        }
        else
        {
            vertical = 0f;
        }

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }
}
