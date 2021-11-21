
using UnityEngine;

public class NavigationNode 
{
    private TileStateManager _tile;
    private Vector2 _coordinates;
    private int _fCost;
    private int _hCost;
    private int _gCost;
    private NavigationNode _prevNode;

    public NavigationNode(TileStateManager tile, Vector2 coordinates)
    {
        _tile = tile;
        _coordinates = coordinates;
        _prevNode = null;
    }
    public NavigationNode(TileStateManager tile, int x, int y)
    {
        _tile = tile;
        _coordinates = new Vector2(x, y);
    }

    public void SetPreviousNode(NavigationNode prevNode)
    {
        _prevNode = prevNode;
    }

    public NavigationNode GetPreviousNode()
    {
        return _prevNode;
    }
    public void SetTile(TileStateManager tile)
    {
        _tile = tile; 
    }

    public Vector2 GetTileWorldPosition()
    {
        return _tile.transform.position;
    }
    public void SetCoordinates(Vector2 coordinates)
    {
        _coordinates = coordinates;
    }

    public void SetCoordinates(int x, int y)
    {
        _coordinates = new Vector2(x, y);
    }

    public Vector2 GetCoordinates()
    {
        return _coordinates;
    }
    public TileStateManager GetTile()
    {
        return _tile; 
    }
    public bool GetTraversable()
    {
        return !_tile.WillCollide;
    }
    public int GetFCost()
    {
        return _fCost;
    }
    public int GetHCost()
    {
        return _hCost;
    }

    public int GetGCost()
    {
        return _gCost;
    }
    public void SetHCost(int hCost)
    {
        _hCost = hCost;
    }
    public void SetGCost(int gCost)
    {
        _gCost = gCost;
    }



}