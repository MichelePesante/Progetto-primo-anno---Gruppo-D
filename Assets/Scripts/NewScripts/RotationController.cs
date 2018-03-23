using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

	void OnMouseDown () {
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation) {
			if (this.gameObject.name == "MyLeftRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridLeftRotation");
				DeactivateButton (FindObjectOfType<NewGridController>().MyLeftRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().MyRightRotationButton);
				FindObjectOfType<NewGridController> ().GridRotated++;
			}
			if (this.gameObject.name == "MyRightRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridRightRotation");
				DeactivateButton (FindObjectOfType<NewGridController>().MyRightRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().MyLeftRotationButton);
				FindObjectOfType<NewGridController> ().GridRotated++;
			}
			if (this.gameObject.name == "EnemyLeftRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridLeftRotation");
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyLeftRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyRightRotationButton);
				FindObjectOfType<NewGridController> ().GridRotated++;
			}
			if (this.gameObject.name == "EnemyRightRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridRightRotation");
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyRightRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyLeftRotationButton);
				FindObjectOfType<NewGridController> ().GridRotated++;
			}
		}

		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation) {
			if (this.gameObject.name == "EnemyLeftRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridLeftRotation");
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyLeftRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyRightRotationButton);
				FindObjectOfType<NewGridController> ().GridRotated++;
			}
			if (this.gameObject.name == "EnemyRightRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridRightRotation");
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyRightRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyLeftRotationButton);
				FindObjectOfType<NewGridController> ().GridRotated++;
			}
			if (this.gameObject.name == "MyLeftRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridLeftRotation");
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().MyLeftRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().MyRightRotationButton);
				FindObjectOfType<NewGridController> ().GridRotated++;
			}
			if (this.gameObject.name == "MyRightRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridRightRotation");
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().MyRightRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().MyLeftRotationButton);
				FindObjectOfType<NewGridController> ().GridRotated++;
			}
		}

		if (FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation && this.gameObject.name == "EndRotationButton") {
			FindObjectOfType<TurnManager> ().CurrentTurnState = TurnManager.TurnState.battle;
		}
	}

	private void RotateGrid (string _gridToRotateName, string _clipName) {
		GameObject.Find(_gridToRotateName).GetComponent<Animator> ().Play (_clipName);
	}

	private void ActiveEndRotationButton () {
		FindObjectOfType<NewGridController> ().EndRotationButton.SetActive (true);
	}

	private void DeactivateButton (GameObject _buttonToDeactivate) {
		_buttonToDeactivate.SetActive (false);
	}
}
