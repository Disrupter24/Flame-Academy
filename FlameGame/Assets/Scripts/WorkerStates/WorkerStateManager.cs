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
    public bool ForceMove = false;

    // Tasks
    public List<TileStateManager> TaskList = new List<TileStateManager>();
    public TileStateManager CurrentTask;
    public TileStateManager CancelledTask;
    public int CurrentTaskID;

    // Item worker is carrying
    public TileStateManager.ObjectStates HeldItem;

    // Misc. pointers
    private SpriteRenderer _sprite;

    private void Awake()
    {
        // Initial state of worker
        _currentState = IdleState;

        _sprite = gameObject.GetComponent<SpriteRenderer>();
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
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void FindNextTask()
    {
        // Clear current task
        CurrentTask = null;

        Debug.Log("Finding task");

        // While there are tasks on the tasklist, search for nearest tile with a valid task
        while(CurrentTask == null && TaskList.Count > 0)
        {
            // Find nearest tile with a task available
            TileStateManager nearestTile = null;
            float nearestTileDistance = 100000; // Arbitrarily large float
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

            // Check status of tile. If it has a task, set it as the current task
            if (nearestTile.TaskState == TileStateManager.TaskStates.Harvest || nearestTile.TaskState == TileStateManager.TaskStates.Gather || ForceMove)
            {
                CurrentTask = nearestTile;
                CurrentTaskID = TaskList.IndexOf(nearestTile);
            }
            
            // Now that the task has been handled, it's removed from the list
            TaskList.Remove(nearestTile);
            
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

    public void CancelTask()
    {
        _currentState.CancelAction(this);
    }

    public void MoveTowardsEmptyTile()
    {
        Debug.Log("Moving to empty tile");
        ForceMove = true;
        FindNextTask();
    }

    public void CollectItem(TileStateManager.ObjectStates item)
    {
        HeldItem = item;
        // Display visual for item...
    }

    public void DropItem()
    {
        Debug.Log("Dropping item");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, 1 << 7);

        if (hit.collider.gameObject.GetComponent<TileStateManager>() != null)
        {
            Debug.Log("hit tile");
            TileStateManager tile = hit.collider.gameObject.GetComponent<TileStateManager>();
            switch (HeldItem)
            {
                case TileStateManager.ObjectStates.Grass:
                    tile.SwitchObjectState(tile.ObjectGrassState);
                    break;
                case TileStateManager.ObjectStates.Log:
                    tile.SwitchObjectState(tile.ObjectLogState);
                    break;
            }
            HeldItem = TileStateManager.ObjectStates.None;
        }
    }
        

}
