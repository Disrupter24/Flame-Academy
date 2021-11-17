using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectBrazierState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = true;
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
