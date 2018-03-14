using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawn : MonoBehaviour {

	[SerializeField] public List<Card> PlacedCards;

	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameController.Instance;
		PlacedCards = new List<Card> ();
	}

	public void PlaceCardAndSetPlacedCard (int _x, int _y, Hand _handToPlaceCardFrom, Color _teamColor) {
		PlacedCards.Add (new Card(_x, _y, _handToPlaceCardFrom.cards[gc.cardSelector].Name, _handToPlaceCardFrom.cards[gc.cardSelector].Value, true, _teamColor));
		FindObjectOfType<PawnScript>().SetVariables(_x, _y, _handToPlaceCardFrom.cards[gc.cardSelector].Name, _handToPlaceCardFrom.cards[gc.cardSelector].Value, true, _teamColor);
		_handToPlaceCardFrom.RemoveCardFromHand (gc.cardSelector);
	}
}
