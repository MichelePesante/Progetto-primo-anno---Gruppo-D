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

	void OnMouseDown () {
		print ("Ho clickato sulla cella: " + FindObjectsOfType<GridController> () [0].GetWorldPosition (X, Y));

		if (this.placeable == true && gc.clickCounter < gc.totalPlaceableCardsP1 && gc.Hand[0].cardsInHand > 0) {
			CardPositioning (gc.Hand [0]);
			cardModify (gc.Hand [0], Color.red);
			gc.clickCounter++;
		}
	}


	public void SetPosition (int _x, int _y) {
		X = _x;
		Y = _y;
	}

	public void CardPositioning (Hand _ownHand) {
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

			_ownHand.RemoveCardFromHand (gc.cardSelector);
			SetParentPosition (this.gameObject, SinglePawn);
		}
	}

	public void cardModify (Hand _ownHand, Color _ownColor) {
		if (this.X == 1 && this.Y == 0) {
			_ownHand.cards [gc.cardSelector].X = this.X;
			_ownHand.cards [gc.cardSelector].Y = this.Y;
			_ownHand.cards [gc.cardSelector].IsPlaced = true;
			_ownHand.cards [gc.cardSelector].Team = _ownColor;
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
