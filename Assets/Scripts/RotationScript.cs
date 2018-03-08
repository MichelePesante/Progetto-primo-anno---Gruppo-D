using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationScript : MonoBehaviour {

	public GameObject MyButtonsRotation;
	public GameObject EnemyButtonsRotation;

	public void OnRightRotationFirstGrid () {
		GameController.Instance.GridC [0].GetComponentInChildren<Transform> ().Find ("Tasselli").gameObject.transform.Rotate (0f, 90f, 0f);
		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == -1) {
				card.X += 0;
				card.Y += 2;
			} else if (card.X == -1 && card.Y == 1) {
				card.X += 2;
				card.Y += 0;
			} else if (card.X == 1 && card.Y == 1) {
				card.X += 0;
				card.Y += -2;
			} else if (card.X == 1 && card.Y == -1) {
				card.X += -2;
				card.Y += 0;
			}
			if (card.X == -1 && card.Y == 0) {
				card.X += 1;
				card.Y += 1;
			} else if (card.X == 0 && card.Y == 1) {
				card.X += 1;
				card.Y += -1;
			} else if (card.X == 1 && card.Y == 0) {
				card.X += -1;
				card.Y += -1;
			} else if (card.X == 0 && card.Y == -1) {
				card.X += -1;
				card.Y += 1;
			}
		}
		DisableGridButtons (MyButtonsRotation);
	}

	public void OnRightRotationSecondGrid () {
		GameController.Instance.GridC [1].GetComponentInChildren<Transform> ().Find ("Tasselli 2").gameObject.transform.Rotate (0f, 90f, 0f);
		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == 3) {
				card.X += 0;
				card.Y += 2;
			} else if (card.X == -1 && card.Y == 5) {
				card.X += 2;
				card.Y += 0;
			} else if (card.X == 1 && card.Y == 5) {
				card.X += 0;
				card.Y += -2;
			} else if (card.X == 1 && card.Y == 3) {
				card.X += -2;
				card.Y += 0;
			}
			if (card.X == -1 && card.Y == 4) {
				card.X += 1;
				card.Y += 1;
			} else if (card.X == 0 && card.Y == 5) {
				card.X += 1;
				card.Y += -1;
			} else if (card.X == 1 && card.Y == 4) {
				card.X += -1;
				card.Y += -1;
			} else if (card.X == 0 && card.Y == 3) {
				card.X += -1;
				card.Y += 1;
			}
		}
		DisableGridButtons (EnemyButtonsRotation);
		EndRotationPhase ();
	}

	public void OnLeftRotationFirstGrid () {
		GameController.Instance.GridC [0].GetComponentInChildren<Transform> ().Find ("Tasselli").gameObject.transform.Rotate (0f, -90f, 0f);
		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == -1) {
				card.X += 2;
				card.Y += 0;
			} else if (card.X == -1 && card.Y == 1) {
				card.X += 0;
				card.Y += -2;
			} else if (card.X == 1 && card.Y == 1) {
				card.X += -2;
				card.Y += 0;
			} else if (card.X == 1 && card.Y == -1) {
				card.X += 0;
				card.Y += 2;
			}
			if (card.X == -1 && card.Y == 0) {
				card.X += 1;
				card.Y += -1;
			} else if (card.X == 0 && card.Y == 1) {
				card.X += -1;
				card.Y += -1;
			} else if (card.X == 1 && card.Y == 0) {
				card.X += -1;
				card.Y += 1;
			} else if (card.X == 0 && card.Y == -1) {
				card.X += 1;
				card.Y += 1;
			}
		}
		DisableGridButtons (MyButtonsRotation);
	}

	public void OnLeftRotationSecondGrid () {
		GameController.Instance.GridC [1].GetComponentInChildren<Transform> ().Find ("Tasselli 2").gameObject.transform.Rotate (0f, -90f, 0f);
		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == 3) {
				card.X += 2;
				card.Y += 0;
			} else if (card.X == -1 && card.Y == 5) {
				card.X += 0;
				card.Y += -2;
			} else if (card.X == 1 && card.Y == 5) {
				card.X += -2;
				card.Y += 0;
			} else if (card.X == 1 && card.Y == 3) {
				card.X += 0;
				card.Y += 2;
			}
			if (card.X == -1 && card.Y == 4) {
				card.X += 1;
				card.Y += -1;
			} else if (card.X == 0 && card.Y == 5) {
				card.X += -1;
				card.Y += -1;
			} else if (card.X == 1 && card.Y == 4) {
				card.X += -1;
				card.Y += 1;
			} else if (card.X == 0 && card.Y == 3) {
				card.X += 1;
				card.Y += 1;
			}
		}
		DisableGridButtons (EnemyButtonsRotation);
		EndRotationPhase ();
	}

	private void DisableGridButtons (GameObject _buttonsToDisable) {
		_buttonsToDisable.SetActive (false);
	}

	private void EndRotationPhase () {
		if (EnemyButtonsRotation.activeInHierarchy == false) {
			DisableGridButtons (MyButtonsRotation);
			FindObjectOfType<StateMachine> ().CurrentPhase = StateMachine.BattlePhase.Battle;
		}
	}

	#region API

	public void SwitchButtonsPosition () {
		Vector3 tempPosition;
		tempPosition = MyButtonsRotation.transform.position;
		MyButtonsRotation.transform.position = EnemyButtonsRotation.transform.position;
		EnemyButtonsRotation.transform.position = tempPosition;
		Debug.Log ("Ho scambiato");
	}

	public void EnableGridButtons () {
		MyButtonsRotation.SetActive (true);
		EnemyButtonsRotation.SetActive (true);
	}

	#endregion
}