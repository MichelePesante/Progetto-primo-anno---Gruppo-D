﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public int RotationTurn = 0;
	public int BattleTurn = 0;
	public int ScoreToReach = 5;
	public int ScoreP1;
	public int ScoreP2;

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
	public enum PlayerTurn { P1_Turn, P2_Turn };
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

    private void Start()
    {

        //at the start of the game, the various states will be:
        CurrentMacroPhase = MacroPhase.Preparation;
        CurrentTurnState = TurnState.choosePlayer;
		CurrentPlayerTurn = PlayerTurn.P1_Turn;
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
				FindObjectOfType<RobotManager> ().Shuffle (FindObjectOfType<RobotManager> ().RobotCurvi);
				FindObjectOfType<RobotManager> ().Shuffle (FindObjectOfType<RobotManager> ().RobotQuadrati);
				CurrentTurnState = TurnState.choosePlayer;	
				CurrentPlayerTurn = PlayerTurn.P1_Turn;
	            break;
			case MacroPhase.Game:
				CurrentPlayerTurn = PlayerTurn.P1_Turn;
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
				if (CurrentTurnState != TurnState.placing && CurrentTurnState != TurnState.battle)
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
				CurrentPlayerTurn = PlayerTurn.P1_Turn;
				FindObjectOfType<RobotManager> ().SetPositions (FindObjectOfType<RobotManager> ().PosizioniRobotCurvi);
				FindObjectOfType<RobotManager> ().SetPositions (FindObjectOfType<RobotManager> ().PosizioniRobotQuadrati);
				FindObjectOfType<CameraController>().transform.localPosition = new Vector3 (0f, 9.13f, -9.58f);
                break;
			case TurnState.rotation:
				NewUIManager.Instance.Display_P1.SetActive (true);
				NewUIManager.Instance.Display_P2.SetActive (true);
				FindObjectOfType<RobotManager> ().SetGraphicAsParent ();
				FindObjectOfType<NewGridController> ().MyLeftRotationButton.SetActive (true);
				FindObjectOfType<NewGridController> ().MyRightRotationButton.SetActive (true);
				FindObjectOfType<NewGridController> ().EnemyLeftRotationButton.SetActive (true);
				FindObjectOfType<NewGridController> ().EnemyRightRotationButton.SetActive (true);
                break;
			case TurnState.battle:
				FindObjectOfType<NewGridController> ().MyLeftRotationButton.SetActive (false);
				FindObjectOfType<NewGridController> ().MyRightRotationButton.SetActive (false);
				FindObjectOfType<NewGridController> ().EnemyLeftRotationButton.SetActive (false);
				FindObjectOfType<NewGridController> ().EnemyRightRotationButton.SetActive (false);
				FindObjectOfType<NewGridController> ().EndRotationButton.SetActive (false);
				if (_currentPlayerTurn == PlayerTurn.P1_Turn) {
					FindObjectOfType<CameraController> ().GetComponentInParent<Animator> ().Play ("BattleCameraFirstPlayer");
					FindObjectOfType<RobotManager> ().Battle ();
					CurrentTurnState = TurnState.rotation;
					ChangeTurn ();
				} 
				else {
					FindObjectOfType<CameraController> ().GetComponentInParent<Animator> ().Play ("BattleCameraSecondPlayer");
					FindObjectOfType<RobotManager> ().Battle ();
					CurrentTurnState = TurnState.rotation;
					//CurrentTurnState = TurnState.upgrade;
					ChangeTurn ();
				}
                break;
            case TurnState.upgrade:
                break;
            case TurnState.useEnergy:
                break;
            default:
                break;
        }
    }

	void OnTurnStart (PlayerTurn newTurn)
    {
		if (newTurn == PlayerTurn.P1_Turn) {
			switch (CurrentMacroPhase) {
			case MacroPhase.Preparation:
				switch (CurrentTurnState) {
				case TurnState.choosePlayer:
					break;
				case TurnState.placing:
					FindObjectOfType<RobotManager> ().RobotsCurviInHand = FindObjectOfType<RobotManager> ().Draw (FindObjectOfType<RobotManager> ().RobotCurviInHand, FindObjectOfType<RobotManager> ().RobotCurvi, FindObjectOfType<RobotManager> ().RobotsCurviInHand);
					FindObjectOfType<CameraController>().GetComponentInParent<Animator> ().Play ("PreparationCameraReturn");
					break;
				}
				break;
			case MacroPhase.Game:
				CurrentTurnState = TurnState.rotation;
				break;
			default:
				Debug.Log ("Errore: Nessuna macro fase");
				break;
			}
        }
		if (newTurn == PlayerTurn.P2_Turn) {
			switch (CurrentMacroPhase) {
			case MacroPhase.Preparation:
				switch (CurrentTurnState) {
				case TurnState.choosePlayer:
					break;
				case TurnState.placing:
					FindObjectOfType<RobotManager> ().RobotsQuadratiInHand = FindObjectOfType<RobotManager> ().Draw (FindObjectOfType<RobotManager> ().RobotQuadratiInHand, FindObjectOfType<RobotManager> ().RobotQuadrati, FindObjectOfType<RobotManager> ().RobotsQuadratiInHand);
					FindObjectOfType<CameraController>().GetComponentInParent<Animator> ().Play ("PreparationCameraStart");
					break;
				}
				break;
			case MacroPhase.Game:
				CurrentTurnState = TurnState.rotation;
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
        if (CurrentPlayerTurn == PlayerTurn.P1_Turn)
        {
            CurrentPlayerTurn = PlayerTurn.P2_Turn;
        }
        else if (CurrentPlayerTurn == PlayerTurn.P2_Turn)
        {
            CurrentPlayerTurn = PlayerTurn.P1_Turn;
        }
    }
}
