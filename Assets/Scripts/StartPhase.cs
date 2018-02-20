using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StartPhase {

	/// <summary>
	/// Giocatore che gioca per primo.
	/// </summary>
	private static int StartingPlayer;

	public static void OnGameStart () {

		// Scelgo casualmente chi è il primo giocatore.
		StartingPlayer = Random.Range (1, 3);

		// Se il numero uscito è 1
		if (StartingPlayer == 1) {
			// Il primo a giocare sarà il giocatore 1.
			GameController.Instance.CurrentPlayerTurn = PlayerTurn.TurnPlayer1;
		}
		// Altrimenti
		else {
			// Il primo a giocare sarà il giocatore 2.
			GameController.Instance.CurrentPlayerTurn = PlayerTurn.TurnPlayer2;
		}

		// Visualizzo a schermo chi sarà il primo a giocare.
		CustomLogger.Log ("Il giocatore ad eseguire le proprie azioni per primo sarà il numero: {0}", StartingPlayer);
	}
}
