using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationGrid : MonoBehaviour
{

    //just for temporary 
    //contained in Level Class
    private static TileStateManager [] s_allTilesOnMap;
    //public Vector2 bottomLeftCornerTilePosition; 
    private static Vector2 s_startTilePosition;
    //public Vector2 mapSize;
    private static Vector2 s_mapDimention;

    [SerializeField]
    private Sprite _destinationMarker;

    [SerializeField]
    private Sprite _targetMarker1;
    [SerializeField]
    private Sprite _targetMarker2;
  


    private static List<NavigationNode> s_path;

    private static NavigationNode[,] s_nodeGrid;
    private static int s_mapHeight;
    private static int s_mapWidth;

    private const int _fixRoundingErrorsHack = 3000;

    [SerializeField]
    private static int s_tileDimention;

    private static NavigationGrid s_instance;


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
    }

    //private NavigationNode GetNavigationNodeFromPosition(float x, float y)
    //{

    //}

    private void OnEnable()
    {
        GameAction.OnLevelStart += OnStartLevel;
    }
    private void OnDisable()
    {
        GameAction.OnLevelStart -= OnStartLevel;
    }

    private void Start()
    {
        //this will be removed eventually
        OnStartLevel();
        //startTilePosition = bottomLeftCornerTilePosition;
        //mapDimention = mapSize;
    }
    //replace with level class

    public static void OnStartLevel()
    {
        s_allTilesOnMap = FindObjectsOfType<TileStateManager>();
        DetermineLevelData(s_allTilesOnMap);
        CreateGrid(s_allTilesOnMap);

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
            if ((tile.transform.position.x - smallestXValue) + 1 > mapWidth) mapWidth = Mathf.CeilToInt( tile.transform.position.x )- Mathf.CeilToInt( smallestXValue) + 1;

            if ((tile.transform.position.y - smallestYValue) + 1 > mapHeight) mapHeight = Mathf.CeilToInt( tile.transform.position.y) - Mathf.CeilToInt( smallestYValue) + 1;

            if ( tile.transform.position.x - smallestXValue < smallestTileDimention && tile.transform.position.x - smallestXValue > 0) smallestTileDimention = tile.transform.position.x - smallestXValue;


        }
        s_tileDimention = (int)smallestTileDimention;

        mapWidth /= s_tileDimention;
        mapHeight /= s_tileDimention;
        smallestXValue /= s_tileDimention;
        smallestYValue /= s_tileDimention;
        s_mapWidth = mapWidth;
        s_mapHeight = mapHeight;
        s_mapDimention = new Vector2 (mapWidth, mapHeight);
        s_startTilePosition = new Vector2(smallestXValue, smallestYValue);
        //Debug.Log("level dim " + s_mapDimention + " start " + s_startTilePosition + " tile dim " + s_tileDimention);
    }
    public static void CreateGrid(TileStateManager[] tileArray = null)
    {
        Vector2 worldPosition;
        int xIndex;
        int yIndex;
       // _nodeGrid = new NavigationNode[_mapWidth, _mapHeight];
        s_nodeGrid = new NavigationNode[Mathf.CeilToInt( s_mapDimention.x), Mathf.CeilToInt( s_mapDimention.y)];

        NavigationNode newNode;

        foreach (TileStateManager tile in tileArray)
        {
            worldPosition = tile.transform.position;
            xIndex = GetXIndex(worldPosition.x); 
            yIndex = GetYIndex(worldPosition.y);
            //Debug.Log(xIndex + " " + yIndex + " " + worldPosition);
            newNode = new NavigationNode(tile, xIndex, yIndex);
            s_nodeGrid[xIndex, yIndex] = newNode;

        }

    }

    public Sprite GetDestinationMarker()
    {
        return _destinationMarker;
    }
    public void DestroyMarker(GameObject marker)
    {
        foreach (Transform markerPart in transform)
        {
            Destroy(markerPart);
        }
        Destroy(marker);
    }
    public GameObject SetDestinationMarker(Vector2 position)
    {
        GameObject marker = new GameObject();
        SpriteRenderer spriteRenderer = marker.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = _destinationMarker;
        marker.transform.position = position;
        return marker;
    }
    public GameObject SetTargetMarker(Vector2 position)
    {
        GameObject parent = new GameObject();
        parent.AddComponent<TargetAlternatingSprites>();


        GameObject childMarker1 = new GameObject();
        GameObject childMarker2 = new GameObject();
        childMarker1.transform.position = Vector2.zero;
        childMarker1.transform.parent = parent.transform;
        childMarker2.transform.position = Vector2.zero;
        childMarker2.transform.parent = parent.transform;

        SpriteRenderer spriteRenderer1 = childMarker1.AddComponent<SpriteRenderer>();
        spriteRenderer1.sprite = _targetMarker1;
        spriteRenderer1.enabled = false; 
        SpriteRenderer spriteRenderer2 = childMarker2.AddComponent<SpriteRenderer>();
        spriteRenderer2.sprite = _targetMarker2;

        parent.transform.position = position;
        return parent;
    }
    public static void SetGridHeight(int height)
    {
        s_mapHeight = height;
    }

    public static void SetGridWidth(int width)
    {
        s_mapWidth = width;
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
        return GetSurroundingTiles(i, j, false);
    }
    
    public static List<TileStateManager> GetEmptySurroundingTiles(float xPos, float yPos)
    {
        int i = GetXIndex(xPos);
        int j = GetYIndex(yPos);
        return GetSurroundingTiles(i, j, true);

    }

    public static TileStateManager GetAnEmptySurroundingTile(float xPos, float yPos)
    {
        int i = GetXIndex(xPos);
        int j = GetYIndex(yPos);
        List <TileStateManager> allsurroundingTiles = GetSurroundingTiles(i, j, true);
        if (allsurroundingTiles.Count > 0) return allsurroundingTiles[0];
        return null;
    }

    public static Vector2 GetPositionOfAnEmptySurroundingTile(float xPos, float yPos)
    {
        TileStateManager tile = GetAnEmptySurroundingTile(xPos, yPos);
        if (tile != null) return tile.transform.position;
        return new Vector2(xPos, yPos);
    }

    public static List<TileStateManager> GetSurroundingTiles(int i, int j, bool getEmpty = false)
    {
        Vector2 coordinates = new Vector2(i, j);
        List<TileStateManager> neighbouringTiles = new List<TileStateManager>();
        if (IsValidLocation(i, j))
        {
            List<NavigationNode> allNeighbours;
            if (getEmpty)
            {
                allNeighbours = GetNeighbours(coordinates, false, true, true);
            }
            else
            {
                allNeighbours = GetNeighbours(coordinates, false, false);
            }
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
        return s_nodeGrid[i, j];
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

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        } else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }

    }

    public static NavigationPath GeneratePathFromEndNode(NavigationNode endNode, NavigationNode startNode)
    {
        List<NavigationNode> movementPath = new List<NavigationNode>();
        NavigationNode currentNode = endNode;
        NavigationPath _path;

        while (currentNode != startNode)
        {
            if (currentNode == endNode)
            {
                if (endNode.GetTraversable() && !endNode.IsFull())
                {
                   // Debug.Log("adding endnode " + currentNode.GetTileWorldPosition());
                    movementPath.Add(currentNode);
                    currentNode = currentNode.GetPreviousNode();
                }
                else
                {
                    //Debug.Log("retreating");
                    currentNode = currentNode.GetPreviousNode();
                    endNode = currentNode;
                }
            } else
            {
                movementPath.Add(currentNode);
                currentNode = currentNode.GetPreviousNode();
            }
        }
        movementPath.Reverse();
        _path = new NavigationPath(movementPath);
        return _path;
    }

    public static bool IsValidLocation(Vector2 coordinates, int i, int j)
    {
        if (i < 0 || i > s_mapDimention.x - 1 || j < 0 || j > s_mapDimention.y - 1 || (i == coordinates.x && j == coordinates.y)) return false;
        return true;
    }

    public static bool IsValidLocation(int i, int j)
    {
        if (i < 0 || i > s_mapDimention.x - 1 || j < 0 || j > s_mapDimention.y - 1) return false;
        return true;
    }
    public static List<NavigationNode> GetNeighbours(Vector2 coordinates, bool checkDiagonal=true, bool checkTraversable=false, bool checkOccupied = false, NavigationNode endNode = null)
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



                if (checkDiagonal) {

                    if (i != coordinates.x && j != coordinates.y)
                    {
                        int x = i;
                        int y = (int)coordinates.y;
                        if ((!IsValidLocation(coordinates, x, y) || s_nodeGrid[x, y] == null || !s_nodeGrid[x, y].GetTraversable()))
                            continue;
                        x = (int)coordinates.x;
                        y = j;
                        if ((!IsValidLocation(coordinates, x, y) || s_nodeGrid[x, y] == null || !s_nodeGrid[x, y].GetTraversable()))
                            continue;

                    }

                }
                

                if (s_nodeGrid[i, j] != null)
                {
                    if (checkOccupied && s_nodeGrid[i, j].IsFull()) continue;


                    if ((endNode != null && s_nodeGrid[i,j] == endNode) || !checkTraversable || (checkTraversable && s_nodeGrid[i, j].GetTraversable()))
                    {
                        neighbours.Add(s_nodeGrid[i, j]);
                    }

                }
            }
        }
        return neighbours;
    }

    public static bool IsFullOfWorkersOntile(float xPos, float yPos)
    {
        int xIndex = GetXIndex(xPos);
        int yIndex = GetYIndex(yPos);
        NavigationNode node = GetNode(xIndex, yIndex);
        return node.IsFull();
    }
    public static void RemoveWorkerFromNode(float xPos, float yPos, WorkerStateManager worker)
    {
        int xIndex = GetXIndex(xPos);
        int yIndex = GetYIndex(yPos);
        NavigationNode node = GetNode(xIndex, yIndex);
        node.RemoveWorkerFromTile(worker);
    }
    public static void SetNodeOccupied(float xPos, float yPos, WorkerStateManager worker)
    {
        int xIndex = GetXIndex(xPos);
        int yIndex = GetYIndex(yPos);
        NavigationNode node = GetNode(xIndex, yIndex);
        node.SetWorkerOnTile(worker);

    }
    public static Vector2 GetPathNodePosition(int nodeNumber)
    {
        return s_path[nodeNumber].GetCoordinates();
    }

    public static int GetXIndex(float x)
    {
        return Mathf.CeilToInt((x) / s_tileDimention) - Mathf.CeilToInt(s_startTilePosition.x);
        //return Mathf.FloorToInt(x / s_tileDimention) - Mathf.FloorToInt(s_startTilePosition.x);
        //return Mathf.CeilToInt(x / s_tileDimention) - (int)s_startTilePosition.x;
    }

    public static int GetYIndex(float y)
    {
        return Mathf.CeilToInt((y) / s_tileDimention) - Mathf.CeilToInt(s_startTilePosition.y);

        // return Mathf.CeilToInt(y / s_tileDimention) - (int)s_startTilePosition.y;
        // return Mathf.FloorToInt(y / s_tileDimention) - Mathf.FloorToInt(s_startTilePosition.y);

    }

    public static NavigationPath CalculatePathToDestination(Vector2 startPosition, Vector2 endPosition)
    {
        int xIndex = GetXIndex(startPosition.x);
        int yIndex = GetYIndex(startPosition.y);
        NavigationNode startNode = GetNode(xIndex, yIndex);
        xIndex = GetXIndex(endPosition.x); 
        yIndex = GetYIndex(endPosition.y); 
        NavigationNode endNode = GetNode(xIndex, yIndex);
        //Debug.Log("endnode " + endNode.GetTileWorldPosition());

        if (endNode == null) return null; 

        if (endNode.IsFull() || !endNode.GetTraversable())
        {
            //Debug.Log("alternate");
            NavigationNode tempNode = GetClosestNavigationNode(endNode, GetNode(startPosition.x, startPosition.y));
            if (tempNode != null)
            {
                endNode = tempNode;
            } else
            {
                //Debug.Log("TempNode == null");
            }
        }

        return CalculatePath(startNode, endNode);

    }


    private static NavigationNode GetClosestNavigationNode(NavigationNode blockedPosition, NavigationNode startPosition)
    {
        //Debug.Log("Finding another Close Navigation Node");
        List<NavigationNode> neighbours = GetNeighbours(blockedPosition.GetCoordinates(), false, true, true);
        NavigationNode newUnblockedNode = null;
        if (neighbours.Count == 0) return null;
        float smallestDistance = Mathf.Infinity;
        float distanceBetweenNodes;
        foreach (NavigationNode neighbourNode in neighbours)
        {
            distanceBetweenNodes = GetRawDistance(neighbourNode, startPosition);
            if (distanceBetweenNodes < smallestDistance)
            {
                smallestDistance = distanceBetweenNodes;
                newUnblockedNode = neighbourNode;
            }
        }
        if (newUnblockedNode != null && !newUnblockedNode.GetTraversable()) return GetClosestNavigationNode(newUnblockedNode, startPosition);

        return newUnblockedNode;
    }
    public static NavigationPath CalculatePath(NavigationNode startNode, NavigationNode endNode)
    {
        s_path = new List<NavigationNode>();
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
            foreach (NavigationNode node in GetNeighbours(currentNode.GetCoordinates(), true, true, false,endNode))
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

            if (smallestFCostNode.GetFCost() > node.GetFCost() || (smallestFCostNode.GetFCost() == node.GetFCost() && node.GetHCost() < smallestFCostNode.GetHCost()))

            {
                smallestFCostNode = node;
            }
        }
        return smallestFCostNode;
    }
}
