using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public static TurnManager Instance;

	public int RotationTurn = 0;
	public int BattleTurn = 0;
	public int ScoreToReach = 5;
	public int ScoreCurve;
	public int ScoreQuad;
	public bool TextIsActive;
	public Vector3 CameraPosition;

    private bool isFirstUpgradeTurn = true;
    private bool isCurveRotationTurn = true;

	/// <summary> ENUM per indicare la macro fase di gioco corrente </summary>
	public enum MacroPhase { Preparation, Game };
	private MacroPhase _currentMacroPhase;
	public MacroPhase CurrentMacroPhase
	{
		get
		{
			return _currentMacroPhase;
		}
		set
		{
			if (MacroPhaseChange(value))
			{
				_currentMacroPhase = value;
				OnMacroPhaseStart(_currentMacroPhase);
			}
		}
	}

	/// <summary> ENUM per indicare lo stato corrente a seconda della macro fase </summary>
	public enum TurnState { choosePlayer, placing, rotation, battle, upgrade, useEnergy };
	private TurnState _currentTurnState;
	public TurnState CurrentTurnState
	{
		get
		{
			return _currentTurnState;
		}
		set
		{
			if (OnStateChange(value))
			{
				_currentTurnState = value;
				OnStateStart(_currentTurnState);
			}
		}
	}

	/// <summary> ENUM per indicare di chi è il turno </summary>
	public enum PlayerTurn { Curve_Turn, Quad_Turn };
	private PlayerTurn _currentPlayerTurn;
	public PlayerTurn CurrentPlayerTurn
	{
		get
		{
			return _currentPlayerTurn;
		}
		set
		{
			_currentPlayerTurn = value;
			OnTurnStart(_currentPlayerTurn);
		}
	}

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (gameObject);
		}
	}

    private void Start()
    {

        //at the start of the game, the various states will be:
        CurrentMacroPhase = MacroPhase.Preparation;
        CurrentTurnState = TurnState.choosePlayer;
		CurrentPlayerTurn = PlayerTurn.Curve_Turn;
    }

    /// <summary> Funzione che ritorna bool della macro fase corrente </summary>
    bool MacroPhaseChange(MacroPhase newMacroPhaseChange)
    {
        switch (newMacroPhaseChange)
        {
            case MacroPhase.Preparation:
                return true;
            case MacroPhase.Game:
                if (CurrentMacroPhase != MacroPhase.Preparation)
                    return false;
                return true;
            default:
                return false;
        }
    }

    void OnMacroPhaseStart(MacroPhase newMacroPhase)
    {
        switch (newMacroPhase)
        {
			case MacroPhase.Preparation:
				FindObjectOfType<NewGridController> ().CreateGrid (FindObjectOfType<NewGridController> ().X, FindObjectOfType<NewGridController> ().Y, FindObjectOfType<NewGridController> ().Offset);
				RobotManager.Instance.Shuffle (RobotManager.Instance.RobotCurvi);
				RobotManager.Instance.Shuffle (RobotManager.Instance.RobotQuadrati);
				CurrentTurnState = TurnState.choosePlayer;	
				CurrentPlayerTurn = PlayerTurn.Curve_Turn;
	            break;
			case MacroPhase.Game:
				CurrentPlayerTurn = PlayerTurn.Curve_Turn;
				CurrentTurnState = TurnState.rotation;
                break;
            default:
                break;
        }
    }

    bool OnStateChange(TurnState newState)
    {
        switch (newState)
        {
            case TurnState.choosePlayer:
                if (CurrentTurnState != TurnState.choosePlayer)
                {
                    return false;
                }
                return true;
            case TurnState.placing:
                if (CurrentTurnState != TurnState.choosePlayer)
                {
                    return false;
                }
                return true;
            case TurnState.rotation:
				if (CurrentTurnState != TurnState.placing && CurrentTurnState != TurnState.battle && CurrentTurnState != TurnState.upgrade)
                {
                    return false;
                }
                return true;
            case TurnState.battle:
                if (CurrentTurnState != TurnState.rotation)
                {
                    return false;
                }
                return true;
            case TurnState.upgrade:
                if (CurrentTurnState != TurnState.battle)
                {
                    return false;
                }
                return true;
            case TurnState.useEnergy:
                if (CurrentTurnState != TurnState.upgrade)
                {
                    return false;
                }
                return true;
            default:
                return false;
        }
    }

    void OnStateStart(TurnState newState)
    {
        switch (newState)
        {
			case TurnState.choosePlayer:
				CurrentTurnState = TurnState.placing;
                break;
			case TurnState.placing:
				CurrentPlayerTurn = PlayerTurn.Curve_Turn;
				FindObjectOfType<Camera> ().transform.localPosition = CameraPosition;
				NewUIManager.Instance.ChangeText ("Fase di Preparazione: Posiziona due Robot!");
				NewUIManager.Instance.TutorialBoxSummon ();
			    RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand, Player.Player_Quad);    
				break;
			case TurnState.rotation:
				NewUIManager.Instance.Slots.SetActive (false);
                NewUIManager.Instance.DoubleRotation.SetActive (true);
                NewUIManager.Instance.DoubleUpgrade.SetActive(false);
                NewUIManager.Instance.Rotation_Buttons.SetActive (true);
                NewUIManager.Instance.ChangeText("Fase di Rotazione: Ruotate le vostre plance!");
                NewUIManager.Instance.TutorialBoxSummon();
                RobotManager.Instance.SetGraphicAsParent ();
                ArrowManager.Instance.Frecce.SetActive(false);
                NewUIManager.Instance.GridButtons.SetActive(false);
                ButtonManager.Instance.CurveGridClockwiseButton.gameObject.SetActive (true);
				ButtonManager.Instance.CurveGridCounterclockwiseButton.gameObject.SetActive (true);
				ButtonManager.Instance.QuadGridClockwiseButton.gameObject.SetActive (true);
				ButtonManager.Instance.QuadGridCounterclockwiseButton.gameObject.SetActive (true);
                if (isCurveRotationTurn)
                {
                    NewUIManager.Instance.RB_Button_Curve_Turn.gameObject.SetActive(true);
                    NewUIManager.Instance.LT_Button_Curve_Turn.gameObject.SetActive(true);
                    NewUIManager.Instance.RT_Button_Curve_Turn.gameObject.SetActive(true);
                    NewUIManager.Instance.LB_Button_Curve_Turn.gameObject.SetActive(true);
                    NewUIManager.Instance.RB_Button_Quad_Turn.gameObject.SetActive(false);
                    NewUIManager.Instance.LT_Button_Quad_Turn.gameObject.SetActive(false);
                    NewUIManager.Instance.RT_Button_Quad_Turn.gameObject.SetActive(false);
                    NewUIManager.Instance.LB_Button_Quad_Turn.gameObject.SetActive(false);
                    isCurveRotationTurn = false;
                }
                else if (!isCurveRotationTurn)
                {
                    NewUIManager.Instance.RB_Button_Quad_Turn.gameObject.SetActive(true);
                    NewUIManager.Instance.LT_Button_Quad_Turn.gameObject.SetActive(true);
                    NewUIManager.Instance.RT_Button_Quad_Turn.gameObject.SetActive(true);
                    NewUIManager.Instance.LB_Button_Quad_Turn.gameObject.SetActive(true);
                    NewUIManager.Instance.RB_Button_Curve_Turn.gameObject.SetActive(false);
                    NewUIManager.Instance.LT_Button_Curve_Turn.gameObject.SetActive(false);
                    NewUIManager.Instance.RT_Button_Curve_Turn.gameObject.SetActive(false);
                    NewUIManager.Instance.LB_Button_Curve_Turn.gameObject.SetActive(false);
                    isCurveRotationTurn = true;
                }
                break;
			case TurnState.battle:
				NewUIManager.Instance.Rotation_Buttons.SetActive (false);
                NewUIManager.Instance.DoubleRotation.SetActive (false);
                ButtonManager.Instance.Skip_Turn.gameObject.SetActive (false);
                JoystickManager.Instance.DoubleRotationAlreadyActivated = false;
                if (_currentPlayerTurn == PlayerTurn.Curve_Turn) {
					FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("BattleCameraFirstPlayer");
                    NewUIManager.Instance.Segnapunti.GetComponent<Animator>().Play ("Punteggio_Curve_Start_Curve_Turn");
                    NewUIManager.Instance.Segnapunti.GetComponent<Animator>().Play("Punteggio_Quad_Start_Curve_Turn");
                    GameManager.isSomeAnimationGoing = true;
                    ChangeTurn ();
				} 
				else {
					FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("BattleCameraSecondPlayer");
                    NewUIManager.Instance.Segnapunti.GetComponent<Animator>().Play("Punteggio_Curve_Start_Quad_Turn");
                    NewUIManager.Instance.Segnapunti.GetComponent<Animator>().Play("Punteggio_Quad_Start_Quad_Turn");
                    GameManager.isSomeAnimationGoing = true;
                    ChangeTurn ();
				}
                break;
			case TurnState.upgrade:
                if (isFirstUpgradeTurn) {
                    ArrowManager.Instance.ActiveAllArrows();
                    isFirstUpgradeTurn = false;
                }
                ArrowManager.Instance.Frecce.SetActive(true);
                NewUIManager.Instance.GridButtons.SetActive(true);
                NewUIManager.Instance.DoubleUpgrade.SetActive(true);
                NewUIManager.Instance.Slots.SetActive (true);
				NewUIManager.Instance.Rotation_Buttons.SetActive (false);
                NewUIManager.Instance.ChangeText("Fase di Upgrade: Potenziate i vostri Robot!");
                NewUIManager.Instance.TutorialBoxSummon();
                if (_currentPlayerTurn == PlayerTurn.Curve_Turn)
                {
                JoystickManager.Instance.DoubleUpgradeAlreadyActivated = false;
                RobotManager.Instance.RobotsCurviInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurvi, RobotManager.Instance.RobotsCurviInHand, Player.Player_Curve);
                } 
				else
                {
                JoystickManager.Instance.DoubleUpgradeAlreadyActivated = false;
                RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand, Player.Player_Quad);
                }
                break;
            case TurnState.useEnergy:
                break;
            default:
                break;
        }
    }

	void OnTurnStart (PlayerTurn newTurn)
    {
		if (newTurn == PlayerTurn.Curve_Turn) {
			switch (CurrentMacroPhase) {
			case MacroPhase.Preparation:
				switch (CurrentTurnState) {
				case TurnState.choosePlayer:
					break;
				case TurnState.placing:
					RobotManager.Instance.RobotsCurviInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurvi, RobotManager.Instance.RobotsCurviInHand, Player.Player_Curve);
                    ArrowManager.Instance.Frecce_Curve.SetActive(false);
                    ArrowManager.Instance.Frecce_Quad.SetActive(false);
                    NewUIManager.Instance.A_Button_Curve.gameObject.SetActive(false);
                    NewUIManager.Instance.A_Button_Quad.gameObject.SetActive(false);
                    ArrowManager.Instance.Frecce_Quad.transform.position = ArrowManager.Instance.Frecce_Quad_Starting_Position;
                    ArrowManager.Instance.Frecce_Quad.transform.localScale = ArrowManager.Instance.Frecce_Quad_Starting_Scale;
                    FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("PreparationCameraReturn");
                    GameManager.isSomeAnimationGoing = true;
					break;
				}
				break;
			default:
				break;
			}
            if (CurrentTurnState == TurnState.upgrade)
            {
                JoystickManager.Instance.DoubleUpgradeAlreadyActivated = false;
                ArrowManager.Instance.Frecce_Curve.SetActive(false);
                ArrowManager.Instance.Frecce_Quad.SetActive(false);
                NewUIManager.Instance.DoubleUpgrade.SetActive(true);
            }
        }
		if (newTurn == PlayerTurn.Quad_Turn) {
			switch (CurrentMacroPhase) {
			case MacroPhase.Preparation:
				switch (CurrentTurnState) {
				case TurnState.choosePlayer:
					break;
				case TurnState.placing:
					RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand, Player.Player_Quad);
                    ArrowManager.Instance.Frecce_Quad.SetActive(false);
                    ArrowManager.Instance.Frecce_Curve.SetActive(false);
                    NewUIManager.Instance.A_Button_Curve.gameObject.SetActive(false);
                    NewUIManager.Instance.A_Button_Quad.gameObject.SetActive(false);
                    ArrowManager.Instance.Frecce_Curve.transform.position = ArrowManager.Instance.Frecce_Curve_Starting_Position;
                    ArrowManager.Instance.Frecce_Curve.transform.localScale = ArrowManager.Instance.Frecce_Curve_Starting_Scale;
                    FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("PreparationCameraStart");
                    GameManager.isSomeAnimationGoing = true;
                    break;
				}
				break;
			default:
				break;
			}
            if (CurrentTurnState == TurnState.upgrade)
            {
                JoystickManager.Instance.DoubleUpgradeAlreadyActivated = false;
                ArrowManager.Instance.Frecce_Curve.SetActive(false);
                ArrowManager.Instance.Frecce_Quad.SetActive(false);
                NewUIManager.Instance.DoubleUpgrade.SetActive(true);
            }
        }
    }






    /// <summary>
    /// funzione per cambiare turno
    /// </summary>
    public void ChangeTurn()
    {
        if (CurrentPlayerTurn == PlayerTurn.Curve_Turn)
        {
            CurrentPlayerTurn = PlayerTurn.Quad_Turn;
        }
        else if (CurrentPlayerTurn == PlayerTurn.Quad_Turn)
        {
            CurrentPlayerTurn = PlayerTurn.Curve_Turn;
        }
    }
}