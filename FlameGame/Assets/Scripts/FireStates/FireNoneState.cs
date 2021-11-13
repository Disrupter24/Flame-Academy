using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNoneState : FireBaseState
{
    public override void EnterState(FireStateManager fire)
    {
        if (fire.Temperature >= fire.IgnitionTemperature)
        {
            fire.SwitchState(fire.FullState);
        }
    }
    public override void UpdateState(FireStateManager fire)
    {
        if (fire.Temperature >= fire.IgnitionTemperature)
        {
            fire.SwitchState(fire.FullState);
        }
    }
}
