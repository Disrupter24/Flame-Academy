using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool IsPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
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
        PauseMenu.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1f;
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void BackToWorldMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("WorldMap");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
