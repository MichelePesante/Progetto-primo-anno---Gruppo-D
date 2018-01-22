using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour {

	/// <summary>
	/// Forza della singola pedina.
	/// </summary>
	private int strength;

	/// <summary>
	/// Tasto per andare in alto.
	/// </summary>
	public KeyCode up;

	/// <summary>
	/// Tasto per andare in basso.
	/// </summary>
	public KeyCode down;

	/// <summary>
	/// Tasto per andare a destra.
	/// </summary>
	public KeyCode right;

	/// <summary>
	/// Tasto per andare in sinistra.
	/// </summary>
	public KeyCode left;

	/// <summary>
	/// Riferimento al game manager ha i riferimenti a tutti gli script.
	/// </summary>
	private GameManager gm;

	// Use this for initialization
	void Start () {
		if (this.gameObject.name == "Blue_Pawn")
			strength = 1;
		if (this.gameObject.name == "Red_Pawn")
			strength = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (up)) {
			transform.position = transform.position + Vector3.forward;
		}
		if (Input.GetKeyDown (down)) {
			transform.position = transform.position + Vector3.back;
		}
		if (Input.GetKeyDown (right)) {
			transform.position = transform.position + Vector3.right;
		}
		if (Input.GetKeyDown (left)) {
			transform.position = transform.position + Vector3.left;
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Player") {
			if (collision.gameObject.GetComponent<PawnScript> ().strength < this.strength) {
				Destroy (collision.gameObject);
			} 
			else {
				Destroy (this.gameObject);
			}
		}
	}
}
