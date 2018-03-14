using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnScript : MonoBehaviour {

	public void P1Turn () {
		StateMachine.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer1;
	}
	public void P2Turn() {
		StateMachine.CurrentPlayerTurn = StateMachine.PlayerTurn.TurnPlayer2;
	}
}
