using System;
using UnityEngine;

[Serializable]
public class Card {

	/// <summary>
	/// Coordinata x.
	/// </summary>
	public int X;

	/// <summary>
	/// Coordinata y.
	/// </summary>
	public int Y;

	/// <summary>
	/// Nome della carta.
	/// </summary>
	public string Name;

	/// <summary>
	/// Valore della carta.
	/// </summary>
	public int Value;

	/// <summary>
	/// Se true la carta è piazzata.
	/// </summary>
	public bool IsPlaced;

	/// <summary>
	/// Colore della carta, se rossa appartiene al primo giocatore, altrimenti al secondo.
	/// </summary>
	public Color Team;

	public Card () {
	
	}

	public Card (int _x, int _y, string _name, int _value, bool _isPlaced, Color _team) {
		X = _x;
		Y = _y;
		Name = _name;
		Value = _value;
		IsPlaced = _isPlaced;
		Team = _team;
	}
}