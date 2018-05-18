using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject MainMenu;
    public GameObject OptionsMenu;
	public GameObject AudioMenu;
	public GameObject VideoMenu;
	public GameObject CommandMenu;

    private void Start()
    {
        OptionsMenu.SetActive (false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AttivaImpostazioni()
    {
        MainMenu.SetActive (false);
        OptionsMenu.SetActive (true);
    }

    public void BackToMainMenu()
    {
        MainMenu.SetActive (true);
        OptionsMenu.SetActive (false);
		AudioMenu.SetActive (false);
		VideoMenu.SetActive (false);
    }

	public void AttivaVideoMenu () {
		OptionsMenu.SetActive (false);
		VideoMenu.SetActive (true);
	}

	public void AttivaAudioMenu () {
		OptionsMenu.SetActive (false);
		AudioMenu.SetActive (true);
	}

	public void AttivaCommandMenu () {
		OptionsMenu.SetActive (false);
		CommandMenu.SetActive (true);
	}

    public void RegolazioneAudio()
    {
        
    }

    public void InterfacciaComandi()
    {

    }

    public void ScaleInterfaccia()
    {

    }
}
