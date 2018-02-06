using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	/// <summary>
	/// Lista di carte.
	/// </summary>
	[SerializeField] public List<Card> cards;

	/// <summary>
	/// Numero massimo di carte che si possono tenere in mano.
	/// </summary>
	private int maxHandLimit;

	/// <summary>
	/// Carte che si hanno attualmente in mano.
	/// </summary>
	public int cardsInHand;

	/// <summary>
	/// Carte da pescare.
	/// </summary>
	private Deck cardsToDraw;

	/// <summary>
	/// Riferimento alla classe 'GameController'.
	/// </summary>
	private GameController gc;

	// Use this for initialization
	void Start () {
		// Riferimento al GameController.
		gc = GameController.Instance;

		// Inizializzazione delle variabili.
		cards = new List<Card> ();
		cardsInHand = 0;
		maxHandLimit = 4;

		// Funzione di pesca.
		Draw (cardsInHand);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			Draw (cardsInHand);
		}
	}

	/// <summary>
	/// Funzione di pesca delle carte.
	/// </summary>
	/// <param name="_cardsInHand">Numero di carte che si hanno già in mano.</param>
	private void Draw (int _cardsInHand) {
		int cardPosition = 0;
		if (_cardsInHand < maxHandLimit) {
			for (int i = _cardsInHand; i < maxHandLimit; i++) {
				cards.Add (new Card (gc.Deck.cards[cardPosition].Name, gc.Deck.cards[cardPosition].Value));
				gc.Deck.RemoveCardFromDeck (cardPosition);
				cardsInHand = i + 1;
				Debug.LogFormat ("Ho pescato la carta {0} che vale {1}", cards[i].Name, cards[i].Value);
			}
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