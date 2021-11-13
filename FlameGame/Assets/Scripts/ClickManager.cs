using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Startpoint of box selection, on canvas and in world coordinates
    private Vector2 _selectionStartPositionCanvas;
    private Vector2 _selectionStartPositionWorld;
    // Selection box visual
    [SerializeField] private RectTransform _selectionBox;
    // List of all workers currently selected
    private List<WorkerStateManager> _workersSelected = new List<WorkerStateManager>();

    private void Update()
    {
        ManageWorkerSelection();
        ManageTargetSelection();
    }

    private void ManageWorkerSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
                }
            }
        }


    }

    private void ManageTargetSelection()
    {
        // For now, this gives a worker a single target when the player right clicks on something
        // If we want to queue multiple tasks, this will get more complex

        if (Input.GetMouseButtonDown(1))
        {
            foreach (WorkerStateManager worker in _workersSelected)
            {
                worker.SetTarget(Input.mousePosition);
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