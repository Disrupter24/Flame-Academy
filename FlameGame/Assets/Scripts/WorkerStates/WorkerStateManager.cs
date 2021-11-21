using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerStateManager : MonoBehaviour
{
    // Used this tutorial for worker state machine:
    // https://www.youtube.com/watch?v=Vt8aZDPzRjI&ab_channel=iHeartGameDev

    // If you need to add a state to workers, create a new class that inherits from WorkerBaseState and insert it here.
    private WorkerBaseState _currentState;
    public WorkerWalkingState WalkingState = new WorkerWalkingState();
    public WorkerGatheringState GatheringState = new WorkerGatheringState();
    public WorkerHarvestingState HarvestingState = new WorkerHarvestingState();
    public WorkerIdleState IdleState = new WorkerIdleState();
    public WorkerMovement workerMovement = new WorkerMovement();

    public bool IsSelected;

    // Tasks
    public List<TileStateManager> TaskList = new List<TileStateManager>();
    public TileStateManager CurrentTask;
    public int CurrentTaskID;

    // Misc. pointers
    private SpriteRenderer _sprite;
    private Camera _camera;

    private void Awake()
    {
        // Initial state of worker
        SwitchState(IdleState);

        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _camera = Camera.main;
    }

    private void Update()
    {
        _currentState.UpdateState(this);

        if (IsSelected)
        {
            _sprite.color = Color.yellow;
        }
        else
        {
            _sprite.color = Color.white;
        }
    }

    public void SwitchState(WorkerBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void FindNextTask()
    {
        // Clear current task
        CurrentTask = null;
        
        // While there are tasks on the tasklist, search for nearest tile with a valid task
        while(CurrentTask == null && TaskList.Count > 0)
        {
            // Find nearest tile with a task available
            TileStateManager nearestTile = null;
            float nearestTileDistance = 100000;
            foreach (TileStateManager tile in TaskList)
            {
                // Check distance to tile
                float tileDistance = Vector2.Distance(tile.transform.position, gameObject.transform.position);

                // Get nearest tile
                if (tileDistance < nearestTileDistance)
                {
                    nearestTile = tile;
                }

            }

            // Check status of tile. If it has a task, set it as the target; otherwise, remove it from the list
            if (nearestTile.TaskState == TileStateManager.TaskStates.Harvest || nearestTile.TaskState == TileStateManager.TaskStates.Gather)
            {
                CurrentTask = nearestTile;
                CurrentTaskID = TaskList.IndexOf(nearestTile);
            }
            else
            {
                // Hopefully this works
                TaskList.Remove(nearestTile);
            }
        }

        // Set worker state based on whether it found a task
        if(CurrentTask != null)
        {
            SwitchState(WalkingState);
        }
        else
        {
            SwitchState(IdleState);
        }
    }

}
