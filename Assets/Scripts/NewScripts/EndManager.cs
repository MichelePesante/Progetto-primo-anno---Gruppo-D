using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{

    public static EndManager Instance;

    public Image CurveEnd;
    public Image QuadEnd;
    public Image TieEnd;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (TurnManager.Instance.ScoreCurve >= 5 || TurnManager.Instance.ScoreQuad >= 5)
            ReturnToMainMenu();
    }

    public void OnEndScene()
    {
        if (TurnManager.Instance.ScoreCurve >= 5)
        {
            CurveEnd.gameObject.SetActive(true);
        }

        if (TurnManager.Instance.ScoreQuad >= 5)
        {
            QuadEnd.gameObject.SetActive(true);
        }
        if (TurnManager.Instance.ScoreCurve == TurnManager.Instance.ScoreQuad)
        {
            TieEnd.gameObject.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
    }
}
