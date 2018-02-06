using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	[SerializeField] private List<Card> cards;

	private int maxHandLimit;

	private int cardsInHand;

	private Deck cardsToDraw;

	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameController.Instance;
		cards = new List<Card> ();
		cardsInHand = 0;
		maxHandLimit = 4;
		Draw (cardsInHand);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Draw (int _cardsInHand) {
		int cardPosition = 0;
		if (_cardsInHand < maxHandLimit) {
			for (int i = _cardsInHand; i < maxHandLimit; i++) {
				cards.Add (new Card (gc.Deck.cards[cardPosition].Name, gc.Deck.cards[cardPosition].Value));
				gc.Deck.RemoveCard (i);
				cardPosition++;
			}
		}
	}
}