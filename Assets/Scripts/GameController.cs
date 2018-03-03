﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	/// <summary>
	/// Riferimento alla classe 'GameController'.
	/// </summary>
	public static GameController Instance;

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
	/// Riferimento alla classe 'CardSpawn'.
	/// </summary>
	public CardSpawn CardS;

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

	public GameObject P1Wins;

	public GameObject P2Wins;

	public GameObject ButtonsRotationP1;

	public GameObject ButtonsRotationP2;

	public int cardSelector;

	public int clickCounterP1;

	public int clickCounterP2;

	public int totalPlaceableCardsP1;

	public int totalPlaceableCardsP2;

	public Camera MainCamera;

	public GameObject MainCameraPosition1;

	public GameObject MainCameraPosition2;

	private bool buttonsEnabled;

	private bool battleStarted;

	public bool backupPhaseIsEnded;

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

		// Riferimento a tutti i GridController.
		GridC = FindObjectsOfType<GridController> ();

		// Riferimento a tutti gli Hand.
		Hand = FindObjectsOfType<Hand> ();

		// Riferimento a tutti i Deck.
		Deck = FindObjectsOfType<Deck> ();

		// Riferimento a PedineGiocate.
		CardS = FindObjectOfType<CardSpawn> ();
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
		buttonsEnabled = false;
		battleStarted = false;
		backupPhaseIsEnded = false;
		MainCamera = FindObjectOfType<Camera> ();

		StateMachine.CurrentMacroPhase = StateMachine.MacroPhase.Start;

		if (StateMachine.CurrentMacroPhase == StateMachine.MacroPhase.Start) {
			StartPhase.OnGameStart ();
			StateMachine.CurrentMacroPhase = StateMachine.MacroPhase.Setup;
		}

		// Riferimento a tutti i Tassello(Clone).
		CellS = FindObjectsOfType<CellScript> ();
	}

	void Update () {

		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
			MainCamera.transform.position = MainCameraPosition1.transform.position;
			MainCamera.transform.rotation = MainCameraPosition1.transform.rotation;
		}

		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
			MainCamera.transform.position = MainCameraPosition2.transform.position;
			MainCamera.transform.rotation = MainCameraPosition2.transform.rotation;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && cardSelector > 0) {
			cardSelector--;
			CustomLogger.Log ("Carta numero {0}", cardSelector + 1);
		}

		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0 && cardSelector < Hand[0].cardsInHand - 1) {
				cardSelector++;
				CustomLogger.Log ("Carta numero {0}", cardSelector + 1);
			}
		}

		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0 && cardSelector < Hand[1].cardsInHand - 1) {
				cardSelector++;
				CustomLogger.Log ("Carta numero {0}", cardSelector + 1);
			}
		}


		if (Input.GetKeyDown (KeyCode.H)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
			
		if (scorep1 >= 5 && P2Wins.activeInHierarchy == false) {
			P1Wins.SetActive (true);
		}

		if (scorep2 >= 5 && P1Wins.activeInHierarchy == false) {
			P2Wins.SetActive (true);
		}

		if (SetupPhase.IsSetupPhaseEnded () == true) {
			StateMachine.CurrentMacroPhase = StateMachine.MacroPhase.Core;
		}
			
		if (StateMachine.CurrentMacroPhase == StateMachine.MacroPhase.Core) {
			if (buttonsEnabled == false) {
				FindObjectOfType<RotationScript> ().hasGrid1BeenRotated = false;
				FindObjectOfType<RotationScript> ().hasGrid2BeenRotated = false;
				CorePhase.EnableRotationButtons ();
				buttonsEnabled = true;
			}

			if (FindObjectOfType<RotationScript> ().hasGrid1BeenRotated == true || FindObjectOfType<RotationScript> ().hasGrid2BeenRotated == true) {
				ButtonsRotationP1.SetActive (false);
				ButtonsRotationP2.SetActive (false);
				if (battleStarted == false) {
					CorePhase.BattlePhase ();
					battleStarted = true;
				}
			}

			if (backupPhaseIsEnded == true) {
				buttonsEnabled = false;
				battleStarted = false;
				backupPhaseIsEnded = false;
			}
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
}
