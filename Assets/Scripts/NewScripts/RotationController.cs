using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

	public enum GridStep
	{
		First,
		Second,
		Third,
		Fourth
	}

	public GridStep CurrentCurveGridStep;
	public GridStep CurrentQuadGridStep;

	void OnMouseDown () {
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == PlayerTurn.Curve_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation && GameMenu.GameIsPaused == false) {
			if (this.gameObject.name == "CurveClockwiseRotationButton") {
				RotateGrid ("CurveGrid", this.gameObject.name);
				FindObjectOfType<RobotManager> ().OnClockwiseRotationCurveGrid ();
				DeactivateButton (FindObjectOfType<NewGridController>().CurveClockwiseRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().CurveCounterclockwiseRotationButton);
			}
			if (this.gameObject.name == "CurveCounterclockwiseRotationButton") {
				RotateGrid ("CurveGrid", this.gameObject.name);
				FindObjectOfType<RobotManager> ().OnCounterclockwiseRotationCurveGrid ();
				DeactivateButton (FindObjectOfType<NewGridController>().CurveCounterclockwiseRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().CurveClockwiseRotationButton);
			}
			if (this.gameObject.name == "QuadClockwiseRotationButton") {
				RotateGrid ("QuadGrid", this.gameObject.name);
				FindObjectOfType<RobotManager> ().OnClockwiseRotationQuadGrid ();
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().QuadClockwiseRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().QuadCounterclockwiseRotationButton);
			}
			if (this.gameObject.name == "QuadCounterclockwiseRotationButton") {
				RotateGrid ("QuadGrid", this.gameObject.name);
				FindObjectOfType<RobotManager> ().OnCounterclockwiseRotationQuadGrid ();
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().QuadCounterclockwiseRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().QuadClockwiseRotationButton);
			}
		}

		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == PlayerTurn.Quad_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation && GameMenu.GameIsPaused == false) {
			if (this.gameObject.name == "QuadClockwiseRotationButton") {
				RotateGrid ("QuadGrid", this.gameObject.name);
				FindObjectOfType<RobotManager> ().OnClockwiseRotationQuadGrid ();
				DeactivateButton (FindObjectOfType<NewGridController>().QuadClockwiseRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().QuadCounterclockwiseRotationButton);
			}
			if (this.gameObject.name == "QuadCounterclockwiseRotationButton") {
				RotateGrid ("QuadGrid", this.gameObject.name);
				FindObjectOfType<RobotManager> ().OnCounterclockwiseRotationQuadGrid ();
				DeactivateButton (FindObjectOfType<NewGridController>().QuadCounterclockwiseRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().QuadClockwiseRotationButton);
			}
			if (this.gameObject.name == "CurveClockwiseRotationButton") {
				RotateGrid ("CurveGrid", this.gameObject.name);
				FindObjectOfType<RobotManager> ().OnClockwiseRotationCurveGrid ();
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().CurveClockwiseRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().CurveCounterclockwiseRotationButton);
			}
			if (this.gameObject.name == "CurveCounterclockwiseRotationButton") {
				RotateGrid ("CurveGrid", this.gameObject.name);
				FindObjectOfType<RobotManager> ().OnCounterclockwiseRotationCurveGrid ();
				ActiveEndRotationButton ();
				DeactivateButton (FindObjectOfType<NewGridController>().CurveCounterclockwiseRotationButton);
				DeactivateButton (FindObjectOfType<NewGridController>().CurveClockwiseRotationButton);
			}
		}

		if (FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.rotation && this.gameObject.name == "EndRotationButton" && GameMenu.GameIsPaused == false) {
			FindObjectOfType<TurnManager> ().CurrentTurnState = TurnManager.TurnState.battle;
		}

		if (FindObjectOfType<NewGridController> ().CurveClockwiseRotationButton.activeInHierarchy == false && FindObjectOfType<NewGridController> ().CurveCounterclockwiseRotationButton.activeInHierarchy == false && FindObjectOfType<NewGridController> ().QuadClockwiseRotationButton.activeInHierarchy == false && FindObjectOfType<NewGridController> ().QuadCounterclockwiseRotationButton.activeInHierarchy == false && GameMenu.GameIsPaused == false) {
			FindObjectOfType<TurnManager> ().CurrentTurnState = TurnManager.TurnState.battle;
		}
	}

	private void RotateGrid (string _gridToRotateName, string _buttonToPressName) {
		GameObject gridToRotate = GameObject.Find (_gridToRotateName);
		if (_gridToRotateName == "CurveGrid" && _buttonToPressName == "CurveClockwiseRotationButton") {
			switch (CurrentCurveGridStep) {
				case GridStep.First:
					gridToRotate.GetComponent<Animator> ().Play ("CurveGridFirstClockwiseRotation");
					CurrentCurveGridStep = GridStep.Second;
					break;
				case GridStep.Second:
					gridToRotate.GetComponent<Animator> ().Play ("CurveGridSecondClockwiseRotation");
					CurrentCurveGridStep = GridStep.Third;
					break;
				case GridStep.Third:
					gridToRotate.GetComponent<Animator> ().Play ("CurveGridThirdClockwiseRotation");
					CurrentCurveGridStep = GridStep.Fourth;
					break;
				case GridStep.Fourth:
					gridToRotate.GetComponent<Animator> ().Play ("CurveGridFourthClockwiseRotation");
					CurrentCurveGridStep = GridStep.First;
					break;
				default:
					break;
			}
		}
		if (_gridToRotateName == "CurveGrid" && _buttonToPressName == "CurveCounterclockwiseRotationButton") {
			switch (CurrentCurveGridStep) {
				case GridStep.First:
					gridToRotate.GetComponent<Animator> ().Play ("CurveGridFirstCounterclockwiseRotation");
					CurrentCurveGridStep = GridStep.Fourth;
					break;
				case GridStep.Second:
					gridToRotate.GetComponent<Animator> ().Play ("CurveGridSecondCounterclockwiseRotation");
					CurrentCurveGridStep = GridStep.First;
					break;
				case GridStep.Third:
					gridToRotate.GetComponent<Animator> ().Play ("CurveGridThirdCounterclockwiseRotation");
					CurrentCurveGridStep = GridStep.Second;
					break;
				case GridStep.Fourth:
					gridToRotate.GetComponent<Animator> ().Play ("CurveGridFourthCounterclockwiseRotation");
					CurrentCurveGridStep = GridStep.Third;
					break;
				default:
					break;
			}
		}
		if (_gridToRotateName == "QuadGrid" && _buttonToPressName == "QuadClockwiseRotationButton") {
			switch (CurrentQuadGridStep) {
				case GridStep.First:
					CurrentQuadGridStep = GridStep.Second;
					gridToRotate.GetComponent<Animator> ().Play ("QuadGridFirstClockwiseRotation");
					break;
				case GridStep.Second:
					gridToRotate.GetComponent<Animator> ().Play ("QuadGridSecondClockwiseRotation");
					CurrentQuadGridStep = GridStep.Third;
					break;
				case GridStep.Third:
					gridToRotate.GetComponent<Animator> ().Play ("QuadGridThirdClockwiseRotation");
					CurrentQuadGridStep = GridStep.Fourth;
					break;
				case GridStep.Fourth:
					gridToRotate.GetComponent<Animator> ().Play ("QuadGridFourthClockwiseRotation");
					CurrentQuadGridStep = GridStep.First;
					break;
				default:
					break;
			}
		}
		if (_gridToRotateName == "QuadGrid" && _buttonToPressName == "QuadCounterclockwiseRotationButton") {
			switch (CurrentQuadGridStep) {
				case GridStep.First:
					gridToRotate.GetComponent<Animator> ().Play ("QuadGridFirstCounterclockwiseRotation");
					CurrentQuadGridStep = GridStep.Fourth;
					break;
				case GridStep.Second:
					gridToRotate.GetComponent<Animator> ().Play ("QuadGridSecondCounterclockwiseRotation");
					CurrentQuadGridStep = GridStep.First;
					break;
				case GridStep.Third:
					gridToRotate.GetComponent<Animator> ().Play ("QuadGridThirdCounterclockwiseRotation");
					CurrentQuadGridStep = GridStep.Second;
					break;
				case GridStep.Fourth:
					gridToRotate.GetComponent<Animator> ().Play ("QuadGridFourthCounterclockwiseRotation");
					CurrentQuadGridStep = GridStep.Third;
					break;
				default:
					break;
			}
		}
	}

	private void ActiveEndRotationButton () {
		FindObjectOfType<NewGridController> ().EndRotationButton.SetActive (true);
	}

	private void DeactivateButton (GameObject _buttonToDeactivate) {
		_buttonToDeactivate.SetActive (false);
	}
}