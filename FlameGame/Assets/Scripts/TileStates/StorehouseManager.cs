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
    public TMP_Text LogText;
    public TMP_Text GrassText;

    public void Start()
    {
        //Set starting values of logs etc. according to the map.
        Logs = 0;
        Grass = 0;
    }
    public void UpdateStorehouseUI() // This will be called by the worker when they deposit resources into the storehouse to prevent every-frame checking.
    {
        LogText.text = ("Wood: " + Logs);
        GrassText.text = ("Grass: " + Grass);
    }

    public List<TileStateManager> Storehouses = new List<TileStateManager>();

    private void Awake()
    {
        _instance = this;
    }

    public void StoreItem(TileStateManager.ObjectStates item)
    {
        switch(item)
        {
            case TileStateManager.ObjectStates.Log:
                Logs += 1;
                break;
            case TileStateManager.ObjectStates.Grass:
                Grass += 1;
                break;
        }

        UpdateStorehouseUI();
    }

}
