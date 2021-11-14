using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public enum sceneManagement {loadScene, quitGame, resumeGame}

    public sceneManagement whatToDo;

    [SerializeField] string levelToLoad;
    [SerializeField] GameObject pausePanel;

    private bool isGamePaused;

    public bool IsGamePaused { get => isGamePaused; set => isGamePaused = value; }
    public GameObject PausePanel { get => pausePanel; set => pausePanel = value; }

    public void ExecuteAction()
    {
        if (whatToDo == sceneManagement.loadScene)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else if (whatToDo == sceneManagement.quitGame)
        {
            Application.Quit();
        }
        else if (whatToDo == sceneManagement.resumeGame)
        {
            if (IsGamePaused)
            {
                Time.timeScale = 1;
                PausePanel.SetActive(false);
            }
        }
    }
}
