using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Placing_State : GamePlayStateBase
{
    protected override void Tick()
    {
        base.Tick();
        myContext.myRB.ChangeRobotToPlay();
        if (TurnManager.Instance.CurrentPlayerTurn == PlayerTurn.Curve_Turn && GameMenu.GameIsPaused == false)
        {
            if (GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
                myContext.myRB.PlayRobot(myContext.myRB.RobotCurviInHand, myContext.myRB.RobotCurviGiocati);
            myContext.myCM.HighlightCard(Player.Player_Curve, myContext.myRB.robotToPlay);
        }
        //else if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade)
        //{
        //    CardManager.Instance.HighlightCard(Player.Player_Curve, myContext.myRB.robotToPlay);    // DA METTERE NELLO STATO DI UPGRADE
        //}
        if (TurnManager.Instance.CurrentPlayerTurn == PlayerTurn.Quad_Turn && GameMenu.GameIsPaused == false)
        {
            if (GameManager.isSomeAnimationGoing == false && GameManager.isTutorialOn == false)
                myContext.myRB.PlayRobot(myContext.myRB.RobotQuadratiInHand, myContext.myRB.RobotQuadratiGiocati);
            myContext.myCM.HighlightCard(Player.Player_Quad, myContext.myRB.robotToPlay);
        }
        //else if (TurnManager.Instance.CurrentTurnState == TurnManager.TurnState.upgrade)
        //{
        //    CardManager.Instance.HighlightCard(Player.Player_Quad, myContext.myRB.robotToPlay);       // DA METTERE NELLO STATO DI UPGRADE
        //}

        myContext.myRB.SetAbilityValues(Player.Player_Curve);
        myContext.myRB.SetAbilityValues(Player.Player_Quad);
        myContext.myRB.CalculateStrength();
        myContext.myRB.SwitchPlacingTurn();

        if (myContext.myRB.currentTurn == myContext.myRB.maxPreparationTurns)
        {
            myContext.CallBack();
        }
    }
}