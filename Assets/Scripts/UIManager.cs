using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public TextMeshProUGUI MicroFase;

    public TextMeshProUGUI TurnCountText;

    public TextMeshProUGUI P1ScoreText;

    public TextMeshProUGUI P2ScoreText;

    public TextMeshProUGUI P1Wins;

    public TextMeshProUGUI P2Wins;

    public static UIManager Instance;

    public int TurnCount = 1;

    public int MaxScore = 5;

    public int MaxTurns = 16;

    public int P1Score;

    public int P2Score;

    private void Start()
    {
        TurnCountText.text = "Turno: " + TurnCount;
        MicroFase.text = "Posiziona le tue carte!";
    }

    private void Update()
    {
        ShowMicro();
        ShowTurn();
        ShowP1Score();
        ShowP2Score();
        Reset();
    }

    private void Awake()
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && TurnCount < MaxTurns)
        {
            TurnCount = TurnCount + 1;
            TurnCountText.text = "Turno: " + TurnCount;
        }
       
    }


    public void ShowP1Score()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && P1Score < MaxScore)
        {
            P1Score = P1Score + 1;
            P1ScoreText.text = "" + P1Score;
        }
    }



    public void ShowP2Score()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && P2Score < MaxScore)
        {
            P2Score = P2Score + 1;
            P2ScoreText.text = "" + P2Score;
        }
    }

    private void Reset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            P1Score = 0;
            P2Score = 0;
            TurnCount = 0;
            TurnCountText.text = "Turno: " + TurnCount;
            P2ScoreText.text = "" + P2Score;
            P1ScoreText.text = "" + P1Score;
        }
    }
}
