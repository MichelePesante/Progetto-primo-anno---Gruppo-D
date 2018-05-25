using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour {

	public static ButtonManager Instance;

	public Button CurveGridClockwiseButton;
	public Button CurveGridCounterclockwiseButton;
	public Button QuadGridClockwiseButton;
	public Button QuadGridCounterclockwiseButton;
	public Button Skip_Turn;

	private float rotationSpeed;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (this.gameObject);
		}
	}
		
	void Start () {
		rotationSpeed = 1f;
	}

	private void EnableButton (Button _buttonToEnable) {
		_buttonToEnable.gameObject.SetActive (true);
	}

	private void DisableButton (Button _buttonToDisable) {
		_buttonToDisable.gameObject.SetActive (false);
	}

	private void RotateRobotsClockwise (List <RobotController> _robotsToRotate, float _degreesToRotate) {
		Vector3 rotationToReach;
		foreach (RobotController robot in _robotsToRotate) {
			rotationToReach = robot.gameObject.transform.rotation.eulerAngles;
			rotationToReach.y += _degreesToRotate;
			robot.gameObject.transform.DORotate (rotationToReach, rotationSpeed);
		}
	}

	private void RotateRobotsCounterclockwise (List <RobotController> _robotsToRotate, float _degreesToRotate) {
		Vector3 rotationToReach;
		foreach (RobotController robot in _robotsToRotate) {
			rotationToReach = robot.gameObject.transform.rotation.eulerAngles;
			rotationToReach.y += -_degreesToRotate;
			robot.gameObject.transform.DORotate (rotationToReach, rotationSpeed);
		}
	}

	#region API

	public void CurveGridClockwiseRotation () {
		Vector3 rotationToReach;
		rotationToReach = FindObjectOfType<NewGridController> ().CurveTilesContainer.transform.rotation.eulerAngles;
		rotationToReach.y += 90f;
		FindObjectOfType<NewGridController> ().CurveTilesContainer.transform.DORotate (rotationToReach, rotationSpeed);
		RotateRobotsCounterclockwise (RobotManager.Instance.RobotCurviGiocati, 360f);
		RobotManager.Instance.OnClockwiseRotationCurveGrid ();
		DisableButton (CurveGridClockwiseButton);
		DisableButton (CurveGridCounterclockwiseButton);
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn && (QuadGridClockwiseButton.gameObject.activeInHierarchy == true)) {
			EnableButton (Skip_Turn);
		}
	}

	public void CurveGridCounterclockwiseRotation () {
		Vector3 rotationToReach;
		rotationToReach = FindObjectOfType<NewGridController> ().CurveTilesContainer.transform.rotation.eulerAngles;
		rotationToReach.y += -90f;
		FindObjectOfType<NewGridController> ().CurveTilesContainer.transform.DORotate (rotationToReach, rotationSpeed);
		RotateRobotsClockwise (RobotManager.Instance.RobotCurviGiocati, 360f);
		RobotManager.Instance.OnCounterclockwiseRotationCurveGrid ();
		DisableButton (CurveGridCounterclockwiseButton);
		DisableButton (CurveGridClockwiseButton);
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn && (QuadGridClockwiseButton.gameObject.activeInHierarchy == true)) {
			EnableButton (Skip_Turn);
		}
	}

	public void QuadGridClockwiseRotation () {
		Vector3 rotationToReach;
		rotationToReach = FindObjectOfType<NewGridController> ().QuadTilesContainer.transform.rotation.eulerAngles;
		rotationToReach.y += 90f;
		FindObjectOfType<NewGridController> ().QuadTilesContainer.transform.DORotate (rotationToReach, rotationSpeed);
		RotateRobotsCounterclockwise (RobotManager.Instance.RobotQuadratiGiocati, 360f);
		RobotManager.Instance.OnClockwiseRotationQuadGrid ();
		DisableButton (QuadGridClockwiseButton);
		DisableButton (QuadGridCounterclockwiseButton);
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn && (CurveGridClockwiseButton.gameObject.activeInHierarchy == true)) {
			EnableButton (Skip_Turn);
		}
	}

	public void QuadGridCounterclockwiseRotation () {
		Vector3 rotationToReach;
		rotationToReach = FindObjectOfType<NewGridController> ().QuadTilesContainer.transform.rotation.eulerAngles;
		rotationToReach.y += -90f;
		FindObjectOfType<NewGridController> ().QuadTilesContainer.transform.DORotate (rotationToReach, rotationSpeed);
		RotateRobotsClockwise (RobotManager.Instance.RobotQuadratiGiocati, 360f);
		RobotManager.Instance.OnCounterclockwiseRotationQuadGrid ();
		DisableButton (QuadGridCounterclockwiseButton);
		DisableButton (QuadGridClockwiseButton);
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn && (CurveGridClockwiseButton.gameObject.activeInHierarchy == true)) {
			EnableButton (Skip_Turn);
		}
	}

	public void RotationCheck () {
		if (CurveGridClockwiseButton.gameObject.activeInHierarchy == false && QuadGridClockwiseButton.gameObject.activeInHierarchy == false) {
			EndRotationTurn ();
		}
	}

	public void EndRotationTurn () {
		TurnManager.Instance.CurrentTurnState = TurnManager.TurnState.battle;
	}

	#endregion
}