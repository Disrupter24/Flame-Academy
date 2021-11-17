using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationGrid : MonoBehaviour
{

    //just for temporary 
    //contained in Level Class
    private static TileStateManager [] allTilesOnMap;
    public Vector2 bottomLeftCornerTilePosition; 
    private static Vector2 startTilePosition;
    public Vector2 mapSize;
    private static Vector2 mapDimention;


    private static List<NavigationNode> _path;
    private static bool _hasStartedNavigating;
    private static NavigationNode _currentNode; 

    private static NavigationGrid s_instance;
    private static bool _isCalculatingNavigation;
    private static NavigationNode[,] _nodeGrid;
    private static int _mapHeight;
    private static int _mapWidth;

    [SerializeField]
    private static int _tileDimention;

    

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
    private void Start()
    {
        startTilePosition = bottomLeftCornerTilePosition;
        _tileDimention = 1;
        allTilesOnMap = FindObjectsOfType<TileStateManager>();
        mapDimention = mapSize;
        _hasStartedNavigating = false; 
        CreateGrid(allTilesOnMap);
    }
    public static void CreateGrid(TileStateManager[] tileArray = null)
    {

        Vector2 worldPosition;
        int xIndex;
        int yIndex;
       // _nodeGrid = new NavigationNode[_mapWidth, _mapHeight];
        _nodeGrid = new NavigationNode[(int) mapDimention.x, (int) mapDimention.y];

        NavigationNode newNode;

        foreach (TileStateManager tile in tileArray)
        {
            worldPosition = tile.transform.position;
            xIndex = Mathf.FloorToInt(worldPosition.x / _tileDimention) - (int) startTilePosition.x;
            yIndex = Mathf.FloorToInt(worldPosition.y / _tileDimention) - (int) startTilePosition.y;
            newNode = new NavigationNode(tile, xIndex, yIndex);
            //Debug.Log("x: " + xIndex + " y: " + yIndex + " worldx " + worldPosition.x + " worldy " + worldPosition.y);
            _nodeGrid[xIndex, yIndex] = newNode;

        }

    }
    public static void SetGridHeight(int height)
    {
        _mapHeight = height;
    }

    public static void SetGridWidth(int width)
    {
        _mapWidth = width;
    }
    public static NavigationNode GetNode(int i , int j)
    {
        return _nodeGrid[i, j];
    }

    public static int GetDistance(NavigationNode startNode, NavigationNode endNode)
    {
        Vector2 startNodeCoordinates = startNode.GetCoordinates();
        Vector2 endNodeCoordinates = endNode.GetCoordinates();
        int distanceX = Mathf.Abs((int)startNodeCoordinates.x - (int) endNodeCoordinates.x);
        int distanceY = Mathf.Abs((int)startNodeCoordinates.y - (int)endNodeCoordinates.y);

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
        for (int i = (int) coordinates.x - 1; i <= coordinates.x + 1; i++ )
        {
            for (int j = (int)coordinates.y - 1; j <= coordinates.y + 1; j++)
            {
                if (i < 0 || i > mapDimention.x - 1 || j < 0 || j > mapDimention.y || (i == coordinates.x && j == coordinates.y)) continue;
                neighbours.Add(_nodeGrid[i, j]);
            }
        }
        return neighbours;
    }


    public static bool IsReadyToCalculateNavigation()
    {
        return _isCalculatingNavigation;
    }
    public static Vector2 GetPathNodePosition(int nodeNumber)
    {
        return _path[nodeNumber].GetCoordinates();
    }

    public static bool StartNavigation()
    {
        _hasStartedNavigating = true;
        if (_path.Count == 0) return false; 
        _currentNode = _path[0];
        _path.Remove(_currentNode);
        return true;
    }

    public static Vector2 GetNodePosition()
    {
        return _currentNode.GetTileWorldPosition();
    }
    public static bool IsEndOfPath()
    {
        return (_path.Count == 0);
    }
    public static Vector2 GetNextNodePosition()
    {
        Vector2 coordinates = _currentNode.GetTileWorldPosition();
        _currentNode = _path[0];
        _path.Remove(_currentNode);
        return coordinates;
    }

    public static void CalculatePathToDestination(Vector2 startPosition, Vector2 endPosition)
    {
        int xIndex = Mathf.FloorToInt(startPosition.x / _tileDimention) - (int)startTilePosition.x;
        int yIndex = Mathf.FloorToInt(startPosition.y / _tileDimention) - (int)startTilePosition.y;
        NavigationNode startNode = GetNode(xIndex, yIndex);
        xIndex = Mathf.FloorToInt(endPosition.x / _tileDimention) - (int)startTilePosition.x;
        yIndex = Mathf.FloorToInt(endPosition.y / _tileDimention) - (int)startTilePosition.y;
        NavigationNode endNode = GetNode(xIndex, yIndex);
        CalculatePath(startNode, endNode);

    }

    public static void CalculatePath(NavigationNode startNode, NavigationNode endNode)
    {
        _hasStartedNavigating = false;
        _isCalculatingNavigation = true;
        _path = new List<NavigationNode>();
        List<NavigationNode> _openNodeList = new List<NavigationNode>();
        List<NavigationNode> _closedNodeList = new List<NavigationNode>();
        _openNodeList.Add(startNode);
        NavigationNode currentNode;
        int counter = 0; 
        while (_openNodeList.Count > 0)
        {
            currentNode = GetNodeWithLowestFCost(_openNodeList);
            _openNodeList.Remove(currentNode);
            _closedNodeList.Add(currentNode);
            if (currentNode == endNode)
            {
                _path =  GeneratePathFromEndNode(endNode, startNode);
                Debug.Log(_path.Count);
                return;
            }
            foreach (NavigationNode node in GetNeighbours(currentNode.GetCoordinates()))
            {
                if (!node.GetTraversable() || _closedNodeList.Contains(node))
                {
                    continue;
                }
                int pathDistancetoNeighbourNode = currentNode.GetGCost() + GetDistance(currentNode, node);
                Debug.Log("Neighbours " + node.GetCoordinates() + " is walkable " + node.GetTraversable() + " distance " + pathDistancetoNeighbourNode);

                if (!_openNodeList.Contains(node) || pathDistancetoNeighbourNode < node.GetGCost())
                {
                    node.SetGCost(pathDistancetoNeighbourNode);
                    node.SetHCost(GetDistance(node, endNode));
                    node.SetPreviousNode(currentNode);
                    if (!_openNodeList.Contains(node)) _openNodeList.Add(node);
                }
            }
            Debug.Log("Count " + counter );
            counter++;
        }
        Debug.Log("_openNodeList is empty");
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
