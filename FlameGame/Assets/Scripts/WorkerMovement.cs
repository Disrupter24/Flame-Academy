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
    private GameObject _marker; 

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
                WorkerPathOnComplete();
            }
            else
            {
                _pointToMoveTowards = _path.GetNextNodePosition();
            }

        }
        return currentPosition;
    }

    private void WorkerPathOnComplete()
    {
        _isMoving = false;
        _isAtDestination = true;
        if (_marker != null) NavigationGrid.Instance.DestroyMarker(_marker);
        _marker = null;
        if (_path != null)
        _path.RemoveWorkerFromLastNode();
        _path = null;
    }
    public bool IsAtDestination()
    {
        return _isAtDestination; 
    }
    public bool MoveTo(WorkerStateManager worker, Vector2 startPosition, Vector2 targetPosition)
    {
        bool newPathCreatedSuccessfully = false; 
        NavigationPath tempPath = NavigationGrid.CalculatePathToDestination(startPosition, targetPosition);
        if (tempPath != null)
        {
            if(_path!= null)
            {
                WorkerPathOnComplete();
            }
            _path = tempPath;
            if (_path.IsAtEndOfPath())
            {
                WorkerPathOnComplete();

            }  else
            {
                _isAtDestination = false;
                newPathCreatedSuccessfully = true;
                _isMoving = true;
                _pointToMoveTowards = _path.GetNextNodePosition();
                _marker = NavigationGrid.Instance.SetDestinationMarker(_path.GetFinalPosition());
                _path.OccupyLastNodeWithWorker(worker);
            }

        }

        return newPathCreatedSuccessfully;        

    }
}
