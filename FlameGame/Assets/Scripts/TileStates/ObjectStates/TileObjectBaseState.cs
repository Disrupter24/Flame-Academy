using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileObjectBaseState
{
    public abstract void EnterState(TileStateManager tile);
    public abstract void UpdateState(TileStateManager tile);
}
