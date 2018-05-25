using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public static bool GameIsPaused;

    public GameObject PauseMenu;
	public GameObject OptionsMenu;
	public GameObject AudioMenu;
	public GameObject VideoMenu;
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

		if (Input.GetKeyDown (KeyCode.Space)) {
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
		OptionsMenu.SetActive(false);
		AudioMenu.SetActive(false);
		VideoMenu.SetActive(false);
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

	public void OpenOptionsMenu () {
		PauseMenu.SetActive (false);
		OptionsMenu.SetActive (true);
	}

	public void OpenAudioMenu () {
		OptionsMenu.SetActive (false);
		AudioMenu.SetActive (true);
	}

	public void OpenVideoMenu () {
		OptionsMenu.SetActive (false);
		VideoMenu.SetActive (true);
	}

	public void OpenCommandMenu () {
		OptionsMenu.SetActive (false);
		CommandMenu.SetActive (true);
	}

	public void PreviousMenu () {
		if (PauseMenu.activeInHierarchy) {
			Resume ();
		} 
		if (OptionsMenu.activeInHierarchy) {
			PauseMenu.SetActive (true);
			OptionsMenu.SetActive (false);
		} 
		else if (AudioMenu.activeInHierarchy) {
			OptionsMenu.SetActive (true);
			AudioMenu.SetActive (false);
		} 
		else if (VideoMenu.activeInHierarchy) {
			OptionsMenu.SetActive (true);
			VideoMenu.SetActive (false);
		} 
		else if (CommandMenu.activeInHierarchy) {
			OptionsMenu.SetActive (true);
			CommandMenu.SetActive (false);
		}
	}
}
