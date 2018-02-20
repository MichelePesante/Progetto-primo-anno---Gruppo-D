using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour {

	private Material StartMaterial;

	public Material SelectedObject;

	public GameObject pawn;

	void Start () {
		StartMaterial = this.gameObject.GetComponent<MeshRenderer> ().material;
	}

	void OnMouseEnter () {
		this.gameObject.GetComponent<MeshRenderer> ().material = SelectedObject;
	}

	void OnMouseExit () {
		this.gameObject.GetComponent<MeshRenderer> ().material = StartMaterial;
	}

	void OnMouseDown () {
		Instantiate (pawn, new Vector3 (transform.position.x, transform.position.y + transform.localScale.y, transform.position.z), transform.rotation);
	}
}
