﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TpPlayer : MonoBehaviour
{
    public enum tpType { tpTransform, tpScene };

    public tpType thisType;

    [SerializeField] Transform destination;
    [SerializeField] string sceneToLoad;

    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (thisType == tpType.tpTransform)
            {
                collision.transform.position = destination.position;
                if (player.GoInCave)
                {
                    player.GoInCave = false;
                }
                else
                {
                    player.GoInCave = true;
                }
            }

            if (thisType == tpType.tpScene)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
