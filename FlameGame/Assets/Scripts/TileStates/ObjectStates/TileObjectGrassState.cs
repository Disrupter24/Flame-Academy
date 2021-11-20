using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectGrassState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetProperties();
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Grass;
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = false;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[1];
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
