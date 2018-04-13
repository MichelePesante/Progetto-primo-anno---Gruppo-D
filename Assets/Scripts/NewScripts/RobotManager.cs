using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour {

	public static RobotManager Instance;

	public int RobotPlayed;
	public int MaxRobotToPlay = 2;
	public int RobotsCurviInHand;
	public int RobotsQuadratiInHand;

	[Header ("Liste Robot")]
	public List<RobotController> RobotCurvi;
	public List<RobotController> RobotQuadrati;
	public List<RobotController> RobotCurviGiocati;
	public List<RobotController> RobotQuadratiGiocati;
	public List<RobotController> RobotCurviInHand;
	public List<RobotController> RobotQuadratiInHand;

	[Header ("Posizioni in mano Robot")]
	public GameObject[] PosizioniRobotCurvi;
	public GameObject[] PosizioniRobotQuadrati;

	private int currentFirstRobotCurvo;
	private int currentFirstRobotQuadrato;
	private int robotToPlay;
	private int maxRobotsInHand = 4;
	private int currentTurn;
	private int maxPreparationTurns = 16;
	private Vector3[] standardPositionsCurvi = new Vector3[4];
	private Vector3[] standardPositionsQuadrati = new Vector3[4];
	private Vector3[] highlightedPositionsCurvi = new Vector3[4];
	private Vector3[] highlightedPositionsQuadrati = new Vector3[4];

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.placing && GameMenu.GameIsPaused == false) {
			SwitchRobotToPlay (PosizioniRobotCurvi, standardPositionsCurvi, highlightedPositionsCurvi, RobotsCurviInHand);
			PlayRobot (RobotCurviInHand, RobotCurviGiocati);
			RobotPositioning (RobotCurviInHand, PosizioniRobotCurvi, RobotsCurviInHand);
		}
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn && FindObjectOfType<TurnManager> ().CurrentTurnState == TurnManager.TurnState.placing && GameMenu.GameIsPaused == false) {
			SwitchRobotToPlay (PosizioniRobotQuadrati, standardPositionsQuadrati, highlightedPositionsQuadrati, RobotsQuadratiInHand);
			PlayRobot (RobotQuadratiInHand, RobotQuadratiGiocati);
			RobotPositioning (RobotQuadratiInHand, PosizioniRobotQuadrati, RobotsQuadratiInHand);
		}
		SwitchPlacingTurn ();
		EndPreparationPhase ();
	}


	#region API

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

	public int Draw (List<RobotController> _listTofill, List<RobotController> _listToDrawFrom, int _robotsInHand) {
		int cardPosition = 0;
		if (_robotsInHand < maxRobotsInHand) {
			for (int i = _robotsInHand; i < maxRobotsInHand && _listToDrawFrom.Count > 0; i++) {
				_listTofill.Add (_listToDrawFrom [cardPosition]);
				RemoveRobotFromList (_listToDrawFrom, cardPosition);
				_robotsInHand = i + 1;
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

		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn) {
			if (Physics.Raycast (_ray, out _hit) && Input.GetMouseButtonDown (0)) {
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>() == null) {
					return;
				}
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y >= 0 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y <= 2 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().IsPlaceable) {
					_listToFill.Add (_listToPlayRobotFrom [robotToPlay]);
					_listToPlayRobotFrom [robotToPlay].transform.position = _hit.collider.gameObject.GetComponentInChildren<ColliderController>().WorldPosition + new Vector3 (0f, 1.5f, 0f);
					_listToPlayRobotFrom [robotToPlay].transform.SetParent (_hit.transform);
					_listToPlayRobotFrom [robotToPlay].SetPosition ();
					RemoveRobotFromList (_listToPlayRobotFrom, robotToPlay);
					RobotPlayed++;
					RobotsCurviInHand--;
					_hit.collider.gameObject.GetComponentInChildren<ColliderController> ().IsPlaceable = false;
					robotToPlay = 0;
					currentTurn++;
				}
			}
		}

		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn) {
			if (Physics.Raycast (_ray, out _hit) && Input.GetMouseButtonDown (0)) {
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>() == null) {
					return;
				}
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y >= 4 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y <= 6 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().IsPlaceable) {
					_listToFill.Add (_listToPlayRobotFrom [robotToPlay]);
					_listToPlayRobotFrom [robotToPlay].transform.position = _hit.collider.gameObject.GetComponentInChildren<ColliderController>().WorldPosition + new Vector3 (0f, 1.5f, 0f);
					_listToPlayRobotFrom [robotToPlay].transform.SetParent (_hit.transform);
					_listToPlayRobotFrom [robotToPlay].SetPosition ();
					RemoveRobotFromList (_listToPlayRobotFrom, robotToPlay);
					RobotPlayed++;
					RobotsQuadratiInHand--;
					_hit.collider.gameObject.GetComponentInChildren<ColliderController> ().IsPlaceable = false;
					robotToPlay = 0;
					currentTurn++;
				}
			}
		}
	}
		
	/// <summary>
	/// Funzione che permette di cambiare il focus da un robot ad un altro tramite rotellina.
	/// </summary>
	public void SwitchRobotToPlay (GameObject[] _positionToHighlight, Vector3[] _standardPositions, Vector3[] _highlightedPositions, int _robotsInHand) {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && robotToPlay < _robotsInHand - 1) {
			robotToPlay++;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && robotToPlay > 0) {
			robotToPlay--;
		}

		switch (robotToPlay) {
		case 0:
			_positionToHighlight [0].transform.position = _highlightedPositions [0];
			_positionToHighlight [1].transform.position = _standardPositions [1];
			_positionToHighlight [2].transform.position = _standardPositions [2];
			_positionToHighlight [3].transform.position = _standardPositions [3];
			break;
		case 1:
			_positionToHighlight [0].transform.position = _standardPositions [0];
			_positionToHighlight [1].transform.position = _highlightedPositions [1];
			_positionToHighlight [2].transform.position = _standardPositions [2];
			_positionToHighlight [3].transform.position = _standardPositions [3];
			break;
		case 2:
			_positionToHighlight [0].transform.position = _standardPositions [0];
			_positionToHighlight [1].transform.position = _standardPositions [1];
			_positionToHighlight [2].transform.position = _highlightedPositions [2];
			_positionToHighlight [3].transform.position = _standardPositions [3];
			break;										
		case 3:										   
			_positionToHighlight [0].transform.position = _standardPositions [0];
			_positionToHighlight [1].transform.position = _standardPositions [1];
			_positionToHighlight [2].transform.position = _standardPositions [2];
			_positionToHighlight [3].transform.position = _highlightedPositions [3];
			break;
		default:
			break;
		}
	}

	public void SetPositions (GameObject[] _positionToSet) {
		if (_positionToSet == PosizioniRobotCurvi) {
			for (int i = 0; i < standardPositionsCurvi.Length; i++) {
				standardPositionsCurvi [i] = _positionToSet [i].transform.position;
				highlightedPositionsCurvi [i] = standardPositionsCurvi [i] + Vector3.up;
			}
		}

		if (_positionToSet == PosizioniRobotQuadrati) {
			for (int i = 0; i < standardPositionsQuadrati.Length; i++) {
				standardPositionsQuadrati [i] = _positionToSet [i].transform.position;
				highlightedPositionsQuadrati [i] = standardPositionsQuadrati [i] + Vector3.up;
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

	#region RobotRotation

	public void OnClockwiseRotationCurveGrid () {
		foreach (RobotController robot in RobotCurviGiocati) {
			if (robot.X == 0 && robot.Y == 0) {
				robot.X += 0;
				robot.Y += 2;
			} else if (robot.X == 0 && robot.Y == 2) {
				robot.X += 2;
				robot.Y += 0;
			} else if (robot.X == 2 && robot.Y == 2) {
				robot.X += 0;
				robot.Y += -2;
			} else if (robot.X == 2 && robot.Y == 0) {
				robot.X += -2;
				robot.Y += 0;
			}
			if (robot.X == 0 && robot.Y == 1) {
				robot.X += 1;
				robot.Y += 1;
			} else if (robot.X == 1 && robot.Y == 2) {
				robot.X += 1;
				robot.Y += -1;
			} else if (robot.X == 2 && robot.Y == 1) {
				robot.X += -1;
				robot.Y += -1;
			} else if (robot.X == 1 && robot.Y == 0) {
				robot.X += -1;
				robot.Y += 1;
			}
		}
	}

	public void OnCounterclockwiseRotationCurveGrid () {
		foreach (RobotController robot in RobotCurviGiocati) {
			if (robot.X == 0 && robot.Y == 0) {
				robot.X += 2;
				robot.Y += 0;
			} else if (robot.X == 0 && robot.Y == 2) {
				robot.X += 0;
				robot.Y += -2;
			} else if (robot.X == 2 && robot.Y == 2) {
				robot.X += -2;
				robot.Y += 0;
			} else if (robot.X == 2 && robot.Y == 0) {
				robot.X += 0;
				robot.Y += 2;
			}
			if (robot.X == 0 && robot.Y == 1) {
				robot.X += 1;
				robot.Y += -1;
			} else if (robot.X == 1 && robot.Y == 2) {
				robot.X += -1;
				robot.Y += -1;
			} else if (robot.X == 2 && robot.Y == 1) {
				robot.X += -1;
				robot.Y += 1;
			} else if (robot.X == 1 && robot.Y == 0) {
				robot.X += 1;
				robot.Y += 1;
			}
		}
	}

	public void OnClockwiseRotationQuadGrid () {
		foreach (RobotController robot in RobotQuadratiGiocati) {
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

	public void Battle () {
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
				ForzaPedina1p1 = robot.GetStrength();
				CustomLogger.Log ("Forza pedina 1 player 1:   " + ForzaPedina1p1);
			}
			if (robot.X == 1 && robot.Y == 2) {
				ForzaPedina2p1 = robot.GetStrength();
				CustomLogger.Log ("Forza pedina 2 player 1:   " + ForzaPedina2p1);
			}
			if (robot.X == 2 && robot.Y == 2) {
				ForzaPedina3p1 = robot.GetStrength();
				CustomLogger.Log ("Forza pedina 3 player 1:   " + ForzaPedina3p1);
			}
		}

		foreach (RobotController robot in RobotQuadratiGiocati) {
			if (robot.X == 0 && robot.Y == 4) {
				ForzaPedina1p2 = robot.GetStrength();
				CustomLogger.Log ("Forza pedina 1 player 2:   " + ForzaPedina1p2);
			}
			if (robot.X == 1 && robot.Y == 4) {
				ForzaPedina2p2 = robot.GetStrength();
				CustomLogger.Log ("Forza pedina 2 player 2:   " + ForzaPedina2p2);
			}
			if (robot.X == 2 && robot.Y == 4) {
				ForzaPedina3p2 = robot.GetStrength();
				CustomLogger.Log ("Forza pedina 3 player 2:   " + ForzaPedina3p2);
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
			FindObjectOfType<TurnManager>().ScoreP1 += finalScore;
		}
		if (scoretemp1 < scoretemp2) {
			finalScore = scoretemp2 - scoretemp1;
			FindObjectOfType<TurnManager>().ScoreP2 += finalScore;
		}
	}

	#endregion

	private void RobotPositioning (List<RobotController> _listToPositionRobotFrom, GameObject[] _robotPositions, int _robotsInHand) {
		for (int i = 0; i < _robotsInHand; i++) {
			_listToPositionRobotFrom [i].transform.position = _robotPositions[i].transform.position;
		}
	}

	private void RemoveRobotFromList (List<RobotController> _listToRemoveRobotFrom, int _indexRobotToRemove) {
		_listToRemoveRobotFrom.Remove (_listToRemoveRobotFrom [_indexRobotToRemove]);
	}

	private void SwitchPlacingTurn () {
		if (RobotPlayed == MaxRobotToPlay) {
			FindObjectOfType<TurnManager> ().ChangeTurn ();
			RobotPlayed = 0;
		}
	}

	private void EndPreparationPhase () {
		if (currentTurn == maxPreparationTurns)
			FindObjectOfType<TurnManager> ().CurrentMacroPhase = TurnManager.MacroPhase.Game;
	}
}