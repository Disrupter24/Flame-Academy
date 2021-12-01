using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "FlameGame/GameData", order = 51)]
public class GameData : ScriptableObject
{
    [Header("Worker Units")]

    [Space(10)]

    [Tooltip("How Fast Do you run")]
    [SerializeField]
    private float _runningSpeed;


    [Header("Actions")]

    [Space(10)]

    [Tooltip("Chopping Trees Time")]
    [SerializeField]
    private float _choppingTreeSpeed;

    [Tooltip("Chopping Grass Time")]
    [SerializeField]
    private float _choppingGrassSpeed;

    [Tooltip("Dropping Time")]
    [SerializeField]
    private float _DroppingSpeed;

    [Tooltip("Pickup Time")]
    [SerializeField]
    private float _PickingUpSpeed;


    [Header("Wood Variables")]

    [Space(10)]

    [Tooltip("Wood Ignition Temperature")]
    [SerializeField]
    private int _woodIgnitionTemperature = 100;

    [Tooltip("Wood Spread Radius")]
    [SerializeField]
    private int _woodSpreadRadius = 1;

    [Tooltip("Wood burning Time")]
    [SerializeField]
    private float _woodBurnTime = 15f;

    [Tooltip("Wood Heat Transfer")]
    [SerializeField]
    private float _woodHeatTransfer = 0.2f;

    [Header("Tree Variables")]

    [Space(10)]

    [Tooltip("Tree Ignition Temperature")]
    [SerializeField]
    private int _treeIgnitionTemperature = 125;

    [Tooltip("Tree Spread Radius")]
    [SerializeField]
    private int _treeSpreadRadius = 1;

    [Tooltip("Tree burning Time")]
    [SerializeField]
    private float _treeBurnTime = 25f;

    [Tooltip("Tree Heat Transfer")]
    [SerializeField]
    private float _treeHeatTransfer = 0.12f;

    [Header("Grass Variables")]

    [Space(10)]

    [Tooltip("Grass Ignition Temperature")]
    [SerializeField]
    private int _grassIgnitionTemperature = 25;

    [Tooltip("Grass Spread Radius")]
    [SerializeField]
    private int _grassSpreadRadius = 1;

    [Tooltip("Grass burning Time")]
    [SerializeField]
    private float _grassBurnTime = 3f;

    [Tooltip("Grass Heat Transfer")]
    [SerializeField]
    private float _grassHeatTransfer = 0.80f;

    [Header("Goalpost Variables")]

    [Space(10)]

    [Tooltip("Goalpost Ignition Temperature")]
    [SerializeField]
    private int _goalIgnitionTemperature = 75;

    [Tooltip("Goalpost Spread Radius")]
    [SerializeField]
    private int _goalSpreadRadius = 0;

    [Tooltip("Goalpost burning Time")]
    [SerializeField]
    private float _goalBurnTime = 1f;

    [Tooltip("Goalpost Heat Transfer")]
    [SerializeField]
    private float _goalHeatTransfer = 0f;

    [Header("Brazier Variables")]

    [Space(10)]

    [Tooltip("Brazier Ignition Temperature")]
    [SerializeField]
    private int _brazierIgnitionTemperature = 200;

    [Tooltip("Brazier Spread Radius")]
    [SerializeField]
    private int _brazierSpreadRadius = 1;

    [Tooltip("Brazier burning Time")]
    [SerializeField]
    private float _brazierBurnTime = 200f;

    [Tooltip("Brazier Heat Transfer")]
    [SerializeField]
    private float _brazierHeatTransfer = 0.12f;



    public float GetRunningSpeed()
    {
        return _runningSpeed;
    }
    public float GetTreeChoppingSpeed()
    {
        return _choppingTreeSpeed;
    }
    public float GetGrassCuttingSpeed()
    {
        return _choppingGrassSpeed;
    }

    public int GetWoodIgnitionTemp()
    {
        return _woodIgnitionTemperature;
    }

    public int GetWoodSpreadRadius()
    {
        return _woodSpreadRadius;
    }

    public float GetWoodBurningTime()
    {
        return _woodBurnTime;
    }

    public float GetWoodHeatTransfer()
    {
        return _woodHeatTransfer;
    }

    public int GetTreeIgnitionTemp()
    {
        return _treeIgnitionTemperature;
    }

    public int GetTreeSpreadRadius()
    {
        return _treeSpreadRadius;
    }

    public float GetTreeBurningTime()
    {
        return _treeBurnTime;
    }

    public float GetTreeHeatTransfer()
    {
        return _treeHeatTransfer;
    }

    public int GetGrassIgnitionTemp()
    {
        return _grassIgnitionTemperature;
    }

    public int GetGrassSpreadRadius()
    {
        return _grassSpreadRadius;
    }

    public float GetGrassBurningTime()
    {
        return _grassBurnTime;
    }

    public float GetGrassHeatTransfer()
    {
        return _grassHeatTransfer;
    }

    public int GetGoalIgnitionTemp()
    {
        return _goalIgnitionTemperature;
    }

    public int GetGoalSpreadRadius()
    {
        return _goalSpreadRadius;
    }

    public float GetGoalBurningTime()
    {
        return _goalBurnTime;
    }

    public float GetGoalHeatTransfer()
    {
        return _goalHeatTransfer;
    }
    public int GetBrazierIgnitionTemp()
    {
        return _brazierIgnitionTemperature;
    }

    public int GetBrazierSpreadRadius()
    {
        return _brazierSpreadRadius;
    }

    public float GetBrazierBurningTime()
    {
        return _brazierBurnTime;
    }

    public float GetBrazierHeatTransfer()
    {
        return _brazierHeatTransfer;
    }

}
