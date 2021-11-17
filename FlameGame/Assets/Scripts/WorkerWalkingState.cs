using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerWalkingState : WorkerBaseState
{
    // This function defines the worker's walking behaviour
    // The worker will move towards a target (probably using a navmesh)
    // Upon reaching the target, the worker will enter a new state based on the nature of the target
    // For example, if the target is a tile with a tree, the worker will enter "WorkerChoppingState"

    public override void EnterState(WorkerStateManager worker)
    {
        
    }

    public override void UpdateState(WorkerStateManager worker)
    {
        
    }

    public override void ExitState(WorkerStateManager worker)
    {
        
    }
}
