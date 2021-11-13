using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFullState : FireBaseState
{
    public override void EnterState(FireStateManager fire)
    {
        Debug.Log("Burning");
        Debug.Log(fire.Temperature);
        
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
            if(fire.Temperature < 2 * fire.IgnitionTemperature)
            {
                fire.Temperature += Mathf.RoundToInt(Time.deltaTime * fire.Temperature);
            }
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
                if (Vector2.Distance(FireVariables.s_listOfCombustibles[i].transform.position, fire.transform.position) < (fire.SpreadRadius + 1) && (FireVariables.s_listOfCombustibles[i].currentState == FireVariables.s_listOfCombustibles[i].NoneState))
                {
                    if(FireVariables.s_listOfCombustibles[i].Temperature < 2 * FireVariables.s_listOfCombustibles[i].IgnitionTemperature)
                    {
                        FireVariables.s_listOfCombustibles[i].Temperature += Mathf.RoundToInt(Time.deltaTime * fire.HeatTransfer * fire.Temperature);
                    }
                }
            }
        }
    }
    private void UpdateSprite(FireStateManager fire)
    {
        fire.SpriteRenderer.color = new Color(1,0,0,(fire.BurnTime / fire.MaxBurnTime));
    }
}
