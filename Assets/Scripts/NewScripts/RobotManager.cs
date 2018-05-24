using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotManager : MonoBehaviour {

	public static RobotManager Instance;

	public int RobotPlayed;
	public int MaxRobotToPlay = 2;
	public int maxRobotsInHand = 4;
	public int RobotsCurviInHand;
	public int RobotsQuadratiInHand;
	public bool firstTurnPassed;
	public int robotToPlay;
	public int robotUpgraded;

	[Header ("Liste Robot")]
	public List<RobotController> RobotCurvi;
	public List<RobotController> RobotQuadrati;
	public List<RobotController> RobotCurviGiocati;
	public List<RobotController> RobotQuadratiGiocati;
	public List<RobotController> RobotCurviInHand;
	public List<RobotController> RobotQuadratiInHand;

	public int[,] AbilityCurveValues = new int[3, 3];
	public int[,] AbilityQuadValues = new int[3, 3];

	private int currentFirstRobotCurvo;
	private int currentFirstRobotQuadrato;
	private int currentTurn;
	private int maxPreparationTurns = 16;
	private Vector3[] standardPositionsCurvi = new Vector3[4];
	private Vector3[] standardPositionsQuadrati = new Vector3[4];
	private Vector3[] highlightedPositionsCurvi = new Vector3[4];
	private Vector3[] highlightedPositionsQuadrati = new Vector3[4];
	private Vector3[] standardScalesCurvi = new Vector3[4];
	private Vector3[] standardScalesQuadrati = new Vector3[4];
	private Vector3[] highlightedScalesCurvi = new Vector3[4];
	private Vector3[] highlightedScalesQuadrati = new Vector3[4];

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ChangeRobotToPlay ();
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn && GameMenu.GameIsPaused == false) {
			if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing) {
				PlayRobot (RobotCurviInHand, RobotCurviGiocati);
				CardManager.Instance.HighlightCard (Player.Player_Curve, robotToPlay);
			}
			else if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade) {
				CardManager.Instance.HighlightCard (Player.Player_Curve, robotToPlay);
			}
		}
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn && GameMenu.GameIsPaused == false) {
			if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing) {
				PlayRobot (RobotQuadratiInHand, RobotQuadratiGiocati);
				CardManager.Instance.HighlightCard (Player.Player_Quad, robotToPlay);
			}
			else if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade) {
				CardManager.Instance.HighlightCard (Player.Player_Quad, robotToPlay);
			}
		}
		SetAbilityValues (Player.Player_Curve);
		SetAbilityValues (Player.Player_Quad);
		CalculateStrength ();
		SwitchPlacingTurn ();
		EndPreparationPhase ();
	}


	#region API

	public int GetCurrentIndex () {
		return robotToPlay;
	}

	public void Shuffle (List<RobotController> _listToShuffle) {
		// Variabile temporanea.
		RobotController temporaryRobot;

		// Per un numero casuale di volte.
		for (int i = 0; i < Random.Range (1, 20); i++) {
			// Per tutta la lunghezza della lista.
			for (int c = 0; c < _listToShuffle.Count; c++) {
				// Posizione presa casualmente.
				int randomCard = Random.Range (0, _listToShuffle.Count - 1);
				// Elemento 'i' viene assegnato all'elemento temporaneo.
				temporaryRobot = _listToShuffle[c];
				// Posizione casuale viene assegnata all'elemento 'i'.
				_listToShuffle [c] = _listToShuffle [randomCard];
				// Elemento temporaneo viene assegnato alla posizione casuale.
				_listToShuffle [randomCard] = temporaryRobot;
			}
		}
	}

	public int Draw (List<RobotController> _listToFill, List<RobotController> _listToDrawFrom, int _robotsInHand, Player player) {
		int cardPosition = 0;
		CardManager cm = FindObjectOfType<CardManager> ();
		if (_robotsInHand < maxRobotsInHand) {
			for (int i = _robotsInHand; i < maxRobotsInHand && _listToDrawFrom.Count > 0; i++) {
				_listToFill.Add (_listToDrawFrom [cardPosition]);
				RemoveRobotFromList (_listToDrawFrom, cardPosition);
				_robotsInHand = i + 1;
			}
		}
		if (player == Player.Player_Curve) {
			for (int i = 0; i < cm.CurveCards.Count; i++) {
				cm.CurveCards [i].SetData (_listToFill [i].Data);
				cm.CurveCards [i].hasBeenPlaced = false;
			}
		}
		else if (player == Player.Player_Quad) {
			for (int i = 0; i < cm.QuadCards.Count; i++) {
				cm.QuadCards [i].SetData (_listToFill [i].Data);
				cm.QuadCards [i].hasBeenPlaced = false;
			}
		}
		return _robotsInHand;
	}

	/// <summary>
	/// Funzione che permette di piazzare un robot.
	/// </summary>
	/// <param name="_listToPlayRobotFrom">List to play robot from.</param>
	/// <param name="_cardToClick">Card to click.</param>
	public void PlayRobot (List<RobotController> _listToPlayRobotFrom, List<RobotController> _listToFill) {
		Camera _camera = FindObjectOfType<Camera>();

		Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit _hit;

		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn) {
			if (Physics.Raycast (_ray, out _hit) && Input.GetMouseButtonDown (0)) {
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>() == null) {
					return;
				}
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y >= 0 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y <= 2 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().IsPlaceable) {
					_listToFill.Add (_listToPlayRobotFrom [robotToPlay]);
					_listToPlayRobotFrom [robotToPlay].transform.position = _hit.collider.gameObject.GetComponentInChildren<ColliderController> ().WorldPosition + new Vector3 (0f, 0.3f, 0f);
					_listToPlayRobotFrom [robotToPlay].transform.SetParent (_hit.transform);
					_listToPlayRobotFrom [robotToPlay].SetPosition ();
					RemoveRobotFromList (_listToPlayRobotFrom, robotToPlay);
					RobotPlayed++;
					RobotsCurviInHand--;
					_hit.collider.gameObject.GetComponentInChildren<ColliderController> ().IsPlaceable = false;
					FindObjectOfType<CardManager> ().PlaceCard (Player.Player_Curve, robotToPlay);
					robotToPlay = 0;
					currentTurn++;
					if (firstTurnPassed == false)
						firstTurnPassed = true;
				}
			}
		}

		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn) {
			if (Physics.Raycast (_ray, out _hit) && Input.GetMouseButtonDown (0)) {
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>() == null) {
					return;
				}
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y >= 4 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y <= 6 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().IsPlaceable) {
					_listToFill.Add (_listToPlayRobotFrom [robotToPlay]);
					_listToPlayRobotFrom [robotToPlay].transform.position = _hit.collider.gameObject.GetComponentInChildren<ColliderController>().WorldPosition + new Vector3 (0f, 0.3f, 0f);
					_listToPlayRobotFrom [robotToPlay].transform.SetParent (_hit.transform);
					_listToPlayRobotFrom [robotToPlay].SetPosition ();
					RemoveRobotFromList (_listToPlayRobotFrom, robotToPlay);
					RobotPlayed++;
					RobotsQuadratiInHand--;
					_hit.collider.gameObject.GetComponentInChildren<ColliderController> ().IsPlaceable = false;
					FindObjectOfType<CardManager> ().PlaceCard (Player.Player_Quad, robotToPlay);
					robotToPlay = 0;
					currentTurn++;
				}
			}
		}
	}

	public void SetGraphicAsParent () {
		foreach (RobotController robot in RobotCurviGiocati) {
			robot.transform.SetParent (FindObjectOfType<NewGridController> ().CurveTilesContainer.transform);
		}
		foreach (RobotController robot in RobotQuadratiGiocati) {
			robot.transform.SetParent (FindObjectOfType<NewGridController> ().QuadTilesContainer.transform);
		}
	}

	public void ChangeRobotToPlay () {
		CardManager cm = FindObjectOfType<CardManager> ();
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f && robotToPlay > 0) {
			robotToPlay--;
		} 
		else if (Input.GetAxis ("Mouse ScrollWheel") < 0f) {
			if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn && robotToPlay < RobotsCurviInHand - 1) {
				robotToPlay++;
			} 
			else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn && robotToPlay < RobotsQuadratiInHand - 1) {
				robotToPlay++;
			}
		}
	}

	#region RobotRotation

	public void OnClockwiseRotationCurveGrid () {
		foreach (RobotController robot in RobotCurviGiocati) {
			robot.RotateRobotMatrix (1);
			if (robot.X == 0 && robot.Y == 0) {
				robot.X += 0;
				robot.Y += 2;
			} 
			else if (robot.X == 0 && robot.Y == 2) {
				robot.X += 2;
				robot.Y += 0;
			} 
			else if (robot.X == 2 && robot.Y == 2) {
				robot.X += 0;
				robot.Y += -2;
			} 
			else if (robot.X == 2 && robot.Y == 0) {
				robot.X += -2;
				robot.Y += 0;
			}
			if (robot.X == 0 && robot.Y == 1) {
				robot.X += 1;
				robot.Y += 1;
			} 
			else if (robot.X == 1 && robot.Y == 2) {
				robot.X += 1;
				robot.Y += -1;
			} 
			else if (robot.X == 2 && robot.Y == 1) {
				robot.X += -1;
				robot.Y += -1;
			} 
			else if (robot.X == 1 && robot.Y == 0) {
				robot.X += -1;
				robot.Y += 1;
			}
		}
	}

	public void OnCounterclockwiseRotationCurveGrid () {
		foreach (RobotController robot in RobotCurviGiocati) {
			robot.RotateRobotMatrix (-1);
			if (robot.X == 0 && robot.Y == 0) {
				robot.X += 2;
				robot.Y += 0;
			} 
			else if (robot.X == 0 && robot.Y == 2) {
				robot.X += 0;
				robot.Y += -2;
			} 
			else if (robot.X == 2 && robot.Y == 2) {
				robot.X += -2;
				robot.Y += 0;
			} 
			else if (robot.X == 2 && robot.Y == 0) {
				robot.X += 0;
				robot.Y += 2;
			}
			if (robot.X == 0 && robot.Y == 1) {
				robot.X += 1;
				robot.Y += -1;
			} 
			else if (robot.X == 1 && robot.Y == 2) {
				robot.X += -1;
				robot.Y += -1;
			} 
			else if (robot.X == 2 && robot.Y == 1) {
				robot.X += -1;
				robot.Y += 1;
			} 
			else if (robot.X == 1 && robot.Y == 0) {
				robot.X += 1;
				robot.Y += 1;
			}
		}
	}

	public void OnClockwiseRotationQuadGrid () {
		foreach (RobotController robot in RobotQuadratiGiocati) {
			robot.RotateRobotMatrix (1);
			if (robot.X == 0 && robot.Y == 4) {
				robot.X += 0;
				robot.Y += 2;
			} else if (robot.X == 0 && robot.Y == 6) {
				robot.X += 2;
				robot.Y += 0;
			} else if (robot.X == 2 && robot.Y == 6) {
				robot.X += 0;
				robot.Y += -2;
			} else if (robot.X == 2 && robot.Y == 4) {
				robot.X += -2;
				robot.Y += 0;
			}
			if (robot.X == 0 && robot.Y == 5) {
				robot.X += 1;
				robot.Y += 1;
			} else if (robot.X == 1 && robot.Y == 6) {
				robot.X += 1;
				robot.Y += -1;
			} else if (robot.X == 2 && robot.Y == 5) {
				robot.X += -1;
				robot.Y += -1;
			} else if (robot.X == 1 && robot.Y == 4) {
				robot.X += -1;
				robot.Y += 1;
			}
		}
	}

	public void OnCounterclockwiseRotationQuadGrid () {
		foreach (RobotController robot in RobotQuadratiGiocati) {
			robot.RotateRobotMatrix (-1);
			if (robot.X == 0 && robot.Y == 4) {
				robot.X += 2;
				robot.Y += 0;
			} else if (robot.X == 0 && robot.Y == 6) {
				robot.X += 0;
				robot.Y += -2;
			} else if (robot.X == 2 && robot.Y == 6) {
				robot.X += -2;
				robot.Y += 0;
			} else if (robot.X == 2 && robot.Y == 4) {
				robot.X += 0;
				robot.Y += 2;
			}
			if (robot.X == 0 && robot.Y == 5) {
				robot.X += 1;
				robot.Y += -1;
			} else if (robot.X == 1 && robot.Y == 6) {
				robot.X += -1;
				robot.Y += -1;
			} else if (robot.X == 2 && robot.Y == 5) {
				robot.X += -1;
				robot.Y += 1;
			} else if (robot.X == 1 && robot.Y == 4) {
				robot.X += 1;
				robot.Y += 1;
			}
		}
	}

	#endregion

	#region Battle

	public void FinalBattle () {
		int battleResult1 = 0;
		int battleResult2 = 0;
		int battleResult3 = 0;

		int scoretemp1 = 0;
		int scoretemp2 = 0;
		int finalScore = 0;

		int ForzaPedina1p1 = 0;
		int ForzaPedina2p1 = 0;
		int ForzaPedina3p1 = 0;
		int ForzaPedina1p2 = 0;
		int ForzaPedina2p2 = 0;
		int ForzaPedina3p2 = 0;

		foreach (RobotController robot in RobotCurviGiocati) {
			if (robot.X == 0 && robot.Y == 2) {
				ForzaPedina1p1 = robot.strength;
			}
			if (robot.X == 1 && robot.Y == 2) {
				ForzaPedina2p1 = robot.strength;
			}
			if (robot.X == 2 && robot.Y == 2) {
				ForzaPedina3p1 = robot.strength;
			}
		}

		foreach (RobotController robot in RobotQuadratiGiocati) {
			if (robot.X == 0 && robot.Y == 4) {
				ForzaPedina1p2 = robot.strength;
			}
			if (robot.X == 1 && robot.Y == 4) {
				ForzaPedina2p2 = robot.strength;
			}
			if (robot.X == 2 && robot.Y == 4) {
				ForzaPedina3p2 = robot.strength;
			}
		}

		battleResult1 = ForzaPedina1p1 - ForzaPedina1p2;
		if (battleResult1 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult1 < 0) {
			scoretemp2 += 1;
		}
		battleResult2 = ForzaPedina2p1 - ForzaPedina2p2;
		if (battleResult2 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult2 < 0) {
			scoretemp2 += 1;
		}
		battleResult3 = ForzaPedina3p1 - ForzaPedina3p2;
		if (battleResult3 > 0) {
			scoretemp1 += 1;
		}
		if (battleResult3 < 0) {
			scoretemp2 += 1;
		}
		if (scoretemp1 > scoretemp2) {
			finalScore = scoretemp1 - scoretemp2;
			FindObjectOfType<TurnManager>().ScoreCurve += finalScore;
		}
		if (scoretemp1 < scoretemp2) {
			finalScore = scoretemp2 - scoretemp1;
			FindObjectOfType<TurnManager>().ScoreQuad += finalScore;
		}
	}

	public int FirstBattle () {
		Animator curveRobotAnimator = null;
		Animator quadRobotAnimator = null;

		int battleResult1 = 0;
		int scoretemp = 0;

		int ForzaPedina1p1 = 0;
		int ForzaPedina1p2 = 0;

		foreach (RobotController robot in RobotCurviGiocati) {
			if (robot.X == 0 && robot.Y == 2) {
				curveRobotAnimator = robot.GetComponentInChildren<Animator>();
				ForzaPedina1p1 = robot.strength;
			}
		}

		foreach (RobotController robot in RobotQuadratiGiocati) {
			if (robot.X == 0 && robot.Y == 4) {
				quadRobotAnimator = robot.GetComponentInChildren<Animator>();
				ForzaPedina1p2 = robot.strength;
			}
		}

		battleResult1 = ForzaPedina1p1 - ForzaPedina1p2;

		if (battleResult1 > 0) {
			scoretemp = 1;
			curveRobotAnimator.Play ("Attack");
			quadRobotAnimator.Play ("Hitted");
			return scoretemp;
		}

		if (battleResult1 < 0) {
			scoretemp = 1;
			quadRobotAnimator.Play ("Attack");
			curveRobotAnimator.Play ("Hitted");
			return scoretemp;
		}

		return 0;
	}

	//public int SecondBattle () {
	//
	//}
	//
	//public int ThirdBattle () {
	//
	//}

	#endregion

	public void RemoveRobotFromList (List<RobotController> _listToRemoveRobotFrom, int _indexRobotToRemove) {
		_listToRemoveRobotFrom.Remove (_listToRemoveRobotFrom [_indexRobotToRemove]);
	}

	public RobotController GetRobotController (Player player, int _x, int _y) {
		
		if (player == Player.Player_Curve) {
			_y = 2 - _y;
			foreach (RobotController robot in RobotCurviGiocati) {
				if (robot.X == _x && robot.Y == _y) {
					return robot;
				}
			}
			return null;
		}
		else if (player == Player.Player_Quad) {
			_y = 4 + _y;
			_x = 2 - _x;
			foreach (RobotController robot in RobotQuadratiGiocati) {
				if (robot.X == _x && robot.Y== _y) {
					return robot;
				}
			}
			return null;
		}
		else {
			return null;
		}
	}

	#region AbilityValues

	public void SetAbilityValues (Player player) {
		if (player == Player.Player_Curve) {
			RobotController[,] CurveRobots = new RobotController[3, 3];
			for (int main_row = 0; main_row < AbilityCurveValues.GetLength (0); main_row++) {
				for (int main_column = 0; main_column < AbilityCurveValues.GetLength (1); main_column++) {
					CurveRobots [main_row, main_column] = GetRobotController (player, main_row, main_column);
					if (CurveRobots [main_row, main_column] != null) {
						CurveRobots [main_row, main_column].SetAbilityCheckToFalse ();
					}
					AbilityCurveValues [main_row, main_column] = 0;
				}
			}

			for (int main_row = 0; main_row < AbilityCurveValues.GetLength(0); main_row++) {																			// scorro le righe della matrice principale
				for (int main_column = 0; main_column < AbilityCurveValues.GetLength(1); main_column++) {																//   scorro le colonne della matrice principale
					if (CurveRobots [main_row, main_column] != null) {																									//     controllo che sia presente un robot nella casella attuale della matrice principale
						for (int sub_row = -1; sub_row <= 1; sub_row++) {																								//       scorro le righe della matrice delle abilità del robot
							for (int sub_column = -1; sub_column <= 1; sub_column++) {																					//         scorro le colonne della matrice delle abilità del robot
								if (CheckIndex(main_row + sub_row, main_column + sub_column)) {																			//           controllo che gli indici ottenuti sommando gli indici della matrice principale e della matrice delle abilità siano all interno della matrice principale
									if (CurveRobots [main_row + sub_row, main_column + sub_column] != null) {															//             controllo che l'elemento adiacente esista
										if (CurveRobots [main_row, main_column].AbilityCheck [sub_row+1, sub_column+1] != true 
											&& CurveRobots [main_row, main_column].Abilities [sub_row+1, sub_column+1] != 0) {											//               controllo che la mia abilità su quel lato non sia gia stata calcolata precedentemente da un altro oggetto
											int value = CurveRobots [main_row, main_column].Abilities [sub_row+1, sub_column+1];										//                 salvo in "value" il valore di incremento
											CurveRobots [main_row, main_column].AbilityCheck [sub_row+1, sub_column+1] = true;											//                 imposto che l'abilità su questo lato è gia stata controllata
											if (CurveRobots [main_row + sub_row, main_column + sub_column].Abilities [1 -sub_row, 1-sub_column] != 0) {					//                 controllo che il valore di incremento del robot adiacente, nella direzione opposta a quella che sto controllando sia diverso da 0 
												value += CurveRobots [main_row + sub_row, main_column + sub_column].Abilities [1-sub_row, 1-sub_column];				//                   incremento "value" con il valore di incremento del robot adiacente, della direzione opposta a quella controllata
												CurveRobots [main_row + sub_row, main_column + sub_column].AbilityCheck [1-sub_row, 1-sub_column] = true;				//                   imposto che l'abilità del robot adiacente, sul lato opposto a quello controllato, è gia stata calcolata
												AbilityCurveValues [main_row, main_column] += value;																	//                   incremento il valore della matrice definitiva all indice del robot adiacente a quello controllato di value
											}
											AbilityCurveValues [main_row + sub_row, main_column + sub_column] += value;													//                 incremento il valore della matrice definitiva all indice del robot controllato di value
										}
									}
								}
							}
						}
					}
				}
			}
		} 
		else if (player == Player.Player_Quad) {
			RobotController[,] QuadRobots = new RobotController[3, 3];
			for (int main_row = 0; main_row < AbilityQuadValues.GetLength (0); main_row++) {
				for (int main_column = 0; main_column < AbilityQuadValues.GetLength (1); main_column++) {
					QuadRobots [main_row, main_column] = GetRobotController (player, main_row, main_column);
					if (QuadRobots [main_row, main_column] != null) {
						QuadRobots [main_row, main_column].SetAbilityCheckToFalse ();
					}
					AbilityQuadValues [main_row, main_column] = 0;
				}
			}

			for (int main_row = 0; main_row < AbilityQuadValues.GetLength(0); main_row++) {																				// scorro le righe della matrice principale
				for (int main_column = 0; main_column < AbilityQuadValues.GetLength(1); main_column++) {																//   scorro le colonne della matrice principale
					if (QuadRobots [main_row, main_column] != null) {																									//     controllo che sia presente un robot nella casella attuale della matrice principale
						for (int sub_row = -1; sub_row <= 1; sub_row++) {																								//       scorro le righe della matrice delle abilità del robot
							for (int sub_column = -1; sub_column <= 1; sub_column++) {																					//         scorro le colonne della matrice delle abilità del robot
								if (CheckIndex(main_row + sub_row, main_column + sub_column)) {																			//           controllo che gli indici ottenuti sommando gli indici della matrice principale e della matrice delle abilità siano all interno della matrice principale
									if (QuadRobots [main_row + sub_row, main_column + sub_column] != null) {															//             controllo che l'elemento adiacente esista
										if (QuadRobots [main_row, main_column].AbilityCheck [sub_row+1, sub_column+1] != true 
											&& QuadRobots [main_row, main_column].Abilities [sub_row+1, sub_column+1] != 0) {											//               controllo che la mia abilità su quel lato non sia gia stata calcolata precedentemente da un altro oggetto
											int value = QuadRobots [main_row, main_column].Abilities [sub_row+1, sub_column+1];										//                 salvo in "value" il valore di incremento
											QuadRobots [main_row, main_column].AbilityCheck [sub_row+1, sub_column+1] = true;											//                 imposto che l'abilità su questo lato è gia stata controllata
											if (QuadRobots [main_row + sub_row, main_column + sub_column].Abilities [1 -sub_row, 1-sub_column] != 0) {					//                 controllo che il valore di incremento del robot adiacente, nella direzione opposta a quella che sto controllando sia diverso da 0 
												value += QuadRobots [main_row + sub_row, main_column + sub_column].Abilities [1-sub_row, 1-sub_column];				//                   incremento "value" con il valore di incremento del robot adiacente, della direzione opposta a quella controllata
												QuadRobots [main_row + sub_row, main_column + sub_column].AbilityCheck [1-sub_row, 1-sub_column] = true;				//                   imposto che l'abilità del robot adiacente, sul lato opposto a quello controllato, è gia stata calcolata
												AbilityQuadValues [main_row, main_column] += value;																	//                   incremento il valore della matrice definitiva all indice del robot adiacente a quello controllato di value
											}
											AbilityQuadValues [main_row + sub_row, main_column + sub_column] += value;													//                 incremento il valore della matrice definitiva all indice del robot controllato di value
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	#endregion    // NON TOCCARE!!!

	public void CalculateStrength () {
		for (int i = 0; i < AbilityCurveValues.GetLength (0); i++) {
			for (int j = 0; j < AbilityCurveValues.GetLength (1); j++) {
				RobotController curveRobotController = GetRobotController (Player.Player_Curve, i, j);
				RobotController quadRobotController = GetRobotController (Player.Player_Quad, i, j);
				if (curveRobotController != null)
					curveRobotController.strength = curveRobotController.OriginalStrength + curveRobotController.UpgradedValue + AbilityCurveValues [i, j];
				if (quadRobotController != null)
					quadRobotController.strength = quadRobotController.OriginalStrength + quadRobotController.UpgradedValue + AbilityQuadValues [i, j];
			} 
		}
	}

	#endregion

	private bool CheckIndex (int row, int column) {
		return row >= 0 && row < AbilityCurveValues.GetLength (0) && column >= 0 && column < AbilityCurveValues.GetLength (1);
	}

	private void SwitchPlacingTurn () {
		if (RobotPlayed == MaxRobotToPlay) {
			TurnManager.Instance.ChangeTurn ();
			RobotPlayed = 0;
		}
	}

	private void EndPreparationPhase () {
		if (currentTurn == maxPreparationTurns) {
			TurnManager.Instance.CurrentMacroPhase = TurnManager.MacroPhase.Game;
		}
	}
}