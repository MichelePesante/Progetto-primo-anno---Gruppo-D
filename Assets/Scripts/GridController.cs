using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

	/// <summary>
	/// Punto tassello finale sull'asse x.
	/// </summary>
	private int xPoint;

	/// <summary>
	/// Punto tassello finale sull'asse y.
	/// </summary>
	private int yPoint;

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
	/// GameObject preso da prefab.
	/// </summary>
	public GameObject CellCollider;

	/// <summary>
	/// Contenitore cell colliders del player 1.
	/// </summary>
	public GameObject CellCollidersP1;

	/// <summary>
	/// Contenitore cell colliders del player 2.
	/// </summary>
	public GameObject CellCollidersP2;

	public GameObject TilesP1;

	public GameObject TilesP2;

	/// <summary>
	/// Lista di tutte le celle.
	/// </summary>
	public List<CellData> cells = new List<CellData>();

	private GameObject newCellScript;

	private GameObject newTile;

	void Awake () {
		
	}

	void Start () {
		// Inizializzazione variabili.
		xPoint = 2;
		offset = 1.2f;
		// Set della variabile 'offsettedSize' con la x del prefab.
		offsettedSize = Tile.transform.localScale.x + offset;

		// Controllo quale GameObject sto usando.
		if (this == GameController.Instance.GridC [0]) {
			// Modifico valore del punto dell'ultimo tassello y.
			yPoint = 2;

			// Creazione griglia.
			for (int x = -1; x < xPoint; x++) {
				for (int y = -1; y < yPoint; y++) {
					cells.Add (new CellData (x, y, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y)));
					newCellScript = Instantiate (CellCollider, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), transform.rotation, CellCollidersP1.transform);
					newCellScript.GetComponent<ColliderScript> ().SetPosition (x, y);
					newTile = Instantiate (Tile, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), transform.rotation, TilesP1.transform);
					newTile.GetComponent<CellScript> ().SetPosition (x, y);
					if (x == 0 && y == 0) {
						newCellScript.SetActive (false);
						newTile.SetActive (false);
					}
				}
			}
		}

		if (this == GameController.Instance.GridC [1]) {
			// Modifico valore del punto dell'ultimo tassello y.
			yPoint = 6;

			// Creazione griglia.
			for (int x = -1; x < xPoint; x++) {
				for (int y = 3; y < yPoint; y++) {
					cells.Add (new CellData (x, y, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y)));
					newCellScript = Instantiate (CellCollider, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), transform.rotation, CellCollidersP2.transform);
					newCellScript.GetComponent<ColliderScript> ().SetPosition (x, y);
					newTile = Instantiate (Tile, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), transform.rotation, TilesP2.transform);
					newTile.GetComponent<CellScript> ().SetPosition (x, y);
					if (x == 0 && y == 4) {
						newCellScript.SetActive (false);
						newTile.SetActive (false);
					}
				}
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
	/// Controlla se la casella esiste.
	/// </summary>
	/// <returns><c>true</c> se la posizione della casella specificata è valida, in caso contrario <c>false</c>.</returns>
	/// <param name="_x">Posizione x.</param>
	/// <param name="_y">Posizione y.</param>
	public bool positionCheck (int _x, int _y) {
		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1) {
			if (this == GameController.Instance.GridC [0]) {
				if (_x < -1 || _y < -1)
					return false;
				if (_x > xPoint - 1 || _y > yPoint - 1)
					return false;
			}
		}

		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2) {
			if (this == GameController.Instance.GridC [1]) {
				if (_x < -1 || _y < 3)
					return false;
				if (_x > xPoint - 1 || _y > yPoint - 1)
					return false;
			}
		}

		if (_x == 0 && _y == 0 || _x == 0 && _y == 4)
			return false;
		
		return true;
	}
	#endregion
}