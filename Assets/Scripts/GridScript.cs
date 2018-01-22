using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

	/// <summary>
	/// Riferimento al game manager.
	/// </summary>
	private GameManager gm;

	/// <summary>
	/// Riferimeno alla prima griglia, quella dei neri.
	/// </summary>
	private GameObject grid_1;

	/// <summary>
	/// Riferimeno alla seconda griglia, quella dei bianchi.
	/// </summary>
	private GameObject grid_2;

	// Use this for initialization
	void Start () {
		grid_1 = GameObject.Find ("Grid_1");
		grid_2 = GameObject.Find ("Grid_2");
		gm = GameObject.FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q) && this.gameObject.name == grid_1.name  && gm.whiteTurn == true) {
			grid_1.transform.position = grid_1.transform.position + Vector3.forward;
		}
	}
}
