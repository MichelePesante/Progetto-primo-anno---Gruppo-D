using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationScript : MonoBehaviour {

	public void OnRightRotationFirstGrid () {
		GameController.Instance.GridC [0].transform.Rotate (0f, 90f, 0f);
		foreach (PawnData pawn in GameController.Instance.GridC[0].pawns) {
			if (pawn.X == -1 && pawn.Y == -1) {
				pawn.X += 0;
				pawn.Y += 2;
			}
			else if (pawn.X == -1 && pawn.Y == 1) {
				pawn.X += 2;
				pawn.Y += 0;
			}
			else if (pawn.X == 1 && pawn.Y == 1) {
				pawn.X += 0;
				pawn.Y += -2;
			}
			else if (pawn.X == 1 && pawn.Y == -1) {
				pawn.X += -2;
				pawn.Y += 0;
			}
			if (pawn.X == -1 && pawn.Y == 0) {
				pawn.X += 1;
				pawn.Y += 1;
			}
			else if (pawn.X == 0 && pawn.Y == 1) {
				pawn.X += 1;
				pawn.Y += -1;
			}
			else if (pawn.X == 1 && pawn.Y == 0) {
				pawn.X += -1;
				pawn.Y += -1;
			}
			else if (pawn.X == 0 && pawn.Y == -1) {
				pawn.X += -1;
				pawn.Y += 1;
			}
		}
	}

	public void OnRightRotationSecondGrid () {
		GameController.Instance.GridC [1].transform.Rotate (0f, 90f, 0f);
		foreach (PawnData pawn in GameController.Instance.GridC[1].pawns) {
			if (pawn.X == -1 && pawn.Y == 3) {
				pawn.X += 0;
				pawn.Y += 2;
			}
			else if (pawn.X == -1 && pawn.Y == 5) {
				pawn.X += 2;
				pawn.Y += 0;
			}
			else if (pawn.X == 1 && pawn.Y == 5) {
				pawn.X += 0;
				pawn.Y += -2;
			}
			else if (pawn.X == 1 && pawn.Y == 3) {
				pawn.X += -2;
				pawn.Y += 0;
			}
			if (pawn.X == -1 && pawn.Y == 4) {
				pawn.X += 1;
				pawn.Y += 1;
			}
			else if (pawn.X == 0 && pawn.Y == 5) {
				pawn.X += 1;
				pawn.Y += -1;
			}
			else if (pawn.X == 1 && pawn.Y == 4) {
				pawn.X += -1;
				pawn.Y += -1;
			}
			else if (pawn.X == 0 && pawn.Y == 3) {
				pawn.X += -1;
				pawn.Y += 1;
			}
		}
	}

	public void OnLeftRotationFirstGrid () {
		GameController.Instance.GridC [0].transform.Rotate (0f, -90f, 0f);
		foreach (PawnData pawn in GameController.Instance.GridC[0].pawns) {
			if (pawn.X == -1 && pawn.Y == -1) {
				pawn.X += 2;
				pawn.Y += 0;
			}
			else if (pawn.X == -1 && pawn.Y == 1) {
				pawn.X += 0;
				pawn.Y += -2;
			}
			else if (pawn.X == 1 && pawn.Y == 1) {
				pawn.X += -2;
				pawn.Y += 0;
			}
			else if (pawn.X == 1 && pawn.Y == -1) {
				pawn.X += 0;
				pawn.Y += 2;
			}
			if (pawn.X == -1 && pawn.Y == 0) {
				pawn.X += 1;
				pawn.Y += -1;
			}
			else if (pawn.X == 0 && pawn.Y == 1) {
				pawn.X += -1;
				pawn.Y += -1;
			}
			else if (pawn.X == 1 && pawn.Y == 0) {
				pawn.X += -1;
				pawn.Y += 1;
			}
			else if (pawn.X == 0 && pawn.Y == -1) {
				pawn.X += 1;
				pawn.Y += 1;
			}
		}
	}

	public void OnLeftRotationSecondGrid () {
		GameController.Instance.GridC [1].transform.Rotate (0f, -90f, 0f);
		foreach (PawnData pawn in GameController.Instance.GridC[1].pawns) {
			if (pawn.X == -1 && pawn.Y == 3) {
				pawn.X += 2;
				pawn.Y += 0;
			}
			else if (pawn.X == -1 && pawn.Y == 5) {
				pawn.X += 0;
				pawn.Y += -2;
			}
			else if (pawn.X == 1 && pawn.Y == 5) {
				pawn.X += -2;
				pawn.Y += 0;
			}
			else if (pawn.X == 1 && pawn.Y == 3) {
				pawn.X += 0;
				pawn.Y += 2;
			}
			if (pawn.X == -1 && pawn.Y == 4) {
				pawn.X += 1;
				pawn.Y += -1;
			}
			else if (pawn.X == 0 && pawn.Y == 5) {
				pawn.X += -1;
				pawn.Y += -1;
			}
			else if (pawn.X == 1 && pawn.Y == 4) {
				pawn.X += -1;
				pawn.Y += 1;
			}
			else if (pawn.X == 0 && pawn.Y == 3) {
				pawn.X += 1;
				pawn.Y += 1;
			}
		}
	}
}
