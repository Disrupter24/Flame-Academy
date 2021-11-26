using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNoneState : FireBaseState
{
    public override void EnterState(FireStateManager fire)
    {
        fire.FireParticles.SetActive(false);
    }
    public override void UpdateState(FireStateManager fire)
    {
        if (fire.Temperature >= fire.IgnitionTemperature)
        {
            fire.SwitchState(fire.FullState);
        }
        else if (fire.Temperature > 0)
        {
          fire.Temperature -= (fire.HeatTransfer * Time.deltaTime);
        }
    }
}
