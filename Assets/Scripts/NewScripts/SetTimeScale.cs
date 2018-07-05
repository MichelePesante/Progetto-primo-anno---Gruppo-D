using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour {

	public float timeScale = 1.5f;
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = timeScale;
	}
}
