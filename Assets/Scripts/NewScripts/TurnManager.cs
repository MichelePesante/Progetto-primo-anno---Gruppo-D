using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    
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


    private void Start()
    {

        //at the start of the game, the various states will be:
        CurrentPlayerTurn = PlayerTurn.P1_Turn;
        CurrentMacroPhase = MacroPhase.Preparation;
        CurrentTurnState = TurnState.choosePlayer;
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
                if (CurrentTurnState == TurnState.battle)
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
                break;
            case TurnState.placing:
                CurrentPlayerTurn = PlayerTurn.P1_Turn;

                break;
            case TurnState.rotation:
                break;
            case TurnState.battle:
                break;
            case TurnState.upgrade:
                break;
            case TurnState.useEnergy:
                //if player has done everything, ChangeTurn();
                break;
            default:
                break;
        }
    }

    void OnTurnStart (PlayerTurn newTurn)
    {
        switch(CurrentMacroPhase)
        {
            case MacroPhase.Preparation:
                switch (CurrentTurnState)
                {
                    case TurnState.choosePlayer:
                        //if player has been chosen, go to placing
                        break;
                    case TurnState.placing:
                        //if 3+3 turns have passed (or some other thing to check for), go to MacroPhase game
                        break;
                }
                break;
            case MacroPhase.Game:
                CurrentTurnState = TurnState.rotation;
                //check for win/lose conditions here
                break;
            default:
                Debug.Log("Errore: Nessuna macro fase");
                break;
        }
    }






    /// <summary>
    /// funzione per cambiare turno
    /// </summary>
    public void ChangeTurn()
    {
        if (CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn)
        {
            CurrentPlayerTurn = TurnManager.PlayerTurn.P2_Turn;
        }
        else if (CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn)
        {
            CurrentPlayerTurn = TurnManager.PlayerTurn.P1_Turn;
        }
    }
}
