using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour {

	/// <summary>
	/// Coordinata x.
	/// </summary>
	public int X;

	/// <summary>
	/// Coordinata y.
	/// </summary>
	public int Y;

	/// <summary>
	/// Se true, vuol dire che la pedina corrente è stata piazzata.
	/// </summary>
	public bool HasBeenPlaced;

	/// <summary>
	/// Riferimento alla classe GameController.
	/// </summary>
	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameController.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
