

public class WorkerGatheringState : WorkerBaseState
{
    // Change tile's task state to none
    // Change tile's task state to none
    // Play "pick up item" animation
    // When the animation's done, set the nearest storehouse as the next task
<<<<<<< Updated upstream

    public override void EnterState(WorkerStateManager worker)
    {

=======
    // A list of all storehouses can be stored somewhere
    // Whenever a tile's object state becomes Storehouse, it updates that list

    private float _totalGatheringTime; // In case certain objects take longer to pick up
    private float _gatheringTimer;

    public override void EnterState(WorkerStateManager worker)
    {
        Debug.Log("Entered gathering state");

        // Change tile's task state to none
        worker.CurrentTask.SwitchTaskState(worker.CurrentTask.TaskEmptyState);

        // Set worker's item being carried (this will be cancelled if the task is not completed)
        worker.ItemBeingCarried = worker.CurrentTask.ObjectState;

        // Enter animation state and set timer
        _totalGatheringTime = 3f;
        _gatheringTimer = 0;
>>>>>>> Stashed changes
    }

    public override void UpdateState(WorkerStateManager worker)
    {
        // Very basic timer. If we have "halfway" animations for resources being harvested, will have to figure that out
        // Could also switch out the whole thing for coroutines

        _gatheringTimer += Time.deltaTime;
        if (_gatheringTimer >= _totalGatheringTime)
        {
            // Change tile's object state
                // worker.CurrentTask.SwitchObjectState(TileStateManager.ObjectStates.None);
            
            // Find nearest storehouse tile and place it at top of tasklist
                // Need a list with all storehouses
            // Change worker state
            worker.SwitchState(worker.WalkingState);
        }
    }

    public override void ExitState(WorkerStateManager worker)
    {
        // Remove item from worker if action is canceled prematurely
        if(_gatheringTimer < _totalGatheringTime)
        {
            worker.ItemBeingCarried = TileStateManager.ObjectStates.None;
        }
    }
}
