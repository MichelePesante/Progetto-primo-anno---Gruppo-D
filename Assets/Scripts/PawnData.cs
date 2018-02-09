using System;
using UnityEngine;

[Serializable]
public class PawnData {

	/// <summary>
	/// Coordinata x.
	/// </summary>
	public int X;

	/// <summary>
	/// Coordinata y.
	/// </summary>
	public int Y;

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
	/// World position.
	/// </summary>
	public Vector3 WorldPosition;

	/// <summary>
	/// Inizializza una nuova istanza con il nome, la forza, lo stato di vita e il player a cui appartiene.
	/// </summary>
	/// <param name="_x">Coordinata x.</param>
	/// <param name="_y">Coordinata y.</param>
	/// <param name="_name">Nome.</param>
	/// <param name="_strength">Forza.</param>
	/// <param name="_isAlive">Se <c>true</c> è viva.</param>
	/// <param name="_team">Player a cui appartiene.</param>
	public PawnData (int _x, int _y, string _name, int _strength, bool _isAlive, Color _team, Vector3 _worldPosition) {
		X = _x;
		Y = _y;
		Name = _name;
		Strength = _strength;
		IsAlive = _isAlive;
		Team = _team;
		WorldPosition = _worldPosition;
	}
}