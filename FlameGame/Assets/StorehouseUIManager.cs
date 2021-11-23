using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorehouseUIManager : MonoBehaviour
{
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
    public void UpdateChart() // This will be called by the worker when they deposit resources into the storehouse to prevent every-frame checking.
    {
        LogText.text = ("Wood: " + Logs);
        GrassText.text = ("Grass: " + Logs);
    }
}
