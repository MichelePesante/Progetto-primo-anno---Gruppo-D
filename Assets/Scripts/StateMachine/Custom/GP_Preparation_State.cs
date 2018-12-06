using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_Preparation_State : GamePlayStateBase
{
    protected override void Enter()
    {
        base.Enter();
        myContext.myTM.CurrentPlayerTurn = PlayerTurn.Curve_Turn;
        FindObjectOfType<Camera>().transform.localPosition = myContext.myTM.CameraPosition;
        NewUIManager.Instance.ChangeText("Preparation phase: Place two robots!");
        NewUIManager.Instance.TutorialBoxSummon();
        myContext.myRB.RobotsQuadratiInHand = myContext.myRB.Draw(myContext.myRB.RobotQuadratiInHand, myContext.myRB.RobotQuadrati, myContext.myRB.RobotsQuadratiInHand, Player.Player_Quad);
        myContext.CallBack();
    }
}
