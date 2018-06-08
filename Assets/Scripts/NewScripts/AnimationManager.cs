using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	public void GoToRotationTurn () {
		TurnManager.Instance.CurrentTurnState = TurnManager.TurnState.rotation;
	}

	public void GoToUpgradeTurn () {
		TurnManager.Instance.CurrentTurnState = TurnManager.TurnState.upgrade;
	}

	public void SeeBattleResults () {
		RobotManager.Instance.BattleResults ();
	}

	public void DoFirstBattle () {
		RobotManager.Instance.FirstBattle ();
	}

	public void DoSecondBattle () {
		RobotManager.Instance.SecondBattle ();
	}

	public void DoThirdBattle () {
		RobotManager.Instance.ThirdBattle ();
	}

	public void ResetRotationFlags () {
		JoystickManager.Instance.hasMyGridAlreadyBeenRotated = false;
		JoystickManager.Instance.hasEnemyGridAlreadyBeenRotated = false;
	}

    public void ShowEndImage() {
        EndManager.Instance.OnEndScene();
    }

    public void ResetAnimationBool() {
        GameManager.isSomeAnimationGoing = false;
    }

    public void ResetTutorialBool() {
        GameManager.isTutorialOn = false;
    }

    public void ShowCurveArrows() {
        ArrowManager.Instance.Frecce_Curve.SetActive(true);
    }

    public void ShowQuadArrows()
    {
        ArrowManager.Instance.Frecce_Quad.SetActive(true);
    }
}
