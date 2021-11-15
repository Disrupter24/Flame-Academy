using UnityEngine;
public class WorkerIdleState : WorkerBaseState
{
    // Play idle animation

    public override void EnterState(WorkerStateManager worker)
    {
        Debug.Log("Entered idle state");
    }

    public override void UpdateState(WorkerStateManager worker)
    {

    }

    public override void ExitState(WorkerStateManager worker)
    {

    }
}
