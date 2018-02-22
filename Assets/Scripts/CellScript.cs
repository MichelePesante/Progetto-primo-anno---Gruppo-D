using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour {

	/// <summary>
	/// Materiale iniziale.
	/// </summary>
	private Material StartMaterial;

	/// <summary>
	/// Materiale utilizzato quando un oggetto è stato selezionato.
	/// </summary>
	public Material SelectedObject;

	/// <summary>
	/// Se true, è possibile piazzare una carta.
	/// </summary>
	public bool placeable;

	void Start () {
		StartMaterial = this.gameObject.GetComponent<MeshRenderer> ().material;
		placeable = true;
	}

	void OnMouseEnter () {
		if (this.placeable == true)
			this.gameObject.GetComponent<MeshRenderer> ().material = SelectedObject;
	}

	void OnMouseExit () {
		this.gameObject.GetComponent<MeshRenderer> ().material = StartMaterial;
	}
}
