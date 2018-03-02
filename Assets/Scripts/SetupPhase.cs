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
	private static int maxSetupTurns = 6;

	/// <summary>
	/// Conteggio delle pedine piazzate ogni turno.
	/// </summary>
	private static int pawnPlaced = 0;

	/// <summary>
	/// Pedine da posizionare ogni turno;
	/// </summary>
	private static int pawnsToPlace = 2;

	/// <summary>
	/// Funzione che permette ai giocatori di pescare.
	/// </summary>
	public static void DrawPhase () {
		DrawScript.Draw ();
	}

	public static void PositioningPhase (ColliderScript _collider) {

		if (setupTurnCount < maxSetupTurns) {

			if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
				if (_collider.GetComponentInParent<GridController> () == GameController.Instance.GridC [0]) {
					if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
						if (_collider.placeable == true && GameController.Instance.clickCounterP1 < GameController.Instance.totalPlaceableCardsP1 && GameController.Instance.Hand [0].cardsInHand > 0) {
							_collider.CardPositioning (GameController.Instance.Hand [0], GameObject.Find("Tasselli"));
							GameController.Instance.CardS.PlaceCardAndSetPlacedCard (_collider.X, _collider.Y, GameController.Instance.Hand[0], Color.red);
							GameController.Instance.clickCounterP1++;
							pawnPlaced++;
						}
					}
				}
			} 

			else if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
				if (_collider.GetComponentInParent<GridController> () == GameController.Instance.GridC [1]) {
					if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
						if (_collider.placeable == true && GameController.Instance.clickCounterP2 < GameController.Instance.totalPlaceableCardsP2 && GameController.Instance.Hand [1].cardsInHand > 0) {
							_collider.CardPositioning (GameController.Instance.Hand [1], GameObject.Find("Tasselli 2"));
							GameController.Instance.CardS.PlaceCardAndSetPlacedCard (_collider.X, _collider.Y, GameController.Instance.Hand[1], Color.blue);
							GameController.Instance.clickCounterP2++;
							pawnPlaced++;
						}
					}
				}
			}

			if (pawnPlaced == pawnsToPlace) {

				DrawPhase ();

				if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
					StateMachine.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer2;
					setupTurnCount++;
				} else {
					StateMachine.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer1;
					setupTurnCount++;
				}

				pawnPlaced = 0;
			}
		}
	}

	public static bool IsSetupPhaseEnded () {
		if (setupTurnCount >= maxSetupTurns) {
			return true;
		}
		return false;
	}
}
