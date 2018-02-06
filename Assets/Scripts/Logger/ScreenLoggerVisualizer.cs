using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenLoggerVisualizer : MonoBehaviour {

	public TextMeshProUGUI TextArea;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TextArea.text = CustomLogger.currentLogString;
	}
}
