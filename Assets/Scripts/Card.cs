using System;

[Serializable]
public class Card {

	/// <summary>
	/// Nome della carta.
	/// </summary>
	public string Name;

	/// <summary>
	/// Valore della carta.
	/// </summary>
	public int Value;

	public Card () {
	
	}

	public Card (string _name, int _value) {
		Name = _name;
		Value = _value;
	}
}