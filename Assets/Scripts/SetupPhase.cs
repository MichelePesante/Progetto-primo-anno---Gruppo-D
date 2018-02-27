using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetupPhase {

	/// <summary>
	/// Conteggio interno dei turni di setup.
	/// </summary>
	private static int setupTurnCount = 0;

	/// <summary>
	/// Numero di turni nella fase di Setup.
	/// </summary>
	private static int maxSetupTurns = 12;

	/// <summary>
	/// Conteggio delle pedine piazzate ogni turno.
	/// </summary>
	private static int pawnPlaced = 0;

	/// <summary>
	/// Pedine da posizionare ogni turno;
	/// </summary>
	private static int pawnsToPlace = 2;

	/// <summary>
	/// Riferimento alla classe GameController.
	/// </summary>
	private static GameController gc = GameController.Instance;

	public static void DrawPhase () {

		if (gc.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
			gc.Hand [0].Draw (gc.Hand [0].cardsInHand);
		}

		if (gc.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
			gc.Hand [1].Draw (gc.Hand [1].cardsInHand);
		}
	}

	public static void PositioningPhase () {

		if (setupTurnCount < maxSetupTurns) {

			/*
			if (gc.CurrentPlayerTurn == PlayerTurn.TurnPlayer1) {
				gc.SpawnC [0].PawnPositioning (gc.GridC [0], gc.Hand [0], Color.red);
				pawnPlaced++;
			} 

			else if (gc.CurrentPlayerTurn == PlayerTurn.TurnPlayer2) {
				gc.SpawnC [1].PawnPositioning (gc.GridC [1], gc.Hand [1], Color.blue);
				pawnPlaced++;
			}
	        */
			if (pawnPlaced == pawnsToPlace) {
			
				if (gc.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1)
					gc.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer2;
				else
					gc.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer1;
			
				pawnPlaced = 0;
			}

			setupTurnCount++;
		}
	}
}
