using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewUIManager : MonoBehaviour {

	public static NewUIManager Instance;
	public TextMeshProUGUI Punteggio_P1;
	public TextMeshProUGUI Punteggio_P2;
	public GameObject Display_Curve;
	public GameObject Display_Quad;
	public GameObject Energies;
	public GameObject Slots;

	// Use this for initialization
	void Awake () {
		if (Instance == null)
			Instance = this;
		else
			GameObject.Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		Punteggio_P1.text = FindObjectOfType<TurnManager> ().ScoreCurve.ToString();
		Punteggio_P2.text = FindObjectOfType<TurnManager> ().ScoreQuad.ToString();
	}
}
