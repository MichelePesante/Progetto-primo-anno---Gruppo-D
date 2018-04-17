using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCanvasController : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		transform.LookAt (GameObject.Find("PositionToLookAt").transform.position);
	}
}
