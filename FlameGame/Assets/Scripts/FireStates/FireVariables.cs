using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireVariables : MonoBehaviour
{
    public static FireStateManager[] s_listOfCombustibles;
    //This script is just a database of the properties of the various materials, accessed through the "FuelTypes" enum in "FireStateManager" for now.
    /*Object Properties
    public static int s_objectIgnitionTemperature = int; (arbitrary number, woohoo!)
    public static int s_objectSpreadRadius = int; (radius in tiles, example: 1 would spread the fire to the 8 tiles surrounding it)
    public static float s_objectBurnTime = float; (in seconds)*/
    //Wood Properties
    public static int s_woodIgnitionTemperature = 100;
    public static int s_woodSpreadRadius = 1;
    public static float s_woodBurnTime = 15f;
    public static float s_woodHeatTransfer = 1.5f;
    //Tree Properties
    public static int s_treeIgnitionTemperature = 150;
    public static int s_treeSpreadRadius = 1;
    public static float s_treeBurnTime = 25f;
    public static float s_treeHeatTransfer = 1f;
    //Grass Properties
    public static int s_grassIgnitionTemperature = 20;
    public static int s_grassSpreadRadius = 1;
    public static float s_grassBurnTime = 2f;
    public static float s_grassHeatTransfer = 6f;
    private void Start() // This is pretty expensive, so we'll run it once per level.
    {
        s_listOfCombustibles = FindObjectsOfType<FireStateManager>();
    }
}
