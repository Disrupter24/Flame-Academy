using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireVariables : MonoBehaviour
{
    //public GameObject TileParent;
    public static FireStateManager[] s_listOfCombustibles;
    public static TileStateManager[] s_listOfTiles;
    public GameObject LoseMenu;
    //This script is just a database of the properties of the various materials, accessed through the "FuelTypes" enum in "FireStateManager" for now.
    /*Object Properties
    public static int s_objectIgnitionTemperature = int; (arbitrary number, woohoo!)
    public static int s_objectSpreadRadius = int; (radius in tiles, example: 1 would spread the fire to the 8 tiles surrounding it)
    public static float s_objectBurnTime = float; (in seconds)*/
    //Wood Properties
    public static int s_woodIgnitionTemperature = 100;
    public static int s_woodSpreadRadius = 1;
    public static float s_woodBurnTime = 15f;
    public static float s_woodHeatTransfer = 0.2f;
    //Tree Properties
    public static int s_treeIgnitionTemperature = 125;
    public static int s_treeSpreadRadius = 1;
    public static float s_treeBurnTime = 25f;
    public static float s_treeHeatTransfer = 0.12f;
    //Grass Properties
    public static int s_grassIgnitionTemperature = 25;
    public static int s_grassSpreadRadius = 1;
    public static float s_grassBurnTime = 3f;
    public static float s_grassHeatTransfer = 0.80f;
    //Goalpost Properties
    public static int s_goalpostIgnitionTemperature = 75;
    public static int s_goalpostSpreadRadius = 0;
    public static float s_goalpostBurnTime = 1f;
    public static float s_goalpostHeatTransfer = 0f;
    //Brazier Properties
    public static int s_brazierIgnitionTemperature = 200;
    public static int s_brazierSpreadRadius = 1;
    public static float s_brazierBurnTime = 200f;
    public static float s_brazierHeatTransfer = 0.12f;
    private void Start() // This is pretty expensive, so we'll run it once per level.
    {

        s_listOfCombustibles = FindObjectsOfType<FireStateManager>();
        s_listOfTiles = FindObjectsOfType<TileStateManager>();

        // BELOW: wrote a method for filling lists without using FindObjectsOfType, but it's no faster at our current scale.
        // It's a bit inconvenient because it relies on parenting all tiles under one object and linking that object to this one in the inspector. With no upsides, better not to use it.

        //List<FireStateManager> tempListCombustibles = new List<FireStateManager>();
        //List<TileStateManager> tempListTiles = new List<TileStateManager>();

        //for (int i = 0; i < TileParent.transform.childCount; i++)
        //{
        //    GameObject tile = TileParent.transform.GetChild(i).gameObject;
        //    tempListCombustibles.Add(tile.GetComponent<FireStateManager>());
        //    tempListTiles.Add(tile.GetComponent<TileStateManager>());
        //}

        //s_listOfCombustibles = tempListCombustibles.ToArray();
        //s_listOfTiles = tempListTiles.ToArray();

        // Compile starting list of storehouses
        foreach (TileStateManager tile in s_listOfTiles)
        {
            if(tile.ObjectState == TileStateManager.ObjectStates.Storehouse)
            {
                Debug.Log("Adding storehouse");
                StorehouseManager.Instance.Storehouses.Add(tile);
            }
        }
        InvokeRepeating("LossCheck", 5f, 5f);
    }

    private void LossCheck()
    {
        for (int i = 0; i < s_listOfCombustibles.Length; i++)
        {
            if (s_listOfCombustibles[i].currentState != s_listOfCombustibles[i].NoneState && s_listOfCombustibles[i].enabled)
            {
                return;
            }
        }
        LoseMenu.SetActive(true);
    }
}
