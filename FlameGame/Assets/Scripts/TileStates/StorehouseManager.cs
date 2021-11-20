using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<TileStateManager> Storehouses = new List<TileStateManager>();

    private void Awake()
    {
        _instance = this;
    }

    public void StoreItem(TileStateManager.ObjectStates item)
    {
        //Debug.Log("stored item");
    }

}
