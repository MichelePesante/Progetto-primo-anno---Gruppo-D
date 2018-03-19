﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public GameObject PointToLookP1;
	public GameObject PointToLookP2;
	public GameObject PreparationCamera;

	void Update () {
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn) {
			transform.LookAt (PointToLookP1.transform.position);
		}
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn) {
			transform.LookAt (PointToLookP2.transform.position);
		}
	}
}
