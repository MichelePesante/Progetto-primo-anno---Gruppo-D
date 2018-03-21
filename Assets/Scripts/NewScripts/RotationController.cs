using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

	void OnMouseDown () {
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation) {
			if (this.gameObject.name == "MyLeftRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridLeftRotation");
			}
			if (this.gameObject.name == "MyRightRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridRightRotation");
			}
			if (this.gameObject.name == "EnemyLeftRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridLeftRotation");
			}
			if (this.gameObject.name == "EnemyRightRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridRightRotation");
			}
		}

		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation) {
			if (this.gameObject.name == "MyLeftRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridLeftRotation");
			}
			if (this.gameObject.name == "MyRightRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridRightRotation");
			}
			if (this.gameObject.name == "EnemyLeftRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridLeftRotation");
			}
			if (this.gameObject.name == "EnemyRightRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridRightRotation");
			}
		}
	}

	private void RotateGrid (string _gridToRotateName, string _clipName) {
		GameObject.Find(_gridToRotateName).GetComponent<Animator> ().Play (_clipName);
	}
}
