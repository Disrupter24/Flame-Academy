using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectBrazierState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetProperties();
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Log; // Make Brazier fueltype
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = true;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[0];
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
