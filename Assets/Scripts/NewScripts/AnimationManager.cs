using UnityEngine;
using UnityEngine.UI;

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
		JoystickManager.Instance.HasMyGridAlreadyBeenRotated = false;
		JoystickManager.Instance.HasEnemyGridAlreadyBeenRotated = false;
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

    public void ShowEnergy() {
        NewUIManager.Instance.Energies.GetComponentsInChildren<Image>()[0].color = new Color(1f, 1f, 1f, 1f);
        NewUIManager.Instance.Energies.GetComponentsInChildren<Image>()[1].color = new Color(1f, 1f, 1f, 1f);
    }

    public void HideEnergy()
    {
        NewUIManager.Instance.Energies.GetComponentsInChildren<Image>()[0].color = new Color(1f, 1f, 1f, 0f);
        NewUIManager.Instance.Energies.GetComponentsInChildren<Image>()[1].color = new Color(1f, 1f, 1f, 0f);
    }

    public void Swap_Start_Energy()
    {
        NewUIManager.Instance.Energies.GetComponent<Animator>().Play("Swap_Start");
    }

    public void Swap_Return_Energy()
    {
        NewUIManager.Instance.Energies.GetComponent<Animator>().Play("Swap_Return");
    }
}
