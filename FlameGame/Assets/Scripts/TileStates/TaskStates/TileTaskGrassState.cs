using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTaskGrassState : TileTaskBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetFireProperties();
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Grass;
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.ReloadFire();
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
