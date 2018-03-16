using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject CameraPlayer1;
	public GameObject CameraPlayer2;

	void Update () {
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn) {
			transform.position = CameraPlayer1.transform.position;
			transform.rotation = CameraPlayer1.transform.rotation;
		}
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn) {
			transform.position = CameraPlayer2.transform.position;
			transform.rotation = CameraPlayer2.transform.rotation;
		}
	}
}
