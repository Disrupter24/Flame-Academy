using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFullState : FireBaseState
{
    public override void EnterState(FireStateManager fire)
    {
        fire.FireParticles.SetActive(true);
        fire.TileManager.TaskState = TileStateManager.TaskStates.Burning;
        fire.BurnTile();
    }
    public override void UpdateState(FireStateManager fire)
    {
        if(fire.BurnTime <= 0)
        {
            fire.BurnOut();
        }
        else
        {
            fire.BurnTime -= Time.deltaTime;
            WarmNearby(fire);
        }
        UpdateSprite(fire);
    }
    private void WarmNearby(FireStateManager fire)
    {
        for (int i = 0; i < FireVariables.s_listOfCombustibles.Length; i++)
        {
            if (FireVariables.s_listOfCombustibles[i] != null)
            {
                if (Vector2.Distance(FireVariables.s_listOfCombustibles[i].transform.position, fire.transform.position) < (fire.SpreadRadius + 0.1f) && (FireVariables.s_listOfCombustibles[i].currentState == FireVariables.s_listOfCombustibles[i].NoneState))
                {
                    if(FireVariables.s_listOfCombustibles[i].Temperature < 2 * FireVariables.s_listOfCombustibles[i].IgnitionTemperature && !FireVariables.s_listOfCombustibles[i].TileManager.IsGhost)
                    {
                        FireVariables.s_listOfCombustibles[i].Temperature += (Time.deltaTime * fire.HeatTransfer * fire.Temperature);
                    }
                }
            }
        }
    }
    private void UpdateSprite(FireStateManager fire)
    {
        fire.TileManager.ObjectRenderer.color = new Color(1,0,0,(fire.BurnTime / fire.MaxBurnTime));
    }
}
