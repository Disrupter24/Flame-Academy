using UnityEngine;
public class WorkerIdleState : WorkerBaseState
{
    // Play idle animation

    public override void EnterState(WorkerStateManager worker)
    {
        worker.CurrentTask = null;
        if(worker.LevelVictory)
        {
            worker.transform.localScale = Vector3.one * 5f;
            worker.EnterAnimationState("Won");
            // ENTER DANCE ANIMATION
        }
        else
        {
            worker.ResetMostAnimationBools();
            // ENTER IDLE ANIMATION
        }
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
