using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour {

	public bool placeable;

	public int xCoordinate;

	public int yCoordinate;

	private Material StartMaterial;

	public Material SelectedObject;

	public GameObject basePawn;

	public GameObject advancedPawn;

	private GameObject SinglePawn;

	private GameController gc;

	private int cardSelector;

	private int clickCounter;

	void Start () {
		gc = GameController.Instance;
		StartMaterial = this.gameObject.GetComponent<MeshRenderer> ().material;
		placeable = true;
		cardSelector = 0;
	}

	void OnMouseEnter () {
		this.gameObject.GetComponent<MeshRenderer> ().material = SelectedObject;
	}

	void OnMouseExit () {
		this.gameObject.GetComponent<MeshRenderer> ().material = StartMaterial;
	}

	void OnMouseDown () {
		if (this.placeable == true) {
			PawnPositioning (gc.Hand [0], gc.GridC [0], Color.red);
		}

		clickCounter++;
	}

	public void PawnPositioning (Hand _ownHand) {
		if (_ownHand.cardsInHand > 0) {
			if (_ownHand.cards [cardSelector].Value == 1 || _ownHand.cards [cardSelector].Value == 2) {
				SinglePawn = PawnSpawn (basePawn);
				this.placeable = false;
			}

			if (_ownHand.cards [cardSelector].Value == 3 || _ownHand.cards [cardSelector].Value == 4) {
				SinglePawn = PawnSpawn (advancedPawn);
				this.placeable = false;
			}

			_ownHand.RemoveCardFromHand (cardSelector);
		}

		SetParentPosition (this.gameObject, SinglePawn);
	}

	public GameObject PawnSpawn (GameObject _pawnType) {
		GameObject thisPawn = Instantiate (_pawnType, new Vector3 (transform.position.x, transform.localScale.y, transform.position.z), transform.rotation);
		return thisPawn;
	}

	private void SetParentPosition (GameObject _newParent, GameObject _child) {
		_child.transform.parent = _newParent.transform;
	}
}
