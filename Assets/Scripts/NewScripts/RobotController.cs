using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RobotController : MonoBehaviour {

	public RobotData Data;
	public TextMeshProUGUI AttackText;
	public Canvas myCanvas;

	public int X;
	public int Y;
	public int ID;

	private int strength;
	private int upgrade;
	private int abilità_1;
	private int abilità_2;
	private int abilità_3;
	private int abilità_4;
	private RobotData InstanceData;

	// Use this for initialization
	void Start () {
		Setup ();
		AttackText = GetComponentInChildren<TextMeshProUGUI> ();
		myCanvas = GetComponentInChildren<Canvas> ();
		myCanvas.gameObject.SetActive (false);
	}

	void Update () {
		AttackText.text = strength.ToString();
		if (Input.GetKeyDown (KeyCode.Space) && GameMenu.GameIsPaused == false) {
			myCanvas.gameObject.SetActive (true);
		}
		if (Input.GetKeyUp (KeyCode.Space) && GameMenu.GameIsPaused == false) {
			myCanvas.gameObject.SetActive (false);
		}
	}

	void OnMouseDown () {
		
	}

	private void Setup () {
		if (!Data) {
			return;
		}
		InstanceData = Instantiate <RobotData> (Data);
		strength = Data.Strength;
		upgrade = Data.Upgrade;
		abilità_1 = Data.Abilità_1;
		abilità_2 = Data.Abilità_2;
		abilità_3 = Data.Abilità_3;
		abilità_4 = Data.Abilità_4;
	}

	#region API

	public void SetPosition () {
		X = GetComponentInParent<ColliderController> ().X;
		Y = GetComponentInParent<ColliderController> ().Y;
	}

	public int GetStrength () {
		return strength;
	}

	public void AddStrength () {
		
	}

	#endregion
}
