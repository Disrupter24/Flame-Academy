using UnityEngine;
public class WorkerIdleState : WorkerBaseState
{
    // Play idle animation

    public override void EnterState(WorkerStateManager worker)
    {
        worker.CurrentTask = null;
    }

    public override void UpdateState(WorkerStateManager worker)
    {

    }

    public override void ExitState(WorkerStateManager worker)
    {

    }

    public override void CancelAction(WorkerStateManager worker)
    {

    }
}
