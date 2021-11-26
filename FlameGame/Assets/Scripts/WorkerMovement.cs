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
    private GameObject _destinationMarker;
    private GameObject _targetMarker; 
    // have a data class to hold all this information like speeds and such. 
    private float _movementSpeed = 8.0f;
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
    
    private void WorkerOnNewPath(Vector2 position)
    {
        if (_destinationMarker != null) NavigationGrid.Instance.DestroyMarker(_destinationMarker);
        _destinationMarker = null;
        if (_targetMarker != null) NavigationGrid.Instance.DestroyMarker(_targetMarker);
        _targetMarker = null;
        if (_path != null)
        {
            _path.RemoveWorkerFromLastNode();
        } else
        {
            NavigationGrid.RemoveWorkerFromNode(position.x, position.y);
        }
    }
    private void WorkerPathOnComplete()
    {
        _isMoving = false;
        _isAtDestination = true;
        if (_destinationMarker != null) NavigationGrid.Instance.DestroyMarker(_destinationMarker);
        _destinationMarker = null;
        if (_targetMarker != null) NavigationGrid.Instance.DestroyMarker(_targetMarker);
        _targetMarker = null;
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
            //worker.gameObject.GetComponent<SoundEffects>().PlayGoTo();

            if (tempPath.IsAtEndOfPath())
            {
                WorkerPathOnComplete();

            }  else
            {

                WorkerOnNewPath(startPosition);

                _isAtDestination = false;
                newPathCreatedSuccessfully = true;
                _isMoving = true;
                _path = tempPath;
                _pointToMoveTowards = _path.GetNextNodePosition();
                _destinationMarker = NavigationGrid.Instance.SetDestinationMarker(_path.GetFinalPosition());
                _targetMarker = NavigationGrid.Instance.SetTargetMarker(targetPosition);
                _path.OccupyLastNodeWithWorker(worker);
            }

        } else
        {
            worker.gameObject.GetComponent<SoundEffects>().PlayCannotGoTo();
        }

        return newPathCreatedSuccessfully;        

    }
}
