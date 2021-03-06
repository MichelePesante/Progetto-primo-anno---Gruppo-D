﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

	public static CardManager Instance;

	public Vector3 IncScale;
	public Vector3 IncPosition;

	public List <CardController> CurveCards;
	public List <CardController> QuadCards;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} 
		else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RefreshCardsInHand (Player.Player_Curve);
		RefreshCardsInHand (Player.Player_Quad);
		SortCards (Player.Player_Curve);
		SortCards (Player.Player_Quad);
	}

	public void RefreshCardsInHand (Player _currentPlayer) {
		if (_currentPlayer == Player.Player_Curve) {
			foreach (CardController card in CurveCards) {
				card.RefreshCards ();
			}
		}
		else if (_currentPlayer == Player.Player_Quad) {
			foreach (CardController card in QuadCards) {
				card.RefreshCards ();
			}
		}
	}

	public void SortCards (Player _currentPlayer) {
		int currentIndex;
		if (_currentPlayer == Player.Player_Curve) {
			currentIndex = CurveCards.Count;
			if (CurveCards [3].hasBeenPlaced == true) {
				currentIndex = CurveCards.Count - 1;
			}
			for (int i = 0; i < currentIndex; i++) {
				if (CurveCards [i].hasBeenPlaced == true) {
					if (i + 1 < currentIndex) {
						CurveCards [i].SetData (CurveCards [i + 1].Data);
						CurveCards [i].hasBeenPlaced = false;
						CurveCards [i + 1].hasBeenPlaced = true;
					}
				}
			} 
		}
		else if (_currentPlayer == Player.Player_Quad) {
			currentIndex = QuadCards.Count;
			if (QuadCards [3].hasBeenPlaced == true) {
				currentIndex = QuadCards.Count - 1;
			}
			for (int i = 0; i < currentIndex; i++) {
				if (QuadCards [i].hasBeenPlaced == true) {
					if (i + 1 < currentIndex) {
						QuadCards [i].SetData (QuadCards [i + 1].Data);
						QuadCards [i].hasBeenPlaced = false;
						QuadCards [i + 1].hasBeenPlaced = true;
					}
				}
			}
		}
	}

	public void PlaceCard (Player player, int index) {
		if (player == Player.Player_Curve) {
			CurveCards [index].hasBeenPlaced = true;
		} 
		else if (player == Player.Player_Quad) {
			QuadCards [index].hasBeenPlaced = true;
		}
	}

	public void HighlightCard (Player player, int index) {
		for (int i = 0; i < CurveCards.Count; i++) {
			CurveCards [i].isHighlighted = false;
			QuadCards [i].isHighlighted = false;
		}
		if (player == Player.Player_Curve) {
			CurveCards [index].isHighlighted = true;
		}
		else if (player == Player.Player_Quad) {
			QuadCards [index].isHighlighted = true;
		}
	}
}