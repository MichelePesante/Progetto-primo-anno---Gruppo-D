using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	/// <summary>
	/// Variabile per modificare a piacere la transform della camera.
	/// </summary>
	private Transform cameraTransform;

	void Awake () {
		cameraTransform = transform;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			cameraTransform = GameObject.Find ("CameraPosition_2").transform;
			transform.position = cameraTransform.position;
			transform.rotation = cameraTransform.rotation;
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			cameraTransform = GameObject.Find ("CameraPosition_1").transform;
			transform.position = cameraTransform.position;
			transform.rotation = cameraTransform.rotation;
		}
	}
}
