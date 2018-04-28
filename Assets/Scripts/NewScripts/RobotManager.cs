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

	[Header ("Carte")]
	public List <Image> CarteRobotCurvi;
	public List <Image> CarteRobotQuadrati;
	public List <Image> CarteRobotCurviInHand;
	public List <Image> CarteRobotQuadratiInHand;
	public List <Image> BackupCarteRobotCurviInHand;
	public List <Image> BackupCarteRobotQuadratiInHand;

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
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing && GameMenu.GameIsPaused == false) {
			RobotPositioning (RobotCurviInHand, CarteRobotCurvi, RobotsCurviInHand);
			PlayRobot (RobotCurviInHand, RobotCurviGiocati);
			SwitchRobotToPlay (CarteRobotCurviInHand, standardPositionsCurvi, standardScalesCurvi, highlightedPositionsCurvi, highlightedScalesCurvi, RobotsCurviInHand);
		}
		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn && TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing && GameMenu.GameIsPaused == false) {
			RobotPositioning (RobotQuadratiInHand, CarteRobotQuadrati, RobotsQuadratiInHand);
			PlayRobot (RobotQuadratiInHand, RobotQuadratiGiocati);
			SwitchRobotToPlay (CarteRobotQuadratiInHand, standardPositionsQuadrati, standardScalesQuadrati, highlightedPositionsQuadrati, highlightedScalesQuadrati, RobotsQuadratiInHand);
		}
		if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade && GameMenu.GameIsPaused == false) {
			if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn)
				SwitchRobotToPlay (CarteRobotCurviInHand, standardPositionsCurvi, standardScalesCurvi, highlightedPositionsCurvi, highlightedScalesCurvi, RobotsCurviInHand);
			if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn)
				SwitchRobotToPlay (CarteRobotQuadratiInHand, standardPositionsQuadrati, standardScalesQuadrati, highlightedPositionsQuadrati, highlightedScalesQuadrati, RobotsQuadratiInHand);
		}
		SetCardsValue ();
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

	//public void AddRemovedCards (List <Image> _listToAddTo) {
	//	if (_listToAddTo == CarteRobotCurviInHand) {
	//		CarteRobotCurviInHand = new List<Image> (4);
	//	}
	//		
	//	if (_listToAddTo == CarteRobotQuadratiInHand) {
	//		CarteRobotQuadratiInHand = new List<Image> (4);
	//	}
	//}

	public void SetCardsInHand (List <Image> _listToCheck) {
		if (_listToCheck == CarteRobotCurviInHand) {
			for (int i = 0; i < CarteRobotCurviInHand.Count; i++) {
				CarteRobotCurviInHand [i].GetComponent<Image>().sprite = CarteRobotCurvi [i].GetComponent<Image>().sprite;
				//SetHighlightedCards (_listToCheck);
			}
		}

		if (_listToCheck == CarteRobotQuadratiInHand) {
			for (int i = 0; i < CarteRobotQuadratiInHand.Count; i++) {
				CarteRobotQuadratiInHand [i].GetComponent<Image>().sprite = CarteRobotQuadrati [i].GetComponent<Image>().sprite;
				//SetHighlightedCards (_listToCheck);
			}
		}
	}

	public void SetHighlightedCards (List <Image> _listToCheck) {
		if (_listToCheck == CarteRobotCurviInHand) {
			for (int i = 0; i < CarteRobotCurviInHand.Count; i++) {
				if (CarteRobotCurviInHand [robotToPlay].transform.position == standardPositionsCurvi [robotToPlay]) {
					switch (RobotCurvi [i].ID) {
					case 11:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_1_1_Highlighted;
						break;
					case 12:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_1_2_Highlighted;
						break;
					case 13:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_1_3_Highlighted;
						break;
					case 14:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_1_4_Highlighted;
						break;
					case 21:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_2_1_Highlighted;
						break;
					case 22:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_2_2_Highlighted;
						break;
					case 23:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_2_3_Highlighted;
						break;
					case 24:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_2_4_Highlighted;
						break;
					case 31:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_3_1_Highlighted;
						break;
					case 32:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_3_2_Highlighted;
						break;
					case 33:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_3_3_Highlighted;
						break;
					case 34:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_3_4_Highlighted;
						break;
					case 41:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_4_1_Highlighted;
						break;
					case 42:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_4_2_Highlighted;
						break;
					case 43:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_4_3_Highlighted;
						break;
					case 44:
						CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_4_4_Highlighted;
						break;
					default:
						break;
					}
				}
			}
		}
		if (_listToCheck == CarteRobotQuadratiInHand) {
			for (int i = 0; i < CarteRobotQuadratiInHand.Count; i++) {
				if (CarteRobotQuadratiInHand [robotToPlay].transform.position == standardPositionsQuadrati [robotToPlay]) {
					switch (RobotQuadrati [i].ID) {
					case 11:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_1_1_Highlighted;
						break;
					case 12:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_1_2_Highlighted;
						break;
					case 13:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_1_3_Highlighted;
						break;
					case 14:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_1_4_Highlighted;
						break;
					case 21:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_2_1_Highlighted;
						break;
					case 22:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_2_2_Highlighted;
						break;
					case 23:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_2_3_Highlighted;
						break;
					case 24:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_2_4_Highlighted;
						break;
					case 31:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_3_1_Highlighted;
						break;
					case 32:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_3_2_Highlighted;
						break;
					case 33:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_3_3_Highlighted;
						break;
					case 34:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_3_4_Highlighted;
						break;
					case 41:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_4_1_Highlighted;
						break;
					case 42:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_4_2_Highlighted;
						break;
					case 43:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_4_3_Highlighted;
						break;
					case 44:
						CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_4_4_Highlighted;
						break;
					default:
						break;
					}
				}
			}
		}
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

		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn) {
			if (Physics.Raycast (_ray, out _hit) && Input.GetMouseButtonDown (0)) {
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>() == null) {
					return;
				}
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y >= 0 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y <= 2 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().IsPlaceable) {
					_listToFill.Add (_listToPlayRobotFrom [robotToPlay]);
					_listToPlayRobotFrom [robotToPlay].transform.position = _hit.collider.gameObject.GetComponentInChildren<ColliderController>().WorldPosition + new Vector3 (0f, 1.5f, 0f);
					_listToPlayRobotFrom [robotToPlay].transform.SetParent (_hit.transform);
					_listToPlayRobotFrom [robotToPlay].SetPosition ();
					CarteRobotCurvi [robotToPlay].gameObject.SetActive (false);
					CarteRobotCurvi.Remove (CarteRobotCurvi [robotToPlay]);
					//CarteRobotCurviInHand [robotToPlay].gameObject.SetActive (false);
					//CarteRobotCurviInHand.Remove (CarteRobotCurviInHand [robotToPlay]);
					RemoveRobotFromList (_listToPlayRobotFrom, robotToPlay);
					RobotPlayed++;
					RobotsCurviInHand--;
					_hit.collider.gameObject.GetComponentInChildren<ColliderController> ().IsPlaceable = false;
					robotToPlay = 0;
					currentTurn++;
					if (firstTurnPassed == false)
						firstTurnPassed = true;
				}
			}
		}

		if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn) {
			if (Physics.Raycast (_ray, out _hit) && Input.GetMouseButtonDown (0)) {
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>() == null) {
					return;
				}
				if (_hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y >= 4 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().Y <= 6 && _hit.collider.gameObject.GetComponentInChildren<ColliderController>().IsPlaceable) {
					_listToFill.Add (_listToPlayRobotFrom [robotToPlay]);
					_listToPlayRobotFrom [robotToPlay].transform.position = _hit.collider.gameObject.GetComponentInChildren<ColliderController>().WorldPosition + new Vector3 (0f, 1.5f, 0f);
					_listToPlayRobotFrom [robotToPlay].transform.SetParent (_hit.transform);
					_listToPlayRobotFrom [robotToPlay].SetPosition ();
					CarteRobotQuadrati [robotToPlay].gameObject.SetActive (false);
					CarteRobotQuadrati.Remove (CarteRobotQuadrati [robotToPlay]);
					//CarteRobotQuadratiInHand [robotToPlay].gameObject.SetActive (false);
					//CarteRobotQuadratiInHand.Remove (CarteRobotQuadratiInHand [robotToPlay]);
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
	public void SwitchRobotToPlay (List <Image> _cardToHighlight, Vector3[] _standardPositions, Vector3[] _standardScale, Vector3[] _highlightedPositions, Vector3[] _highlightedScale, int _robotsInHand) {
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && robotToPlay < _robotsInHand - 1) {
			robotToPlay++;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && robotToPlay > 0) {
			robotToPlay--;
		}

		CardPositionReset (_cardToHighlight);

		_cardToHighlight [robotToPlay].transform.position = _highlightedPositions [robotToPlay];
		_cardToHighlight [robotToPlay].transform.localScale = _highlightedScale [robotToPlay];

		#region Shitty script

		/*
		switch (robotToPlay) {
		case 0:
			_cardToHighlight [0].transform.position = _highlightedPositions [0];
			_cardToHighlight [0].transform.localScale = _highlightedScale [0];
			_cardToHighlight [1].transform.position = _standardPositions [1];
			_cardToHighlight [1].transform.localScale = _standardScale [1];
			//if (_positionToHighlight.Count == 3)
				_cardToHighlight [2].transform.position = _standardPositions [2];
				_cardToHighlight [2].transform.localScale = _standardScale [2];
			//if (_positionToHighlight.Count == 4)
				_cardToHighlight [3].transform.position = _standardPositions [3];
				_cardToHighlight [3].transform.localScale = _standardScale [3];
			break;
		case 1:
			_cardToHighlight [0].transform.position = _standardPositions [0];
			_cardToHighlight [0].transform.localScale = _standardScale [0];
			_cardToHighlight [1].transform.position = _highlightedPositions [1];
			_cardToHighlight [1].transform.localScale = _highlightedScale [1];
			//if (_positionToHighlight.Count == 3)
				_cardToHighlight [2].transform.position = _standardPositions [2];
				_cardToHighlight [2].transform.localScale = _standardScale [2];
			//if (_positionToHighlight.Count == 4)
				_cardToHighlight [3].transform.position = _standardPositions [3];
				_cardToHighlight [3].transform.localScale = _standardScale [3];
			break;
		case 2:
			_cardToHighlight [0].transform.position = _standardPositions [0];
			_cardToHighlight [0].transform.localScale = _standardScale [0];
			_cardToHighlight [1].transform.position = _standardPositions [1];
			_cardToHighlight [1].transform.localScale = _standardScale [1];
			_cardToHighlight [2].transform.position = _highlightedPositions [2];
			_cardToHighlight [2].transform.localScale = _highlightedScale [2];
			//if (_positionToHighlight.Count == 4)
				_cardToHighlight [3].transform.position = _standardPositions [3];
				_cardToHighlight [3].transform.localScale = _standardScale [3];
			break;										
		case 3:
			_cardToHighlight [0].transform.position = _standardPositions [0];
			_cardToHighlight [0].transform.localScale = _standardScale [0];
			_cardToHighlight [1].transform.position = _standardPositions [1];
			_cardToHighlight [1].transform.localScale = _standardScale [1];
			_cardToHighlight [2].transform.position = _standardPositions [2];
			_cardToHighlight [2].transform.localScale = _standardScale [2];
			_cardToHighlight [3].transform.position = _highlightedPositions [3];
			_cardToHighlight [3].transform.localScale = _highlightedScale [3];
			break;
		default:
			break;
		}
		*/

		#endregion
	}

	public void SetPositions (List <Image> _positionToSet) {
		if (_positionToSet == CarteRobotCurviInHand) {
			for (int i = 0; i < standardPositionsCurvi.Length; i++) {
				standardPositionsCurvi [i] = _positionToSet [i].transform.position;
				standardScalesCurvi [i] = _positionToSet [i].transform.localScale;
				highlightedPositionsCurvi [i] = standardPositionsCurvi [i] + Vector3.right * 150f;
				highlightedScalesCurvi [i] = standardScalesCurvi [i] + new Vector3 (1f, 1f, 1f);
			}
		}

		if (_positionToSet == CarteRobotQuadratiInHand) {
			for (int i = 0; i < standardPositionsQuadrati.Length; i++) {
				standardPositionsQuadrati [i] = _positionToSet [i].transform.position;
				standardScalesQuadrati [i] = _positionToSet [i].transform.localScale;
				highlightedPositionsQuadrati [i] = standardPositionsQuadrati [i] + Vector3.left * 150f;
				highlightedScalesQuadrati [i] = standardScalesQuadrati [i] + new Vector3 (1f, 1f, 1f);
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

	#region Battle

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

	#region CardImage

	public void SetCardImage (List <RobotController> _listToCheck) {
		if (_listToCheck == RobotCurviInHand) {
			for (int i = 0; i < CarteRobotCurvi.Count; i++) {
				switch (RobotCurvi [i].ID) {
				case 11:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_1;
					break;
				case 12:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_1;
					break;
				case 13:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_1;
					break;
				case 14:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_1;
					break;
				case 21:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_2;
					break;
				case 22:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_2;
					break;
				case 23:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_2;
					break;
				case 24:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_2;
					break;
				case 31:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_3;
					break;
				case 32:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_3;
					break;
				case 33:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_3;
					break;
				case 34:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_3;
					break;
				case 41:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_4;
					break;
				case 42:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_4;
					break;
				case 43:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_4;
					break;
				case 44:
					CarteRobotCurvi [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Curve_4;
					break;
				default:
					break;
				}
			}
		}

		if (_listToCheck == RobotQuadratiInHand) {
			for (int i = 0; i <CarteRobotQuadrati.Count; i++) {
				switch (RobotQuadrati [i].ID) {
				case 11:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_1;
					break;
				case 12:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_1;
					break;
				case 13:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_1;
					break;
				case 14:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_1;
					break;
				case 21:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_2;
					break;
				case 22:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_2;
					break;
				case 23:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_2;
					break;
				case 24:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_2;
					break;
				case 31:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_3;
					break;
				case 32:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_3;
					break;
				case 33:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_3;
					break;
				case 34:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_3;
					break;
				case 41:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_4;
					break;
				case 42:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_4;
					break;
				case 43:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_4;
					break;
				case 44:
					CarteRobotQuadrati [i].GetComponent<Image> ().sprite = SpriteManager.Instance.Quad_4;
					break;
				default:
					break;
				}
			}
		}
	}

	#endregion

	public void RemoveRobotFromList (List<RobotController> _listToRemoveRobotFrom, int _indexRobotToRemove) {
		_listToRemoveRobotFrom.Remove (_listToRemoveRobotFrom [_indexRobotToRemove]);
	}

	public void SetCardsValue () {
		if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.placing) {
			for (int i = 0; i < RobotsCurviInHand; i++) {
				CarteRobotCurviInHand [i].GetComponentInChildren<TextMeshProUGUI> ().text = RobotCurviInHand [i].strength.ToString();
			}
			for (int i = 0; i < RobotsQuadratiInHand; i++) {
				CarteRobotQuadratiInHand [i].GetComponentInChildren<TextMeshProUGUI> ().text = RobotQuadratiInHand [i].strength.ToString();
			}
		}
		if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade) {
			for (int i = 0; i < RobotsCurviInHand; i++) {
				CarteRobotCurviInHand [i].GetComponentInChildren<TextMeshProUGUI> ().text = RobotCurviInHand [i].upgrade.ToString();
			}
			for (int i = 0; i < RobotsQuadratiInHand; i++) {
				CarteRobotQuadratiInHand [i].GetComponentInChildren<TextMeshProUGUI> ().text = RobotQuadratiInHand [i].upgrade.ToString();
			}
		}
	}

	public void CardPositionReset (List <Image> _cardsToReset) {
		if (_cardsToReset == CarteRobotCurviInHand) {
			for (int i = 0; i < 4; i++) {
				_cardsToReset [i].transform.position = standardPositionsCurvi [i];
				_cardsToReset [i].transform.localScale = standardScalesCurvi [i];
			}
		}

		if (_cardsToReset == CarteRobotQuadratiInHand) {
			for (int i = 0; i < 4; i++) {
				_cardsToReset [i].transform.position = standardPositionsQuadrati [i];
				_cardsToReset [i].transform.localScale = standardScalesQuadrati [i];
			}
		}
	}

	#endregion

	private void RobotPositioning (List<RobotController> _listToPositionRobotFrom, List <Image> _robotPositions, int _robotsInHand) {
		for (int i = 0; i < _robotsInHand; i++) {
			_listToPositionRobotFrom [i].transform.position = _robotPositions[i].transform.position;
		}
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