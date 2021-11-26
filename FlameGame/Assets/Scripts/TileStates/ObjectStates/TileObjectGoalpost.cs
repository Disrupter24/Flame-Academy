using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectGoalpostState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetProperties();
        tile.FireStateManager.enabled = true;
        tile.FireStateManager.FuelType = FireStateManager.FuelTypes.Goalpost;
        tile.WillCollide = true;
        tile.ObjectRenderer.enabled = true;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[3];
    }
    public override void UpdateState(TileStateManager tile)
    {
        if (tile.FireStateManager.Temperature >= tile.FireStateManager.IgnitionTemperature)
        {
            tile.WinScreen.SetActive(true);
        }
    }
}
