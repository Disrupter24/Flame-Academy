using UnityEngine;

public abstract class WorkerBaseState
{
    public abstract void EnterState(WorkerStateManager worker);

    public abstract void UpdateState(WorkerStateManager worker);

    public abstract void ExitState(WorkerStateManager worker);

    public abstract void CancelAction(WorkerStateManager worker);
}
