using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JoystickManager : MonoBehaviour {

    public static JoystickManager Instance;

    public StickPosition CurrentStickPosition;

	public bool hasMyGridAlreadyBeenRotated;
	public bool hasEnemyGridAlreadyBeenRotated; 

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
        }

        if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.rotation && GameMenu.GameIsPaused == false && GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
        {
            if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
            {
		        if (Input.GetAxis ("MyGridRotation_Curve") < 0 && !hasMyGridAlreadyBeenRotated)
                {
		        	bm.CurveGridClockwiseRotation ();
		        	bm.RotationCheck ();
		        	hasMyGridAlreadyBeenRotated = true;
		        }
		        else if (Input.GetAxis ("MyGridRotation_Curve") > 0 && !hasMyGridAlreadyBeenRotated)
                {
		        	bm.CurveGridCounterclockwiseRotation ();
		        	bm.RotationCheck ();
		        	hasMyGridAlreadyBeenRotated = true;
		        }
		        if (Input.GetAxis ("EnemyGridClockwiseRotation_Curve") > 0 && !hasEnemyGridAlreadyBeenRotated)
                {
		        	bm.QuadGridClockwiseRotation ();
		        	bm.RotationCheck ();
		        	hasEnemyGridAlreadyBeenRotated = true;
		        }
		        else if (Input.GetAxis ("EnemyGridCounterclockwiseRotation_Curve") > 0 && !hasEnemyGridAlreadyBeenRotated)
                {
		        	bm.QuadGridCounterclockwiseRotation ();
		        	bm.RotationCheck ();
		        	hasEnemyGridAlreadyBeenRotated = true;
		        }
		    }
		    else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
            {
		    	if (Input.GetAxis ("MyGridRotation_Quad") < 0 && !hasMyGridAlreadyBeenRotated)
                {
		    		bm.QuadGridClockwiseRotation ();
		    		bm.RotationCheck ();
		    		hasMyGridAlreadyBeenRotated = true;
		    	}
		    	else if (Input.GetAxis ("MyGridRotation_Quad") > 0 && !hasMyGridAlreadyBeenRotated)
                {
		    		bm.QuadGridCounterclockwiseRotation ();
		    		bm.RotationCheck ();
		    		hasMyGridAlreadyBeenRotated = true;
		    	}
		    	if (Input.GetAxis ("EnemyGridClockwiseRotation_Quad") > 0 && !hasEnemyGridAlreadyBeenRotated)
                {
		    		bm.CurveGridClockwiseRotation ();
		    		bm.RotationCheck ();
		    		hasEnemyGridAlreadyBeenRotated = true;
		    	} 
		    	else if (Input.GetAxis ("EnemyGridCounterclockwiseRotation_Quad") > 0 && !hasEnemyGridAlreadyBeenRotated)
                {
		    		bm.CurveGridCounterclockwiseRotation ();
		    		bm.RotationCheck ();
		    		hasEnemyGridAlreadyBeenRotated = true;
		    	}
		    }
		}
	}

    public void StickOrientation(string xAxis, string yAxis) {
        if ((Input.GetAxis(xAxis) > -0.2f && Input.GetAxis(xAxis) < 0.2f) && (Input.GetAxis(yAxis) > -0.2f && Input.GetAxis(yAxis) < 0.2f))
        {
            CurrentStickPosition = StickPosition.C;
        }
        else if ((Input.GetAxis(xAxis) > 0.3f && Input.GetAxis(xAxis) < 1f) && (Input.GetAxis(yAxis) > 0.3f && Input.GetAxis(yAxis) < 1f))
        {
            CurrentStickPosition = StickPosition.NE;
            PlayRobotFromJoystick(CurrentStickPosition);
            UpgradeRobotFromJoystick(CurrentStickPosition);
        }
        else if ((Input.GetAxis(xAxis) > 0.3f && Input.GetAxis(xAxis) < 1f) && (Input.GetAxis(yAxis) < -0.3f && Input.GetAxis(yAxis) > -1f))
        {
            CurrentStickPosition = StickPosition.SE;
            PlayRobotFromJoystick(CurrentStickPosition);
            UpgradeRobotFromJoystick(CurrentStickPosition);
        }
        else if ((Input.GetAxis(xAxis) < -0.3f && Input.GetAxis(xAxis) > -1f) && (Input.GetAxis(yAxis) > 0.3f && Input.GetAxis(yAxis) < 1f))
        {
            CurrentStickPosition = StickPosition.NW;
            PlayRobotFromJoystick(CurrentStickPosition);
            UpgradeRobotFromJoystick(CurrentStickPosition);
        }
        else if ((Input.GetAxis(xAxis) < -0.3f && Input.GetAxis(xAxis) > -1f) && (Input.GetAxis(yAxis) < -0.3f && Input.GetAxis(yAxis) > -1f))
        {
            CurrentStickPosition = StickPosition.SW;
            PlayRobotFromJoystick(CurrentStickPosition);
            UpgradeRobotFromJoystick(CurrentStickPosition);
        }
        else if (Input.GetAxis(xAxis) < -0.5f)
        {
            CurrentStickPosition = StickPosition.W;
            PlayRobotFromJoystick(CurrentStickPosition);
            UpgradeRobotFromJoystick(CurrentStickPosition);
        }
        else if (Input.GetAxis(xAxis) > 0.5f)
        {
            CurrentStickPosition = StickPosition.E;
            PlayRobotFromJoystick(CurrentStickPosition);
            UpgradeRobotFromJoystick(CurrentStickPosition);
        }
        else if (Input.GetAxis(yAxis) < -0.5f)
        {
            CurrentStickPosition = StickPosition.S;
            PlayRobotFromJoystick(CurrentStickPosition);
            UpgradeRobotFromJoystick(CurrentStickPosition);
        }
        else if (Input.GetAxis(yAxis) > 0.5f)
        {
            CurrentStickPosition = StickPosition.N;
            PlayRobotFromJoystick(CurrentStickPosition);
            UpgradeRobotFromJoystick(CurrentStickPosition);
        }
    }

    private void PlayRobotFromJoystick(StickPosition _currentStickPosition) {
        if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing && GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
        {
            if (Input.GetKeyUp(KeyCode.Joystick1Button0))
            {
                switch (_currentStickPosition)
                {
                    case StickPosition.N:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 1, 2);
                        break;
                    case StickPosition.NE:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 2, 2);
                        break;
                    case StickPosition.E:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 2, 1);
                        break;
                    case StickPosition.SE:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 2, 0);
                        break;
                    case StickPosition.S:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 1, 0);
                        break;
                    case StickPosition.SW:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 0, 0);
                        break;
                    case StickPosition.W:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 0, 1);
                        break;
                    case StickPosition.NW:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurviGiocati, 0, 2);
                        break;
                    default:
                        break;
                }
            }
            if (Input.GetKeyUp(KeyCode.Joystick2Button0))
            {
                switch (_currentStickPosition)
                {
                    case StickPosition.N:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 1, 6);
                        break;
                    case StickPosition.NE:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 2, 6);
                        break;
                    case StickPosition.E:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 2, 5);
                        break;
                    case StickPosition.SE:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 2, 4);
                        break;
                    case StickPosition.S:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 1, 4);
                        break;
                    case StickPosition.SW:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 0, 4);
                        break;
                    case StickPosition.W:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 0, 5);
                        break;
                    case StickPosition.NW:
                        RobotManager.Instance.JoystickRobotPlacement(RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadratiGiocati, 0, 6);
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
            if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn)
            {
                if (Input.GetKeyUp(KeyCode.Joystick1Button0))
                {
                    switch (_currentStickPosition)
                    {
                        case StickPosition.N:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 1, 2);
                            break;
                        case StickPosition.NE:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 2, 2);
                            break;
                        case StickPosition.E:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 2, 1);
                            break;
                        case StickPosition.SE:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 2, 0);
                            break;
                        case StickPosition.S:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 1, 0);
                            break;
                        case StickPosition.SW:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 0, 0);
                            break;
                        case StickPosition.W:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 0, 1);
                            break;
                        case StickPosition.NW:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotCurviInHand, 0, 2);
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn)
            {
                if (Input.GetKeyUp(KeyCode.Joystick2Button0))
                {
                    switch (_currentStickPosition)
                    {
                        case StickPosition.N:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 1, 6);
                            break;
                        case StickPosition.NE:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 2, 6);
                            break;
                        case StickPosition.E:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 2, 5);
                            break;
                        case StickPosition.SE:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 2, 4);
                            break;
                        case StickPosition.S:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 1, 4);
                            break;
                        case StickPosition.SW:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 0, 4);
                            break;
                        case StickPosition.W:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 0, 5);
                            break;
                        case StickPosition.NW:
                            RobotManager.Instance.JoystickRobotUpgrade(RobotManager.Instance.RobotQuadratiInHand, 0, 6);
                            break;
                        default:
                            break;
                    }
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