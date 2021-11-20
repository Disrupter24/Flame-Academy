using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectStoreState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ResetProperties();
        tile.FireStateManager.enabled = false;
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = true;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[2];
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
