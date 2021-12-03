using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorehouseManager : MonoBehaviour
{
    // Singleton pattern
    private static StorehouseManager _instance;

    public static StorehouseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                StorehouseManager singleton = GameObject.FindObjectOfType<StorehouseManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<StorehouseManager>();
                }
            }
            return _instance;
        }
    }

    public static int Logs;
    public static int Grass;
    public static int GhostLogs; // Need to track ghostlogs because we don't want the number of ghostlogs to exceed the number of logs available
    public static int GhostGrass;
    public TMP_Text LogText;
    public TMP_Text GrassText;

    public void Start()
    {
        //Set starting values of logs etc. according to the map.
        Logs = 0;
        Grass = 0;
        GhostLogs = 0;
        GhostGrass = 0;
    }
    public void UpdateStorehouseUI() // This will be called by the worker when they deposit resources into the storehouse to prevent every-frame checking.
    {
        LogText.text = ("Place Wood (" + Logs + ")");
        GrassText.text = ("Place Grass (" + Grass + ")");
    }

    public List<TileStateManager> Storehouses = new List<TileStateManager>();

    private void Awake()
    {
        _instance = this;
    }
    public void SetStartingResources(int startingLogs, int startingGrass)
    {
        Logs = startingLogs;
        Grass = startingGrass;
        UpdateStorehouseUI();
    }
    public void MoveItem(TileStateManager.ObjectStates item, bool puttingIn)
    {
        switch (item)
        {
            case TileStateManager.ObjectStates.Log:
                if (puttingIn)
                {
                    Logs += 1;
                }
                else
                {
                    Logs -= 1;
                    Debug.Log("LOSING GHOST LOGS STOREHOUSE");
                    GhostLogs -= 1;
                }
                break;
            case TileStateManager.ObjectStates.Grass:
                if (puttingIn)
                {
                    Grass += 1;
                }
                else
                {
                    Grass -= 1;
                    Debug.Log("LOSING GHOST GRASS STOREHOUSE");
                    GhostGrass -= 1;
                }
                break;
        }

        UpdateStorehouseUI();
    }

    public TileStateManager FindNearestStorehouse(WorkerStateManager worker)
    {
        // Used by workers to find the nearest storehouse
        TileStateManager nearestStorehouse = null;
        float nearestStorehouseDistance = 100000; // Arbitrarily large float

        foreach (TileStateManager storehouse in StorehouseManager.Instance.Storehouses)
        {
            // Check distance to tile
            float tileDistance = Vector2.Distance(storehouse.transform.position, worker.transform.position);

            // Get nearest tile
            if (tileDistance < nearestStorehouseDistance)
            {
                nearestStorehouse = storehouse;
                nearestStorehouseDistance = tileDistance;
            }

        }

        return nearestStorehouse;
    }

    // Remember that fuel is stored globally, not per storehouse
    // (I forgot this and it caused me some pain and confusion)
    public int CheckRemainingFuel(TileStateManager.ObjectStates fuelType)
    {
        switch (fuelType)
        {
            case TileStateManager.ObjectStates.Log:
                return Logs;
            case TileStateManager.ObjectStates.Grass:
                return Grass;
        }
        Debug.Log("CheckRemainingFuel error!");
        return 0;
    }

    public int CheckGhostFuel(TileStateManager.ObjectStates fuelType)
    {
        switch (fuelType)
        {
            case TileStateManager.ObjectStates.Log:
                return GhostLogs;
            case TileStateManager.ObjectStates.Grass:
                return GhostGrass;
        }
        Debug.Log("CheckGhostFuel error!");
        return 0;
    }
}
