using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTaskEmptyState : TileTaskBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetFireProperties();
        tile.FireStateManager.enabled = false;
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
