using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CorePhase {

	private static int maxCoreTurns = 20;

	public static void EnableRotationButtons () {
		GameController.Instance.ButtonsRotationP1.SetActive (true);
		GameController.Instance.ButtonsRotationP2.SetActive (true);
	}

	public static void BattlePhase () {
		GameObject.FindObjectOfType<BattleScript> ().Battle ();
	}

	public static void BackupPhase () {
		DrawScript.Draw ();
	}

}
