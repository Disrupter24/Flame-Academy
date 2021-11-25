using UnityEngine;

public class WorkerWalkingState : WorkerBaseState
{
    // This function defines the worker's walking behaviour
    // The worker will move towards a target (probably using a navmesh)
    // Upon reaching the target, the worker will enter a new state based on the nature of the target
    // For example, if the target is a tile with a tree, the worker will enter "WorkerHarvestingState"

    // Worker identifies the nearest tile and moves towards it
    // Worker checks tile status when it starts moving and when it arrives
    // Upon arrival, switch state to the one corresponding with the task
    // Move fuel to nearest storehouse
    // Chop down trees
    // If all tiles in selection are burning, workers immolate themselves

    public override void EnterState(WorkerStateManager worker)
    {
        // Enter walking animation state

        // If current tile no longer has a task, remove it from the list and find the next one
        if (!worker.ForceMove && !(worker.CurrentTask.TaskState == TileStateManager.TaskStates.Harvest || worker.CurrentTask.TaskState == TileStateManager.TaskStates.Gather || worker.CurrentTask.TaskState == TileStateManager.TaskStates.Storehouse || worker.CurrentTask.TaskState == TileStateManager.TaskStates.PlaceFuel))
        {
            worker.FindNextTask();
        }

        //Debug.Log("starting path at worker position " + worker.transform.position + " target position " +  worker.CurrentTask.transform.position);
        worker.workerMovement.MoveTo(worker,worker.transform.position, worker.CurrentTask.transform.position);
    }

    public override void UpdateState(WorkerStateManager worker)
    {

        // Worker movement
        worker.transform.position = worker.workerMovement.Move(worker.transform.position);

        // If worker has reached the target, check the target's status
        /*
         * If it's a tree (or other resource), enter harvestingstate
         * If it's fuel, enter gatheringstate
         * If it's fuel placement, put that fuel down
         * If it's a storehouse, deposit held item. If tasked with placing fuel, grab required fuel and go to location
         * If it's empty (because another worker got there first), worker.FindNextTask();
         */
        if (worker.workerMovement.IsAtDestination())
        {
            switch (worker.CurrentTask.TaskState)
            {
                case TileStateManager.TaskStates.Harvest:
                    worker.SwitchState(worker.HarvestingState);
                    break;
                case TileStateManager.TaskStates.Gather:
                    if(!worker.PlacingFuel)
                    {
                        worker.SwitchState(worker.GatheringState);
                    }
                    else
                    {
                        worker.FindNextTask();
                    }
                    break;
                case TileStateManager.TaskStates.PlaceFuel:
                    // Place fuel on ground
                    worker.CurrentTask.ToggleGhost(false, worker.HeldItem);
                    worker.HeldItem = TileStateManager.ObjectStates.None;
                    worker.CurrentTask.TaskState = TileStateManager.TaskStates.Gather;
                    worker.FindNextTask();
                    break;
                case TileStateManager.TaskStates.Storehouse:
                    // Place held item in storehouse
                    if(worker.HeldItem != TileStateManager.ObjectStates.None)
                    {
                        StorehouseManager.Instance.MoveItem(worker.HeldItem, true);
                        worker.HeldItem = TileStateManager.ObjectStates.None;
                    }
                    // If next task is to take fuel, remove fuel from storehouse
                    if(worker.TaskList.Count > 0)
                    {
                        if (worker.TaskList[0].TaskState == TileStateManager.TaskStates.PlaceFuel)
                        {
                            StorehouseManager.Instance.MoveItem(worker.TaskList[0].ObjectState, false);
                            worker.HeldItem = worker.TaskList[0].ObjectState;
                            //worker.workerMovement.MoveTo(worker, worker.transform.position, worker.CurrentTask.transform.position);
                        }
                    }
                    worker.FindNextTask();
                    break;
                case TileStateManager.TaskStates.None:
                    worker.FindNextTask();
                    break;
            }
        }

    }

    public override void ExitState(WorkerStateManager worker)
    {
        // Remove navigation target
        
    }

    public override void CancelAction(WorkerStateManager worker)
    {

    }
}
