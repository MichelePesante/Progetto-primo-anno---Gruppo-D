﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class StateMachineBase : MonoBehaviour {

    protected Animator myAnim;
    protected List<StateBase> states;
    protected ContextGamePlay context;
}
