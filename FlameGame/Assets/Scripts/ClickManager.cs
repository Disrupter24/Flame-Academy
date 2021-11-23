using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Startpoint of box selection, on canvas and in world coordinates
    private Vector2 _selectionStartPositionCanvas;
    private Vector2 _selectionStartPositionWorld;
    // Worker selection box visual
    [SerializeField] private RectTransform _selectionBox;
    // List of all workers currently selected
    private List<WorkerStateManager> _workersSelected = new List<WorkerStateManager>();

    // Same as above, but for target selection box
    private Vector2 _targetingStartPositionCanvas;
    private Vector2 _targetingStartPositionWorld;
    [SerializeField] private RectTransform _targetingBox;
    private bool _drawingTargetingBox;

    private void Update()
    {
        ManageLeftClick();
        ManageRightClick();
    }

    private void ManageLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // CHECK IF CLICKING A STOREHOUSE BUTTON
            // IF SO, DO NOT CREATE A SELECTION BOX

            // Set selection box start point
            _selectionStartPositionCanvas = Input.mousePosition;
            _selectionStartPositionWorld = GetMousePositionInWorld();
        }

        if (Input.GetMouseButton(0))
        {
            // ONLY DO THIS STUFF WHEN NOT IN STOREHOUSE MODE
            // Display selection box according to selection start position and current mouse position
            if (!_selectionBox.gameObject.activeInHierarchy)
            {
                _selectionBox.gameObject.SetActive(true);
            }

            Vector2 currentMousePosition = Input.mousePosition;
            float boxWidth = currentMousePosition.x - _selectionStartPositionCanvas.x;
            float boxHeight = currentMousePosition.y - _selectionStartPositionCanvas.y;

            _selectionBox.sizeDelta = new Vector2(Mathf.Abs(boxWidth), Mathf.Abs(boxHeight));
            _selectionBox.anchoredPosition = _selectionStartPositionCanvas + new Vector2(boxWidth / 2, boxHeight / 2);
        }

        if (Input.GetMouseButtonUp(0))
        {
            // IF CLICKING A STOREHOUSE BUTTON, GAIN THE ABILITY TO PAINT FUEL

            // Hide selection box
            _selectionBox.gameObject.SetActive(false);

            // Deselect currently selected workers
            foreach (WorkerStateManager worker in _workersSelected)
            {
                worker.IsSelected = false;
            }
            // Clear selected workers list
            _workersSelected.Clear();

            // Set selection box end point
            Vector3 currentMousePosition = GetMousePositionInWorld();

            // Get all workers in box, let them know they're selected, and add them to the workers selected list
            foreach (Collider2D workerCollider in Physics2D.OverlapAreaAll(_selectionStartPositionWorld, currentMousePosition, 1 << 6))
            {
                WorkerStateManager worker = workerCollider.gameObject.GetComponent<WorkerStateManager>();
                if (worker != null)
                {
                    _workersSelected.Add(worker);
                    worker.IsSelected = true;
                    // DISPLAY BUTTONS FOR STOREHOUSE COMMANDS
                }
            }
        }


    }

    private void ManageRightClick()
    {

        // Controls for torch
        // With worker selected, if mousing over tile on fire, cursor becomes a torch
        // Right click to have the worker immolate itself

        // Special case:
        // To use storehouse resources, select worker, then select resource button, then paint fuel

        if (_workersSelected.Count > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                // if(clicking on storehouse button)
                // Set selection box start point
                _targetingStartPositionCanvas = Input.mousePosition;
                _targetingStartPositionWorld = GetMousePositionInWorld();
            }

            if (Input.GetMouseButton(1))
            {
                // Display selection box according to selection start position and current mouse position
                if (!_targetingBox.gameObject.activeInHierarchy)
                {
                    _targetingBox.gameObject.SetActive(true);
                }

                Vector2 currentMousePosition = Input.mousePosition;
                float boxWidth = currentMousePosition.x - _targetingStartPositionCanvas.x;
                float boxHeight = currentMousePosition.y - _targetingStartPositionCanvas.y;

                _targetingBox.sizeDelta = new Vector2(Mathf.Abs(boxWidth), Mathf.Abs(boxHeight));
                _targetingBox.anchoredPosition = _targetingStartPositionCanvas + new Vector2(boxWidth / 2, boxHeight / 2);
            }

            if (Input.GetMouseButtonUp(1))
            {
                // Hide selection box
                _targetingBox.gameObject.SetActive(false);

                // Set selection box end point
                Vector3 currentMousePosition = GetMousePositionInWorld();

                // Clear each selected worker's task list
                // Also mark any tasks in progress as cancelled
                foreach (WorkerStateManager worker in _workersSelected)
                {
                    worker.TaskList.Clear();
                    worker.CancelTask();
                    worker.ForceMove = false;
                }

                bool foundTasks = false;

                // Get all tiles in box, check their contents, and add them to the tiles selected list
                foreach (Collider2D tileCollider in Physics2D.OverlapAreaAll(_targetingStartPositionWorld, currentMousePosition, 1 << 7))
                {
                    TileStateManager tile = tileCollider.gameObject.GetComponent<TileStateManager>();

                    
                    // Check if tile has an eligible task (with specialized workers, might need to do this on a per-worker basis)
                    if (tile != null  && tile.TaskState == TileStateManager.TaskStates.Harvest || tile.TaskState == TileStateManager.TaskStates.Gather)
                    {
                        foundTasks = true;
                        foreach (WorkerStateManager worker in _workersSelected)
                        {
                            // Add tile to each worker's task list
                            worker.TaskList.Add(tile);
                        }
                        
                    }
                    
                    
                }

                foreach (WorkerStateManager worker in _workersSelected)
                {
                    if(foundTasks)
                    {
                        // Worker drops item
                        worker.DropItem();
                        // Send the worker on their task
                        worker.FindNextTask();
                    }
                    else
                    {
                        RaycastHit2D hit = Physics2D.Raycast(currentMousePosition, Vector2.zero, 0, 1 << 7);

                        if (hit.collider.gameObject.GetComponent<TileStateManager>() != null)
                        {
                            // Add tile to each worker's task list
                            worker.TaskList.Add(hit.collider.gameObject.GetComponent<TileStateManager>());
                            worker.MoveTowardsEmptyTile();
                        }
                    }
                }

            }


        }
    }

    private Vector2 GetMousePositionInWorld()
    {
        // Converts screen position of a click to world position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePosition.x, mousePosition.y);
    }
}