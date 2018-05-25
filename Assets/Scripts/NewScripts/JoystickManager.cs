using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JoystickManager : MonoBehaviour {

	public static JoystickManager Instance;

	public bool hasMyGridAlreadyBeenRotated;
	public bool hasEnemyGridAlreadyBeenRotated; 

	private ButtonManager bm;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (this.gameObject);
		}
	}

	void Start () {
		bm = ButtonManager.Instance;
	}

	void Update () {
		if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.rotation && GameMenu.GameIsPaused == false) {
			if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Curve_Turn) {
				if (Input.GetAxis ("MyGridRotation_Curve") < 0 && !hasMyGridAlreadyBeenRotated) {
					bm.CurveGridClockwiseRotation ();
					bm.RotationCheck ();
					hasMyGridAlreadyBeenRotated = true;
				}
				else if (Input.GetAxis ("MyGridRotation_Curve") > 0 && !hasMyGridAlreadyBeenRotated) {
					bm.CurveGridCounterclockwiseRotation ();
					bm.RotationCheck ();
					hasMyGridAlreadyBeenRotated = true;
				}
				if (Input.GetAxis ("EnemyGridClockwiseRotation_Curve") > 0 && !hasEnemyGridAlreadyBeenRotated) {
					bm.QuadGridClockwiseRotation ();
					bm.RotationCheck ();
					hasEnemyGridAlreadyBeenRotated = true;
				}
				else if (Input.GetAxis ("EnemyGridCounterclockwiseRotation_Curve") > 0 && !hasEnemyGridAlreadyBeenRotated) {
					bm.QuadGridCounterclockwiseRotation ();
					bm.RotationCheck ();
					hasEnemyGridAlreadyBeenRotated = true;
				}
			}
			else if (TurnManager.Instance.CurrentPlayerTurn == TurnManager.PlayerTurn.Quad_Turn) {
				if (Input.GetAxis ("MyGridRotation_Quad") < 0 && !hasMyGridAlreadyBeenRotated) {
					bm.QuadGridClockwiseRotation ();
					bm.RotationCheck ();
					hasMyGridAlreadyBeenRotated = true;
				}
				else if (Input.GetAxis ("MyGridRotation_Quad") > 0 && !hasMyGridAlreadyBeenRotated) {
					bm.QuadGridCounterclockwiseRotation ();
					bm.RotationCheck ();
					hasMyGridAlreadyBeenRotated = true;
				}
				if (Input.GetAxis ("EnemyGridClockwiseRotation_Quad") > 0 && !hasEnemyGridAlreadyBeenRotated) {
					bm.CurveGridClockwiseRotation ();
					bm.RotationCheck ();
					hasEnemyGridAlreadyBeenRotated = true;
				} 
				else if (Input.GetAxis ("EnemyGridCounterclockwiseRotation_Quad") > 0 && !hasEnemyGridAlreadyBeenRotated) {
					bm.CurveGridCounterclockwiseRotation ();
					bm.RotationCheck ();
					hasEnemyGridAlreadyBeenRotated = true;
				}
			}
		}
	}
}