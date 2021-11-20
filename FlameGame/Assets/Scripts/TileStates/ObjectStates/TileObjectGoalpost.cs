using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectGoalpostState : TileObjectBaseState
{
    //Goalpost logic will be put here, triggers win when temperature exceeds a certain amount.
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetProperties();
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Log; // Make Goalpost fueltype
        tile.WillCollide = true;
        tile.ObjectRenderer.enabled = true;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[3];
    }
    public override void UpdateState(TileStateManager tile)
    {
        // if temperature gets high enough, you win!
    }
}
