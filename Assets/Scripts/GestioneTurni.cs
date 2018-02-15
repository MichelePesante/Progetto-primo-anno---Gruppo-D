using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GestioneTurni : MonoBehaviour
{
    public int TurnCount;

    public int P1Score;

    public int P2Score;

    //numero massimo di turni della fase di preparazione da mostrare sul contatore dei turni
    private int MaxPreparationTurns = 6;

    //numero massimo di turni della partita intera (6 di preparazione + 10 di combattimento strategico)
    private int MaxGameTurns = 16;

    //punteggio massimo raggiungibile per la fine del gioco
    public int MaxScore = 5;

    //enumeratore con le macrofasi del gioco
    public enum State
    {
        //Fase di start up della partita, vengono pescate le carte
        Startup,

        //Fase di preparazione delle prime 6 pedine a giocatore, questa fase dura sempre 6 turni
        Preparation,

        //Fase di combattimento strategico, inizia con la rotazione delle plance, passa al combattimento aperto e infine al potenziamento delle pedine
        Strategy,

        //Fase di fine partita in cui si verifica il punteggio dei player e si determina il vincitore
        End,
    }

    //enumeratore delle microfasi del gioco da inserire nelle varie macrofasi
    public enum Micro
    {
        //fase di pescaggio delle carte a inizio turno
        Draw,

        //fase di posizionamento delle carte a inizio turno
        Position,

        //fase di rotazione delle plance prima del collide
        Rotate,

        //fase di scontro tra le plance con il calcolo del punteggio
        Collide,

        //fase di rinforzamento della propria plancia
        Reinforce,

    }

    public State CurrentState
    {
        get
        {
            return _currentstate;
        }
        set
        {
            if (CheckStateChange(value) == true)
            {
                OnStateEnd(_currentstate);
                _currentstate = value;
                OnStateStart(_currentstate);
            }
           
        }
    }
    private State _currentstate;


    bool CheckStateChange(State newState)
    {
        switch (newState)
        {
            case State.Startup:
            case State.Preparation:
                if (CurrentState != State.Startup)
                    return false;
                return true;
                break;
            case State.Strategy:
                if (CurrentState != State.Preparation)
                    return false;
                return true;
                break;
            case State.End:
                if (CurrentState != State.Strategy)
                    return false;
                return true;
                break;
            default:
                return false;
                break;
        }


    }

    private void Start()
    {
        CurrentState = State.Startup;
        Debug.Log(UIManager.Instance);
    }

    private void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                CurrentState = State.Preparation;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentState = State.Strategy;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentState = State.End;
        }

        OnStateUpdate();

    }

    void OnStateStart(State newState)
    {
        switch (newState)   
        {
            case State.Startup:
                Debug.Log("Inizia il gioco!");
                break;
            case State.Preparation:
                Debug.Log("Prepariamo le carte!");
                break;
            case State.Strategy:
                Debug.Log("E' ora di usare la strategia!");
                break;
            case State.End:
                Debug.Log("Abbiamo un vincitore!");
                break;
            default:
                break;
        }

        UIManager.Instance.ShowFase(newState.ToString());
        

    }

    void OnStateUpdate()
    {
        switch (CurrentState)
        {
            case State.Startup:
                break;
            case State.Preparation:

                TurnUpdate();

                break;
            case State.Strategy:

                TurnUpdate();

                ScoreUpdate();
                
                if(P1Score == MaxScore || P2Score == MaxScore)
                {
                    CurrentState = State.End;
                }

                break;
            case State.End:
                break;
            default:
                break;
        }

        UIManager.Instance.ShowTurn(TurnCount.ToString());
        UIManager.Instance.ShowP1Score(P1Score.ToString());
        UIManager.Instance.ShowP2Score(P2Score.ToString());
    }

    void OnStateEnd(State oldState)
    {
        switch (oldState)   
        {
            case State.Startup:
                break;
            case State.Preparation:
                break;
            case State.Strategy:
                break;
            case State.End:
                break;
            default:
                break;
        }
    }

    //funzione per eseguire l'update del punteggio tramite pulsante
    void ScoreUpdate()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow) && P1Score < MaxScore)
        {            
            P1Score = P1Score + 1;
            TurnCount = TurnCount + 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && P2Score < MaxScore)
        {            
            P2Score = P2Score + 1;
            TurnCount = TurnCount + 1;
        }
    }

    //funzione per l'update dei turni tramite pulsante
    void TurnUpdate()
    {
        if (CurrentState == State.Preparation && TurnCount < MaxPreparationTurns)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TurnCount = TurnCount + 1;
            }

            
        }
        else
        {
            CurrentState = State.Strategy;

        }

        if (CurrentState == State.Strategy && TurnCount < MaxGameTurns)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TurnCount = TurnCount + 1;
            }
        }
        else
        {
            CurrentState = State.End;
        }

    }
}
