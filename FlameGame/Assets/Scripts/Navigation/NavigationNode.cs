
using System.Collections.Generic;
using UnityEngine;
public class NavigationNode 
{
    private TileStateManager _tile;
    private Vector2 _coordinates;
    private int _fCost;
    private int _hCost;
    private int _gCost;
    private NavigationNode _prevNode;
    private bool _isOccupiedByWorker;
    private bool _isFull; 
    private List <WorkerStateManager> _workers;
    private int MaxWorkersOnNode = 4;

    public NavigationNode(TileStateManager tile, Vector2 coordinates)
    {
        _tile = tile;
        _coordinates = coordinates;
        _prevNode = null;
        _workers = new List<WorkerStateManager>();
    }
    public NavigationNode(TileStateManager tile, int x, int y)
    {
        _tile = tile;
        _coordinates = new Vector2(x, y);
        _workers = new List<WorkerStateManager>();

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

    public void  SetWorkerOnTile(WorkerStateManager worker)
    {
        _isOccupiedByWorker = true;
        _workers.Add(worker);
        if (_workers.Count == MaxWorkersOnNode) _isFull = true;


        UpdateWorkersOntile();
    }

    private void UpdateWorkersOntile()
    {
        if (_workers.Count <= 1) return;

        int count = 1;
        float seperationDistance = (32 / (_workers.Count));
        float startingPosition = -16 + (32 / (_workers.Count * 2));

        foreach (WorkerStateManager w in _workers)
        {
            w.workerMovement.SetOffset(new Vector2((startingPosition + count * seperationDistance - 16)/32, 1 - (_workers.Count - 1) * .15f));
            count++;

        }
    }
    public List <WorkerStateManager> GetWorkersOnTile()
    {
        return _workers;
    }

    public bool HasWorkerOnTile()
    {
        return _isOccupiedByWorker;
    }

    public bool IsFull()
    {
        return _isFull;
    }

    public void RemoveWorkerFromTile(WorkerStateManager worker)
    {
        _workers.Remove(worker);
        _isFull = false; 
        if (_workers.Count == 0) _isOccupiedByWorker = false;

        UpdateWorkersOntile();
    }

}