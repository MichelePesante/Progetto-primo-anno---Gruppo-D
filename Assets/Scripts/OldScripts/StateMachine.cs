using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

	public static PlayerTurn CurrentPlayerTurn;

	public static MacroPhase CurrentMacroPhase;

    private BattlePhase _currentPhase;

	public BattlePhase CurrentPhase {
		get {
			return _currentPhase;
		}
		set {
			if (CheckPhaseChange(value) == true) {
				OnPhaseEnd(_currentPhase);
				_currentPhase = value;
				OnPhaseStart(_currentPhase);
			}
			else {
				Debug.Log("Non è possibile passare da " + _currentPhase + " a " + value);
			}
		}
	}

    public enum MacroPhase {
		Start,
		Setup,
		Core,
		End
	}

	public enum PlayerTurn {
		TurnPlayer1,
		TurnPlayer2
	}


    //enumeratore per le fasi di battaglia/core

    public enum BattlePhase {
        Rotation,
        Battle,
        Reinforce,

    }
		
    bool CheckPhaseChange (BattlePhase newPhase) {
        switch (newPhase) {
            case BattlePhase.Rotation:
			if (CurrentPhase != BattlePhase.Reinforce)
				return false;
			return true;
            case BattlePhase.Battle:
                if (CurrentPhase != BattlePhase.Rotation)
                    return false;
                return true;
            case BattlePhase.Reinforce:
                if (CurrentPhase != BattlePhase.Battle)
                    return false;
                return true;
            default:
				return false;
        }
    }


    #region State Machine

    void OnPhaseStart(BattlePhase newPhase) {

        switch (newPhase) {
		case BattlePhase.Rotation:
			Debug.Log ("Sono entrato nello stato di " + newPhase);
			FindObjectOfType<RotationScript> ().EnableGridButtons ();
			FindObjectOfType<RotationScript> ().SwitchButtonsPosition ();
                break;
		case BattlePhase.Battle:
			Debug.Log ("Sono entrato nello stato di " + newPhase);
			FindObjectOfType<BattleScript> ().Battle ();
                break;
            case BattlePhase.Reinforce:
                Debug.Log("Sono entrato nello stato di " + newPhase);
                break;
            default:
                Debug.Log("Sono entrato in uno stato sconosciuto " + newPhase);
                break;
        }
    }

    void OnPhaseUpdate() {

        switch (CurrentPhase) {
		case BattlePhase.Rotation:
				Debug.Log ("Sono nello stato di " + CurrentPhase);
                break;
            case BattlePhase.Battle:
                Debug.Log("Sono nello stato di " + CurrentPhase);
                break;
            case BattlePhase.Reinforce:
                Debug.Log("Sono nello stato di " + CurrentPhase);
                break;
            default:
                Debug.Log("Sono in uno stato sconosciuto " + CurrentPhase);
                break;
        }


    }

    void OnPhaseEnd(BattlePhase oldPhase) {

        switch (oldPhase) {
            case BattlePhase.Rotation:
                Debug.Log("Sto uscendo dallo stato di " + oldPhase);
                break;
            case BattlePhase.Battle:
                Debug.Log("Sto uscendo dallo stato di " + oldPhase);
                break;
            case BattlePhase.Reinforce:
                Debug.Log("Sto uscendo dallo stato di " + oldPhase);
                break;
            default:
                Debug.Log("Sto uscendo da uno stato sconosciuto " + oldPhase);
                break;
        }

    }

    #endregion
}
