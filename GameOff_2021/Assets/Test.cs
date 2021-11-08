using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public enum enemigos {limo, avispa, araña, murcielago, esqueleto};

    public enemigos tipoEnemigo;

    [SerializeField] float moveSpeed;
    Rigidbody2D rb2D;
    BoxCollider2D boxCol;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (tipoEnemigo == enemigos.limo)
        {
            Debug.Log("Slime");
            SlimeBehaviour();
        }
        else if (tipoEnemigo == enemigos.avispa)
        {
            Debug.Log("Avispa");
        }
        else if (tipoEnemigo == enemigos.araña)
        {
            Debug.Log("Araña");
        }
        else if (tipoEnemigo == enemigos.murcielago)
        {
            Debug.Log("Murcierlago");
        }
        else if (tipoEnemigo == enemigos.esqueleto)
        {
            Debug.Log("Esqueleto");
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

    private bool isFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Efficient
        transform.localScale = new Vector2(-(Mathf.Sign(rb2D.velocity.x)), transform.localScale.y);
    }
}
