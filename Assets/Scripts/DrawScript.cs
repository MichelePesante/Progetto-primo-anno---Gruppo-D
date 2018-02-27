using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour {

	public void Draw () {
		if (GameController.Instance.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer1)
			GameController.Instance.Hand[0].Draw (GameController.Instance.Hand[0].cardsInHand);
		if (GameController.Instance.CurrentPlayerTurn == StateMachine.PlayerTurn.TurnPlayer2)
			GameController.Instance.Hand[1].Draw (GameController.Instance.Hand[1].cardsInHand);
	}
}
