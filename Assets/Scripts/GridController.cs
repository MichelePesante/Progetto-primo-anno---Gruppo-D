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
	/// Tassello singolo.
	/// </summary>
	public GameObject thisTile;

	/// <summary>
	/// Lista di tutte le celle.
	/// </summary>
	public List<CellData> cells = new List<CellData>();

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
					thisTile = Instantiate (Tile, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), transform.rotation, transform);
					cells.Add (new CellData (x, y, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), thisTile));
					if (x == 0 && y == 0) {
						thisTile.SetActive (false);
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
					thisTile = Instantiate (Tile, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), transform.rotation, transform);
					cells.Add (new CellData (x, y, new Vector3 (offsettedSize * x, transform.position.y, offsettedSize * y), thisTile));
					if (x == 0 && y == 4) {
						thisTile.SetActive (false);
					}
				}
			}
		}
	}

	void Update () {

		// Controllo quale GameObject sto usando.
		if (this == GameController.Instance.GridC [0]) {
			if (GameController.Instance.CurrentPlayerTurn == PlayerTurn.TurnPlayer1) {
				if (Input.GetKeyDown (KeyCode.Q)) {
					GameController.Instance.GridC [0].transform.Rotate (0f, -90f, 0f);
					OnLeftRotationFirstGrid ();
				}
				if (Input.GetKeyDown (KeyCode.E)) {
					GameController.Instance.GridC [0].transform.Rotate (0f, 90f, 0f);
					OnRightRotationFirstGrid ();
				}
			
				if (Input.GetKeyDown (KeyCode.I)) {
					GameController.Instance.GridC [1].gameObject.transform.Rotate (0f, -90f, 0f);
					OnLeftRotationSecondGrid ();
				}
				if (Input.GetKeyDown (KeyCode.P)) {
					GameController.Instance.GridC [1].gameObject.transform.Rotate (0f, 90f, 0f);
					OnRightRotationSecondGrid ();
				}
			}
		}

		// Controllo quale GameObject sto usando.
		if (this == GameController.Instance.GridC [1]) {
			if (GameController.Instance.CurrentPlayerTurn == PlayerTurn.TurnPlayer2) {
				if (Input.GetKeyDown (KeyCode.Q)) {
					GameController.Instance.GridC [1].transform.Rotate (0f, -90f, 0f);
					OnLeftRotationSecondGrid ();
				}
				if (Input.GetKeyDown (KeyCode.E)) {
					GameController.Instance.GridC [1].transform.Rotate (0f, 90f, 0f);
					OnRightRotationSecondGrid ();
				}

				if (Input.GetKeyDown (KeyCode.I)) {
					GameController.Instance.GridC [0].gameObject.transform.Rotate (0f, -90f, 0f);
					OnLeftRotationFirstGrid ();
				}
				if (Input.GetKeyDown (KeyCode.P)) {
					GameController.Instance.GridC [0].gameObject.transform.Rotate (0f, 90f, 0f);
					OnRightRotationFirstGrid();
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
		if (GameController.Instance.CurrentPlayerTurn == PlayerTurn.TurnPlayer1) {
			if (this == GameController.Instance.GridC [0]) {
				if (_x < -1 || _y < -1)
					return false;
				if (_x > xPoint - 1 || _y > yPoint - 1)
					return false;
			}
		}

		if (GameController.Instance.CurrentPlayerTurn == PlayerTurn.TurnPlayer2) {
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

	/// <summary>
	/// Prende il tassello con l'asse x e y selezionata.
	/// </summary>
	/// <returns>Il tassello.</returns>
	/// <param name="_x">Coordinata x.</param>
	/// <param name="_y">Coordinata y.</param>
	public GameObject GetTile (int _x, int _y) {
		foreach (CellData cell in cells) {
			if (cell.X == _x && cell.Y == _y) {
				thisTile = cell.Tile;
			}
		}
		return thisTile;
	}

	private void OnRightRotationFirstGrid () {
		foreach (PawnData pawn in GameController.Instance.SpawnC[0].pawns) {
			if (pawn.X == -1 && pawn.Y == -1) {
				pawn.X += 0;
				pawn.Y += 2;
			}
			else if (pawn.X == -1 && pawn.Y == 1) {
				pawn.X += 2;
				pawn.Y += 0;
			}
			else if (pawn.X == 1 && pawn.Y == 1) {
				pawn.X += 0;
				pawn.Y += -2;
			}
			else if (pawn.X == 1 && pawn.Y == -1) {
				pawn.X += -2;
				pawn.Y += 0;
			}
			if (pawn.X == -1 && pawn.Y == 0) {
				pawn.X += 1;
				pawn.Y += 1;
			}
			else if (pawn.X == 0 && pawn.Y == 1) {
				pawn.X += 1;
				pawn.Y += -1;
			}
			else if (pawn.X == 1 && pawn.Y == 0) {
				pawn.X += -1;
				pawn.Y += -1;
			}
			else if (pawn.X == 0 && pawn.Y == -1) {
				pawn.X += -1;
				pawn.Y += 1;
			}
		}
	}

	private void OnRightRotationSecondGrid () {
		foreach (PawnData pawn in GameController.Instance.SpawnC[1].pawns) {
			if (pawn.X == -1 && pawn.Y == 3) {
				pawn.X += 0;
				pawn.Y += 2;
			}
			else if (pawn.X == -1 && pawn.Y == 5) {
				pawn.X += 2;
				pawn.Y += 0;
			}
			else if (pawn.X == 1 && pawn.Y == 5) {
				pawn.X += 0;
				pawn.Y += -2;
			}
			else if (pawn.X == 1 && pawn.Y == 3) {
				pawn.X += -2;
				pawn.Y += 0;
			}
			if (pawn.X == -1 && pawn.Y == 4) {
				pawn.X += 1;
				pawn.Y += 1;
			}
			else if (pawn.X == 0 && pawn.Y == 5) {
				pawn.X += 1;
				pawn.Y += -1;
			}
			else if (pawn.X == 1 && pawn.Y == 4) {
				pawn.X += -1;
				pawn.Y += -1;
			}
			else if (pawn.X == 0 && pawn.Y == 3) {
				pawn.X += -1;
				pawn.Y += 1;
			}
		}
	}

	private void OnLeftRotationFirstGrid () {
		foreach (PawnData pawn in GameController.Instance.SpawnC[0].pawns) {
			if (pawn.X == -1 && pawn.Y == -1) {
				pawn.X += 2;
				pawn.Y += 0;
			}
			else if (pawn.X == -1 && pawn.Y == 1) {
				pawn.X += 0;
				pawn.Y += -2;
			}
			else if (pawn.X == 1 && pawn.Y == 1) {
				pawn.X += -2;
				pawn.Y += 0;
			}
			else if (pawn.X == 1 && pawn.Y == -1) {
				pawn.X += 0;
				pawn.Y += 2;
			}
			if (pawn.X == -1 && pawn.Y == 0) {
				pawn.X += 1;
				pawn.Y += -1;
			}
			else if (pawn.X == 0 && pawn.Y == 1) {
				pawn.X += -1;
				pawn.Y += -1;
			}
			else if (pawn.X == 1 && pawn.Y == 0) {
				pawn.X += -1;
				pawn.Y += 1;
			}
			else if (pawn.X == 0 && pawn.Y == -1) {
				pawn.X += 1;
				pawn.Y += 1;
			}
		}
	}

	private void OnLeftRotationSecondGrid () {
		foreach (PawnData pawn in GameController.Instance.SpawnC[1].pawns) {
			if (pawn.X == -1 && pawn.Y == 3) {
				pawn.X += 2;
				pawn.Y += 0;
			}
			else if (pawn.X == -1 && pawn.Y == 5) {
				pawn.X += 0;
				pawn.Y += -2;
			}
			else if (pawn.X == 1 && pawn.Y == 5) {
				pawn.X += -2;
				pawn.Y += 0;
			}
			else if (pawn.X == 1 && pawn.Y == 3) {
				pawn.X += 0;
				pawn.Y += 2;
			}
			if (pawn.X == -1 && pawn.Y == 4) {
				pawn.X += 1;
				pawn.Y += -1;
			}
			else if (pawn.X == 0 && pawn.Y == 5) {
				pawn.X += -1;
				pawn.Y += -1;
			}
			else if (pawn.X == 1 && pawn.Y == 4) {
				pawn.X += -1;
				pawn.Y += 1;
			}
			else if (pawn.X == 0 && pawn.Y == 3) {
				pawn.X += 1;
				pawn.Y += 1;
			}
		}
	}

	#endregion
}