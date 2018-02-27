using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnScript : MonoBehaviour {

	public void P1Turn () {
		GameController.Instance.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer1;
	}
	public void P2Turn() {
		GameController.Instance.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer2;
	}
}
