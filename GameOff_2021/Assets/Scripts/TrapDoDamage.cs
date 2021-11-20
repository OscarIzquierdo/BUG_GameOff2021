using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoDamage : MonoBehaviour
{
    PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.CurrentHP--;
        }
    }
}
