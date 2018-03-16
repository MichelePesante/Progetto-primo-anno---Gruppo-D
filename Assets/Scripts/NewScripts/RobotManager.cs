using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour {

	public int RobotPlayed;
	public int MaxRobotToPlay = 2;

	[Header ("Liste Robot")]
	public List<RobotController> RobotCurvi;
	public List<RobotController> RobotQuadrati;
	public List<RobotController> RobotCurviGiocati;
	public List<RobotController> RobotQuadratiGiocati;

	[Header ("Posizioni Robot")]
	public GameObject[] PosizioniRobotCurvi;
	public GameObject[] PosizioniRobotQuadrati;

	private int currentFirstRobotCurvo;
	private int currentFirstRobotQuadrato;
	private int robotToPlay;
	private int maxRobotInHand = 4;
	private Vector3[] standardPositionsCurvi = new Vector3[4];
	private Vector3[] standardPositionsQuadrati = new Vector3[4];
	private Vector3[] highlightedPositionsCurvi = new Vector3[4];
	private Vector3[] highlightedPositionsQuadrati = new Vector3[4];

	// Use this for initialization
	void Start () {
		SetPositions (PosizioniRobotCurvi);
		SetPositions (PosizioniRobotQuadrati);
	}
	
	// Update is called once per frame
	void Update () {
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P1_Turn) {
			SwitchRobotToPlay (PosizioniRobotCurvi, standardPositionsCurvi, highlightedPositionsCurvi);
			PlayRobot (RobotCurvi, RobotCurviGiocati);
			RobotPositioning (RobotCurvi, PosizioniRobotCurvi);
		}
		if (FindObjectOfType<TurnManager> ().CurrentPlayerTurn == TurnManager.PlayerTurn.P2_Turn) {
			SwitchRobotToPlay (PosizioniRobotQuadrati, standardPositionsQuadrati, highlightedPositionsQuadrati);
			PlayRobot (RobotQuadrati, RobotQuadratiGiocati);
			RobotPositioning (RobotQuadrati, PosizioniRobotQuadrati);
		}
		SwitchPlacingTurn ();
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
	#region API

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
					RemoveRobotFromList (_listToPlayRobotFrom);
					RobotPlayed++;
					_hit.collider.gameObject.GetComponentInChildren<ColliderController> ().IsPlaceable = false;
					robotToPlay = 0;
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
					RemoveRobotFromList (_listToPlayRobotFrom);
					RobotPlayed++;
					_hit.collider.gameObject.GetComponentInChildren<ColliderController> ().IsPlaceable = false;
					robotToPlay = 0;
				}
			}
		}
	}
		
	/// <summary>
	/// Funzione che permette di cambiare il focus da un robot ad un altro tramite rotellina.
	/// </summary>
	public void SwitchRobotToPlay (GameObject[] _positionToHighlight, Vector3[] _standardPositions, Vector3[] _highlightedPositions) {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && robotToPlay < maxRobotInHand - 1) {
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

	#endregion

	private void RobotPositioning (List<RobotController> _listToPositionRobotFrom, GameObject[] _robotPositions) {
		for (int i = 0; i < maxRobotInHand; i++) {
			_listToPositionRobotFrom [i].transform.position = _robotPositions[i].transform.position;
		}
	}

	private void RemoveRobotFromList (List<RobotController> _listToRemoveRobotFrom) {
		_listToRemoveRobotFrom.Remove (_listToRemoveRobotFrom [robotToPlay]);
	}

	private void SwitchPlacingTurn () {
		if (RobotPlayed == MaxRobotToPlay) {
			FindObjectOfType<TurnManager> ().ChangeTurn ();
			RobotPlayed = 0;
		}
	}

	private void SetPositions (GameObject[] _positionToSet) {
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
}
