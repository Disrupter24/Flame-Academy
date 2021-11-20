using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : MonoBehaviour
{

    private bool _isMoving;
    private Vector3 _pointToMoveTowards;
    [SerializeField]
    private float _movementSpeed;
    private void Start()
    {
        _isMoving = false;
        _pointToMoveTowards = Vector3.zero;
        
    }

    private void Update()
    {
        float distanceToMove = _movementSpeed * Time.deltaTime;
        if (!_isMoving)
        {
            return;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointToMoveTowards, distanceToMove);
        }
        if (transform.position == _pointToMoveTowards)
            _isMoving = false;

    }
    public void MoveTo(Vector3 targetPosition)
    {
        _pointToMoveTowards = targetPosition;
        _isMoving = true;
    }
}
