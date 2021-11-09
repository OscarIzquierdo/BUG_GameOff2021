using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    EnemyBehaviour enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = collision.GetComponent<EnemyBehaviour>();
            Debug.Log(collision.name + " ha recibido 1 de daño");
            //enemy.TakeDamage();
        }
    }
}
