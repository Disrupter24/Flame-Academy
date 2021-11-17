using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStateManager : MonoBehaviour
{
    public FireStateManager FireStateManager;
    public SpriteRenderer TileRenderer;
    public SpriteRenderer ObjectRenderer;
    public NavigationNode NavigationNode;
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
    public TileObjectWallState ObjectWallState = new TileObjectWallState();
    protected void Start()
    {
        currentTaskState = TaskEmptyState;// fix this
        currentTaskState.EnterState(this);
        currentObjectState = ObjectEmptyState;//fix this
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
        state.EnterState(this);
    }
    public void SwitchObjectState(TileObjectBaseState state)
    {
        currentObjectState = state;
        state.EnterState(this);
    }
    public void ResetFireProperties()
    {
        FireStateManager.Temperature = 0;
    }
}
