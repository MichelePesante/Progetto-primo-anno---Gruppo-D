using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public TextMeshProUGUI MacroFase;

    public TextMeshProUGUI MicroFase;

    public TextMeshProUGUI TurnCountText;

    public TextMeshProUGUI P1ScoreText;

    public TextMeshProUGUI P2ScoreText;

    public static UIManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            GameObject.Destroy(gameObject);
    }

    public void ShowFase(string _faseName)
    {
        MacroFase.text = _faseName;
    }

    public void ShowMicro(string _microName)
    {
        MicroFase.text = _microName;
    }

    public void ShowTurn(string _turnCount)
    {
        TurnCountText.text = "Turno: " + _turnCount;
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
