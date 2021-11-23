using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStateManager : MonoBehaviour
{
    public TileStateManager TileManager;
    public FireBaseState currentState;
    public FireFullState FullState = new FireFullState();
    public FireNoneState NoneState = new FireNoneState(); 
    public float Temperature;
    [HideInInspector] public int IgnitionTemperature;
    [HideInInspector] public int SpreadRadius;
    [HideInInspector] public float BurnTime;
    [HideInInspector] public float MaxBurnTime;
    [HideInInspector] public float HeatTransfer;
    public SpriteRenderer SpriteRenderer;
    public GameObject FireParticles;
    public enum FuelTypes
    {
        Log, Tree, Grass, Goalpost, Brazier
    }
    public FuelTypes FuelType;
    protected void Start()
    {
        SetProperties(FuelType);
        currentState = NoneState;
        currentState.EnterState(this);
    }
    protected void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(FireBaseState state)
    {
        currentState = state;
        state.EnterState(this);   
    }
    public void BurnOut()
    {
        TileManager.SwitchObjectState(TileManager.ObjectEmptyState);
        FireParticles.SetActive(false);
    }
    public void ReloadFire()
    {
        Start();
    }
    private void SetProperties(FuelTypes fueltype)
    {
        switch (fueltype)
        {
            case FuelTypes.Log:
                {
                    IgnitionTemperature = FireVariables.s_woodIgnitionTemperature;
                    SpreadRadius = FireVariables.s_woodSpreadRadius;
                    BurnTime = FireVariables.s_woodBurnTime;
                    HeatTransfer = FireVariables.s_woodHeatTransfer;
                    break;
                }
            case FuelTypes.Tree:
                {
                    IgnitionTemperature = FireVariables.s_treeIgnitionTemperature;
                    SpreadRadius = FireVariables.s_treeSpreadRadius;
                    BurnTime = FireVariables.s_treeBurnTime;
                    HeatTransfer = FireVariables.s_treeHeatTransfer;
                    break;
                }
            case FuelTypes.Grass:
                {
                    IgnitionTemperature = FireVariables.s_grassIgnitionTemperature;
                    SpreadRadius = FireVariables.s_grassSpreadRadius;
                    BurnTime = FireVariables.s_grassBurnTime;
                    HeatTransfer = FireVariables.s_grassHeatTransfer;
                    break;
                }
            case FuelTypes.Goalpost:
                {
                    IgnitionTemperature = FireVariables.s_goalpostIgnitionTemperature;
                    SpreadRadius = FireVariables.s_goalpostSpreadRadius;
                    BurnTime = FireVariables.s_goalpostBurnTime;
                    HeatTransfer = FireVariables.s_goalpostHeatTransfer;
                    break;
                }
            case FuelTypes.Brazier:
                {
                    IgnitionTemperature = FireVariables.s_brazierIgnitionTemperature;
                    SpreadRadius = FireVariables.s_brazierSpreadRadius;
                    BurnTime = FireVariables.s_brazierBurnTime;
                    HeatTransfer = FireVariables.s_brazierHeatTransfer;
                    break;
                }
        }
        MaxBurnTime = BurnTime;
    }
}
