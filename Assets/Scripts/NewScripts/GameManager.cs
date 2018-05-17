﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (TurnManager.Instance.ScoreCurve >= 5) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}

		if (TurnManager.Instance.ScoreQuad >= 5) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
}

public enum Player {
	Player_Curve = 1, 
	Player_Quad = 2
}
