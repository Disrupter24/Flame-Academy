using UnityEngine;

public class WorkerGatheringState : WorkerBaseState
{
    // Change tile's object state to none
    // Play "pick up item" animation
    // When the animation's done, set the nearest storehouse as the next task
    // A list of all storehouses can be stored somewhere
    // Whenever a tile's object state becomes Storehouse, it updates that list

    private float _totalGatheringTime;
    private float _gatheringTimer;

    public override void EnterState(WorkerStateManager worker)
    {
        Debug.Log("Entered gathering state");
    }

    public override void UpdateState(WorkerStateManager worker)
    {

    }

    public override void ExitState(WorkerStateManager worker)
    {

    }
}
