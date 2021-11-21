using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectGoalpostState : TileObjectBaseState
{
    //Goalpost logic will be put here, triggers win when temperature exceeds a certain amount.
    public override void EnterState(TileStateManager tile)
    {
        tile.WillCollide = true;
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
