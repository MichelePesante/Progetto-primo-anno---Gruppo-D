﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	/// <summary>
	/// Riferimento alla classe 'GameController'.
	/// </summary>
	private GameController gc;

	/// <summary>
	/// Mano del player 1.
	/// </summary>
	public Hand HandPlayer1;

	/// <summary>
	/// Mano del player 2.
	/// </summary>
	public Hand HandPlayer2;

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

	void Start () {
		// Riferimento al GameController.
		gc = FindObjectOfType <GameController> ();

		// Inizializzazione variabili.
		xCoordinate = -1;
		yCoordinate = -1;
		cardSelector = 0;
	}

	void Update () {

		#region Input

		// Movimento in avanti.
		if (Input.GetKeyDown (KeyCode.W)) {
			lastXCoordinate = xCoordinate;
			lastYCoordinate = yCoordinate;
			yCoordinate++;
			SpawnMovement ();
		}

		// Movimento indietro.
		if (Input.GetKeyDown (KeyCode.S)) {
			lastXCoordinate = xCoordinate;
			lastYCoordinate = yCoordinate;
			yCoordinate--;
			SpawnMovement ();
		}

		// Movimento a destra.
		if (Input.GetKeyDown (KeyCode.D)) {
			lastXCoordinate = xCoordinate;
			lastYCoordinate = yCoordinate;
			xCoordinate++;
			SpawnMovement ();
		}

		// Movimento a sinistra.
		if (Input.GetKeyDown (KeyCode.A)) {
			lastXCoordinate = xCoordinate;
			lastYCoordinate = yCoordinate;
			xCoordinate--;
			SpawnMovement ();
		}

		// Turno del player 1.
		if (gc.CurrentPlayerTurn == PlayerTurn.TurnPlayer1) {
		// Spawn della pedina base.
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (HandPlayer1.cardsInHand > 0) {

					if (gc.GridC.cellCheck (xCoordinate, yCoordinate) == false) {
						foreach (PawnData pawn in pawns) {
							if (pawn.Team == Color.red) {
								PawnUpgrade (HandPlayer1.cards[cardSelector].Value, xCoordinate, yCoordinate, HandPlayer1);
								print (cardSelector);
							}
						}
					}

					if (gc.GridC.cellCheck (xCoordinate, yCoordinate) == true) {
						if (HandPlayer1.cards[cardSelector].Value == 1 || HandPlayer1.cards[cardSelector].Value == 2) {
							PawnSpawn (BasePawn, xCoordinate, yCoordinate);
							pawns.Add (new PawnData (xCoordinate, yCoordinate, "Pedina base", HandPlayer1.cards[cardSelector].Value, true, Color.red));
						}
						if (HandPlayer1.cards[cardSelector].Value == 3 || HandPlayer1.cards[cardSelector].Value == 4) {
							PawnSpawn (AdvancedPawn, xCoordinate, yCoordinate);
							pawns.Add (new PawnData (xCoordinate, yCoordinate, "Pedina avanzata", HandPlayer1.cards[cardSelector].Value, true, Color.red));
						}
						HandPlayer1.RemoveCardFromHand(cardSelector);
						//print (gc.EnergyToSpend);
					}
					cardSelector = 0;
				}
			}

			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				if (cardSelector < HandPlayer1.cardsInHand - 1) {
					cardSelector++;
					print ("Carta numero: " + (cardSelector + 1));
				}
			}

			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				if (cardSelector > 0) {
					cardSelector--;
					print ("Carta numero: " + (cardSelector + 1));
				}
			}
		}

		// Turno del player 2.
		if (gc.CurrentPlayerTurn == PlayerTurn.TurnPlayer2) {
			// Spawn della pedina base.
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (HandPlayer2.cardsInHand > 0) {

					if (gc.GridC.cellCheck (xCoordinate, yCoordinate) == false) {
						foreach (PawnData pawn in pawns) {
							if (pawn.Team == Color.blue) {
								PawnUpgrade (HandPlayer2.cards[cardSelector].Value, xCoordinate, yCoordinate, HandPlayer2);
							}
						}
					}

					if (gc.GridC.cellCheck (xCoordinate, yCoordinate) == true) {
						if (HandPlayer2.cards[cardSelector].Value == 1 || HandPlayer2.cards[cardSelector].Value == 2) {
							PawnSpawn (BasePawn, xCoordinate, yCoordinate);
							pawns.Add (new PawnData (xCoordinate, yCoordinate, "Pedina base", HandPlayer2.cards[cardSelector].Value, true, Color.blue));
						}
						if (HandPlayer2.cards[cardSelector].Value == 3 || HandPlayer2.cards[cardSelector].Value == 4) {
							PawnSpawn (AdvancedPawn, xCoordinate, yCoordinate);
							pawns.Add (new PawnData (xCoordinate, yCoordinate, "Pedina avanzata", HandPlayer2.cards[cardSelector].Value, true, Color.blue));
						}
						HandPlayer2.RemoveCardFromHand(cardSelector);
						//print (gc.EnergyToSpend);
					}
					cardSelector = 0;
				}
			}

			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				if (cardSelector < HandPlayer2.cardsInHand - 1) {
					cardSelector++;
					print ("Carta numero: " + (cardSelector + 1));
				}
			}

			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				if (cardSelector > 0) {
					cardSelector--;
					print ("Carta numero: " + (cardSelector + 1));
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

	/// <summary>
	/// Spawn della pedina che disabilita la casella selezionata.
	/// </summary>
	/// <param name="_pawnType">Pedina da spawnare.</param>
	/// <param name="_x">Coordinata x.</param>
	/// <param name="_y">Coordinata Y.</param>
	private void PawnSpawn (GameObject _pawnType, int _x, int _y) {
		Instantiate (_pawnType, new Vector3 (transform.position.x, gc.GridC.Tile.transform.localScale.y, transform.position.z), transform.rotation);
		foreach (CellData cell in gc.GridC.cells) {
			if (cell.X == _x && cell.Y == _y) {
				cell.Placeable = false;
			}
		}
	}

	/// <summary>
	/// Potenziamento della forza pedina.
	/// </summary>
	/// <param name="_strengthToAdd">Forza da aggiungere.</param>
	/// <param name="_x">Posizione x.</param>
	/// <param name="_y">Posizione y.</param>
	/// <param name="_handToRemoveFrom">Mano dalla quale rimuovere la carta.</param>
	private void PawnUpgrade (int _strengthToAdd, int _x, int _y, Hand _handToRemoveFrom) {
		foreach (CellData cell in gc.GridC.cells) {
			if (cell.X == _x && cell.Y == _y) {
				foreach (PawnData pawn in pawns) {
					if (pawn.X == _x && pawn.Y == _y) {
						_handToRemoveFrom.RemoveCardFromHand (cardSelector);
						pawn.Strength += _strengthToAdd;
					}
				}
			}
		}
	}

	/// <summary>
	/// Movimento dello SpawnController.
	/// </summary>
	private void SpawnMovement () {
		if (gc.GridC.positionCheck (xCoordinate, yCoordinate)) {
			transform.position = gc.GridC.GetWorldPosition (xCoordinate, yCoordinate) + Vector3.up * 0.2f;
		} 
		else {
			xCoordinate = lastXCoordinate;
			yCoordinate = lastYCoordinate;
		}
	}
}