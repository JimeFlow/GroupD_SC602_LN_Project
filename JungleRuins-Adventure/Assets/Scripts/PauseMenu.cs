using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPausa;

    private bool gamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        gamePaused = true;
        Time.timeScale = 0.0F;
        menuPausa.SetActive(true);
    }

    public void Resume()
    {
        StartCoroutine(WaitAndResume(0.2f));  
    }

    private IEnumerator WaitAndResume(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        gamePaused = false;
        Time.timeScale = 1.0F;
        menuPausa.SetActive(false);
    }


    public void Restart()
    {
        StartCoroutine(WaitAndRestart(0.2f));  
    }

    private IEnumerator WaitAndRestart(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        gamePaused = false;
        Time.timeScale = 1.0F;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        StartCoroutine(WaitAndQuit(0.2f));  
    }

    private IEnumerator WaitAndQuit(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Debug.Log("Cerrando Juego!");
        Application.Quit();
    }
    

    
}

