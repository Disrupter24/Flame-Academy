
public class WorkerWalkingState : WorkerBaseState
{
    // This function defines the worker's walking behaviour
    // The worker will move towards a target (probably using a navmesh)
    // Upon reaching the target, the worker will enter a new state based on the nature of the target
    // For example, if the target is a tile with a tree, the worker will enter "WorkerChoppingState"

    // Worker identifies the nearest tile and moves towards it
    // Worker checks tile status when it starts moving and when it arrives
    // Upon arrival, switch state to the one corresponding with the task
    // Move fuel to nearest storehouse
    // Chop down trees
    // If all tiles in selection are burning, workers immolate themselves

    public override void EnterState(WorkerStateManager worker)
    {
        // Check the target's status
        // Set worker.CurrentTask as target

    }

    public override void UpdateState(WorkerStateManager worker)
    {
        // Move towards target

        // If worker has reached the target, check the target's status

        /*
         * If it's a tree (or other resource), enter harvestingstate
         * If it's fuel, enter gatheringstate
         * If it's a storehouse, deposit held item
         * If it's empty (because another worker got there first), worker.FindNextTask();
         */
    }

    public override void ExitState(WorkerStateManager worker)
    {
        // Remove navmesh target
    }
}
