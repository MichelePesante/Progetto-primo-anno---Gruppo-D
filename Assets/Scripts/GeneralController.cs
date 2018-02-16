using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour {

	private Material StartMaterial;

	public Material SelectedObject;

	void Start () {
		StartMaterial = this.gameObject.GetComponent<MeshRenderer> ().material;
	}

	void OnMouseEnter () {
		this.gameObject.GetComponent<MeshRenderer> ().material = SelectedObject;
	}

	void OnMouseExit () {
		this.gameObject.GetComponent<MeshRenderer> ().material = StartMaterial;
	}
}
