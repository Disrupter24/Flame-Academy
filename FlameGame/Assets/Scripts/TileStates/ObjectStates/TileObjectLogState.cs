using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectLogState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = false;
        //Set sprite to log
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
