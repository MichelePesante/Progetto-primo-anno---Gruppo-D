using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour {

    public GameObject PauseImage;


    public static bool GameIsPaused;



    public GameObject PauseMenu;



    private void Start()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        PauseImage.SetActive(false);
    }

   
    // Update is called once per frame
    void Update()
    {
        OpenMenu();

		if (Input.GetKeyDown (KeyCode.P)) {
			if (!GameIsPaused) {
				Time.timeScale = 0f;
				GameIsPaused = true;
			} 
			else {
				Time.timeScale = 1f;
				GameIsPaused = false;
			}
		}
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
        PauseImage.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        PauseImage.SetActive(true);
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


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }


}
