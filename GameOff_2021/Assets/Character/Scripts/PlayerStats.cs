using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float playerHP;    
    [SerializeField] float playerMAXHP;    
    [SerializeField] float playerATK;    
    [SerializeField] float totalATK;    
    [SerializeField] float playerDEF; 
    [Space]
    [SerializeField] GameObject weapon;

    public float PlayerHP { get => playerHP; set => playerHP = value; }
    public float TotalATK { get => totalATK; set => totalATK = value; }
    public float PlayerDEF { get => playerDEF; set => playerDEF = value; }
    public float PlayerMAXHP { get => playerMAXHP; set => playerMAXHP = value; }

    private void Awake()
    {
        PlayerHP = PlayerMAXHP;
    }

    private void Update()
    {
        totalATK = playerATK;
    }
}
