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
	public int upgrade;

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
		if (Input.GetKeyDown (KeyCode.Tab) && GameMenu.GameIsPaused == false) {
			myCanvas.gameObject.SetActive (true);
		}
		if (Input.GetKeyUp (KeyCode.Tab) && GameMenu.GameIsPaused == false) {
			myCanvas.gameObject.SetActive (false);
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
		strength = Data.Strength;
		upgrade = Data.Upgrade;
		abilità_1 = Data.Abilità_1;
		abilità_2 = Data.Abilità_2;
		abilità_3 = Data.Abilità_3;
		abilità_4 = Data.Abilità_4;
	}

	public void UpgradeRobot (List <RobotController> _listToUpgradeFrom) {
		if (_listToUpgradeFrom == RobotManager.Instance.RobotCurviInHand && Y < 3) {
			strength += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade;
			RobotManager.Instance.RemoveRobotFromList (_listToUpgradeFrom, RobotManager.Instance.robotToPlay);
			RobotManager.Instance.CarteRobotCurvi.Remove (RobotManager.Instance.CarteRobotCurvi [RobotManager.Instance.robotToPlay]);
			RobotManager.Instance.RobotsCurviInHand--;
			RobotManager.Instance.robotUpgraded++;
			RobotManager.Instance.robotToPlay = 0;
		}

		if (_listToUpgradeFrom == RobotManager.Instance.RobotQuadratiInHand && Y > 3) {
			strength += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade;
			RobotManager.Instance.RemoveRobotFromList (_listToUpgradeFrom, RobotManager.Instance.robotToPlay);
			RobotManager.Instance.CarteRobotQuadrati.Remove (RobotManager.Instance.CarteRobotQuadrati [RobotManager.Instance.robotToPlay]);
			RobotManager.Instance.RobotsQuadratiInHand--;
			RobotManager.Instance.robotUpgraded++;
			RobotManager.Instance.robotToPlay = 0;
		}
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
