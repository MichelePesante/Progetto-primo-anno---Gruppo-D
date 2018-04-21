using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	public void GoToRotationTurn () {
		TurnManager.Instance.CurrentTurnState = TurnManager.TurnState.rotation;
	}

	public void GoToUpgradeTurn () {
		TurnManager.Instance.CurrentTurnState = TurnManager.TurnState.upgrade;
	}

	public void DoBattle () {
		RobotManager.Instance.Battle ();
	}
}
