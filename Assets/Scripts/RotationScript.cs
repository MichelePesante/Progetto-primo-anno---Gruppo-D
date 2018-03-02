using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationScript : MonoBehaviour {

	public bool hasGrid1BeenRotated = false;
	public bool hasGrid2BeenRotated = false;

	public void OnRightRotationFirstGrid () {
		if (hasGrid1BeenRotated == false) {
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
		}
		hasGrid1BeenRotated = true;
	}

	public void OnRightRotationSecondGrid () {
		if (hasGrid2BeenRotated == false) {
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
		}
		hasGrid2BeenRotated = true;
	}

	public void OnLeftRotationFirstGrid () {
		if (hasGrid1BeenRotated == false) {
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
		}
		hasGrid1BeenRotated = true;
	}

	public void OnLeftRotationSecondGrid () {
		if (hasGrid2BeenRotated == false) {
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
		}
		hasGrid2BeenRotated = true;
	}

	public void DisableButton (GameObject _button) {
		_button.SetActive (false);
	}
}
