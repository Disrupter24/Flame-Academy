using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectTreeState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetProperties();
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Tree;
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = true;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[4];
        tile.FireStateManager.BurnTime = tile.FireStateManager.MaxBurnTime;
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
