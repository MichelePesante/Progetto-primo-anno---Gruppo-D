using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject MainMenuGraphic;
    public GameObject OptionsGraphic;
    public GameObject TutorialPanel;

    private void Start()
    {
        OptionsMenu.SetActive(false);
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
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void RegolazioneAudio()
    {

    }

    public void GestioneInterfacciaComandi()
    {

    }

    public void ScaleInterfaccia()
    {

    }
}
