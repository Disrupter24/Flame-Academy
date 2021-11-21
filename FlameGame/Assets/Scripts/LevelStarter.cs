using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelStarter : MonoBehaviour
{
    public GameObject NextUpLevel;
    public GameObject NextDownLevel;
    public GameObject NextLeftLevel;
    public GameObject NextRightLevel;

    [SerializeField] private Transform _player;
    [SerializeField] private float speed;

    public bool IsComplete;
    public bool IsMovingUp;
    public bool IsMovingDown;
    public bool IsMovingLeft;
    public bool IsMovingRight;
    

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && NextUpLevel != null && (IsComplete || NextUpLevel.GetComponent<LevelStarter>().IsComplete))
        {
            IsMovingUp = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && NextLeftLevel != null && (IsComplete || NextLeftLevel.GetComponent<LevelStarter>().IsComplete))
        {
            IsMovingLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && NextDownLevel != null && (IsComplete || NextDownLevel.GetComponent<LevelStarter>().IsComplete))
        {
            IsMovingDown = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && NextRightLevel != null && (IsComplete || NextRightLevel.GetComponent<LevelStarter>().IsComplete))
        {
            IsMovingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(gameObject.name);
        }
        HandleMovement();
    }
    private void HandleMovement()
    {
        if (IsMovingUp)
        {
            if (_player.transform.position != NextUpLevel.transform.position)
            {
                Move(NextUpLevel);
            }
            else
            {
                IsMovingUp = false;
                ChangeCurrentLevel(NextUpLevel);
            }            
        }
        if (IsMovingLeft)
        {
            if (_player.transform.position != NextLeftLevel.transform.position)
            {
                Move(NextLeftLevel);
            }
            else
            {
                IsMovingLeft = false;
                ChangeCurrentLevel(NextLeftLevel);
            }
        }
        if (IsMovingDown)
        {
            if (_player.transform.position != NextDownLevel.transform.position)
            {
                Move(NextDownLevel);
            }
            else
            {
                IsMovingDown = false;
                ChangeCurrentLevel(NextDownLevel);
            }
        }
        if (IsMovingRight)
        {
            if (_player.transform.position != NextRightLevel.transform.position)
            {
                Move(NextRightLevel);
            }
            else
            {
                IsMovingRight = false;
                ChangeCurrentLevel(NextRightLevel);
            }
        }
    }
    private void Move(GameObject target)
    {
        _player.transform.position = Vector3.MoveTowards(_player.transform.position, target.transform.position, speed * Time.deltaTime);
    }
    private void ChangeCurrentLevel(GameObject target)
    {
        gameObject.GetComponent<LevelStarter>().enabled = false;
        target.GetComponent<LevelStarter>().enabled = true;
    }
}
