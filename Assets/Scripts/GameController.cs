using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	/// <summary>
	/// Riferimento alla classe 'GameController'.
	/// </summary>
	public static GameController Instance; 

	/// <summary>
	/// Energia in dotazione al player 1.
	/// </summary>
	public int EnergyPlayer1;

	/// <summary>
	/// Energia in dotazione al player 2.
	/// </summary>
	public int EnergyPlayer2;

	/// <summary>
	/// Energia che si decide di utilizzare.
	/// </summary>
	public int EnergyToSpend;

	/// <summary>
	/// Specificazione di chi è il turno.
	/// </summary>
	public PlayerTurn CurrentPlayerTurn;

	void Awake () {
		// Se non esiste un'istanza di questo script.
		if (Instance == null) {
			// Crea l'istanza.
			Instance = this;
		} 
		// Alrimenti.
		else {
			// Distruggi l'oggetto.
			Destroy (gameObject);
		}
	}

	void Start () {
		// Inizializzazione variabili.
		EnergyPlayer1 = 7;
		EnergyPlayer2 = 7;
		EnergyToSpend = 0;
		CurrentPlayerTurn = PlayerTurn.TurnPlayer1;

		// Riferimento al GameController.
		Instance = this;
	}

	#region Vecchie meccaniche

	/*

	void Update () {
		// Aumento di energia da spendere.
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (EnergyToSpend < 4) {
				EnergyToSpend++;
				//print (EnergyToSpend);
			}
		}

		// Diminuzione di energia da spendere.
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (EnergyToSpend > 0) {
				EnergyToSpend--;
				//print (EnergyToSpend);
			}
		}
	}

	*/

	#endregion
}

public enum PlayerTurn
{
	TurnPlayer1, TurnPlayer2
}