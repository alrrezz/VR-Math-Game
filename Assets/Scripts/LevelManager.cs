using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //--------------------------------------------- Variables ---------------------------------------------------
    [SerializeField] float gameOverDelayInSeconds = 2.0f;




    //---------------------------------------------- Methods ----------------------------------------------------
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadGameOverMenu()
    {
        StartCoroutine(WaitAndLoad());
    }
    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(gameOverDelayInSeconds);
        SceneManager.LoadScene("Game Over Menu");
    }
    public void LoadCreditMenu()
    {
        SceneManager.LoadScene("Credit Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadNextLevel()
    {
        int CurrentScene =
            SceneManager.GetActiveScene()
            .buildIndex;

        SceneManager
            .LoadScene(CurrentScene + 1);

    }
}
