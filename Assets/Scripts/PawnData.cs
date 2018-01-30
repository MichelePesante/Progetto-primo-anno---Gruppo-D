using System;
using UnityEngine;

[Serializable]
public class PawnData {

	/// <summary>
	/// Nome che identifica la pedina.
	/// </summary>
	public string Name;

	/// <summary>
	/// Indicatore di forza di una pedina.
	/// </summary>
	public int Strength;

	/// <summary>
	/// Se true la pedina è viva.
	/// </summary>
	public bool IsAlive;

	/// <summary>
	/// Colore della pedina, se rossa appartiene al primo giocatore, altrimenti al secondo.
	/// </summary>
	public Color Team;

	/// <summary>
	/// Inizializza una nuova istanza con il nome, la forza, lo stato di vita e il player a cui appartiene.
	/// </summary>
	/// <param name="_name">Nome.</param>
	/// <param name="_strength">Forza.</param>
	/// <param name="_isAlive">Se <c>true</c> è viva.</param>
	/// <param name="_team">Player a cui appartiene.</param>
	public PawnData (string _name, int _strength, bool _isAlive, Color _team) {
		Name = _name;
		Strength = _strength;
		IsAlive = _isAlive;
		Team = _team;
	}
}