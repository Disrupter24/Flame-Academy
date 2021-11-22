using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPath 
{

    private List<NavigationNode> _path;
    private int _pathLength;
    private int _currentNodeIndex;
    private NavigationNode _lastNode; 
    public NavigationPath(List<NavigationNode> path)
    {
        _path = path;
        _pathLength = _path.Count;
        _currentNodeIndex = 0;
        if (_pathLength > 0)
        {
            _lastNode = _path[_pathLength - 1];
        } else
        {
            _lastNode = null;
        }
    }

    public NavigationNode GetNavigationNode(int index)
    {
        return _path[index]; 
    }

    public bool IsAtEndOfPath()
    {
        if (_currentNodeIndex == _pathLength) return true;
        return false;
    }

    public void OccupyLastNodeWithWorker(WorkerStateManager worker)
    {
        _lastNode.SetWorkerOnTile(worker);
    }

    public void RemoveWorkerFromLastNode()
    {
        if (_lastNode == null) return;
        _lastNode.RemoveWorkerFromTile();
    }

    public bool CheckIfWorkerOnLastNode()
    {
        return _lastNode.HasWorkerOnTile();
    }
    public Vector2 GetFinalPosition()
    {
        return _lastNode.GetTileWorldPosition();
    }
    public Vector2 GetNextNodePosition()
    {

        Vector2 coordinates = _path[_currentNodeIndex].GetTileWorldPosition();
        _currentNodeIndex++;
        return coordinates;
    }
}
