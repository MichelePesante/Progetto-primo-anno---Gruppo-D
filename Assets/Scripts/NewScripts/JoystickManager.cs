using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour {

    public static JoystickManager Instance;

    public StickPosition CurrentStickPosition;

	public bool HasMyGridAlreadyBeenRotated;
	public bool HasEnemyGridAlreadyBeenRotated;
    private bool isDoubleRotationActive;
    public bool IsDoubleRotationActive { get { return isDoubleRotationActive; } set { if (DoubleRotationAlreadyActivated == false) isDoubleRotationActive = value; } }
    private bool isDoubleUpgradeActive;
    public bool IsDoubleUpgradeActive { get { return isDoubleUpgradeActive; } set { if (DoubleUpgradeAlreadyActivated == false) isDoubleUpgradeActive = value; } }
    public bool DoubleRotationAlreadyActivated;
    public bool DoubleUpgradeAlreadyActivated;

    private ArrowManager am;
	private ButtonManager bm;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (gameObject);
		}
	}

	void Start () {
        am = ArrowManager.Instance;
		bm = ButtonManager.Instance;
	}

	void Update () {
        if (GameMenu.GameIsPaused == false)
        {
            if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
            {
                StickOrientation("X_Curve", "Y_Curve");
            }
            else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
            {
                StickOrientation("X_Quad", "Y_Quad");
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                NewUIManager.Instance.TutorialHelp();
            }
        }

        if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.rotation && GameMenu.GameIsPaused == false && GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
        {
            if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
            {
                if ((Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Q)) && EnergyManager.Instance.Curve_Energy >= EnergyManager.Instance.RotationCost && !DoubleRotationAlreadyActivated && !IsDoubleRotationActive)
                {
                    IsDoubleRotationActive = true;
                    EnergyManager.Instance.SubCurveEnergy(EnergyManager.Instance.RotationCost);
                    EnergyManager.Instance.RefreshEnergy();
                    NewUIManager.Instance.DoubleRotation.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) && ButtonManager.Instance.Skip_Turn.gameObject.activeInHierarchy) {
                    ButtonManager.Instance.EndRotationTurn();
                }
		        if (Input.GetAxis ("MyGridRotation_Curve") < 0 && !HasMyGridAlreadyBeenRotated)
                {
		        	bm.CurveGridClockwiseRotation ();
		        	bm.RotationCheck ();
		        	HasMyGridAlreadyBeenRotated = true;
		        }
		        else if (Input.GetAxis ("MyGridRotation_Curve") > 0 && !HasMyGridAlreadyBeenRotated)
                {
		        	bm.CurveGridCounterclockwiseRotation ();
		        	bm.RotationCheck ();
		        	HasMyGridAlreadyBeenRotated = true;
		        }
		        if (Input.GetAxis ("EnemyGridClockwiseRotation_Curve") > 0 && !HasEnemyGridAlreadyBeenRotated)
                {
		        	bm.QuadGridClockwiseRotation ();
		        	bm.RotationCheck ();
		        	HasEnemyGridAlreadyBeenRotated = true;
		        }
		        else if (Input.GetAxis ("EnemyGridCounterclockwiseRotation_Curve") > 0 && !HasEnemyGridAlreadyBeenRotated)
                {
		        	bm.QuadGridCounterclockwiseRotation ();
		        	bm.RotationCheck ();
		        	HasEnemyGridAlreadyBeenRotated = true;
		        }
		    }
		    else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
            {
                if ((Input.GetKeyDown(KeyCode.Joystick2Button3) || Input.GetKeyDown(KeyCode.Q)) && EnergyManager.Instance.Quad_Energy >= EnergyManager.Instance.RotationCost && !DoubleRotationAlreadyActivated && !IsDoubleRotationActive)
                {
                    IsDoubleRotationActive = true;
                    EnergyManager.Instance.SubQuadEnergy(EnergyManager.Instance.RotationCost);
                    EnergyManager.Instance.RefreshEnergy();
                    NewUIManager.Instance.DoubleRotation.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.Joystick2Button0) && ButtonManager.Instance.Skip_Turn.gameObject.activeInHierarchy)
                {
                    ButtonManager.Instance.EndRotationTurn();
                }
                if (Input.GetAxis ("MyGridRotation_Quad") < 0 && !HasMyGridAlreadyBeenRotated)
                {
		    		bm.QuadGridClockwiseRotation ();
		    		bm.RotationCheck ();
		    		HasMyGridAlreadyBeenRotated = true;
		    	}
		    	else if (Input.GetAxis ("MyGridRotation_Quad") > 0 && !HasMyGridAlreadyBeenRotated)
                {
		    		bm.QuadGridCounterclockwiseRotation ();
		    		bm.RotationCheck ();
		    		HasMyGridAlreadyBeenRotated = true;
		    	}
		    	if (Input.GetAxis ("EnemyGridClockwiseRotation_Quad") > 0 && !HasEnemyGridAlreadyBeenRotated)
                {
		    		bm.CurveGridClockwiseRotation ();
		    		bm.RotationCheck ();
		    		HasEnemyGridAlreadyBeenRotated = true;
		    	} 
		    	else if (Input.GetAxis ("EnemyGridCounterclockwiseRotation_Quad") > 0 && !HasEnemyGridAlreadyBeenRotated)
                {
		    		bm.CurveGridCounterclockwiseRotation ();
		    		bm.RotationCheck ();
		    		HasEnemyGridAlreadyBeenRotated = true;
		    	}
		    }
		}

        if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameMenu.GameIsPaused == false && GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
        {
            if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
            {
                if ((Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Q)) && EnergyManager.Instance.Curve_Energy >= EnergyManager.Instance.UpgradeCost && !DoubleUpgradeAlreadyActivated && !IsDoubleUpgradeActive)
                {
                    IsDoubleUpgradeActive = true;
                    EnergyManager.Instance.SubCurveEnergy(EnergyManager.Instance.UpgradeCost);
                    EnergyManager.Instance.RefreshEnergy();
                    NewUIManager.Instance.DoubleUpgrade.SetActive(false);
                }
            }
            else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
            {
                if ((Input.GetKeyDown(KeyCode.Joystick2Button3) || Input.GetKeyDown(KeyCode.Q)) && EnergyManager.Instance.Quad_Energy >= EnergyManager.Instance.UpgradeCost && !DoubleUpgradeAlreadyActivated && !IsDoubleUpgradeActive)
                {
                    IsDoubleUpgradeActive = true;
                    EnergyManager.Instance.SubQuadEnergy(EnergyManager.Instance.UpgradeCost);
                    EnergyManager.Instance.RefreshEnergy();
                    NewUIManager.Instance.DoubleUpgrade.SetActive(false);
                }
            }
        }
    }


    public void StickOrientation(string xAxis, string yAxis) {
        if (GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
        {
            if ((Input.GetAxis(xAxis) > -0.2f && Input.GetAxis(xAxis) < 0.2f) && (Input.GetAxis(yAxis) > -0.2f && Input.GetAxis(yAxis) < 0.2f))
            {
                CurrentStickPosition = StickPosition.C;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                }
            }
            else if ((Input.GetAxis(xAxis) > 0.3f && Input.GetAxis(xAxis) < 1f) && (Input.GetAxis(yAxis) > 0.3f && Input.GetAxis(yAxis) < 1f))
            {
                CurrentStickPosition = StickPosition.NE;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                    am.Freccia_Nord_Est_Curve.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                    am.Freccia_Nord_Est_Quad.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                PlayRobotFromJoystick(CurrentStickPosition);
                UpgradeRobotFromJoystick(CurrentStickPosition);
            }
            else if ((Input.GetAxis(xAxis) > 0.3f && Input.GetAxis(xAxis) < 1f) && (Input.GetAxis(yAxis) < -0.3f && Input.GetAxis(yAxis) > -1f))
            {
                CurrentStickPosition = StickPosition.SE;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                    am.Freccia_Sud_Est_Curve.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                    am.Freccia_Sud_Est_Quad.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                PlayRobotFromJoystick(CurrentStickPosition);
                UpgradeRobotFromJoystick(CurrentStickPosition);
            }
            else if ((Input.GetAxis(xAxis) < -0.3f && Input.GetAxis(xAxis) > -1f) && (Input.GetAxis(yAxis) > 0.3f && Input.GetAxis(yAxis) < 1f))
            {
                CurrentStickPosition = StickPosition.NW;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                    am.Freccia_Nord_Ovest_Curve.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                    am.Freccia_Nord_Ovest_Quad.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                PlayRobotFromJoystick(CurrentStickPosition);
                UpgradeRobotFromJoystick(CurrentStickPosition);
            }
            else if ((Input.GetAxis(xAxis) < -0.3f && Input.GetAxis(xAxis) > -1f) && (Input.GetAxis(yAxis) < -0.3f && Input.GetAxis(yAxis) > -1f))
            {
                CurrentStickPosition = StickPosition.SW;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                    am.Freccia_Sud_Ovest_Curve.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                    am.Freccia_Sud_Ovest_Quad.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                PlayRobotFromJoystick(CurrentStickPosition);
                UpgradeRobotFromJoystick(CurrentStickPosition);
            }
            else if (Input.GetAxis(xAxis) < -0.5f)
            {
                CurrentStickPosition = StickPosition.W;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                    am.Freccia_Ovest_Curve.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                    am.Freccia_Ovest_Quad.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                PlayRobotFromJoystick(CurrentStickPosition);
                UpgradeRobotFromJoystick(CurrentStickPosition);
            }
            else if (Input.GetAxis(xAxis) > 0.5f)
            {
                CurrentStickPosition = StickPosition.E;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                    am.Freccia_Est_Curve.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                    am.Freccia_Est_Quad.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                PlayRobotFromJoystick(CurrentStickPosition);
                UpgradeRobotFromJoystick(CurrentStickPosition);
            }
            else if (Input.GetAxis(yAxis) < -0.5f)
            {
                CurrentStickPosition = StickPosition.S;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                    am.Freccia_Sud_Curve.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                    am.Freccia_Sud_Quad.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                PlayRobotFromJoystick(CurrentStickPosition);
                UpgradeRobotFromJoystick(CurrentStickPosition);
            }
            else if (Input.GetAxis(yAxis) > 0.5f)
            {
                CurrentStickPosition = StickPosition.N;
                if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
                {
                    am.ResetAllCurveMaterials();
                    am.Freccia_Nord_Curve.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
                {
                    am.ResetAllQuadMaterials();
                    am.Freccia_Nord_Quad.GetComponent<Renderer>().material = am.HighlightMaterial;
                }
                PlayRobotFromJoystick(CurrentStickPosition);
                UpgradeRobotFromJoystick(CurrentStickPosition);
            }
        }
    }

    private void PlayRobotFromJoystick(StickPosition _currentStickPosition) {
        if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing && GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
        {
            if (Input.GetKeyUp(KeyCode.Joystick1Button0) && TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
            {
                switch (_currentStickPosition)
                {
                    case StickPosition.N:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 1, 2);
                        am.Freccia_Nord_Curve.SetActive(false);
                        break;
                    case StickPosition.NE:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 2, 2);
                        am.Freccia_Nord_Est_Curve.SetActive(false);
                        break;
                    case StickPosition.E:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 2, 1);
                        am.Freccia_Est_Curve.SetActive(false);
                        break;
                    case StickPosition.SE:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 2, 0);
                        am.Freccia_Sud_Est_Curve.SetActive(false);
                        break;
                    case StickPosition.S:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 1, 0);
                        am.Freccia_Sud_Curve.SetActive(false);
                        break;
                    case StickPosition.SW:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 0, 0);
                        am.Freccia_Sud_Ovest_Curve.SetActive(false);
                        break;
                    case StickPosition.W:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 0, 1);
                        am.Freccia_Ovest_Curve.SetActive(false);
                        break;
                    case StickPosition.NW:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 0, 2);
                        am.Freccia_Nord_Ovest_Curve.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
            if (Input.GetKeyUp(KeyCode.Joystick2Button0) && TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
            {
                switch (_currentStickPosition)
                {
                    case StickPosition.N:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 1, 6);
                        am.Freccia_Nord_Quad.SetActive(false);
                        break;
                    case StickPosition.NE:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 2, 6);
                        am.Freccia_Nord_Est_Quad.SetActive(false);
                        break;
                    case StickPosition.E:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 2, 5);
                        am.Freccia_Est_Quad.SetActive(false);
                        break;
                    case StickPosition.SE:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 2, 4);
                        am.Freccia_Sud_Est_Quad.SetActive(false);
                        break;
                    case StickPosition.S:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 1, 4);
                        am.Freccia_Sud_Quad.SetActive(false);
                        break;
                    case StickPosition.SW:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 0, 4);
                        am.Freccia_Sud_Ovest_Quad.SetActive(false);
                        break;
                    case StickPosition.W:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 0, 5);
                        am.Freccia_Ovest_Quad.SetActive(false);
                        break;
                    case StickPosition.NW:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 0, 6);
                        am.Freccia_Nord_Ovest_Quad.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void UpgradeRobotFromJoystick(StickPosition _currentStickPosition)
    {
        if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
        {
            if (Input.GetKeyUp(KeyCode.Joystick1Button0) && TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
            {
                switch (_currentStickPosition)
                {
                    case StickPosition.N:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 1, 2);
                        //am.Freccia_Nord_Curve.SetActive(false);
                        break;
                    case StickPosition.NE:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 2, 2);
                        //am.Freccia_Nord_Est_Curve.SetActive(false);
                        break;
                    case StickPosition.E:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 2, 1);
                        //am.Freccia_Est_Curve.SetActive(false);
                        break;
                    case StickPosition.SE:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 2, 0);
                        //am.Freccia_Sud_Est_Curve.SetActive(false);
                        break;
                    case StickPosition.S:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 1, 0);
                        //am.Freccia_Sud_Curve.SetActive(false);
                        break;
                    case StickPosition.SW:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 0, 0);
                        //am.Freccia_Sud_Ovest_Curve.SetActive(false);
                        break;
                    case StickPosition.W:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 0, 1);
                        //am.Freccia_Ovest_Curve.SetActive(false);
                        break;
                    case StickPosition.NW:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 0, 2);
                        //am.Freccia_Nord_Ovest_Curve.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Joystick2Button0) && TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
            {
                switch (_currentStickPosition)
                {
                    case StickPosition.N:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 1, 6);
                        //am.Freccia_Nord_Quad.SetActive(false);
                        break;
                    case StickPosition.NE:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 2, 6);
                        //am.Freccia_Nord_Est_Quad.SetActive(false);
                        break;
                    case StickPosition.E:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 2, 5);
                        //am.Freccia_Est_Quad.SetActive(false);
                        break;
                    case StickPosition.SE:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 2, 4);
                        //am.Freccia_Sud_Est_Quad.SetActive(false);
                        break;
                    case StickPosition.S:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 1, 4);
                        //am.Freccia_Sud_Quad.SetActive(false);
                        break;
                    case StickPosition.SW:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 0, 4);
                        //am.Freccia_Sud_Ovest_Quad.SetActive(false);
                        break;
                    case StickPosition.W:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 0, 5);
                        //am.Freccia_Ovest_Quad.SetActive(false);
                        break;
                    case StickPosition.NW:
                        RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 0, 6);
                        //am.Freccia_Nord_Ovest_Quad.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

public enum StickPosition {
    C = 0,
    N = 1,
    NE = 2,
    E = 3,
    SE = 4,
    S = 5,
    SW = 6,
    W = 7,
    NW = 8
}