using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController2 : MonoBehaviour {

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
		xSize = 3;
		ySize = 3;
		offset = 1.2f;
		// Set della variabile 'offsettedSize' con la x del prefab.
		offsettedSize = Tile.transform.localScale.x + offset;

		// Creazione griglia.
		for (int x = -1; x < xSize - 1; x++) {
			for (int y = 3; y < ySize + 3; y++) {
				GameObject thisTile = Instantiate (Tile, new Vector3(offsettedSize * x, transform.position.y, offsettedSize * y), transform.rotation, transform);
				if (x == 0 && y == 4) {
					thisTile.SetActive (false);
				}
			}
		}
	}

	void Start () {
		// Inizializzazione di ogni cella.
		for (int x = -1; x < xSize - 1; x++) {
			for (int y = 3; y < ySize + 3; y++) {
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
	/// Verifica se su una cella è possibile posizionare una pedina.
	/// </summary>
	/// <returns><c>true</c> se possibile piazzare una pedina, <c>false</c>in caso contrario.</returns>
	/// /// <param name="_x">Posizione x.</param>
	/// <param name="_y">Posizione y.</param>
	public bool cellCheck (int _x, int _y) {
		foreach (CellData cell in cells) {
			if (cell.X == _x && cell.Y == _y && cell.Placeable == true) {
				return true;
			}
			if (cell.X == _x && cell.Y == _y && cell.Placeable == false) {
				return false;
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
		if (_x < -1 || _y < 3)
			return false;
		if (_x > xSize - 2|| _y > ySize + 2)
			return false;
		if (_x == 0 && _y == 4)
			return false;
		return true;
	}

	#endregion
}