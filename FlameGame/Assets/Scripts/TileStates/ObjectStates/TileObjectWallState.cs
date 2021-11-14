using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectWallState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.WillCollide = true;
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
