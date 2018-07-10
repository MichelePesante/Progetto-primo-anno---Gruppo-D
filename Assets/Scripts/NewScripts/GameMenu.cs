using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public static bool GameIsPaused;

    public GameObject PauseMenu;
    public GameObject TutorialMenu;
    public GameObject HowToPlayMenu;
    public GameObject CommandMenu;

    private void Start()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }

   
    // Update is called once per frame
    void Update()
    {
        OpenMenu();

		if (Input.GetKeyDown (KeyCode.JoystickButton1)) {
			PreviousMenu ();
		}

		//if (Input.GetKeyDown (KeyCode.Space)) {
		//	if (!GameIsPaused) {
		//		Time.timeScale = 0f;
		//		GameIsPaused = true;
		//	} 
		//	else {
		//		Time.timeScale = 1f;
		//		GameIsPaused = false;
		//	}
		//}
    }

    public void OpenMenu()
    {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton7))
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
		TutorialMenu.SetActive(false);
		HowToPlayMenu.SetActive(false);
		CommandMenu.SetActive (false);
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


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

	public void OpenTutorialMenu () {
		PauseMenu.SetActive (false);
		TutorialMenu.SetActive (true);
	}

    public void OpenHowToPlayMenu()
    {
        TutorialMenu.SetActive(false);
        HowToPlayMenu.SetActive(true);
    }

    public void OpenCommandMenu()
    {
        TutorialMenu.SetActive(false);
        CommandMenu.SetActive(true);
    }

    public void PreviousMenu () {
		if (PauseMenu.activeInHierarchy) {
			Resume ();
		} 
		else if (TutorialMenu.activeInHierarchy) {
			PauseMenu.SetActive (true);
			TutorialMenu.SetActive (false);
		} 
		else if (HowToPlayMenu.activeInHierarchy) {
            TutorialMenu.SetActive (true);
            HowToPlayMenu.SetActive (false);
		} 
		else if (CommandMenu.activeInHierarchy) {
            TutorialMenu.SetActive (true);
			CommandMenu.SetActive (false);
		} 
	}
}
