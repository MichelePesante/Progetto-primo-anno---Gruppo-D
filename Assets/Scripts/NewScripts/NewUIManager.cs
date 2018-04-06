using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewUIManager : MonoBehaviour {

	public static NewUIManager Instance;
	public TextMeshProUGUI Punteggio_P1;
	public TextMeshProUGUI Punteggio_P2;
	public GameObject Display_P1;
	public GameObject Display_P2;

	// Use this for initialization
	void Awake () {
		if (Instance == null)
			Instance = this;
		else
			GameObject.Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		Punteggio_P1.text = ("" + FindObjectOfType<TurnManager> ().ScoreP1);
		Punteggio_P2.text = ("" + FindObjectOfType<TurnManager> ().ScoreP2);
	}
}
