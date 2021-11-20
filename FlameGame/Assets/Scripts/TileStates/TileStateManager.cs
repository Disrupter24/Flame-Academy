using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStateManager : MonoBehaviour
{
    public FireStateManager FireStateManager;
    public SpriteRenderer TileRenderer;
    public SpriteRenderer ObjectRenderer;
    public NavigationNode NavigationNode;
    public Sprite[] ObjectSpriteSheet;
    public bool WillCollide;
    public enum TaskStates //Information for workers
    {
        Harvest,
        Gather,
        Burning,
        None
    }
    public TaskStates TaskState;
    public enum ObjectStates //Different objects a tile can contain
    {
        Goalpost,
        Grass,
        Log,
        Tree,
        Storehouse,
        Brazier,
        Wall,
        None //General object without specific properties, cannot be walked through or set on fire (good for water / rocks), general hard limits.
    }
    public ObjectStates ObjectState;
    /* public enum TileStates //The aestetic state of a tile (what tilemap it's using)
     {
         //Art stuff, will be fleshed out later.
     }
     public TileStates TileState;
    */
    // Task States
    public TileTaskBaseState currentTaskState;
    public TileTaskHarvestState TaskHarvestState = new TileTaskHarvestState();
    public TileTaskGatherState TaskGatherState = new TileTaskGatherState();
    public TileTaskEmptyState TaskEmptyState = new TileTaskEmptyState();
    public TileTaskBurningState TaskBurningState = new TileTaskBurningState();
    // Object States
    public TileObjectBaseState currentObjectState;
    public TileObjectEmptyState ObjectEmptyState = new TileObjectEmptyState();
    public TileObjectGoalpostState ObjectGoalpostState = new TileObjectGoalpostState();
    public TileObjectGrassState ObjectGrassState = new TileObjectGrassState();
    public TileObjectLogState ObjectLogState = new TileObjectLogState();
    public TileObjectTreeState ObjectTreeState = new TileObjectTreeState();
    public TileObjectStoreState ObjectStoreState = new TileObjectStoreState();
    public TileObjectBrazierState ObjectBrazierState = new TileObjectBrazierState();
    public TileObjectWallState ObjectWallState = new TileObjectWallState();
    protected void Start()
    {
        SetStartingState("Task");
        SetStartingState("Object");
        currentTaskState.EnterState(this);
        currentObjectState.EnterState(this);
    }
    protected void Update()
    {
        currentTaskState.UpdateState(this);
        currentObjectState.UpdateState(this);
    }
    public void SwitchTaskState(TileTaskBaseState state)
    {
        currentTaskState = state;
        UpdateEnumState("Task");
        state.EnterState(this);
    }
    public void SwitchObjectState(TileObjectBaseState state)
    {
        currentObjectState = state;
        UpdateEnumState("Object");
        state.EnterState(this);
    }
    public void ResetFireProperties()
    {
        FireStateManager.Temperature = 0;
    }
    public void UpdateEnumState(string type)
    {
        if (type == "Object")
        {
            if (currentObjectState == ObjectGoalpostState)
                ObjectState = ObjectStates.Goalpost;
            else if (currentObjectState == ObjectGrassState)
                ObjectState = ObjectStates.Grass;
            else if (currentObjectState == ObjectLogState)
                ObjectState = ObjectStates.Log;
            else if (currentObjectState == ObjectTreeState)
                ObjectState = ObjectStates.Tree;
            else if (currentObjectState == ObjectStoreState)
                ObjectState = ObjectStates.Storehouse;
            else if (currentObjectState == ObjectWallState)
                ObjectState = ObjectStates.Wall;
            else if (currentObjectState == ObjectBrazierState)
                ObjectState = ObjectStates.Brazier;
            else if (currentObjectState == ObjectEmptyState)
                ObjectState = ObjectStates.None;
        }
        else if (type == "Task")
        {
            if (currentTaskState == TaskHarvestState)
                TaskState = TaskStates.Harvest;
            else if (currentTaskState == TaskGatherState)
                TaskState = TaskStates.Gather;
            else if (currentTaskState == TaskEmptyState)
                TaskState = TaskStates.None;
            else if (currentTaskState == TaskBurningState)
                TaskState = TaskStates.Burning;
        }
    }
    public void SetStartingState(string type)
    {
        if (type == "Object")
        {
            if (ObjectState == ObjectStates.Goalpost)
                currentObjectState = ObjectGoalpostState;
            else if (ObjectState == ObjectStates.Grass)
                currentObjectState = ObjectGrassState;
            else if (ObjectState == ObjectStates.Log)
                currentObjectState = ObjectLogState;
            else if (ObjectState == ObjectStates.Tree)
                currentObjectState = ObjectTreeState;
            else if (ObjectState == ObjectStates.Storehouse)
                currentObjectState = ObjectStoreState;
            else if (ObjectState == ObjectStates.Wall)
                currentObjectState = ObjectWallState;
            else if (ObjectState == ObjectStates.Brazier)
                currentObjectState = ObjectBrazierState;
            else if (ObjectState == ObjectStates.None)
                currentObjectState = ObjectEmptyState;
        }
        else if (type == "Task")
        {
            if (TaskState == TaskStates.Harvest)
                currentTaskState = TaskHarvestState;
            else if (TaskState == TaskStates.Gather)
                currentTaskState = TaskGatherState;
            else if (TaskState == TaskStates.None)
                currentTaskState = TaskEmptyState;
            else if (TaskState == TaskStates.Burning)
                currentTaskState = TaskBurningState;
        }
    }
}
