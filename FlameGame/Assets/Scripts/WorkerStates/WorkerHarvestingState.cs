using UnityEngine;

public class WorkerHarvestingState : WorkerBaseState
{
    // Change tile's task state to none
    // Play harvest animation
    // Once harvest animation is done, change object state to relevant fuel, task state to fuel, and switch to WorkerGatheringState

    private float _totalHarvestingTime;
    private float _harvestTimer;

    private TileObjectBaseState _harvestedResource;

    public override void EnterState(WorkerStateManager worker)
    {
        // Change tile's task state to none
        worker.CurrentTask.TaskState = TileStateManager.TaskStates.None;

        // Enter animation states and set timer corresponding with resource being harvested
        switch (worker.CurrentTask.ObjectState)
        {
            case TileStateManager.ObjectStates.Tree:
                // Tree animation
                _totalHarvestingTime = 3f;
                _harvestedResource = worker.CurrentTask.ObjectLogState;
                break;
            //case TileStateManager.ObjectStates.Coal:
            //    // Coal animation
            //    // Coal harvesting time = 15f;
            //    break;
        }

        _harvestTimer = 0;
    }

    public override void UpdateState(WorkerStateManager worker)
    {
        // Very basic timer. If we have "halfway" animations for resources being harvested, will have to figure that out
        // Could also switch out the whole thing for coroutines

        _harvestTimer += Time.deltaTime;
        worker.gameObject.GetComponent<SoundEffects>().PlayChopSound(Time.deltaTime);

        if(_harvestTimer >= _totalHarvestingTime)
        {
            // Change tile's object state
            worker.CurrentTask.SwitchObjectState(_harvestedResource);
            
            
            // If there is a storehouse somewhere, pick up item
            if(StorehouseManager.Instance.Storehouses.Count > 0)
            {
                worker.SwitchState(worker.GatheringState);
            }
            else
            {
                worker.FindNextTask();
            }
        }
    }

    public override void ExitState(WorkerStateManager worker)
    {
        
    }

    public override void CancelAction(WorkerStateManager worker)
    {
        // If worker didn't finish harvesting, tile's task state must be changed since it was not completed
        if (_harvestTimer < _totalHarvestingTime)
        {
            worker.CurrentTask.TaskState = TileStateManager.TaskStates.Harvest;
        }
    }

}
