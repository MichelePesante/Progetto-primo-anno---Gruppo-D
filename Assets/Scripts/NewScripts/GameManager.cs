using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else
			GameObject.Destroy(gameObject);
	}

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
