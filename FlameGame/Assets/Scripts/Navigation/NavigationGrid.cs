using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationGrid : MonoBehaviour
{

    //just for temporary 
    //contained in Level Class
    private static TileStateManager [] allTilesOnMap;
    //public Vector2 bottomLeftCornerTilePosition; 
    private static Vector2 startTilePosition;
    //public Vector2 mapSize;
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
        OnStartLevel();
        //startTilePosition = bottomLeftCornerTilePosition;
        //mapDimention = mapSize;
    }
    //replace with level class

    public static void OnStartLevel()
    {
        allTilesOnMap = FindObjectsOfType<TileStateManager>();
        _hasStartedNavigating = false;
        DetermineLevelData(allTilesOnMap);
        CreateGrid(allTilesOnMap);

    }
    private static void DetermineLevelData(TileStateManager[] tileArray = null)
    {
        int mapWidth = 0;
        int mapHeight = 0;
        float smallestXValue = Mathf.Infinity;
        float smallestYValue = Mathf.Infinity;
        float smallestTileDimention = Mathf.Infinity;

        foreach (TileStateManager tile in tileArray)
        {
            if (tile.transform.position.x < smallestXValue) smallestXValue = tile.transform.position.x;

            if (tile.transform.position.y < smallestYValue) smallestYValue = tile.transform.position.y;

        }
        foreach (TileStateManager tile in tileArray)
        { 
            if ((tile.transform.position.x - smallestXValue) + 1 > mapWidth) mapWidth = ((int) tile.transform.position.x - (int) smallestXValue) + 1;

            if ((tile.transform.position.y - smallestYValue) + 1 > mapHeight) mapHeight = ((int) tile.transform.position.y - (int) smallestYValue) + 1;

            if (tile.transform.position.x - smallestXValue < smallestTileDimention && tile.transform.position.x - smallestXValue > 0) smallestTileDimention = tile.transform.position.x - smallestXValue;


        }
        _tileDimention = (int)smallestTileDimention;

        mapWidth /= _tileDimention;
        mapHeight /= _tileDimention;
        smallestXValue /= _tileDimention;
        smallestYValue /= _tileDimention;
        mapDimention = new Vector2 (mapWidth, mapHeight);
        startTilePosition = new Vector2(smallestXValue, smallestYValue);
        Debug.Log("level dim " + mapDimention + " start " + startTilePosition + " tile dim " + _tileDimention);
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
            xIndex = GetXIndex(worldPosition.x); 
            yIndex = GetYIndex(worldPosition.y);
            newNode = new NavigationNode(tile, xIndex, yIndex);
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

    public static TileStateManager GetTile(float xPos, float yPos)
    {
        NavigationNode node = GetNode(xPos, yPos); 
        if (node != null) return node.GetTile();
        return null;
    }

    public static List<TileStateManager> GetSurroundingTiles(float xPos, float yPos)
    {
        int i = GetXIndex(xPos);  
        int j = GetYIndex(yPos); 
        return GetSurroundingTiles(i, j);
    }

    public static List<TileStateManager> GetSurroundingTiles(int i, int j)
    {
        Vector2 coordinates = new Vector2(i, j);
        List<TileStateManager> neighbouringTiles = new List<TileStateManager>();
        if (IsValidLocation(i, j))
        {
            List<NavigationNode> allNeighbours = GetNeighbours(coordinates, false, false);
            foreach (NavigationNode neighbour in allNeighbours)
            {
                neighbouringTiles.Add(neighbour.GetTile());
            }
            return neighbouringTiles;

        }
        return null;


    }
    public static NavigationNode GetNode(int i , int j)
    {
        return _nodeGrid[i, j];
    }

    public static NavigationNode GetNode(float xPos, float yPos)
    {
        int i = GetXIndex(xPos);
        int j = GetYIndex(yPos);
        if (IsValidLocation(i,j))return GetNode(i, j);
        return null;
    }

    public static float GetRawDistance(NavigationNode nodeA, NavigationNode nodeB)
    {
        return (Vector2.Distance(nodeA.GetCoordinates(), nodeB.GetCoordinates()));
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

    public static NavigationPath GeneratePathFromEndNode(NavigationNode endNode, NavigationNode startNode)
    {
        List<NavigationNode> movementPath = new List<NavigationNode>();
        NavigationNode currentNode = endNode;
        NavigationPath _path;

        while (currentNode != startNode)
        {
            if (!(currentNode == endNode && !endNode.GetTraversable())) 
            movementPath.Add(currentNode);
            currentNode = currentNode.GetPreviousNode();
        }
        movementPath.Reverse();
        foreach (NavigationNode node in movementPath)
        {
            Debug.Log("path part: " + node.GetTileWorldPosition());
        }
        _path = new NavigationPath(movementPath);
        return _path;
    }

    public static bool IsValidLocation(Vector2 coordinates, int i, int j)
    {
        if (i < 0 || i > mapDimention.x - 1 || j < 0 || j > mapDimention.y - 1 || (i == coordinates.x && j == coordinates.y)) return false;
        return true;
    }

    public static bool IsValidLocation(int i, int j)
    {
        if (i < 0 || i > mapDimention.x - 1 || j < 0 || j > mapDimention.y - 1) return false;
        return true;
    }
    public static List<NavigationNode> GetNeighbours(Vector2 coordinates, bool checkDiagonal=true, bool checkTraversable=false, NavigationNode endNode = null)
    {


        List<NavigationNode> neighbours = new List<NavigationNode>(); 
        for (int i = (int) coordinates.x - 1; i <= coordinates.x + 1; i++ )
        {
            for (int j = (int)coordinates.y - 1; j <= coordinates.y + 1; j++)
            {
                if (!IsValidLocation(coordinates, i, j)) {
                    continue;
                }

                //travelling diagonally
                //make sure we don't cut corners
                //highly inefficient but would rather not fix unless we have to
 
                if (checkDiagonal && i != coordinates.x && j != coordinates.y) 
                {
                    int x = i;
                    int y = (int)coordinates.y;
                    if (!(IsValidLocation(coordinates, x,y) || _nodeGrid[x,y] != null || _nodeGrid[x,y].GetTraversable()))
                    continue;
                    x = (int)coordinates.x;
                    y = j;
                    if (!(IsValidLocation(coordinates, x, y) || _nodeGrid[x, y] != null || _nodeGrid[x, y].GetTraversable()))
                    continue;

                }

                if (_nodeGrid[i, j] != null)
                {
                    
                    if ((endNode != null && _nodeGrid[i,j] == endNode) || !checkTraversable || (checkTraversable && _nodeGrid[i, j].GetTraversable()))
                    {
                        neighbours.Add(_nodeGrid[i, j]);
                    }

                }
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

    public static int GetXIndex(float x)
    {
        return Mathf.FloorToInt(x / _tileDimention) - (int)startTilePosition.x;
    }

    public static int GetYIndex(float y)
    {
        return Mathf.FloorToInt(y / _tileDimention) - (int)startTilePosition.y;

    }

    public static NavigationPath CalculatePathToDestination(Vector2 startPosition, Vector2 endPosition)
    {
        int xIndex = GetXIndex(startPosition.x);
        int yIndex = GetYIndex(startPosition.y);
        NavigationNode startNode = GetNode(xIndex, yIndex);
        xIndex = GetXIndex(endPosition.x); 
        yIndex = GetYIndex(endPosition.y); 
        NavigationNode endNode = GetNode(xIndex, yIndex);
        if (endNode == null) return null; 

        /*if (!endNode.GetTraversable())
        {
            List<NavigationNode> neighbours = GetNeighbours(endNode.GetCoordinates(), false, true);
            if (neighbours.Count == 0) return null;
            float smallestDistance = GetRawDistance(startNode, endNode);
            float distanceBetweenNodes; 
            foreach (NavigationNode neighbourNode in neighbours)
            {
                distanceBetweenNodes = GetRawDistance(neighbourNode, startNode);
                if (distanceBetweenNodes < smallestDistance)
                {
                    smallestDistance = distanceBetweenNodes;
                    endNode = neighbourNode;
                }
            }
            if (!endNode.GetTraversable()) return null;
        } */ 

        return CalculatePath(startNode, endNode);

    }

    public static NavigationPath CalculatePath(NavigationNode startNode, NavigationNode endNode)
    {
        _hasStartedNavigating = false;
        _isCalculatingNavigation = true;
        _path = new List<NavigationNode>();
        List<NavigationNode> _openNodeList = new List<NavigationNode>();
        List<NavigationNode> _closedNodeList = new List<NavigationNode>();
        _openNodeList.Add(startNode);
        NavigationNode currentNode;
        while (_openNodeList.Count > 0)
        {
            currentNode = GetNodeWithLowestFCost(_openNodeList);
            _openNodeList.Remove(currentNode);
            _closedNodeList.Add(currentNode);
            if (currentNode == endNode)
            {
                return GeneratePathFromEndNode(endNode, startNode);
            }
            foreach (NavigationNode node in GetNeighbours(currentNode.GetCoordinates(),true, true, endNode))
            {
                //if (!node.GetTraversable() || _closedNodeList.Contains(node))
                if (_closedNodeList.Contains(node))
                {
                    continue;
                }
                int pathDistancetoNeighbourNode = currentNode.GetGCost() + GetDistance(currentNode, node);
                //Debug.Log("Neighbours " + node.GetCoordinates() + " is walkable " + node.GetTraversable() + " distance " + pathDistancetoNeighbourNode);

                if (!_openNodeList.Contains(node) || pathDistancetoNeighbourNode < node.GetGCost())
                {
                    node.SetGCost(pathDistancetoNeighbourNode);
                    node.SetHCost(GetDistance(node, endNode));
                    node.SetPreviousNode(currentNode);
                    if (!_openNodeList.Contains(node)) _openNodeList.Add(node);
                }
            }
        }
        return null;
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
