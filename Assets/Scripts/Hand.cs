using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	/// <summary>
	/// Lista di carte.
	/// </summary>
	[SerializeField] public List<Card> cards;

	/// <summary>
	/// Deck dal quale bisogna pescare le carte.
	/// </summary>
	[SerializeField] public Deck DeckToDrawFrom;

	/// <summary>
	/// Numero massimo di carte che si possono tenere in mano.
	/// </summary>
	private int maxHandLimit;

	/// <summary>
	/// Carte che si hanno attualmente in mano.
	/// </summary>
	public int cardsInHand;

	// Use this for initialization
	void Start () {
		// Inizializzazione delle variabili.
		cardsInHand = 0;
		maxHandLimit = 4;

		if (this == GameController.Instance.Hand[0]) {
			// Inizializzazione delle variabili.
			cards = new List<Card> ();

			Draw (cardsInHand);
		}

		if (this == GameController.Instance.Hand[1]) {
			// Inizializzazione delle variabili.
			cards = new List<Card> ();

			Draw (cardsInHand);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (this == GameController.Instance.Hand [0]) {
			if (GameController.Instance.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
				if (Input.GetKeyDown (KeyCode.R)) {
					Draw (cardsInHand);
				}
			}
		}

		if (this == GameController.Instance.Hand [1]) {
			if (GameController.Instance.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
				if (Input.GetKeyDown (KeyCode.R)) {
					Draw (cardsInHand);
				}
			}
		}
	}

	/// <summary>
	/// Funzione di pesca delle carte.
	/// </summary>
	/// <param name="_cardsInHand">Numero di carte che si hanno già in mano.</param>
	public void Draw (int _cardsInHand) {
		int cardPosition = 0;
		if (_cardsInHand < maxHandLimit) {
			for (int i = _cardsInHand; i < maxHandLimit && DeckToDrawFrom.cards.Count > 0; i++) {
				cards.Add (new Card (DeckToDrawFrom.cards [cardPosition].X, DeckToDrawFrom.cards [cardPosition].Y, DeckToDrawFrom.cards [cardPosition].Name, DeckToDrawFrom.cards [cardPosition].Value,
					DeckToDrawFrom.cards [cardPosition].IsPlaced, DeckToDrawFrom.cards [cardPosition].Team));
				DeckToDrawFrom.RemoveCardFromDeck (cardPosition);
				cardsInHand = i + 1;
				if (this == GameController.Instance.Hand [0]) {
					CustomLogger.Log ("Sono il giocatore 1 e ho pescato la carta {0} che vale {1}", cards [i].Name, cards [i].Value);
				}
				if (this == GameController.Instance.Hand [1]) {
					CustomLogger.Log ("Sono il giocatore 2 e ho pescato la carta {0} che vale {1}", cards [i].Name, cards [i].Value);
				}
			}
		}
		if (DeckToDrawFrom.cards.Count == 0) {
			CustomLogger.Log ("Non ho carte da pescare");
		}
	}

	/// <summary>
	/// Rimuove una carta dalla mano.
	/// </summary>
	/// <param name="_listIndex">Indice della carta che si vuole rimuovere.</param>
	public void RemoveCardFromHand (int _listIndex) {
		cards.Remove (cards [_listIndex]);
		cardsInHand -= 1;
	}
}