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
	public int upgrade;

	private bool isUpgradable;
	private RobotData InstanceData;
	private bool isCanvasActive;

	// Use this for initialization
	void Start () {
		Setup ();
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
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameMenu.GameIsPaused == false) {
			UpgradeRobot (RobotManager.Instance.RobotCurviInHand);
			if (RobotManager.Instance.robotUpgraded == 2) {
				RobotManager.Instance.CardPositionReset (RobotManager.Instance.CarteRobotCurviInHand);
				TurnManager.Instance.ChangeTurn ();
				RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand);
				RobotManager.Instance.robotUpgraded = 0;
				FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("PreparationCameraStart");
			}
		}

		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameMenu.GameIsPaused == false) {
			UpgradeRobot (RobotManager.Instance.RobotQuadratiInHand);
			if (RobotManager.Instance.robotUpgraded == 2) {
				RobotManager.Instance.CardPositionReset (RobotManager.Instance.CarteRobotQuadratiInHand);
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
	}

		
	#region API

	public void UpgradeRobot (List <RobotController> _listToUpgradeFrom) {
		if (_listToUpgradeFrom == RobotManager.Instance.RobotCurviInHand && Y < 3 && isUpgradable) {
			strength += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade;
			AttackText.color = Color.red;
			RobotManager.Instance.RemoveRobotFromList (_listToUpgradeFrom, RobotManager.Instance.robotToPlay);
			//RobotManager.Instance.CarteRobotCurviInHand.Remove (RobotManager.Instance.CarteRobotCurviInHand [RobotManager.Instance.robotToPlay]);
			RobotManager.Instance.RobotsCurviInHand--;
			RobotManager.Instance.robotUpgraded++;
			RobotManager.Instance.robotToPlay = 0;
			isUpgradable = false;
		}
		
		if (_listToUpgradeFrom == RobotManager.Instance.RobotQuadratiInHand && Y > 3 && isUpgradable) {
			strength += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade;
			AttackText.color = Color.red;
			RobotManager.Instance.RemoveRobotFromList (_listToUpgradeFrom, RobotManager.Instance.robotToPlay);
			//RobotManager.Instance.CarteRobotQuadrati.Remove (RobotManager.Instance.CarteRobotQuadrati [RobotManager.Instance.robotToPlay]);
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

	public int GetStrength () {
		return strength;
	}

	public void AddStrength () {
		
	}

	#endregion
}
