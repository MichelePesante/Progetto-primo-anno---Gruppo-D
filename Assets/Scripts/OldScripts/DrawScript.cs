using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawScript {

	public static void Draw () {
		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1)
			GameController.Instance.Hand[0].Draw (GameController.Instance.Hand[0].cardsInHand);
		if (StateMachine.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2)
			GameController.Instance.Hand[1].Draw (GameController.Instance.Hand[1].cardsInHand);
	}
}
