using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	/// <summary>
	/// Riferimento alla classe 'GameController'.
	/// </summary>
	public static GameController Instance;

	/// <summary>
	/// Riferimento a tutte le classi 'SpawnController'.
	/// </summary>
	public SpawnController[] SpawnC;

	/// <summary>
	/// Riferimento a tutte le classi 'GridController'.
	/// </summary>
	public GridController[] GridC;

	/// <summary>
	/// Riferimento alle classi 'Deck'.
	/// </summary>
	public Deck[] Deck;

	/// <summary>
	/// Riferimento a tutte le classi 'Hand'.
	/// </summary>
	public Hand[] Hand;

	/// <summary>
	/// Riferimento a tutte le classi 'CellScript'.
	/// </summary>
	public CellScript[] CellS;

	/// <summary>
	/// Score giocatore 1.
	/// </summary>
	public int scorep1;

	/// <summary>
	/// Score giocatore 2.
	/// </summary>
	public int scorep2;

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

	public int cardSelector;

	public int clickCounterP1;

	public int clickCounterP2;

	public int totalPlaceableCardsP1;

	public int totalPlaceableCardsP2;

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

		// Riferimento a tutti gli SpawnController.
		SpawnC = FindObjectsOfType<SpawnController> ();

		// Riferimento a tutti i GridController.
		GridC = FindObjectsOfType<GridController> ();

		// Riferimento a tutti gli Hand.
		Hand = FindObjectsOfType<Hand> ();

		// Riferimento a tutti i Deck.
		Deck = FindObjectsOfType<Deck> ();
	}

	void Start () {
		// Inizializzazione variabili.
		EnergyPlayer1 = 7;
		EnergyPlayer2 = 7;
		EnergyToSpend = 0;
		scorep1 = 0;
		scorep2 = 0;
		cardSelector = 0;
		clickCounterP1 = 0;
		clickCounterP2 = 0;
		totalPlaceableCardsP1 = 8;
		totalPlaceableCardsP2 = 8;

		// Riferimento a tutti i Tassello(Clone).
		CellS = FindObjectsOfType<CellScript> ();
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			CurrentPlayerTurn = PlayerTurn.TurnPlayer1;
			CustomLogger.Log ("Turno del giocatore 1");
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			CurrentPlayerTurn = PlayerTurn.TurnPlayer2;
			CustomLogger.Log ("Turno del giocatore 2");
		}

		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && cardSelector > 0) {
			cardSelector--;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && cardSelector < 3) {
			cardSelector++;
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			Battle ();
			CustomLogger.Log ("Score del giocatore 1:  {0}     ---     Score del giocatore 2:  {1}", scorep1, scorep2);
		}

		if (Input.GetKeyDown (KeyCode.H)  || scorep1 >= 5 || scorep2 >= 5) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		#region Vecchie meccaniche

		/*
		 
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

		*/

		#endregion
	}

	private void Battle () {
		int battleResult1 = 0;
		int battleResult2 = 0;
		int battleResult3 = 0;

		int scoretemp1 = 0;
		int scoretemp2 = 0;
		int finalScore = 0;

		int ForzaPedina1p1 = 0;
		int ForzaPedina2p1 = 0;
		int ForzaPedina3p1 = 0;
		int ForzaPedina1p2 = 0;
		int ForzaPedina2p2 = 0;
		int ForzaPedina3p2 = 0;

		foreach (PawnData pawn in SpawnC[0].pawns) {
			if (pawn.X == -1 && pawn.Y == 1) {
				ForzaPedina1p1 = pawn.Strength;
				CustomLogger.Log ("Forza pedina 1 player 1:   " + ForzaPedina1p1);
			}
			if (pawn.X == 0 && pawn.Y == 1) {
				ForzaPedina2p1 = pawn.Strength;
				CustomLogger.Log ("Forza pedina 2 player 1:   " + ForzaPedina2p1);
			}
			if (pawn.X == 1 && pawn.Y == 1) {
				ForzaPedina3p1 = pawn.Strength;
				CustomLogger.Log ("Forza pedina 3 player 1:   " + ForzaPedina3p1);
			}
		}

		foreach (PawnData pawn in SpawnC[1].pawns) {
			if (pawn.X == -1 && pawn.Y == 3) {
				ForzaPedina1p2 = pawn.Strength;
				CustomLogger.Log ("Forza pedina 1 player 2:   " + ForzaPedina1p2);
			}
			if (pawn.X == 0 && pawn.Y == 3) {
				ForzaPedina2p2 = pawn.Strength;
				CustomLogger.Log ("Forza pedina 2 player 2:   " + ForzaPedina2p2);
			}
			if (pawn.X == 1 && pawn.Y == 3) {
				ForzaPedina3p2 = pawn.Strength;
				CustomLogger.Log ("Forza pedina 3 player 2:   " + ForzaPedina3p2);
			}
		}

		battleResult1 = ForzaPedina1p1 - ForzaPedina1p2;
		if (battleResult1 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult1 < 0) {
			scoretemp2 += 1;
		}
		battleResult2 = ForzaPedina2p1 - ForzaPedina2p2;
		if (battleResult2 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult2 < 0) {
			scoretemp2 += 1;
		}
		battleResult3 = ForzaPedina3p1 - ForzaPedina3p2;
		if (battleResult3 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult3 < 0) {
			scoretemp2 += 1;
		}
		if (scoretemp1 > scoretemp2) {
			finalScore = scoretemp1 - scoretemp2;
			scorep1 += finalScore;
		}
		if (scoretemp1 < scoretemp2) {
			finalScore = scoretemp2 - scoretemp1;
			scorep2 += finalScore;
		}
	}
}

public enum PlayerTurn
{
	TurnPlayer1, 
	TurnPlayer2
}