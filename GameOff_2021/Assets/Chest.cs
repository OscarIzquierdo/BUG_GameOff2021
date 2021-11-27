using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] bool chestOpen = false;

    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void Open()
    {
        if (!chestOpen)
        {
            player.HasKey = false;
            GetComponent<Animator>().SetTrigger("Open");
        }
    }
}
