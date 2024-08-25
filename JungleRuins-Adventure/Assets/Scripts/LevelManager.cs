using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadLevel(int levelNo)
    {
        SceneManager.LoadScene(levelNo);
    }

    public void WelcomeLevel()
    {
        LoadLevel(0);
    }

    public void FirstLevel()
    {
        LoadLevel(1);
    }

    public void SecondLevel()
    {
        LoadLevel(2);
    }

    public void Next()
    {
        SceneManager.LoadScene("Game2");
    }

    /*
    public void GameWinnerLevel()
    {
        LoadLevel(3);
    }
    */
    public void GameOverLevel()
    {
        LoadLevel(3);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        //Application.Quit();
    }
}
