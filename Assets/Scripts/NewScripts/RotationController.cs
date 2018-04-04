using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

	void OnMouseDown () {
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation && GameMenu.GameIsPaused == false) {
			if (this.gameObject.name == "MyLeftRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridLeftRotation");
				FindObjectOfType<RobotManager> ().OnLeftRotationFirstGrid ();
				DeactivateButton (FindObjectOfType<NewGridController>().MyLeftRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().MyRightRotationButton);
			}
			if (this.gameObject.name == "MyRightRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridRightRotation");
				FindObjectOfType<RobotManager> ().OnRightRotationFirstGrid ();
				DeactivateButton (FindObjectOfType<NewGridController>().MyRightRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().MyLeftRotationButton);
			}
			if (this.gameObject.name == "EnemyLeftRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridLeftRotation");
				FindObjectOfType<RobotManager> ().OnLeftRotationSecondGrid ();
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyLeftRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyRightRotationButton);
			}
			if (this.gameObject.name == "EnemyRightRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridRightRotation");
				FindObjectOfType<RobotManager> ().OnRightRotationSecondGrid ();
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyRightRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyLeftRotationButton);
			}
		}

		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation && GameMenu.GameIsPaused == false) {
			if (this.gameObject.name == "EnemyLeftRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridLeftRotation");
				FindObjectOfType<RobotManager> ().OnLeftRotationSecondGrid ();
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyLeftRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyRightRotationButton);
			}
			if (this.gameObject.name == "EnemyRightRotationButton") {
				RotateGrid ("SecondGrid", "SecondGridRightRotation");
				FindObjectOfType<RobotManager> ().OnRightRotationSecondGrid ();
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyRightRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().EnemyLeftRotationButton);
			}
			if (this.gameObject.name == "MyLeftRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridLeftRotation");
				FindObjectOfType<RobotManager> ().OnLeftRotationFirstGrid ();
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().MyLeftRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().MyRightRotationButton);
			}
			if (this.gameObject.name == "MyRightRotationButton") {
				RotateGrid ("FirstGrid", "FirstGridRightRotation");
				FindObjectOfType<RobotManager> ().OnRightRotationFirstGrid ();
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().MyRightRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().MyLeftRotationButton);
			}
		}

		if (FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation && this.gameObject.name == "EndRotationButton" && GameMenu.GameIsPaused == false) {
			FindObjectOfType<TurnManager> ().CurrentTurnState = TurnManager.TurnState.battle;
		}

		if (FindObjectOfType<NewGridController> ().MyLeftRotationButton.activeInHierarchy == false && FindObjectOfType<NewGridController> ().MyRightRotationButton.activeInHierarchy == false && FindObjectOfType<NewGridController> ().EnemyLeftRotationButton.activeInHierarchy == false && FindObjectOfType<NewGridController> ().EnemyRightRotationButton.activeInHierarchy == false && GameMenu.GameIsPaused == false) {
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
