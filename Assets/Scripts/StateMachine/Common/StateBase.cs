using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : StateMachineBehaviour {

    public abstract StateBase Setup(ContextGamePlay _context);

    protected virtual void Enter()
    {

    }

    protected virtual void Tick()
    {

    }

    protected virtual void Exit()
    {

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Enter();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Tick();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Exit();
    }
}
