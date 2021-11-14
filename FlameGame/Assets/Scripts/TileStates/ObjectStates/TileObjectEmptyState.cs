using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectEmptyState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetFireProperties();
        tile.FireStateManager.enabled = false;
        tile.ObjectRenderer.enabled = false;
    }
    public override void UpdateState(TileStateManager tile)
    {
        //Tile's empty, no need for logic. :)
    }
}
