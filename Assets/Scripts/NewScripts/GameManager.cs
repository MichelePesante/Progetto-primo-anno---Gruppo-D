using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

    public static bool isSomeAnimationGoing;

    public static bool isTutorialOn;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else
			GameObject.Destroy(gameObject);
	}
}

public enum Player {
	Player_Curve = 1, 
	Player_Quad = 2
}
