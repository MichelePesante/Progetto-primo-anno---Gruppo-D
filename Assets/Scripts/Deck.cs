using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	/// <summary>
	/// Lista di tutte le carte.
	/// </summary>
	[SerializeField] public List<Card> cards;

	void Awake () {
		// Creazione dell'istanza della lista.
		cards = new List<Card> ();

		// Aggiunta di 16 elementi alla lista.
		for (int i = 0; i < 16; i++) {
			cards.Add (new Card ());
		}

		// Inizializzazione degli elementi da 0 a 3.
		for (int i = 0; i < 4; i++) {
			cards [i].Name = "Alpha";
			cards [i].Value = 1;
		}

		// Inizializzazione degli elementi da 4 a 7.
		for (int i = 4; i < 8; i++) {
			cards [i].Name = "Beta";
			cards [i].Value = 2;
		}

		// Inizializzazione degli elementi da 8 a 11.
		for (int i = 8; i < 12; i++) {
			cards [i].Name = "Gamma";
			cards [i].Value = 3;
		}

		// Inizializzazione degli elementi da 11 a 15.
		for (int i = 12; i < 16; i++) {
			cards [i].Name = "Delta";
			cards [i].Value = 4;
		}

		Shuffle ();
	}

	void Start () {
		
	}

	void Update () {
		
	}

	/// <summary>
	/// Mischia il mazzo di carte. 
	/// </summary>
	private void Shuffle () {
		// Variabile temporanea.
		Card temporaryCard = new Card ();

		// Per un numero casuale di volte.
		for (int i = 0; i < Random.Range (1, 20); i++) {
			// Per tutta la lunghezza della lista.
			for (int c = 0; c < cards.Count; c++) {
				// Posizione presa casualmente.
				int randomCard = Random.Range (0, cards.Count - 1);
				// Elemento 'i' viene assegnato all'elemento temporaneo.
				temporaryCard = cards[c];
				// Posizione casuale viene assegnata all'elemento 'i'.
				cards [c] = cards [randomCard];
				// Elemento temporaneo viene assegnato alla posizione casuale.
				cards [randomCard] = temporaryCard;
			}
		}
	}

	/// <summary>
	/// Rimuove una carta dal mazzo.
	/// </summary>
	/// <param name="_listIndex">Indice della carta che si vuole rimuovere.</param>
	public void RemoveCardFromDeck (int _listIndex) {
		cards.Remove (cards [_listIndex]);
	}
}