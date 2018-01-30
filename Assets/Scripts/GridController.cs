using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

	/// <summary>
	/// Numero di tasselli sull'asse x.
	/// </summary>
	private int xSize;

	/// <summary>
	/// Numero di tasselli sull'asse y.
	/// </summary>
	private int ySize;

	/// <summary>
	/// Misura di tassello + offset.
	/// </summary>
	private float offsettedSize;

	/// <summary>
	/// Distanza tra un tassello e l'altro.
	/// </summary>
	private float offset;

	/// <summary>
	/// GameObject preso da prefab.
	/// </summary>
	public GameObject Tile;

	/// <summary>
	/// Lista di tutte le celle.
	/// </summary>
	public List<CellData> cells = new List<CellData>();

	void Awake () {
		// Inizializzazione variabili.
		xSize = 4;
		ySize = 4;
		offset = 1.2f;
		// Set della variabile 'offsettedSize' con la x del prefab.
		offsettedSize = Tile.transform.localScale.x + offset;

		// Creazione griglia.
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				Instantiate (Tile, new Vector3(offsettedSize * x, transform.position.y, offsettedSize * y), transform.rotation, transform);
			}
		}
	}
		
	void Start () {
		// Inizializzazione di ogni cella.
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				cells.Add (new CellData (x, y, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), true));
			}
		}
	}

	#region API

	/// <summary>
	/// Restituisce la world position richiesta chiedendo x e y di un tassello.
	/// </summary>
	/// <returns>World position.</returns>
	/// <param name="_xPos">Posizione x.</param>
	/// <param name="_yPos">Posizione y.</param>
	public Vector3 GetWorldPosition (int _xPos, int _yPos) {
		foreach (CellData cell in cells) {
			if (cell.X == _xPos && cell.Y == _yPos) {
				return cell.WorldPosition;
			}
		}
		return cells [0].WorldPosition;
	}

	/// <summary>
	/// Verifica se su una cella è già possibile posizionare una pedina.
	/// </summary>
	/// <returns><c>true</c> se possibile piazzare una pedina, <c>false</c>in caso contrario.</returns>
	/// /// <param name="_x">Posizione x.</param>
	/// <param name="_y">Posizione y.</param>
	public bool cellCheck (int _x, int _y) {
		foreach (CellData cell in cells) {
			if (cell.X == _x && cell.Y == _y && cell.Placeable == true) {
				return true;
			} 
		}
		return false;
	}

	/// <summary>
	/// Controlla se la casella esiste.
	/// </summary>
	/// <returns><c>true</c> se la posizione della casella specificata è valida, in caso contrario <c>false</c>.</returns>
	/// <param name="_x">Posizione x.</param>
	/// <param name="_y">Posizione y.</param>
	public bool positionCheck (int _x, int _y) {
		if (_x < 0 || _y < 0)
			return false;
		if (_x > xSize - 1|| _y > ySize - 1)
			return false;
		return true;
	}

	#endregion
}