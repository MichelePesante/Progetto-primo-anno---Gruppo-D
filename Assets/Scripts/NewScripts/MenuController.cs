using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {




    
	




	
	void OnMouseDown () {
		if(this.gameObject.name == "NuovaPartita")
        {
            StartGame();
        }

        if (this.gameObject.name == "QuitGame")
        {
            QuitGame();
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





}
