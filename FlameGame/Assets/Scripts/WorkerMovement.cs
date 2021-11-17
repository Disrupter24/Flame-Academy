using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement
{

    private bool _isMoving;
    private bool _isAtDestination; 
    private Vector2 _pointToMoveTowards;
    private NavigationPath _path; 

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

        if (currentPosition == _pointToMoveTowards)
        {
            if (_path.IsAtEndOfPath())
            {
                _isMoving = false;
                _isAtDestination = true;
                Debug.Log("worker final position " + currentPosition);
            }
            else
            {
                _pointToMoveTowards = _path.GetNextNodePosition();
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
        _path = NavigationGrid.CalculatePathToDestination(startPosition, targetPosition);
        if (_path != null)
        {
            _isMoving = true;
            _pointToMoveTowards = _path.GetNextNodePosition();
            Debug.Log("Path Created");
        }
        return _isMoving;
    }
}
