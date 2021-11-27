using UnityEngine;

public class WorkerGatheringState : WorkerBaseState
{
    // Change tile's object state to none
    // Play "pick up item" animation
    // When the animation's done, set the nearest storehouse as the next task
    // A list of all storehouses can be stored somewhere
    // Whenever a tile's object state becomes Storehouse, it updates that list

    private float _totalGatheringTime; // In case certain objects take longer to pick up
    private float _gatheringTimer;

    public override void EnterState(WorkerStateManager worker)
    {
        // Change tile's task state to none
        worker.CurrentTask.TaskState = TileStateManager.TaskStates.None;

        // Enter animation state and set timer
        _totalGatheringTime = worker.gameData.GetGrassCuttingSpeed();
        _gatheringTimer = 0;

        if (worker.CurrentTask.ObjectState == TileStateManager.ObjectStates.Grass)
        {
            worker.gameObject.GetComponent<SoundEffects>().PlaySwingSound();
        }


    }

    public override void UpdateState(WorkerStateManager worker)
    {
        // Very basic timer. If we have "halfway" animations for resources being harvested, will have to figure that out
        // Could also switch out the whole thing for coroutines

        //if (worker.CurrentTask.ObjectState == TileStateManager.ObjectStates.Grass)
       // {
       //     worker.gameObject.GetComponent<SoundEffects>().PlaySwingSound(Time.deltaTime);
       // }


        _gatheringTimer += Time.deltaTime;
        if (_gatheringTimer >= _totalGatheringTime)
        {

            // Worker picks up item
            worker.CollectItem(worker.CurrentTask.ObjectState);
            worker.gameObject.GetComponent<SoundEffects>().PlayPickup();

            // Change tile's object state
            worker.CurrentTask.SwitchObjectState(worker.CurrentTask.ObjectEmptyState);

            TileStateManager nearestStorehouse = StorehouseManager.Instance.FindNearestStorehouse(worker);

            if(nearestStorehouse != null)
            {
                // Set storehouse as next task
                worker.CurrentTask = nearestStorehouse;
                worker.SwitchState(worker.WalkingState);
            }
            else
            {
                // If there are no storehouses, workers will just look for the next task
                worker.FindNextTask();
            }
            
        }
    }

    public override void ExitState(WorkerStateManager worker)
    {
        
    }

    public override void CancelAction(WorkerStateManager worker)
    {
        // If action is cancelled prematurely, reset task state of tile
        if (_gatheringTimer < _totalGatheringTime)
        {
            worker.CurrentTask.TaskState = TileStateManager.TaskStates.Gather;
        }
    }
}
