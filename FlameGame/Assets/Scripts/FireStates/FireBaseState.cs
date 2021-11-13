using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireBaseState
{
    public abstract void EnterState(FireStateManager fire);
    public abstract void UpdateState(FireStateManager fire);
}
