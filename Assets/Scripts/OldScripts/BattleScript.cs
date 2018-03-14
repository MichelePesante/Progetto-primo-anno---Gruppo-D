using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour {

	public void Battle () {
		int battleResult1 = 0;
		int battleResult2 = 0;
		int battleResult3 = 0;

		int scoretemp1 = 0;
		int scoretemp2 = 0;
		int finalScore = 0;

		int ForzaPedina1p1 = 0;
		int ForzaPedina2p1 = 0;
		int ForzaPedina3p1 = 0;
		int ForzaPedina1p2 = 0;
		int ForzaPedina2p2 = 0;
		int ForzaPedina3p2 = 0;

		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == 1) {
				ForzaPedina1p1 = card.Strength;
				CustomLogger.Log ("Forza pedina 1 player 1:   " + ForzaPedina1p1);
			}
			if (card.X == 0 && card.Y == 1) {
				ForzaPedina2p1 = card.Strength;
				CustomLogger.Log ("Forza pedina 2 player 1:   " + ForzaPedina2p1);
			}
			if (card.X == 1 && card.Y == 1) {
				ForzaPedina3p1 = card.Strength;
				CustomLogger.Log ("Forza pedina 3 player 1:   " + ForzaPedina3p1);
			}
		}

		foreach (PawnScript card in FindObjectsOfType<PawnScript>()) {
			if (card.X == -1 && card.Y == 3) {
				ForzaPedina1p2 = card.Strength;
				CustomLogger.Log ("Forza pedina 1 player 2:   " + ForzaPedina1p2);
			}
			if (card.X == 0 && card.Y == 3) {
				ForzaPedina2p2 = card.Strength;
				CustomLogger.Log ("Forza pedina 2 player 2:   " + ForzaPedina2p2);
			}
			if (card.X == 1 && card.Y == 3) {
				ForzaPedina3p2 = card.Strength;
				CustomLogger.Log ("Forza pedina 3 player 2:   " + ForzaPedina3p2);
			}
		}

		battleResult1 = ForzaPedina1p1 - ForzaPedina1p2;
		if (battleResult1 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult1 < 0) {
			scoretemp2 += 1;
		}
		battleResult2 = ForzaPedina2p1 - ForzaPedina2p2;
		if (battleResult2 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult2 < 0) {
			scoretemp2 += 1;
		}
		battleResult3 = ForzaPedina3p1 - ForzaPedina3p2;
		if (battleResult3 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult3 < 0) {
			scoretemp2 += 1;
		}
		if (scoretemp1 > scoretemp2) {
			finalScore = scoretemp1 - scoretemp2;
			GameController.Instance.scorep1 += finalScore;
		}
		if (scoretemp1 < scoretemp2) {
			finalScore = scoretemp2 - scoretemp1;
			GameController.Instance.scorep2 += finalScore;
		}

		EndBattlePhase ();
	}

	private void EndBattlePhase () {
		if (GameController.Instance.scorep1 < 5 || GameController.Instance.scorep1 < 5)
			FindObjectOfType<StateMachine> ().CurrentPhase = StateMachine.BattlePhase.Reinforce;
	}

	private void EndGame () {
		if (GameController.Instance.scorep1 >= 5 || GameController.Instance.scorep2 >= 5) {
			if (GameController.Instance.scorep1 > GameController.Instance.scorep2) {
				// Inserire immagine "Vince player 1".
			}
			if (GameController.Instance.scorep2 > GameController.Instance.scorep1) {
				// Inserire immagine "Vince player 2".
			}
			if (GameController.Instance.scorep1 == GameController.Instance.scorep2) {
				// Inserire immagine "Pareggio".
			}
		}
	}
}
