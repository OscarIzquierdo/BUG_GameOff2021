using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseComponent : MonoBehaviour
{
    [SerializeField] ButtonLogic pause;

    private void Start()
    {
        pause.IsGamePaused = false;
        pause.PausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.IsGamePaused = true;
            Time.timeScale = 0;
            pause.PausePanel.SetActive(true);
        }
    }
}
