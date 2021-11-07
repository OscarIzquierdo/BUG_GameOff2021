using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float enemyHP;
    [SerializeField] float enemyMAXHP;
    [SerializeField] float enemyATK;
    [SerializeField] float enemyDEF;

    public float EnemyHP { get => enemyHP; set => enemyHP = value; }
    public float EnemyATK { get => enemyATK; set => enemyATK = value; }
    public float EnemyDEF { get => enemyDEF; set => enemyDEF = value; }
    public float EnemyMAXHP { get => enemyMAXHP; set => enemyMAXHP = value; }

    private void Awake()
    {
        enemyHP = enemyMAXHP;
    }
}
