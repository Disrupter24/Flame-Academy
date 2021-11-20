using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectTreeState : TileObjectBaseState
{
    public override void EnterState(TileStateManager tile)
    {
        tile.ObjectRenderer.enabled = true;
        tile.WillCollide = true;
        tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[4];
    }
    public override void UpdateState(TileStateManager tile)
    {

    }
}
