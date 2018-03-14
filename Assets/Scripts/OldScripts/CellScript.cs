using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour {

	public int X;

	public int Y;

	/// <summary>
	/// Materiale iniziale.
	/// </summary>
	public Material StartMaterial;

	/// <summary>
	/// Materiale utilizzato quando un oggetto è stato selezionato.
	/// </summary>
	public Material SelectedObject;

	void Start () {
		StartMaterial = this.gameObject.GetComponent<MeshRenderer> ().material;
	}

	public void SetPosition (int _x, int _y) {
		X = _x;
		Y = _y;
	}

	public void ChangeColorOnEnter () {
		this.gameObject.GetComponent<MeshRenderer> ().material = SelectedObject;
	}

	public void ChangeColorOnExit () {
		this.gameObject.GetComponent<MeshRenderer> ().material = StartMaterial;
	}
}
