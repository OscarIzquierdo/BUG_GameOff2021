using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTurn : MonoBehaviour
{

    [SerializeField] TurnController turn;
    [Header("Player")]
    [SerializeField] GameObject player;
    [SerializeField] Image playerHPFill;
    [Header("Enemy")]
    [SerializeField] GameObject enemy;
    [SerializeField] Image enemyHPFill;

    private PlayerStats pStats;
    private EnemyStats eStats;

    public PlayerStats PStats { get => pStats; set => pStats = value; }
    public EnemyStats EStats { get => eStats; set => eStats = value; }

    private void Awake()
    {
        this.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (turn.PlayerTurn)
            {
                if (PStats.PlayerHP <= 0)
                {
                    EndCombat(false);
                }
                else if (PStats.PlayerHP > 0)
                {
                    PlayerTurn();
                }
            }
            else
            {
                if (EStats.EnemyHP <= 0)
                {
                    EndCombat(true);
                }
                else if (EStats.EnemyHP > 0)
                {
                    EnemyTurn();
                }
            }
        }
    }

    void PlayerTurn()
    {
        enemy.transform.position = new Vector2(-3, -.25f);
        EStats.EnemyHP -= PStats.TotalATK - EStats.EnemyDEF;
        enemyHPFill = GameObject.Find("E1_HP_FG").GetComponent<Image>();
        enemyHPFill.fillAmount = EStats.EnemyHP / EStats.EnemyMAXHP;
        player.transform.position = new Vector2(0, .25f);
        turn.PlayerTurn = false;
    }

    void EnemyTurn()
    {
        player.transform.position = new Vector2(3, .25f);
        PStats.PlayerHP -= EStats.EnemyATK - PStats.PlayerDEF;
        playerHPFill = GameObject.Find("P_HP_FG").GetComponent<Image>();
        playerHPFill.fillAmount = PStats.PlayerHP / PStats.PlayerMAXHP;
        enemy.transform.position = new Vector2(0, -.25f);
        turn.PlayerTurn = true;
    }

    void EndCombat(bool playerWin)
    {
        if (playerWin)
        {
            Destroy(enemy, 0.1f);
            player.GetComponent<PlayerMovement>().enabled = true;
            player.transform.localScale = Vector2.one;
            player.GetComponent<Animator>().SetBool("Battle", false);
            this.enabled = false;
        }
        else
        {
            Destroy(player, 0.1f);
            this.enabled = false;
        }
    }
}
