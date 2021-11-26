using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectBrazierState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Brazier;
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = true;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[0];
    }
    public override void UpdateState(TileStateManager tile)
    {
        tile.FireStateManager.BrazierBar.value = (tile.FireStateManager.BurnTime / tile.FireStateManager.MaxBurnTime);
    }
}
