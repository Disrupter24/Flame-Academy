using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    // Startpoint of box selection, on canvas and in world coordinates
    private Vector2 _selectionStartPositionCanvas;
    private Vector2 _selectionStartPositionWorld;
    // Cannot draw selection box while pressing a button
    private bool _canDrawSelectionBox = true;
    // Worker selection box visual
    [SerializeField] private RectTransform _selectionBox;
    // List of all workers currently selected
    private List<WorkerStateManager> _workersSelected = new List<WorkerStateManager>();

    // Same as above, but for target selection box
    private Vector2 _targetingStartPositionCanvas;
    private Vector2 _targetingStartPositionWorld;
    [SerializeField] private RectTransform _targetingBox;

    [SerializeField] private GameObject _treeButton;
    [SerializeField] private GameObject _grassButton;

    private bool _placingFuel = false;
    private TileStateManager.ObjectStates _fuelToPlace;

    private void Start()
    {
        ToggleStorehouseButtons(false);
    }

    private void Update()
    {
        ManageLeftClick();
        ManageRightClick();
    }

    private void ToggleStorehouseButtons(bool isEnabled)
    {
        _treeButton.GetComponent<Button>().enabled = isEnabled;
        _treeButton.GetComponent<Image>().enabled = isEnabled;
        _grassButton.GetComponent<Button>().enabled = isEnabled;
        _grassButton.GetComponent<Image>().enabled = isEnabled;
    }

    public void ButtonWoodPointerDown()
    {
        _canDrawSelectionBox = false;
    }
    public void ButtonWoodOnClick()
    {
        _placingFuel = true;
        _fuelToPlace = TileStateManager.ObjectStates.Log;

        // Clear each selected worker's task list
        // Also mark any tasks in progress as cancelled
        foreach (WorkerStateManager worker in _workersSelected)
        {
            if (worker.TaskList.Count == 0)
            {
                worker.CancelTask();
                worker.ForceMove = false;
            }
            else if (worker.TaskList[0].TaskState != TileStateManager.TaskStates.PlaceFuel)
            {
                worker.TaskList.Clear();
                worker.CancelTask();
                worker.ForceMove = false;
            }
            
        }
    }

    public void ButtonGrassPointerDown()
    {
        _canDrawSelectionBox = false;
    }

    public void ButtonGrassOnClick()
    {
        _placingFuel = true;
        _fuelToPlace = TileStateManager.ObjectStates.Grass;

        // Clear each selected worker's task list
        // Also mark any tasks in progress as cancelled
        foreach (WorkerStateManager worker in _workersSelected)
        {
            if (worker.TaskList.Count == 0)
            {
                worker.CancelTask();
                worker.ForceMove = false;
            }
            else if (worker.TaskList[0].TaskState != TileStateManager.TaskStates.PlaceFuel)
            {
                worker.TaskList.Clear();
                worker.CancelTask();
                worker.ForceMove = false;
            }
        }
    }

    private void ManageLeftClick()
    {
        
        if(_canDrawSelectionBox)
        {
            DrawSelectionBox();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _canDrawSelectionBox = true;
        }
        
    }

    private void DrawSelectionBox()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _placingFuel = false;
            ToggleStorehouseButtons(false);

            // Set selection box start point
            _selectionStartPositionCanvas = Input.mousePosition;
            _selectionStartPositionWorld = GetMousePositionInWorld();
        }

        if (Input.GetMouseButton(0))
        {
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
                    ToggleStorehouseButtons(true);
                }
            }
        }
    }

    private void ManageRightClick()
    {
        if(_workersSelected.Count > 0)
        {
            if (_placingFuel)
            {
                DrawFuel();
            }
            else
            {
                DrawTargetingBox();
            }
        }

        

        // Controls for torch
        // With worker selected, if mousing over tile on fire, cursor becomes a torch
        // Right click to have the worker immolate itself

        // Special case:
        // To use storehouse resources, select worker, then select resource button, then paint fuel

        
    }

    private void DrawFuel()
    {
        
        if (Input.GetMouseButton(1) && StorehouseManager.Instance.CheckRemainingFuel(_fuelToPlace) > StorehouseManager.Instance.CheckGhostFuel(_fuelToPlace))
        {
            RaycastHit2D hit = Physics2D.Raycast(GetMousePositionInWorld(), Vector2.zero, 0, 1 << 7);

            if (hit.collider.gameObject.GetComponent<TileStateManager>() != null)
            {
                TileStateManager tile = hit.collider.gameObject.GetComponent<TileStateManager>();

                if(tile.currentObjectState == tile.ObjectEmptyState)
                {
                    tile.ToggleGhost(true, _fuelToPlace);

                    switch (_fuelToPlace)
                    {
                        case TileStateManager.ObjectStates.Log:
                            tile.SwitchObjectState(tile.ObjectLogState);
                            break;
                        case TileStateManager.ObjectStates.Grass:
                            tile.SwitchObjectState(tile.ObjectGrassState);
                            break;
                    }

                    foreach (WorkerStateManager worker in _workersSelected)
                    {
                        // Add tile to each worker's task list
                        worker.TaskList.Add(hit.collider.gameObject.GetComponent<TileStateManager>());
                    }
                }

                
                
            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            foreach (WorkerStateManager worker in _workersSelected)
            {
                
                // Send the worker on their task
                if(worker.CurrentTask == null)
                {
                    worker.FindNextTask();
                }
                
            }
        }
    }

    private void DrawTargetingBox()
    {
        if (Input.GetMouseButtonDown(1))
        {
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

            // If the worker is currently placing fuel, cancel fuel tasks and remove ghosts
            // FEATURE TO ADD: worker does not drop item if the player instructs them to visit a storehouse
            foreach (WorkerStateManager worker in _workersSelected)
            {
                worker.PlacingFuel = false;

                if(worker.CurrentTask != null)
                {
                    if (worker.CurrentTask.TaskState == TileStateManager.TaskStates.PlaceFuel)
                    {
                        switch (worker.CurrentTask.ObjectState)
                        {
                            case TileStateManager.ObjectStates.Log:
                                StorehouseManager.GhostLogs -= 1;
                                break;
                            case TileStateManager.ObjectStates.Grass:
                                StorehouseManager.GhostGrass -= 1;
                                break;
                        }
                        worker.CurrentTask.ToggleGhost(false, TileStateManager.ObjectStates.None);
                        worker.DropItem();
                    }
                }

                if(worker.TaskList.Count > 0)
                {
                    if (worker.TaskList[0].TaskState == TileStateManager.TaskStates.PlaceFuel)
                    {
                        foreach (TileStateManager tile in worker.TaskList)
                        {
                            switch (tile.ObjectState)
                            {
                                case TileStateManager.ObjectStates.Log:
                                    StorehouseManager.GhostLogs -= 1;
                                    break;
                                case TileStateManager.ObjectStates.Grass:
                                    StorehouseManager.GhostGrass -= 1;
                                    break;
                            }

                            tile.ToggleGhost(false, TileStateManager.ObjectStates.None);
                            
                        }
                    }
                }

                // Clear each selected worker's task list
                // Also mark any tasks in progress as cancelled
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
                if (tile != null && tile.TaskState == TileStateManager.TaskStates.Harvest || tile.TaskState == TileStateManager.TaskStates.Gather)
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
                if (foundTasks)
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
    private Vector2 GetMousePositionInWorld()
    {
        // Converts screen position of a click to world position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePosition.x, mousePosition.y);
    }
}