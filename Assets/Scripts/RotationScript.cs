using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationScript : MonoBehaviour {

	public void OnRightRotationFirstGrid () {
		GameController.Instance.GridC [0].GetComponentInChildren<Transform> ().Find ("Tasselli").gameObject.transform.Rotate(0f, 90f, 0f);
		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == -1) {
				card.X += 0;
				card.Y += 2;
			}
			else if (card.X == -1 && card.Y == 1) {
				card.X += 2;
				card.Y += 0;
			}
			else if (card.X == 1 && card.Y == 1) {
				card.X += 0;
				card.Y += -2;
			}
			else if (card.X == 1 && card.Y == -1) {
				card.X += -2;
				card.Y += 0;
			}
			if (card.X == -1 && card.Y == 0) {
				card.X += 1;
				card.Y += 1;
			}
			else if (card.X == 0 && card.Y == 1) {
				card.X += 1;
				card.Y += -1;
			}
			else if (card.X == 1 && card.Y == 0) {
				card.X += -1;
				card.Y += -1;
			}
			else if (card.X == 0 && card.Y == -1) {
				card.X += -1;
				card.Y += 1;
			}
		}
	}

	public void OnRightRotationSecondGrid () {
		GameController.Instance.GridC [1].GetComponentInChildren<Transform> ().Find ("Tasselli 2").gameObject.transform.Rotate (0f, 90f, 0f);
		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == 3) {
				card.X += 0;
				card.Y += 2;
			}
			else if (card.X == -1 && card.Y == 5) {
				card.X += 2;
				card.Y += 0;
			}
			else if (card.X == 1 && card.Y == 5) {
				card.X += 0;
				card.Y += -2;
			}
			else if (card.X == 1 && card.Y == 3) {
				card.X += -2;
				card.Y += 0;
			}
			if (card.X == -1 && card.Y == 4) {
				card.X += 1;
				card.Y += 1;
			}
			else if (card.X == 0 && card.Y == 5) {
				card.X += 1;
				card.Y += -1;
			}
			else if (card.X == 1 && card.Y == 4) {
				card.X += -1;
				card.Y += -1;
			}
			else if (card.X == 0 && card.Y == 3) {
				card.X += -1;
				card.Y += 1;
			}
		}
	}

	public void OnLeftRotationFirstGrid () {
		GameController.Instance.GridC [0].GetComponentInChildren<Transform> ().Find ("Tasselli").gameObject.transform.Rotate (0f, -90f, 0f);
		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == -1) {
				card.X += 2;
				card.Y += 0;
			}
			else if (card.X == -1 && card.Y == 1) {
				card.X += 0;
				card.Y += -2;
			}
			else if (card.X == 1 && card.Y == 1) {
				card.X += -2;
				card.Y += 0;
			}
			else if (card.X == 1 && card.Y == -1) {
				card.X += 0;
				card.Y += 2;
			}
			if (card.X == -1 && card.Y == 0) {
				card.X += 1;
				card.Y += -1;
			}
			else if (card.X == 0 && card.Y == 1) {
				card.X += -1;
				card.Y += -1;
			}
			else if (card.X == 1 && card.Y == 0) {
				card.X += -1;
				card.Y += 1;
			}
			else if (card.X == 0 && card.Y == -1) {
				card.X += 1;
				card.Y += 1;
			}
		}
	}

	public void OnLeftRotationSecondGrid () {
		GameController.Instance.GridC [1].GetComponentInChildren<Transform> ().Find ("Tasselli 2").gameObject.transform.Rotate (0f, -90f, 0f);
		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == 3) {
				card.X += 2;
				card.Y += 0;
			}
			else if (card.X == -1 && card.Y == 5) {
				card.X += 0;
				card.Y += -2;
			}
			else if (card.X == 1 && card.Y == 5) {
				card.X += -2;
				card.Y += 0;
			}
			else if (card.X == 1 && card.Y == 3) {
				card.X += 0;
				card.Y += 2;
			}
			if (card.X == -1 && card.Y == 4) {
				card.X += 1;
				card.Y += -1;
			}
			else if (card.X == 0 && card.Y == 5) {
				card.X += -1;
				card.Y += -1;
			}
			else if (card.X == 1 && card.Y == 4) {
				card.X += -1;
				card.Y += 1;
			}
			else if (card.X == 0 && card.Y == 3) {
				card.X += 1;
				card.Y += 1;
			}
		}
	}
}
