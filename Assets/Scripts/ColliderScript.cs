using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour {

	/// <summary>
	/// Coordinata x singolo tassello.
	/// </summary>
	public int X;

	/// <summary>
	/// Coordinata y singolo tassello.
	/// </summary>
	public int Y;

	/// <summary>
	/// Se true, è possibile piazzare una carta.
	/// </summary>
	public bool placeable;

	/// <summary>
	/// Pedina base.
	/// </summary>
	public GameObject basePawn;

	/// <summary>
	/// Pedina avanzata.
	/// </summary>
	public GameObject advancedPawn;

	/// <summary>
	/// Riferimento ad ogni pedina giocata.
	/// </summary>
	private GameObject SinglePawn;

	/// <summary>
	/// Riferimento alla classe GameController.
	/// </summary>
	private GameController gc;

	void Start () {
		gc = GameController.Instance;
		placeable = true;
	}

	/*
	void OnMouseEnter () {
		if (this.placeable == true) {
			foreach (CellScript cell in gc.CellS) {
				if (this.X == cell.X && this.Y == cell.Y) {
					cell.ChangeColorOnEnter ();
				}
			}
		}
	}

	void OnMouseExit () {
		foreach (CellScript cell in gc.CellS) {
			if (this.X == cell.X && this.Y == cell.Y) {
				cell.ChangeColorOnExit ();
			}
		}
	}
	*/

	void OnMouseDown () {
		if (GetComponentInParent<GridController> () == gc.GridC [0]) {
			if (gc.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
				if (this.placeable == true && gc.clickCounterP1 < gc.totalPlaceableCardsP1 && gc.Hand [0].cardsInHand > 0) {
					CardPositioning (gc.Hand [0], GameObject.Find("Tasselli"));
					gc.CardS.PlaceCardAndSetPlacedCard (this.X, this.Y, gc.Hand[0], Color.red);
					gc.clickCounterP1++;
				}
			}
		}

		if (GetComponentInParent<GridController> () == gc.GridC [1]) {
			if (gc.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
				if (this.placeable == true && gc.clickCounterP2 < gc.totalPlaceableCardsP2 && gc.Hand [1].cardsInHand > 0) {
					CardPositioning (gc.Hand [1], GameObject.Find("Tasselli 2"));
					gc.CardS.PlaceCardAndSetPlacedCard (this.X, this.Y, gc.Hand[1], Color.blue);
					gc.clickCounterP2++;
				}
			}
		}
	}


	public void SetPosition (int _x, int _y) {
		X = _x;
		Y = _y;
	}

	public void CardPositioning (Hand _ownHand, GameObject _parentTransform) {
		if (_ownHand.cardsInHand > 0) {
			switch (_ownHand.cards [gc.cardSelector].Value) {
			case 1:
				SinglePawn = PawnSpawn (basePawn);
				this.placeable = false;
				break;
			case 2:
				SinglePawn = PawnSpawn (basePawn);
				this.placeable = false;
				break;
			case 3:
				SinglePawn = PawnSpawn (advancedPawn);
				this.placeable = false;
				break;
			case 4:
				SinglePawn = PawnSpawn (advancedPawn);
				this.placeable = false;
				break;
			default:
				break;
			}


			SetParentPosition (_parentTransform, SinglePawn);
		}
	}

	public GameObject PawnSpawn (GameObject _pawnType) {
		GameObject thisPawn = Instantiate (_pawnType, new Vector3 (transform.position.x, transform.localScale.y, transform.position.z), transform.rotation);
		return thisPawn;
	}

	private void SetParentPosition (GameObject _newParent, GameObject _child) {
		_child.transform.parent = _newParent.transform;
	}
}
