using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private AttackTurn turnPrep;
    [SerializeField] GameObject enemyCanvas;

    private void Awake()
    {
        turnPrep = FindObjectOfType<AttackTurn>();
        enemyCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemyCanvas.SetActive(true);
            turnPrep.PStats = other.GetComponent<PlayerStats>();
            transform.position = FindObjectOfType<BattleHandler>().PosE.position;
            transform.localScale = Vector2.one * 2f;
            GetComponent<EnemyBehaviour>().enabled = false;
            GetComponent<Animator>().SetFloat("Horizontal", 1);
            GetComponent<Animator>().SetFloat("Vertical", 0);
            FindObjectOfType<AttackTurn>().enabled = true;
        }
    }
}
