using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject GameTitleGraphic;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject MainMenuGraphic;
    public GameObject OptionsGraphic;
    public GameObject TutorialPanel;
    public Slider VolumeSlider;
    public Slider InterfaceSlider;
    public AudioSource GameMusic;


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
        GameTitleGraphic.SetActive(false);
        MainMenu.SetActive(false);
        MainMenuGraphic.SetActive(false);
        OptionsMenu.SetActive(true);
        OptionsGraphic.SetActive(true);
    }

    public void BackToMainMenu()
    {
        GameTitleGraphic.SetActive(true);
        MainMenu.SetActive(true);
        MainMenuGraphic.SetActive(true);
        OptionsMenu.SetActive(false);
        OptionsGraphic.SetActive(false);
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
