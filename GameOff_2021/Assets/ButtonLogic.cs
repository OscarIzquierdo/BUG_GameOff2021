using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public enum sceneManagement {loadScene, quitGame}

    public sceneManagement whatToDo;

    [SerializeField] string levelToLoad;

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
    }
}
