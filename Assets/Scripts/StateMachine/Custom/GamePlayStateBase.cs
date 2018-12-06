using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayStateBase : StateBase {

    protected ContextGamePlay myContext;

    public override StateBase Setup(ContextGamePlay _context)
    {
        myContext = _context;
        return this;
    }
}
