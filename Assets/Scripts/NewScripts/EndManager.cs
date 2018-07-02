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

    public void Update()
    {
        if ((CurveEnd.gameObject.activeInHierarchy || QuadEnd.gameObject.activeInHierarchy || TieEnd.gameObject.activeInHierarchy) && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade)
        {
            ReturnToMainMenu();
        }
    }

    public void OnEndScene()
    {
        if (TurnManager.Instance.ScoreCurve >= TurnManager.Instance.ScoreToReach)
        {
            CurveEnd.gameObject.SetActive(true);
            AudioManager.Instance.Background.Stop();
            AudioManager.Instance.SFX_1.Stop();
            AudioManager.Instance.SFX_2.Stop();
        }

        if (TurnManager.Instance.ScoreQuad >= TurnManager.Instance.ScoreToReach)
        {
            QuadEnd.gameObject.SetActive(true);
            AudioManager.Instance.Background.Stop();
            AudioManager.Instance.SFX_1.Stop();
            AudioManager.Instance.SFX_2.Stop();
        }
        if (TurnManager.Instance.ScoreCurve == TurnManager.Instance.ScoreQuad && (RobotManager.Instance.RobotsCurviInHand == 2 && RobotManager.Instance.RobotsQuadratiInHand == 2))
        {
            TieEnd.gameObject.SetActive(true);
            AudioManager.Instance.Background.Stop();
            AudioManager.Instance.SFX_1.Stop();
            AudioManager.Instance.SFX_2.Stop();
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
