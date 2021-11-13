using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerStateManager : MonoBehaviour
{
    // Used this tutorial for worker state machine:
    // https://www.youtube.com/watch?v=Vt8aZDPzRjI&ab_channel=iHeartGameDev

    // If you need to add a state to workers, create a new class that inherits from WorkerBaseState and insert it here.
    private WorkerBaseState _currentState;
    public WorkerChoppingState ChoppingState = new WorkerChoppingState();
    public WorkerWalkingState WalkingState = new WorkerWalkingState();
    public WorkerIdleState IdleState = new WorkerIdleState();

    public bool IsSelected;

    // tasks (might need to be a list if we're stacking actions)
    public List<Tile> TaskList = new List<Tile>();

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

    public void SetTarget(Vector2 targetPosition)
    {
        // Here we're going to have logic around what happens when the player right clicks on a tile

        // The Vector2 passed to this function should be the worldposition of the target (ClickManager.GetMousePositionInWorld() takes care of this)
        // We use this Vector2 to find the tile at the target location with a raycast

        //// Set target to the tile at the location
        // Layermask is the Tile layer, it needs to be layer#7 for this to work
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, (1 << 7));

        if (hit.collider != null)
        {
            Debug.Log("Found target");
        }

        // For now, in terms of states, we're just going to focus on movement
        // Might want a navmesh in the near future
        SwitchState(WalkingState);
    }

    public void SwitchState(WorkerBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
