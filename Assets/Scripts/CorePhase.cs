using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CorePhase {

	private static int maxCoreTurns = 20;

	private static int coreTurnCount = 0;

	private static int movesDone = 0;

	private static int maxPlayerMoves = 2;

	public static void EnableRotationButtons () {
		GameController.Instance.ButtonsRotationP1.SetActive (true);
		GameController.Instance.ButtonsRotationP2.SetActive (true);
	}

	public static void BattlePhase () {
		GameObject.FindObjectOfType<BattleScript> ().Battle ();
	}
		
	public static void PositioningPhase (ColliderScript _collider) {

		if (movesDone < maxPlayerMoves) {

			if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
				if (_collider.GetComponentInParent<GridController> () == GameController.Instance.GridC [0]) {
					if (_collider.placeable == true && GameController.Instance.Hand [0].cardsInHand > 0) {
						_collider.CardPositioning (GameController.Instance.Hand [0], GameObject.Find ("Tasselli"));
						GameController.Instance.CardS.PlaceCardAndSetPlacedCard (_collider.X, _collider.Y, GameController.Instance.Hand [0], Color.red);
						movesDone++;
						EndTurnCheck ();
					}
				}
			}

			if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
				if (_collider.GetComponentInParent<GridController> () == GameController.Instance.GridC [1]) {
					if (_collider.placeable == true && GameController.Instance.Hand [1].cardsInHand > 0) {
						_collider.CardPositioning (GameController.Instance.Hand [1], GameObject.Find ("Tasselli 2"));
						GameController.Instance.CardS.PlaceCardAndSetPlacedCard (_collider.X, _collider.Y, GameController.Instance.Hand [1], Color.blue);
						movesDone++;
						EndTurnCheck ();
					}
				}
			}
		}
	}

	public static void UpgradingPhase (PawnScript _pawn) {

		if (movesDone < maxPlayerMoves) {
		
			if (_pawn.GetComponentInParent<GridController> () == GameController.Instance.GridC [0]) {
				if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
					if (_pawn.HasBeenPlaced == true && GameController.Instance.Hand [0].cardsInHand > 0) {
						_pawn.Strength += GameController.Instance.Hand [0].cards [GameController.Instance.cardSelector].Value;
						GameController.Instance.Hand [0].RemoveCardFromHand (GameController.Instance.cardSelector);
						movesDone++;
						EndTurnCheck ();
					}
				}
			}

			if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
				if (_pawn.GetComponentInParent<GridController> () == GameController.Instance.GridC [1]) {
					if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
						if (_pawn.HasBeenPlaced == true && GameController.Instance.Hand [1].cardsInHand > 0) {
							_pawn.Strength += GameController.Instance.Hand [1].cards [GameController.Instance.cardSelector].Value;
							GameController.Instance.Hand [1].RemoveCardFromHand (GameController.Instance.cardSelector);
							movesDone++;
							EndTurnCheck ();
						}
					}
				}
			}
		}
	}

	private static void EndTurnCheck () {
		if (movesDone == maxPlayerMoves) {

			DrawScript.Draw ();

			if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
				StateMachine.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer2;
			} else {
				StateMachine.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer1;
			}

			maxCoreTurns++;
			movesDone = 0;
			GameController.Instance.backupPhaseIsEnded = true;
		}
	}
}
