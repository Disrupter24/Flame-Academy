using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationGrid : MonoBehaviour
{
    private static NavigationGrid s_instance;
    private static bool _isCalculatingNavigation;
    private static NavigationNode[,] _nodeGrid;
    private static int _mapHeight;
    private static int _mapWidth;


    public static NavigationGrid Instance
    {
        get
        {
            if (s_instance == null)
            {
                NavigationGrid singleton = GameObject.FindObjectOfType<NavigationGrid>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    s_instance = go.AddComponent<NavigationGrid>();
                }
            }
            return s_instance;
        }
    }

    private void Awake()
    {
        s_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //private NavigationNode GetNavigationNodeFromPosition(float x, float y)
    //{

    //}

    public static void CreateGrid(TileStateManager[,] tileArray)
    {

        _mapWidth = tileArray.GetLength(1);
        _mapHeight = tileArray.GetLength(2);

        _nodeGrid = new NavigationNode [_mapWidth, _mapHeight];

        NavigationNode newNode; 

        for (int i = 0; i < _mapWidth; i++)
        {
            for (int j = 0; j < _mapHeight; j++)
            {
                newNode = new NavigationNode(tileArray[i, j], i, j);
                _nodeGrid[i, j] = newNode;
            }

        }
    }

    public static NavigationNode GetNode(int i , int j)
    {
        return _nodeGrid[i, j];
    }

    public static int GetDistance(NavigationNode startNode, NavigationNode endNode)
    {
        Vector2 startNodeCoordinates = startNode.GetCoordinates();
        Vector2 endNodeCoordinates = endNode.GetCoordinates();
        int distanceX = (int) endNodeCoordinates.x - (int) startNodeCoordinates.x;
        int distanceY = (int)endNodeCoordinates.y - (int)startNodeCoordinates.y;

        if (distanceX > distanceY) return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceY + 10 * (distanceY - distanceX); 

    }

    public static List<NavigationNode> GeneratePathFromEndNode(NavigationNode endNode, NavigationNode startNode)
    {
        List<NavigationNode> movementPath = new List<NavigationNode>();
        NavigationNode currentNode = endNode;
        while (currentNode != startNode)
        {
            movementPath.Add(currentNode);
            currentNode = currentNode.GetPreviousNode();
        }
        movementPath.Reverse();
        return movementPath;
    }
    public static List<NavigationNode> GetNeighbours(Vector2 coordinates)
    {
        List<NavigationNode> neighbours = new List<NavigationNode>(); 
        for (int i = (int) coordinates.x - 1; i < coordinates.x + 2; i++ )
        {
            for (int j = (int)coordinates.y - 1; j < coordinates.y + 2; j++)
            {
                if (i < 0 || i > _mapWidth - 1 || j < 0 || j > _mapHeight || (i == coordinates.x && j == coordinates.y)) continue;
                neighbours.Add(_nodeGrid[i, j]);
            }
        }
        return neighbours;
    }


    public static bool IsReadyToCalculateNavigation()
    {
        return _isCalculatingNavigation;
    }

    public static List<NavigationNode> CalculatePathToDestination(NavigationNode startNode, NavigationNode endNode)
    {
        _isCalculatingNavigation = true;
        List<NavigationNode> _openNodeList = new List<NavigationNode>();
        List<NavigationNode> _closedNodeList = new List<NavigationNode>();
        _openNodeList.Add(startNode);
        NavigationNode currentNode;
        while (true)
        {
            currentNode = GetNodeWithLowestFCost(_openNodeList);
            _openNodeList.Remove(currentNode);
            _closedNodeList.Add(currentNode);
            if (currentNode == endNode)
            {
                return GeneratePathFromEndNode(endNode, startNode);
            }
            foreach (NavigationNode node in GetNeighbours(currentNode.GetCoordinates()))
            {
                if (!node.GetTraversable() || _closedNodeList.Contains(node))
                {
                    continue;
                }
                int pathDistancetoNeighbourNode = currentNode.GetGCost() + GetDistance(currentNode, node);
                if (_openNodeList.Contains(node) || pathDistancetoNeighbourNode < node.GetGCost())
                {
                    node.SetGCost(pathDistancetoNeighbourNode);
                    node.SetHCost(GetDistance(node, endNode));
                    node.SetPreviousNode(currentNode);
                    if (!_openNodeList.Contains(node)) _openNodeList.Add(node);
                }
            }

        }
    }

    private static NavigationNode GetNodeWithLowestFCost(List<NavigationNode> nodeList)
    {
        NavigationNode smallestFCostNode = nodeList[0];
        foreach (NavigationNode node in nodeList)
        {

            if (smallestFCostNode.GetFCost() > node.GetFCost() || smallestFCostNode.GetFCost() == node.GetFCost() && node.GetHCost() < smallestFCostNode.GetHCost())

            {
                smallestFCostNode = node;
            }
        }
        return smallestFCostNode;
    }
}
