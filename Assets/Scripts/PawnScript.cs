using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour {

	/// <summary>
	/// Materiale iniziale.
	/// </summary>
	public Material StartMaterial;

	public int X;

	public int Y;

	public string Name;

	public int Strength;

	public bool HasBeenPlaced;

	public Color Team;

	public RobotData Data;

	private RobotData instanceData;

	/// <summary>
	/// Materiale utilizzato quando un oggetto è stato selezionato.
	/// </summary>
	public Material SelectedObject;

	// Use this for initialization
	void Start () {
		StartMaterial = this.gameObject.GetComponent<MeshRenderer> ().material;
		Setup ();
	}

	/*
	void OnMouseEnter () {
		this.gameObject.GetComponent<MeshRenderer> ().material = SelectedObject;
	}

	void OnMouseExit () {
		this.gameObject.GetComponent<MeshRenderer> ().material = StartMaterial;
	}
	*/

	void OnMouseDown () {
		if (StateMachine.CurrentMacroPhase == StateMachine.MacroPhase.Core) {
			CorePhase.UpgradingPhase (this);
		}
	}

	void OnMouseOver () {
		
	}

	public void SetVariables (int _x, int _y, string _name, int _strength, bool _hasBeenPlaced, Color _team) {
		X = _x;
		Y = _y;
		Name = _name;
		Strength = _strength;
		HasBeenPlaced = _hasBeenPlaced;
		Team = _team;
	}

	private void Setup () {
		if (!Data)
			return;
		instanceData = Instantiate<RobotData> (Data);
		Strength = instanceData.Strength;
		X = instanceData.X;
		Y = instanceData.Y;
	}
}
