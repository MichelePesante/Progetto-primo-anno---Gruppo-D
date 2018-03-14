using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {
	
	void Update () {
		if (Input.anyKeyDown == true) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
