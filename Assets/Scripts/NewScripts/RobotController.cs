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
	public int strength;
	public int OriginalStrength;
	public int UpgradedValue;
	public int upgrade;
	public int [,] Abilities = new int[3, 3];
	public bool[,] AbilityCheck = new bool[3, 3];

	private bool isUpgradable;
	private RobotData InstanceData;
	private bool isCanvasActive;

	// Use this for initialization
	void Start () {
		Setup ();
		SetAbilityCheckToFalse ();
		OriginalStrength = strength;
		isUpgradable = true;
		AttackText = GetComponentInChildren<TextMeshProUGUI> ();
		myCanvas = GetComponentInChildren<Canvas> ();
		myCanvas.gameObject.SetActive (false);
	}

	void Update () {
		AttackText.text = strength.ToString();
		if (Input.GetKeyDown (KeyCode.Tab) && GameMenu.GameIsPaused == false) {
			if (isCanvasActive) {
				myCanvas.gameObject.SetActive (false);
				isCanvasActive = false;
			} 
			else {
				myCanvas.gameObject.SetActive (true);
				isCanvasActive = true;
			}
		}
	}

	void OnMouseDown () {
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameMenu.GameIsPaused == false) {
			UpgradeRobot (RobotManager.Instance.RobotCurviInHand);
			if (RobotManager.Instance.robotUpgraded == 2) {
				TurnManager.Instance.ChangeTurn ();
				RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand);
				RobotManager.Instance.robotUpgraded = 0;
				FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("PreparationCameraStart");
			}
		}

		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameMenu.GameIsPaused == false) {
			UpgradeRobot (RobotManager.Instance.RobotQuadratiInHand);
			if (RobotManager.Instance.robotUpgraded == 2) {
				TurnManager.Instance.CurrentTurnState = TurnManager.TurnState.rotation;
				TurnManager.Instance.ChangeTurn ();
				RobotManager.Instance.robotUpgraded = 0;
				FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("PreparationCameraReturn");
			}
		}
	}

	private void Setup () {
		if (!Data) {
			return;
		}
		InstanceData = Instantiate <RobotData> (Data);
		ID = InstanceData.Unique_ID;
		strength = InstanceData.Strength;
		upgrade = InstanceData.Upgrade;
		for (int i = 0; i < Data.Ability_Array.Length; i++) {
			Abilities [i / 3, i % 3] = InstanceData.Ability_Array [i];
		}
	}
		
	#region API

	public void UpgradeRobot (List <RobotController> _listToUpgradeFrom) {
		if (_listToUpgradeFrom == RobotManager.Instance.RobotCurviInHand && Y < 3 && isUpgradable) {
			UpgradedValue += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade;
			AttackText.color = Color.red;
			RobotManager.Instance.RemoveRobotFromList (_listToUpgradeFrom, RobotManager.Instance.robotToPlay);
			FindObjectOfType<CardManager> ().PlaceCard (Player.Player_Curve, RobotManager.Instance.robotToPlay);
			RobotManager.Instance.RobotsCurviInHand--;
			RobotManager.Instance.robotUpgraded++;
			RobotManager.Instance.robotToPlay = 0;
			isUpgradable = false;
		}
		
		if (_listToUpgradeFrom == RobotManager.Instance.RobotQuadratiInHand && Y > 3 && isUpgradable) {
			UpgradedValue += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade;
			AttackText.color = Color.red;
			RobotManager.Instance.RemoveRobotFromList (_listToUpgradeFrom, RobotManager.Instance.robotToPlay);
			FindObjectOfType<CardManager> ().PlaceCard (Player.Player_Quad, RobotManager.Instance.robotToPlay);
			RobotManager.Instance.RobotsQuadratiInHand--;
			RobotManager.Instance.robotUpgraded++;
			RobotManager.Instance.robotToPlay = 0;
			isUpgradable = false;
		}
	}

	public void ResetRobotStrength () {
		strength = OriginalStrength;
	}

	public void SetPosition () {
		X = GetComponentInParent<ColliderController> ().X;
		Y = GetComponentInParent<ColliderController> ().Y;
	}

	public void SetAbilityCheckToFalse () {
		for (int i = 0; i < AbilityCheck.GetLength(0); i++) {
			for (int j = 0; j < AbilityCheck.GetLength (1); j++) {
				AbilityCheck [i, j] = false;
			}
		}
	}

	#endregion
}