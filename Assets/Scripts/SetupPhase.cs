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

	/// <summary>
	/// Funzione che permette ai giocatori di pescare.
	/// </summary>
	public static void DrawPhase () {
		DrawScript.Draw ();
	}

	public static void PositioningPhase () {

		if (setupTurnCount < maxSetupTurns) {


			if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
				
			} 

			else if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
				
			}
	        
			if (pawnPlaced == pawnsToPlace) {
			
				if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1)
					StateMachine.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer2;
				else
					StateMachine.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer1;
			
				pawnPlaced = 0;
			}

			setupTurnCount++;
		}
	}

	public static bool IsSetupPhaseEnded () {
		if (setupTurnCount >= maxSetupTurns) {
			return true;
		}
		return false;
	}
}
