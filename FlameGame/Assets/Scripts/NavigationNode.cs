using UnityEngine;

public class NavigationNode 
{
    private Tile _tile;
    private Vector2 _coordinates;
    private int _fCost;
    private int _hCost;
    private int _gCost;


    public NavigationNode(Tile tile, Vector2 coordinates)
    {
        _tile = tile;
        _coordinates = coordinates;
    }
    public NavigationNode(Tile tile, int x, int y)
    {
        _tile = tile;
        _coordinates = new Vector2(x, y);
    }
    public void SetTile(Tile tile)
    {
        _tile = tile; 
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

    public bool GetTraversable()
    {

        return true; 
        //_tile.IsTraversable();
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
    public void SetFCost(int fCost)
    {
        _fCost = fCost;
    }



}
