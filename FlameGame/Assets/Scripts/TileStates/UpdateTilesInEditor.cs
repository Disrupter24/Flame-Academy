using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateTilesInEditor : MonoBehaviour
{

    TileStateManager tile;

    
    private void Awake()
    {
        tile = gameObject.GetComponent<TileStateManager>();
    }

    private void Update()
    {
        switch (tile.ObjectState)
        {
            case TileStateManager.ObjectStates.Brazier:
                tile.ObjectRenderer.enabled = true;
                tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[0];
                break;
            case TileStateManager.ObjectStates.Grass:
                tile.ObjectRenderer.enabled = true;
                tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[1];
                break;
            case TileStateManager.ObjectStates.Storehouse:
                tile.ObjectRenderer.enabled = true;
                tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[2];
                break;
            case TileStateManager.ObjectStates.Goalpost:
                tile.ObjectRenderer.enabled = true;
                tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[3];
                break;
            case TileStateManager.ObjectStates.Tree:
                tile.ObjectRenderer.enabled = true;
                tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[4];
                break;
            case TileStateManager.ObjectStates.Log:
                tile.ObjectRenderer.enabled = true;
                tile.ObjectRenderer.sprite = tile.ObjectSpriteSheet[5];
                break;
            case TileStateManager.ObjectStates.None:
                tile.ObjectRenderer.enabled = false;
                break;
        }

    }
}
