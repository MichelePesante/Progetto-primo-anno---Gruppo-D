using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour {

	public bool placeable;

	private Material StartMaterial;

	public Material SelectedObject;

	public GameObject basePawn;

	public GameObject advancedPawn;

	private GameObject SinglePawn;

	private GameController gc;

	private static int clickCounter;

	void Start () {
		gc = GameController.Instance;
		StartMaterial = this.gameObject.GetComponent<MeshRenderer> ().material;
		placeable = true;
		clickCounter = 0;
	}

	void OnMouseEnter () {
		this.gameObject.GetComponent<MeshRenderer> ().material = SelectedObject;
	}

	void OnMouseExit () {
		this.gameObject.GetComponent<MeshRenderer> ().material = StartMaterial;
	}

	void OnMouseDown () {
		if (this.placeable == true && clickCounter < gc.GridC[0].pawns.Count) {
			PawnPositioning (gc.Hand [0]);
			clickCounter++;
		}
	}

	public void PawnPositioning (Hand _ownHand) {
		if (_ownHand.cardsInHand > 0) {
			if (_ownHand.cards [gc.cardSelector].Value == 1 || _ownHand.cards [gc.cardSelector].Value == 2) {
				SinglePawn = PawnSpawn (basePawn);
				PawnChange ("Pedina base", gc.GridC [0], gc.Hand [0], Color.red);
				this.placeable = false;
			}

			if (_ownHand.cards [gc.cardSelector].Value == 3 || _ownHand.cards [gc.cardSelector].Value == 4) {
				SinglePawn = PawnSpawn (advancedPawn);
				PawnChange ("Pedina avanzata", gc.GridC [0], gc.Hand [0], Color.red);
				this.placeable = false;
			}

			_ownHand.RemoveCardFromHand (gc.cardSelector);
			SetParentPosition (this.gameObject, SinglePawn);
		}
	}

	public void PawnChange (string _ownName, GridController _ownGrid, Hand _ownHand, Color _ownColor) {
		_ownGrid.pawns[clickCounter].Strength = _ownHand.cards [gc.cardSelector].Value;
		_ownGrid.pawns[clickCounter].Name = _ownName;
		_ownGrid.pawns[clickCounter].IsAlive = true;
		_ownGrid.pawns[clickCounter].Team = _ownColor;
	}

	public GameObject PawnSpawn (GameObject _pawnType) {
		GameObject thisPawn = Instantiate (_pawnType, new Vector3 (transform.position.x, transform.localScale.y, transform.position.z), transform.rotation);
		return thisPawn;
	}

	private void SetParentPosition (GameObject _newParent, GameObject _child) {
		_child.transform.parent = _newParent.transform;
	}
}
