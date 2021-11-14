using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTaskLogState : TileTaskBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetFireProperties();
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Log;
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.ReloadFire();
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
