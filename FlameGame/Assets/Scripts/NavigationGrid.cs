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



    public static void CreateGrid(Tile[,] tileArray)
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
}
