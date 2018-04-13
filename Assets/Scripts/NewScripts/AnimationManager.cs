using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	public void GoToRotationTurn () {
		TurnManager.Instance.CurrentTurnState = TurnManager.TurnState.rotation;
		TurnManager.Instance.ChangeTurn ();
	}

	public void DoBattle () {
		RobotManager.Instance.Battle ();
	}
}
