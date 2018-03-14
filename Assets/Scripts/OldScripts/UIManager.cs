using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public TextMeshProUGUI MicroFase;

    public TextMeshProUGUI TurnP1Text;

	public TextMeshProUGUI TurnP2Text;

    public TextMeshProUGUI P1ScoreText;

    public TextMeshProUGUI P2ScoreText;

    public TextMeshProUGUI P1Wins;

    public TextMeshProUGUI P2Wins;

    public static UIManager Instance;

    public int MaxScore = 5;

    public int MaxTurns = 16;

    public int P1Score;

    public int P2Score;

    private void Start()
    {
        MicroFase.text = "Posiziona le tue carte!";
    }

    void Update()
    {
        ShowMicro();
        ShowTurn();
        ShowP1Score();
        ShowP2Score();
        Reset();
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            GameObject.Destroy(gameObject);
    }

    public void ShowMicro()
    {
       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MicroFase.text = "Scegli come ruotare le plance!";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MicroFase.text = "La prima linea combatte!";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MicroFase.text = "Rinforza la tua formazione!";
        }
    }

    public void ShowTurn()
    {
		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1)
        {
			TurnP1Text.text = "Turno P1";
			TurnP2Text.text = "";
        }
		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2)
		{
			TurnP2Text.text = "Turno P2";
			TurnP1Text.text = "";
		}
    }


    public void ShowP1Score()
    {
		P1Score = GameController.Instance.scorep1;
        P1ScoreText.text = "" + P1Score;
    }



    public void ShowP2Score()
    {
		P2Score = GameController.Instance.scorep2;
        P2ScoreText.text = "" + P2Score;
    }

    private void Reset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            P1Score = 0;
            P2Score = 0;
            P2ScoreText.text = "" + P2Score;
            P1ScoreText.text = "" + P1Score;
        }
    }
}
