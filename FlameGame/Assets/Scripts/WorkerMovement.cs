using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement
{

    private bool _isMoving;
    private bool _isAtDestination; 
    private Vector2 _pointToMoveTowards;

    // have a data class to hold all this information like speeds and such. 
    private float _movementSpeed = 1.0f;
    private void Start()
    {
        _isMoving = false;
        _isAtDestination = false; 
        _pointToMoveTowards = Vector3.zero;
        
    }
    public Vector3 Move(Vector2 currentPosition)
    {
        float distanceToMove = _movementSpeed * Time.deltaTime;

        if (!_isMoving || _isAtDestination)
        {
            return currentPosition;
        }

        currentPosition = Vector3.MoveTowards(currentPosition, _pointToMoveTowards, distanceToMove);

        if (Vector2.Distance(currentPosition,_pointToMoveTowards) < 0.01)
        {
            if (NavigationGrid.IsEndOfPath())
            {
                _isMoving = false;
                _isAtDestination = true;
            }
            else
            {
                _pointToMoveTowards = NavigationGrid.GetNextNodePosition();
            }

        }
        return currentPosition;
    }


    public bool IsAtDestination()
    {
        return _isAtDestination; 
    }
    public bool MoveTo(Vector2 startPosition, Vector2 targetPosition)
    {
        _isAtDestination = false;
        _isMoving = false;
        _pointToMoveTowards = targetPosition;
        NavigationGrid.CalculatePathToDestination(startPosition, targetPosition);
        if (NavigationGrid.StartNavigation())
        {
            _isMoving = true;
            _pointToMoveTowards = NavigationGrid.GetNodePosition();
            Debug.Log("Path Created");
        }
        return _isMoving;
    }
}
