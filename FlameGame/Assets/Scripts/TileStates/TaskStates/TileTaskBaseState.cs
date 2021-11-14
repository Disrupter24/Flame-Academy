using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileTaskBaseState
{
    public abstract void EnterState(TileStateManager tile);
    public abstract void UpdateState(TileStateManager tile);
}
