using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	/// <summary>
	/// Pedina base.
	/// </summary>
	public GameObject BasePawn;

	/// <summary>
	/// Pedina avanzata.
	/// </summary>
	public GameObject AdvancedPawn;

	/// <summary>
	/// Coordinata x attuale;
	/// </summary>
	private int xCoordinate;

	/// <summary>
	/// Coordinata y attuale.
	/// </summary>
	private int yCoordinate;

	/// <summary>
	/// Coordinata x precedente;
	/// </summary>
	private int lastXCoordinate;

	/// <summary>
	/// Coordinata y precedente.
	/// </summary>
	private int lastYCoordinate;

	/// <summary>
	/// Lista di tutte le pedine.
	/// </summary>
	public List<PawnData> pawns = new List<PawnData> ();

	/// <summary>
	/// Indice delle carte in mano.
	/// </summary>
	private int cardSelector;

	public GameObject Tile;

	public GameObject Pawn;

	void Start () {
		// Inizializzazione variabili.
		xCoordinate = -1;
		cardSelector = 0;

		if (this == GameController.Instance.SpawnC [0]) {
			yCoordinate = -1;
		}

		if (this == GameController.Instance.SpawnC [1]) {
			yCoordinate = 3;
		}
	}

	void Update () {

		#region Input

		// Controllo quale SpawnController sto usando.
		if (this == GameController.Instance.SpawnC[0]) {
			// Turno del player 1.
			if (GameController.Instance.CurrentPlayerTurn == PlayerTurn.TurnPlayer1) {

				// Movimento in avanti.
				if (Input.GetKeyDown (KeyCode.W)) {
					lastXCoordinate = xCoordinate;
					lastYCoordinate = yCoordinate;
					yCoordinate++;
					SpawnMovement (GameController.Instance.GridC[0]);
				}

				// Movimento indietro.
				if (Input.GetKeyDown (KeyCode.S)) {
					lastXCoordinate = xCoordinate;
					lastYCoordinate = yCoordinate;
					yCoordinate--;
					SpawnMovement (GameController.Instance.GridC[0]);
				}

				// Movimento a destra.
				if (Input.GetKeyDown (KeyCode.D)) {
					lastXCoordinate = xCoordinate;
					lastYCoordinate = yCoordinate;
					xCoordinate++;
					SpawnMovement (GameController.Instance.GridC[0]);
				}

				// Movimento a sinistra.
				if (Input.GetKeyDown (KeyCode.A)) {
					lastXCoordinate = xCoordinate;
					lastYCoordinate = yCoordinate;
					xCoordinate--;
					SpawnMovement (GameController.Instance.GridC[0]);
				}

				// Spawn della pedina base.
				if (Input.GetKeyDown (KeyCode.Space)) {
					if (GameController.Instance.Hand[0].cardsInHand > 0) {

						if (PawnCheck (xCoordinate, yCoordinate) == false) {
							PawnUpgrade (GameController.Instance.Hand[0].cards[cardSelector].Value, xCoordinate, yCoordinate, GameController.Instance.Hand[0], GameController.Instance.GridC[0], Color.red);
						}

						if (PawnCheck (xCoordinate, yCoordinate) == true) {
							PawnPositioning (GameController.Instance.GridC[0], GameController.Instance.Hand[0], Color.red);
						}

					}
					else {
						CustomLogger.Log ("Non ho carte da giocare");
					}
				}

				if (Input.GetKeyDown (KeyCode.RightArrow)) {
					if (cardSelector < GameController.Instance.Hand[0].cardsInHand - 1) {
						cardSelector++;
						CustomLogger.Log ("Carta numero: " + (cardSelector + 1));
					}
				}

				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					if (cardSelector > 0) {
						cardSelector--;
						CustomLogger.Log ("Carta numero: " + (cardSelector + 1));
					}
				}

				if (Input.GetKeyDown(KeyCode.Tab)) {
					if (PawnCheck (xCoordinate, yCoordinate) == false) {
						foreach (PawnData pawn in pawns) {
							if (pawn.X == xCoordinate && pawn.Y == yCoordinate && pawn.Team == Color.red) {
								CustomLogger.Log ("Questa pedina attualmente ha valore {0}", pawn.Strength);
							}
						}
					}
				}
			}
		}

		// Controllo quale SpawnController sto usando.
		if (this == GameController.Instance.SpawnC[1]) {
			// Turno del player 2.
			if (GameController.Instance.CurrentPlayerTurn == PlayerTurn.TurnPlayer2) {

				// Movimento in avanti.
				if (Input.GetKeyDown (KeyCode.W)) {
					lastXCoordinate = xCoordinate;
					lastYCoordinate = yCoordinate;
					yCoordinate++;
					SpawnMovement (GameController.Instance.GridC[1]);
				}

				// Movimento indietro.
				if (Input.GetKeyDown (KeyCode.S)) {
					lastXCoordinate = xCoordinate;
					lastYCoordinate = yCoordinate;
					yCoordinate--;
					SpawnMovement (GameController.Instance.GridC[1]);
				}

				// Movimento a destra.
				if (Input.GetKeyDown (KeyCode.D)) {
					lastXCoordinate = xCoordinate;
					lastYCoordinate = yCoordinate;
					xCoordinate++;
					SpawnMovement (GameController.Instance.GridC[1]);
				}

				// Movimento a sinistra.
				if (Input.GetKeyDown (KeyCode.A)) {
					lastXCoordinate = xCoordinate;
					lastYCoordinate = yCoordinate;
					xCoordinate--;
					SpawnMovement (GameController.Instance.GridC[1]);
				}

				// Spawn della pedina base.
				if (Input.GetKeyDown (KeyCode.Space)) {
					if (GameController.Instance.Hand[1].cardsInHand > 0) {

						if (PawnCheck (xCoordinate, yCoordinate) == false) {
							PawnUpgrade (GameController.Instance.Hand[1].cards[cardSelector].Value, xCoordinate, yCoordinate, GameController.Instance.Hand[1], GameController.Instance.GridC[1], Color.blue);
						}

						if (PawnCheck (xCoordinate, yCoordinate) == true) {
							PawnPositioning (GameController.Instance.GridC[1], GameController.Instance.Hand[1], Color.blue);
						}
					}
					else {
						CustomLogger.Log ("Non ho carte da giocare");
					}
				}

				if (Input.GetKeyDown (KeyCode.RightArrow)) {
					if (cardSelector < GameController.Instance.Hand[1].cardsInHand - 1) {
						cardSelector++;
						CustomLogger.Log ("Carta numero: " + (cardSelector + 1));
					}
				}

				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					if (cardSelector > 0) {
						cardSelector--;
						CustomLogger.Log ("Carta numero: " + (cardSelector + 1));
					}
				}

				if (Input.GetKeyDown(KeyCode.Tab)) {
					if (PawnCheck (xCoordinate, yCoordinate) == false) {
						foreach (PawnData pawn in pawns) {
							if (pawn.X == xCoordinate && pawn.Y == yCoordinate && pawn.Team == Color.blue) {
								CustomLogger.Log ("Questa pedina attualmente ha valore {0}", pawn.Strength);
							}
						}
					}
				}
			}
		}
			
		#region Vecchie meccaniche

			/* Meccanica di energia

			gc.EnergyPlayer1 -= gc.EnergyToSpend;
				gc.EnergyToSpend = 0;

			*/

			/* Vecchia meccanica di spawn di pedine diverse	

			// Spawn della pedina avanzata.
			if (Input.GetKeyDown (KeyCode.L)) {
				if (gc.GridC.cellCheck (xCoordinate, yCoordinate) == true) {
					PawnSpawn (AdvancedPawn, xCoordinate, yCoordinate);
					pawns.Add (new PawnData ("Pedina avanzata", gc.EnergyToSpend, true, Color.red));
					//print (gc.EnergyToSpend);
				}
			}

			*/
			
			/* Meccanica di energia

			gc.EnergyPlayer1 -= gc.EnergyToSpend;
				gc.EnergyToSpend = 0;

			*/

		/*

		// Turno del player 2.
		if (gc.CurrentPlayerTurn == PlayerTurn.TurnPlayer2) {
			// Spawn della pedina base.
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (gc.GridC.cellCheck (xCoordinate, yCoordinate) == true) {
					PawnSpawn (BasePawn, xCoordinate, yCoordinate);
					pawns.Add (new PawnData ("Pedina base", gc.EnergyToSpend, true, Color.blue));
					//print (gc.EnergyToSpend);
					gc.EnergyPlayer2 -= gc.EnergyToSpend;
					gc.EnergyToSpend = 0;
				}
			}

			// Spawn della pedina avanzata.
			if (Input.GetKeyDown (KeyCode.L)) {
				if (gc.GridC.cellCheck (xCoordinate, yCoordinate) == true) {
					PawnSpawn (AdvancedPawn, xCoordinate, yCoordinate);
					pawns.Add (new PawnData ("Pedina avanzata", gc.EnergyToSpend, true, Color.blue));
					//print (gc.EnergyToSpend);
					gc.EnergyPlayer2 -= gc.EnergyToSpend;
					gc.EnergyToSpend = 0;
				}
			}
		}

		*/

		#endregion

		#endregion
	}

	public void PawnPositioning (GridController _ownGrid, Hand _ownHand, Color _ownColor) {
			
		if (_ownHand.cards [cardSelector].Value == 1 || _ownHand.cards [cardSelector].Value == 2) {
			Pawn = PawnSpawn (BasePawn, _ownGrid);
			pawns.Add (new PawnData (xCoordinate, yCoordinate, "Pedina base", _ownHand.cards [cardSelector].Value, true, _ownColor));
			CustomLogger.Log ("Ho posizionato la pedina {0} che vale {1}", _ownHand.cards [cardSelector].Name, _ownHand.cards [cardSelector].Value);
		}
			
		if (_ownHand.cards [cardSelector].Value == 3 || _ownHand.cards [cardSelector].Value == 4) {
			Pawn = PawnSpawn (AdvancedPawn, _ownGrid);
			pawns.Add (new PawnData (xCoordinate, yCoordinate, "Pedina avanzata", _ownHand.cards [cardSelector].Value, true, _ownColor));
			CustomLogger.Log ("Ho posizionato la pedina {0} che vale {1}", _ownHand.cards [cardSelector].Name, _ownHand.cards [cardSelector].Value);
		}

		_ownHand.RemoveCardFromHand (cardSelector);
			
		SetParentPosition (Tile, Pawn);

		cardSelector = 0;
	}
		

	/// <summary>
	/// Spawn della pedina che disabilita la casella selezionata.
	/// </summary>
	/// <param name="_pawnType">Pedina da spawnare.</param>
	/// <param name="_x">Coordinata x.</param>
	/// <param name="_y">Coordinata Y.</param>
	private GameObject PawnSpawn (GameObject _pawnType, GridController _gridSelect) {
		GameObject thisPawn = Instantiate (_pawnType, new Vector3 (transform.position.x, _gridSelect.Tile.transform.localScale.y, transform.position.z), transform.rotation);
		return thisPawn;
	}

	/// <summary>
	/// Potenziamento della forza pedina.
	/// </summary>
	/// <param name="_strengthToAdd">Forza da aggiungere.</param>
	/// <param name="_x">Posizione x.</param>
	/// <param name="_y">Posizione y.</param>
	/// <param name="_handToRemoveFrom">Mano dalla quale rimuovere la carta.</param>
	private void PawnUpgrade (int _strengthToAdd, int _x, int _y, Hand _handToRemoveFrom, GridController _gridSelect, Color _ownColor) {

		foreach (PawnData pawn in pawns) {
			if (pawn.X == _x && pawn.Y == _y && pawn.Team == _ownColor) {
				CustomLogger.Log ("Ho usato la carta {0} che vale {1} per potenziare una pedina", _handToRemoveFrom.cards[cardSelector].Name, _handToRemoveFrom.cards[cardSelector].Value);
				_handToRemoveFrom.RemoveCardFromHand (cardSelector);
				pawn.Strength += _strengthToAdd;
			}
		}
	}

	/// <summary>
	/// Movimento dello SpawnController.
	/// </summary>
	private void SpawnMovement (GridController _gridSelect) {
		if (_gridSelect.positionCheck (xCoordinate, yCoordinate)) {
			transform.position = _gridSelect.GetWorldPosition (xCoordinate, yCoordinate) + Vector3.up * 0.2f;
		} 
		else {
			xCoordinate = lastXCoordinate;
			yCoordinate = lastYCoordinate;
		}
	}

	public bool PawnCheck (int _x, int _y) {
		foreach (PawnData pawn in pawns) {
			if (pawn.X == _x && pawn.Y == _y)
				return false;
		}
		return true;
	}

	private void SetParentPosition (GameObject _newParent, GameObject _child) {
		_child.transform.parent = _newParent.transform;
	}

	public int[] RotatePosition (bool _isRight, int _x, int _y) {
		int[] coordinates = new int[2];
		int x;
		int y;
		x = _x;
		y = _y;
		if (this == GameController.Instance.SpawnC [0])
		if (_isRight) {
			
		} 
		else {
			if (_x < 0 && _y < 0) {
				x *= -1;
				y *= 1;
			} else if (_x < 0 && _y > 0) {
				x *= 1;
				y *= -1;
			} else if (_x > 0 && _y > 0) {
				x *= -1;
				y *= 1;
			} else if (_x > 0 && _y < 0) {
				x *= 1;
				y *= -1;
			}
		}
		coordinates [0] = x;
		coordinates [1] = y;
		return coordinates;
	}
}