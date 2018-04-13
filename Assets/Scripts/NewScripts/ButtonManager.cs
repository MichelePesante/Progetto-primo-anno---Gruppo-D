using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour {

	public Button CurveGridClockwiseButton;
	public Button CurveGridCounterclockwiseButton;
	public Button QuadGridClockwiseButton;
	public Button QuadGridCounterclockwiseButton;
	public Button Skip_Turn;

	private float rotationSpeed;

	// Use this for initialization
	void Start () {
		rotationSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CurveGridClockwiseRotation () {
		Vector3 rotationToReach;
		rotationToReach = FindObjectOfType<NewGridController> ().CurveTilesContainer.transform.rotation.eulerAngles;
		rotationToReach.y += 90f;
		FindObjectOfType<NewGridController> ().CurveTilesContainer.transform.DORotate (rotationToReach, rotationSpeed);
	}

	public void CurveGridCounterclockwiseRotation () {
		Vector3 rotationToReach;
		rotationToReach = FindObjectOfType<NewGridController> ().CurveTilesContainer.transform.rotation.eulerAngles;
		rotationToReach.y += -90f;
		FindObjectOfType<NewGridController> ().CurveTilesContainer.transform.DORotate (rotationToReach, rotationSpeed);
	}

	public void QuadGridClockwiseRotation () {
		Vector3 rotationToReach;
		rotationToReach = FindObjectOfType<NewGridController> ().QuadTilesContainer.transform.rotation.eulerAngles;
		rotationToReach.y += 90f;
		FindObjectOfType<NewGridController> ().QuadTilesContainer.transform.DORotate (rotationToReach, rotationSpeed);
	}

	public void QuadGridCounterclockwiseRotation () {
		Vector3 rotationToReach;
		rotationToReach = FindObjectOfType<NewGridController> ().QuadTilesContainer.transform.rotation.eulerAngles;
		rotationToReach.y += -90f;
		FindObjectOfType<NewGridController> ().QuadTilesContainer.transform.DORotate (rotationToReach, rotationSpeed);
	}
}