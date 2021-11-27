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

   
}
