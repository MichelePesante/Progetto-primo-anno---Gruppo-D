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

    public void OnEndScene()
    {
        if (TurnManager.Instance.ScoreCurve >= TurnManager.Instance.ScoreToReach)
        {
            CurveEnd.gameObject.SetActive(true);
            ReturnToMainMenu();
        }

        if (TurnManager.Instance.ScoreQuad >= TurnManager.Instance.ScoreToReach)
        {
            QuadEnd.gameObject.SetActive(true);
            ReturnToMainMenu();
        }
        if (TurnManager.Instance.ScoreCurve == TurnManager.Instance.ScoreQuad && (RobotManager.Instance.RobotsCurviInHand == 0 && RobotManager.Instance.RobotsQuadratiInHand == 0))
        {
            TieEnd.gameObject.SetActive(true);
            ReturnToMainMenu();
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
