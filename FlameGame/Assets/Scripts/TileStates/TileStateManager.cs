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

    [HideInInspector]
    public bool IsGhost = false; // Indicates that a worker intends to place fuel here

    [HideInInspector]
    public enum TaskStates //Information for workers
    {
        Harvest,
        Gather,
        PlaceFuel,
        Storehouse,
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
        SetStartingState();
        currentObjectState.EnterState(this);
    }
    protected void Update()
    {
        currentObjectState.UpdateState(this);
    }

    public void SwitchObjectState(TileObjectBaseState state)
    {
        currentObjectState = state;
        UpdateEnumState();
        state.EnterState(this);
    }
    public void ResetProperties()
    {
        FireStateManager.Temperature = 0;
        ObjectRenderer.color = new Color(1, 1, 1, 1); // Resets the colour to white (for perfect sprite display)
    }
    public void UpdateEnumState()
    {
        
        if (currentObjectState == ObjectGoalpostState)
        {
            ObjectState = ObjectStates.Goalpost;
            TaskState = TaskStates.None;
        }
        else if (currentObjectState == ObjectGrassState)
        {
            ObjectState = ObjectStates.Grass;

            if (IsGhost)
                TaskState = TaskStates.PlaceFuel;
            else
                TaskState = TaskStates.Gather;
        }
        else if (currentObjectState == ObjectLogState)
        {
            ObjectState = ObjectStates.Log;

            if (IsGhost)
                TaskState = TaskStates.PlaceFuel;
            else
                TaskState = TaskStates.Gather;
        }
        else if (currentObjectState == ObjectTreeState)
        {
            ObjectState = ObjectStates.Tree;
            TaskState = TaskStates.Harvest;
        }
        else if (currentObjectState == ObjectStoreState)
        {
            ObjectState = ObjectStates.Storehouse;
            TaskState = TaskStates.Storehouse;
        }
        else if (currentObjectState == ObjectWallState)
        {
            ObjectState = ObjectStates.Wall;
            TaskState = TaskStates.None;
        }
        else if (currentObjectState == ObjectBrazierState)
        {
            ObjectState = ObjectStates.Brazier;
            TaskState = TaskStates.Burning;
        }
        else if (currentObjectState == ObjectEmptyState)
        {
            ObjectState = ObjectStates.None;
            TaskState = TaskStates.None;
        }


    }
    public void SetStartingState()
    {
        if (ObjectState == ObjectStates.Goalpost)
        {
            currentObjectState = ObjectGoalpostState;
            TaskState = TaskStates.None;
        }   
        else if (ObjectState == ObjectStates.Grass)
        {
            currentObjectState = ObjectGrassState;
            TaskState = TaskStates.Gather;
        }
        else if (ObjectState == ObjectStates.Log)
        {
            currentObjectState = ObjectLogState;
            TaskState = TaskStates.Gather;
        }
        else if (ObjectState == ObjectStates.Tree)
        {
            currentObjectState = ObjectTreeState;
            TaskState = TaskStates.Harvest;
        }
        else if (ObjectState == ObjectStates.Storehouse)
        {
            currentObjectState = ObjectStoreState;
            TaskState = TaskStates.Storehouse;
        }
        else if (ObjectState == ObjectStates.Wall)
        {
            currentObjectState = ObjectWallState;
            TaskState = TaskStates.None;
        }
        else if (ObjectState == ObjectStates.Brazier)
        {
            currentObjectState = ObjectBrazierState;
            TaskState = TaskStates.Burning;
        }
        else if (ObjectState == ObjectStates.None)
        {
            currentObjectState = ObjectEmptyState;
            TaskState = TaskStates.None;
        }

    }
}
