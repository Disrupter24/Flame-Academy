using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    private static Navigation s_instance;
    private static bool _isCalculatingNavigation;


    public static Navigation Instance
    {
        get
        {
            if (s_instance == null)
            {
                Navigation singleton = GameObject.FindObjectOfType<Navigation>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    s_instance = go.AddComponent<Navigation>();
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
                return _closedNodeList;
            }
            foreach (NavigationNode node in NavigationGrid.GetNeighbours(currentNode.GetCoordinates()))
            {
                if (!node.GetTraversable() || _closedNodeList.Contains(node))
                {
                    continue; 
                }

                if (_openNodeList.Contains(node) || )
            }

        }
    }

    private static NavigationNode GetNodeWithLowestFCost(List<NavigationNode> nodeList)
    {
        NavigationNode smallestFCostNode = nodeList[0]; 
        foreach (NavigationNode node in nodeList)
            {
            if (smallestFCostNode.GetFCost() > node.GetFCost() || smallestFCostNode.GetFCost() == node.GetFCost() && node. )
            {
                smallestFCostNode = node;
            }
        }
        return smallestFCostNode;
    }
       
    }

