using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour {

	[Header ("Posizione cella")]
	public int X;
	public int Y;
	public bool IsPlaceable = true;
	public Vector3 WorldPosition;

	public void SetPosition (int _x, int _y, Vector3 _worldPosition) {
		X = _x;
		Y = _y;
		WorldPosition = _worldPosition;
	}
}
