using UnityEngine;

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

    float movementTimer;
    public override void EnterState(WorkerStateManager worker)
    {
        Debug.Log("entered moving state");
        if (!(worker.CurrentTask.TaskState == TileStateManager.TaskStates.Harvest || worker.CurrentTask.TaskState == TileStateManager.TaskStates.Gather))
        {
            worker.TaskList.RemoveAt(worker.CurrentTaskID);
            worker.FindNextTask();
        }
        Debug.Log("starting path");
        worker.workerMovement.MoveTo(worker.transform.position, worker.CurrentTask.transform.position);
        movementTimer = 0;
    }

    public override void UpdateState(WorkerStateManager worker)
    {
        // If worker has reached the target, check the target's status

        /*
         * If it's a tree (or other resource), enter harvestingstate
         * If it's fuel, enter gatheringstate
         * If it's a storehouse, deposit held item
         * If it's empty (because another worker got there first), worker.FindNextTask();
         */

        // Move towards target (I hate this movement logic but it's temporary)
        worker.transform.position = worker.workerMovement.Move(worker.transform.position); 
        if (worker.workerMovement.IsAtDestination())
        {
            Debug.Log("at desination");
            switch (worker.CurrentTask.TaskState)
            {
                case TileStateManager.TaskStates.Harvest:
                    worker.SwitchState(worker.HarvestingState);
                    break;
                case TileStateManager.TaskStates.Gather:
                    worker.SwitchState(worker.GatheringState);
                    break;
                case TileStateManager.TaskStates.None:
                    worker.FindNextTask();
                    break;
            }
        }

    }

    public override void ExitState(WorkerStateManager worker)
    {
        // Remove navmesh target
    }
}
