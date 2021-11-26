using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireVariables : MonoBehaviour
{
    public static FireStateManager[] s_listOfCombustibles;
    public static TileStateManager[] s_listOfTiles;
    public GameObject LoseMenu;
    //This script is just a database of the properties of the various materials, accessed through the "FuelTypes" enum in "FireStateManager" for now.
    /*Object Properties
    public static int s_objectIgnitionTemperature = int; (arbitrary number, woohoo!)
    public static int s_objectSpreadRadius = int; (radius in tiles, example: 1 would spread the fire to the 8 tiles surrounding it)
    public static float s_objectBurnTime = float; (in seconds)*/
    //Wood Properties
    public static int s_woodIgnitionTemperature = 200;
    public static int s_woodSpreadRadius = 1;
    public static float s_woodBurnTime = 15f;
    public static float s_woodHeatTransfer = 0.1f;
    //Tree Properties
    public static int s_treeIgnitionTemperature = 250;
    public static int s_treeSpreadRadius = 1;
    public static float s_treeBurnTime = 25f;
    public static float s_treeHeatTransfer = 0.06f;
    //Grass Properties
    public static int s_grassIgnitionTemperature = 50;
    public static int s_grassSpreadRadius = 1;
    public static float s_grassBurnTime = 3f;
    public static float s_grassHeatTransfer = 0.40f;
    //Goalpost Properties
    public static int s_goalpostIgnitionTemperature = 150;
    public static int s_goalpostSpreadRadius = 0;
    public static float s_goalpostBurnTime = 1f;
    public static float s_goalpostHeatTransfer = 0f;
    //Brazier Properties
    public static int s_brazierIgnitionTemperature = 100;
    public static int s_brazierSpreadRadius = 1;
    public static float s_brazierBurnTime = 200f;
    public static float s_brazierHeatTransfer = 0.06f;
    private void Start() // This is pretty expensive, so we'll run it once per level.
    {
        s_listOfCombustibles = FindObjectsOfType<FireStateManager>();
        s_listOfTiles = FindObjectsOfType<TileStateManager>();

        // Compile starting list of storehouses
        foreach(TileStateManager tile in s_listOfTiles)
        {
            if(tile.TaskState == TileStateManager.TaskStates.Storehouse)
            {
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
