using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    private AttackTurn turnPrep;
    [SerializeField] GameObject playerCanvas;

    private void Awake()
    {
        turnPrep = FindObjectOfType<AttackTurn>();
        playerCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            playerCanvas.SetActive(true);
            turnPrep.EStats = other.GetComponent<EnemyStats>();
            transform.position = FindObjectOfType<BattleHandler>().PosP.position;
            transform.localScale = Vector2.one * 1.5f;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Animator>().SetBool("Battle", true);
        }
    }
}
