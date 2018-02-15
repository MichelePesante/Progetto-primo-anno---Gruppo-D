using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	/// <summary>
	/// Riferimento alla classe 'GameController'.
	/// </summary>
	public static GameController Instance;

	public SpawnController SpawnC;

	public SpawnController2 SpawnC2;

	public GridController GridC;

	public GridController2 GridC2;

	public int scorep1;

	public int scorep2;

	/// <summary>
	/// Energia in dotazione al player 1.
	/// </summary>
	public int EnergyPlayer1;

	/// <summary>
	/// Energia in dotazione al player 2.
	/// </summary>
	public int EnergyPlayer2;

	/// <summary>
	/// Energia che si decide di utilizzare.
	/// </summary>
	public int EnergyToSpend;

	/// <summary>
	/// Specificazione di chi è il turno.
	/// </summary>
	public PlayerTurn CurrentPlayerTurn;

	void Awake () {
		// Se non esiste un'istanza di questo script.
		if (Instance == null) {
			// Crea l'istanza.
			Instance = this;
		} 
		// Alrimenti.
		else {
			// Distruggi l'oggetto.
			Destroy (gameObject);
		}
	}

	void Start () {
		// Inizializzazione variabili.
		EnergyPlayer1 = 7;
		EnergyPlayer2 = 7;
		EnergyToSpend = 0;
		scorep1 = 0;
		scorep2 = 0;
		CurrentPlayerTurn = PlayerTurn.TurnPlayer1;

		// Riferimento al GameController.
		Instance = this;
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			CurrentPlayerTurn = PlayerTurn.TurnPlayer1;
			print ("Turno del giocatore 1");
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			CurrentPlayerTurn = PlayerTurn.TurnPlayer2;
			print ("Turno del giocatore 2");
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			Battle ();
		}

        #region Vecchie meccaniche

        /*
		 
		// Aumento di energia da spendere.
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (EnergyToSpend < 4) {
				EnergyToSpend++;
				//print (EnergyToSpend);
			}
		}

		// Diminuzione di energia da spendere.
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (EnergyToSpend > 0) {
				EnergyToSpend--;
				//print (EnergyToSpend);
			}
		}

		*/

        #endregion

        #region rotazione

        if (GameController.Instance.CurrentPlayerTurn == PlayerTurn.TurnPlayer1) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                GridC.gameObject.transform.Rotate(0f, -90f, 0f);
                foreach (var pawn in SpawnC.pawns) {
                    int[] newPos = GridC.RotatePosition(false, pawn.X, pawn.Y);
                    int x = newPos[0];
                    int y = newPos[1];
                    pawn.X = x;
                    pawn.Y = y;
                }
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                GridC.gameObject.transform.Rotate(0f, 90f, 0f);
            }
        }

        if (GameController.Instance.CurrentPlayerTurn == PlayerTurn.TurnPlayer1) {
            if (Input.GetKeyDown(KeyCode.I)) {
                GridC2.gameObject.transform.Rotate(0f, -90f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.P)) {
                GridC2.gameObject.transform.Rotate(0f, 90f, 0f);
            }
        }

        #endregion
    }

	private void Battle () {
		int battleResult1 = 0;
		int battleResult2 = 0;
		int battleResult3 = 0;

		int ForzaPedina1p1 = 0;
		int ForzaPedina2p1 = 0;
		int ForzaPedina3p1 = 0;
		int ForzaPedina1p2 = 0;
		int ForzaPedina2p2 = 0;
		int ForzaPedina3p2 = 0;

		foreach (PawnData pawn in SpawnC.pawns) {
			if (pawn.X == -1 && pawn.Y == 1) {
				ForzaPedina1p1 = pawn.Strength;
				print ("Forza pedina 1 player 1:   " + ForzaPedina1p1);
			}
			if (pawn.X == 0 && pawn.Y == 1) {
				ForzaPedina2p1 = pawn.Strength;
				print ("Forza pedina 2 player 1:   " + ForzaPedina2p1);
			}
			if (pawn.X == 1 && pawn.Y == 1) {
				ForzaPedina3p1 = pawn.Strength;
				print ("Forza pedina 3 player 1:   " + ForzaPedina3p1);
			}
		}

		foreach (PawnData pawn in SpawnC2.pawns) {
			if (pawn.X == -1 && pawn.Y == 3) {
				ForzaPedina1p2 = pawn.Strength;
				print ("Forza pedina 1 player 2:   " + ForzaPedina1p2);
			}
			if (pawn.X == 0 && pawn.Y == 3) {
				ForzaPedina2p2 = pawn.Strength;
				print ("Forza pedina 2 player 2:   " + ForzaPedina2p2);
			}
			if (pawn.X == 1 && pawn.Y == 3) {
				ForzaPedina3p2 = pawn.Strength;
				print ("Forza pedina 3 player 2:   " + ForzaPedina3p2);
			}
		}

		battleResult1 = ForzaPedina1p1 - ForzaPedina1p2;
		if (battleResult1 > 0) {
			scorep1 += 1;
		}
		if (battleResult1 < 0) {
			scorep2 += 1;
		}
		battleResult2 = ForzaPedina2p1 - ForzaPedina2p2;
		if (battleResult2 > 0) {
			scorep1 += 1;
		}
		if (battleResult2 < 0) {
			scorep2 += 1;
		}
		battleResult3 = ForzaPedina3p1 - ForzaPedina3p2;
		if (battleResult3 > 0) {
			scorep1 += 1;
		}
		if (battleResult3 < 0) {
			scorep2 += 1;
		}
	}
}

public enum PlayerTurn
{
	TurnPlayer1, TurnPlayer2
}