using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	/// <summary>
	/// Riferimento alla classe CameraScript.
	/// </summary>
	public CameraScript myCamera;

	/// <summary>
	/// Riferimento alla classe GridScript.
	/// </summary>
	public GridScript[] myGrid;

	/// <summary>
	/// Riferimento alla classe PawnScript.
	/// </summary>
	public PawnScript[] myPawn;

	/// <summary>
	/// Consente di capire di chi è il turno, (Quando 'false' è il turno dei neri, che partono per primi)
	/// </summary>
	public bool whiteTurn;

	// Use this for initialization
	void Start () {
		whiteTurn = false;

		myGrid = new GridScript[2];
		myPawn = new PawnScript[2];

		myCamera = GameObject.FindObjectOfType<CameraScript> ();
		myGrid = GameObject.FindObjectsOfType<GridScript> ();
		myPawn = GameObject.FindObjectsOfType<PawnScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			whiteTurn = true;
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			whiteTurn = false;
		}
	}
}
