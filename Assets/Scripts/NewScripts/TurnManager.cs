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
	public Vector3 CameraPosition;

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
			Destroy (this.gameObject);
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
				RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand);
                break;
			case TurnState.rotation:
				NewUIManager.Instance.Slots.SetActive (false);
				NewUIManager.Instance.Display_P1.SetActive (true);
				NewUIManager.Instance.Display_P2.SetActive (true);
				RobotManager.Instance.SetGraphicAsParent ();
				ButtonManager.Instance.CurveGridClockwiseButton.gameObject.SetActive (true);
				ButtonManager.Instance.CurveGridCounterclockwiseButton.gameObject.SetActive (true);
				ButtonManager.Instance.QuadGridClockwiseButton.gameObject.SetActive (true);
				ButtonManager.Instance.QuadGridCounterclockwiseButton.gameObject.SetActive (true);
                break;
			case TurnState.battle:
				ButtonManager.Instance.CurveGridClockwiseButton.gameObject.SetActive (false);
				ButtonManager.Instance.CurveGridCounterclockwiseButton.gameObject.SetActive (false);
				ButtonManager.Instance.QuadGridClockwiseButton.gameObject.SetActive (false);
				ButtonManager.Instance.QuadGridCounterclockwiseButton.gameObject.SetActive (false);
				ButtonManager.Instance.Skip_Turn.gameObject.SetActive (false);
				if (_currentPlayerTurn == PlayerTurn.Curve_Turn) {
					FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("BattleCameraFirstPlayer");
					ChangeTurn ();
				} 
				else {
					FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("BattleCameraSecondPlayer");
					ChangeTurn ();
				}
                break;
			case TurnState.upgrade:
				NewUIManager.Instance.Slots.SetActive (true);
				NewUIManager.Instance.Display_P1.SetActive (false);
				NewUIManager.Instance.Display_P2.SetActive (false);
				if (_currentPlayerTurn == PlayerTurn.Curve_Turn) {
					RobotManager.Instance.RobotsCurviInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurvi, RobotManager.Instance.RobotsCurviInHand);
				} 
				else {
				RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand);
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
					//RobotManager.Instance.AddRemovedCards (RobotManager.Instance.CarteRobotCurviInHand);
					RobotManager.Instance.RobotsCurviInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotCurviInHand, RobotManager.Instance.RobotCurvi, RobotManager.Instance.RobotsCurviInHand);
					FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("PreparationCameraReturn");
					break;
				}
				break;
			case MacroPhase.Game:
				break;
			default:
				Debug.Log ("Errore: Nessuna macro fase");
				break;
			}
        }
		if (newTurn == PlayerTurn.Quad_Turn) {
			switch (CurrentMacroPhase) {
			case MacroPhase.Preparation:
				switch (CurrentTurnState) {
				case TurnState.choosePlayer:
					break;
				case TurnState.placing:
					RobotManager.Instance.RobotsQuadratiInHand = RobotManager.Instance.Draw (RobotManager.Instance.RobotQuadratiInHand, RobotManager.Instance.RobotQuadrati, RobotManager.Instance.RobotsQuadratiInHand);
					FindObjectOfType<Camera> ().GetComponentInParent<Animator> ().Play ("PreparationCameraStart");
					break;
				}
				break;
			case MacroPhase.Game:
				break;
			default:
				Debug.Log ("Errore: Nessuna macro fase");
				break;
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
