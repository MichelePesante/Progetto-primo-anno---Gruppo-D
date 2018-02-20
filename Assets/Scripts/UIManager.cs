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

    private void Start()
    {
        TurnCountText.text = "Turno: " + TurnCount;
    }

    private void Update()
    {
        ShowMicro();
        ShowTurn();
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
            MicroFase.text = "Posiziona le tue carte!";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MicroFase.text = "Scegli come ruotare le plance!";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MicroFase.text = "La prima linea combatte!";
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MicroFase.text = "Rinforza la tua formazione!";
        }
    }

    public void ShowTurn()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TurnCount = TurnCount + 1;
            TurnCountText.text = "Turno: " + TurnCount;
        }
       
    }


    public void ShowP1Score(string _p1Score)
    {
        P1ScoreText.text = _p1Score;
    }



    public void ShowP2Score(string _p2Score)
    {
        P2ScoreText.text = _p2Score;
    }
}
