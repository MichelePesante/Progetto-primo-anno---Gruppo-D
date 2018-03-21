using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour {


    public static bool GameIsPaused = false;

    private void Start()
    {
        PauseMenu.SetActive(false);
    }


    public GameObject PauseMenu;
    // Update is called once per frame
    void Update()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
