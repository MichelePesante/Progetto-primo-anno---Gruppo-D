using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineGamePlay : StateMachineBase {

    protected void Start()
    {
        context = new ContextGamePlay()
        {
            CallBack = GoToNext,
            myRB = FindObjectOfType<RobotManager>(),
            myTM = FindObjectOfType<TurnManager>(),
            myCM = FindObjectOfType<CardManager>()
        };
        myAnim = GetComponent<Animator>();
        states = new List<StateBase>();
        foreach (StateBase state in myAnim.GetBehaviours<StateBase>())
        {
            states.Add(state.Setup(context));
        }
    }

    private void GoToNext()
    {
        myAnim.SetTrigger("GoToNext");
    }
}