using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject MainMenu;
    public GameObject TutorialMenu;
    public GameObject HowToPlayMenu;
    public GameObject CreditsMenu;
	public GameObject CommandMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            PreviousMenu();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.Background.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AttivaTutorial()
    {
        MainMenu.SetActive(false);
        TutorialMenu.SetActive(true);
    }

    public void AttivaCredits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void AttivaHowToPlayMenu ()
    {
        TutorialMenu.SetActive(false);
        HowToPlayMenu.SetActive(true);
    }

    public void AttivaCommandMenu () {
		TutorialMenu.SetActive (false);
		CommandMenu.SetActive (true);
	}

	public void PreviousMenu () {
		if (TutorialMenu.activeInHierarchy) {
			MainMenu.SetActive (true);
            TutorialMenu.SetActive (false);
		}
        else if (CreditsMenu.activeInHierarchy)
        {
            MainMenu.SetActive(true);
            CreditsMenu.SetActive(false);
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
