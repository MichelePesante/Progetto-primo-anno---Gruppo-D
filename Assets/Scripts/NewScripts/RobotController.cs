using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RobotController : MonoBehaviour {

	public RobotData Data;
	public TextMeshProUGUI AttackText;
	public Canvas myCanvas;
    public bool isUpgradable;

    public int X;
	public int Y;
	public int ID;
	public int strength;
	public int OriginalStrength;
	public int UpgradedValue;
	public int upgrade;
	public TurnableGrid3x3 <int> Abilities = new TurnableGrid3x3 <int>();
	public bool [,] AbilityCheck = new bool [3, 3];
    public ParticleSystem spawn;
    public ParticleSystem PowerUp;
    public AudioClip AttackClip;

    private RobotData InstanceData;
	private bool isCanvasActive;

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
		if ((Input.GetKeyDown (KeyCode.Tab) || Input.GetKeyDown (KeyCode.JoystickButton2)) && GameMenu.GameIsPaused == false) {
			SwitchCanvas ();
		}
	}

	void OnMouseDown () {
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameMenu.GameIsPaused == false) {
            if (GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
			    UpgradeRobot (RobotManager.Instance.RobotCurviInHand);
		}

		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameMenu.GameIsPaused == false) {
            if (GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
                UpgradeRobot (RobotManager.Instance.RobotQuadratiInHand);
		}
	}

	private void Setup () {
		if (!Data) {
			return;
		}
		InstanceData = Instantiate(Data);
		ID = InstanceData.Unique_ID;
		strength = InstanceData.Strength;
		upgrade = InstanceData.Upgrade;
		for (int i = 0; i < Data.Ability_Array.Length; i++) {
			Abilities [i % 3, i / 3] = InstanceData.Ability_Array [i];
		}
	}
		
	#region API

	public void UpgradeRobot (List <RobotController> _listToUpgradeFrom) {
		if (_listToUpgradeFrom == RobotManager.Instance.RobotCurviInHand && Y < 3 && isUpgradable) {
            if (JoystickManager.Instance.IsDoubleUpgradeActive == false)
            {
                UpgradedValue += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade;
            }
            else
            {
                UpgradedValue += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade * 2;
                JoystickManager.Instance.IsDoubleUpgradeActive = false;
                JoystickManager.Instance.DoubleUpgradeAlreadyActivated = true;
            }
            AttackText.color = Color.red;
            PowerUp.Play();
			RobotManager.Instance.RemoveRobotFromList (_listToUpgradeFrom, RobotManager.Instance.robotToPlay);
			FindObjectOfType<CardManager> ().PlaceCard (Player.Player_Curve, RobotManager.Instance.robotToPlay);
			RobotManager.Instance.RobotsCurviInHand--;
			RobotManager.Instance.robotUpgraded++;
			RobotManager.Instance.robotToPlay = 0;
			isUpgradable = false;
            if (RobotManager.Instance.robotUpgraded == 2)
            {
                TurnManager.Instance.ChangeTurn();
                RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand, Player.Player_Quad);
                RobotManager.Instance.robotUpgraded = 0;
                FindObjectOfType<Camera>().GetComponentInParent<Animator>().Play("PreparationCameraStart");
                GameManager.isSomeAnimationGoing = true;
            }
        }
		
		if (_listToUpgradeFrom == RobotManager.Instance.RobotQuadratiInHand && Y > 3 && isUpgradable) {
            if (JoystickManager.Instance.IsDoubleUpgradeActive == false)
            {
                UpgradedValue += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade;
            }
            else
            {
                UpgradedValue += _listToUpgradeFrom[RobotManager.Instance.robotToPlay].upgrade * 2;
                JoystickManager.Instance.IsDoubleUpgradeActive = false;
                JoystickManager.Instance.DoubleUpgradeAlreadyActivated = true;
            }
            AttackText.color = Color.red;
            PowerUp.Play();
            RobotManager.Instance.RemoveRobotFromList (_listToUpgradeFrom, RobotManager.Instance.robotToPlay);
			FindObjectOfType<CardManager> ().PlaceCard (Player.Player_Quad, RobotManager.Instance.robotToPlay);
			RobotManager.Instance.RobotsQuadratiInHand--;
			RobotManager.Instance.robotUpgraded++;
			RobotManager.Instance.robotToPlay = 0;
			isUpgradable = false;
            if (RobotManager.Instance.robotUpgraded == 2)
            {
                TurnManager.Instance.CurrentTurnState = TurnManager.TurnState.rotation;
                TurnManager.Instance.ChangeTurn();
                RobotManager.Instance.robotUpgraded = 0;
                FindObjectOfType<Camera>().GetComponentInParent<Animator>().Play("PreparationCameraReturn");
                GameManager.isSomeAnimationGoing = true;
            }
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
			for (int j = 0; j < AbilityCheck.GetLength(1); j++) {
				AbilityCheck [i, j] = false;
			}
		}
	}

	public void RotateRobotMatrix (int _rotationStep) {
		Abilities.Rotate (_rotationStep);
	}

	public void SwitchCanvas () {
		if (isCanvasActive) {
			myCanvas.gameObject.SetActive (false);
			isCanvasActive = false;
		} 
		else {
			myCanvas.gameObject.SetActive (true);
			isCanvasActive = true;
		}
	}

	#endregion
}