using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPath 
{

    private List<NavigationNode> _path;
    private int _pathLength;
    private int _currentNodeIndex; 
    public NavigationPath(List<NavigationNode> path)
    {
        _path = path;
        _pathLength = _path.Count;
        _currentNodeIndex = 0;
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

    public Vector2 GetNextNodePosition()
    {

        Vector2 coordinates = _path[_currentNodeIndex].GetTileWorldPosition();
        _currentNodeIndex++;
        return coordinates;
    }
}
