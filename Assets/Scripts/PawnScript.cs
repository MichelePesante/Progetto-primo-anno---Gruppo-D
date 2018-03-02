using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour {

	/// <summary>
	/// Riferimento alla classe GameController.
	/// </summary>
	private GameController gc;

	/// <summary>
	/// Materiale iniziale.
	/// </summary>
	public Material StartMaterial;

	public int X;

	public int Y;

	public string Name;

	public int Strength;

	public bool HasBeenPlaced;

	public Color Team;

	/// <summary>
	/// Materiale utilizzato quando un oggetto è stato selezionato.
	/// </summary>
	public Material SelectedObject;

	// Use this for initialization
	void Start () {
		gc = GameController.Instance;
		StartMaterial = this.gameObject.GetComponent<MeshRenderer> ().material;
	}

	/*
	void OnMouseEnter () {
		this.gameObject.GetComponent<MeshRenderer> ().material = SelectedObject;
	}

	void OnMouseExit () {
		this.gameObject.GetComponent<MeshRenderer> ().material = StartMaterial;
	}
	*/

	void OnMouseDown () {
		if (GetComponentInParent<GridController> () == gc.GridC [0]) {
			if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
				if (this.HasBeenPlaced == true && gc.Hand [0].cardsInHand > 0) {
					UpgradeCard (gc.Hand [0]);
				}
			}
		}

		if (GetComponentInParent<GridController> () == gc.GridC [1]) {
			if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
				if (this.HasBeenPlaced == true && gc.Hand [1].cardsInHand > 0) {
					UpgradeCard (gc.Hand [1]);
				}
			}
		}
	}

	void OnMouseOver () {
		
	}

	private void UpgradeCard (Hand _handToPlaceCardFrom) {
		Strength += _handToPlaceCardFrom.cards [gc.cardSelector].Value;

		_handToPlaceCardFrom.RemoveCardFromHand (gc.cardSelector);
	}

	public void SetVariables (int _x, int _y, string _name, int _strength, bool _hasBeenPlaced, Color _team) {
		X = _x;
		Y = _y;
		Name = _name;
		Strength = _strength;
		HasBeenPlaced = _hasBeenPlaced;
		Team = _team;
	}
}
