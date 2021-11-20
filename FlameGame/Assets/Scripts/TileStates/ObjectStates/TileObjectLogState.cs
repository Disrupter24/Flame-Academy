using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectLogState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetProperties();
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Log;
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = false;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[5];
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
