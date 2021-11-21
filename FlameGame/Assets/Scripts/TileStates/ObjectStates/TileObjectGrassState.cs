using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectGrassState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = false;
        //Set sprite to grass
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
