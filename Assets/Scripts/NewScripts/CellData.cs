using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CellData {

	/// <summary>
	/// Valore punto x della cella.
	/// </summary>
	public int X;

	/// <summary>
	/// Valore punto y della cella.
	/// </summary>
	public int Y;

	/// <summary>
	/// Posizione nel mondo della cella.
	/// </summary>
	public Vector3 WorldPosition;

	public bool IsValid = true;

	/// <summary>
	/// Inizializza una nuova istanza con le variabili x, y, la posizione nel mondo e la piazzabilità.
	/// </summary>
	/// <param name="_x">X.</param>
	/// <param name="_y">Y.</param>
	/// <param name="_worldPosition">World position.</param>
	public CellData (int _x, int _y, Vector3 _worldPosition) {
		X = _x;
		Y = _y;
		WorldPosition = _worldPosition;
	}

	public void SetValidity(bool _isValid) {
		IsValid = _isValid;
	}
}