using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectTreeState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ObjectRenderer.enabled = true;
        //set sprite to tree
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
