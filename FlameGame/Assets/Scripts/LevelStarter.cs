using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelStarter : MonoBehaviour
{
    [Header("Level info")]
    public Level level;
    //public bool IsComplete;
    //[SerializeField] private string _levelName = "[level name]";
    // bool Star1, Star2, Star3

    [Header("Other levels")]
    public GameObject NextUpLevel;
    public GameObject NextDownLevel;
    public GameObject NextLeftLevel;
    public GameObject NextRightLevel;

    [Header("UI Handler")]
    /*[SerializeField] private GameObject level_UI;
    [SerializeField] private TextMeshProUGUI text_UI;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite selectedStar;
    [SerializeField] private Sprite unselectedStar;*/
    [SerializeField] private Level_UI level_UI;

    [Header("Player Movement")]
    [SerializeField] private PlayerMovement_WM _player;
    
    private bool IsMovingUp;
    private bool IsMovingDown;
    private bool IsMovingLeft;
    private bool IsMovingRight;
    

    private void Start()
    {
        ChangeCurrentLevel(gameObject);
        if (LevelManager.Instance.GetCurrentLevel().gameObject != gameObject)
        {
            ChangeCurrentLevel(LevelManager.Instance.GetCurrentLevel().gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && NextUpLevel != null && (level.IsComplete || NextUpLevel.GetComponent<LevelStarter>().level.IsComplete))
        {
            IsMovingUp = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && NextLeftLevel != null && (level.IsComplete || NextLeftLevel.GetComponent<LevelStarter>().level.IsComplete))
        {
            IsMovingLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && NextDownLevel != null && (level.IsComplete || NextDownLevel.GetComponent<LevelStarter>().level.IsComplete))
        {
            IsMovingDown = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && NextRightLevel != null && (level.IsComplete || NextRightLevel.GetComponent<LevelStarter>().level.IsComplete))
        {
            IsMovingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(gameObject.name);
            LevelManager.Instance.SetCurrentLevel(gameObject.GetComponent<LevelStarter>());
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (IsMovingUp)
        {
            if (_player.gameObject.transform.position != NextUpLevel.transform.position)
            {
                Move(NextUpLevel);
                level_UI.level_UI.SetActive(false);
            }
            else
            {
                IsMovingUp = false;
                ChangeCurrentLevel(NextUpLevel);
            }            
        }
        if (IsMovingLeft)
        {
            if (_player.gameObject.transform.position != NextLeftLevel.transform.position)
            {
                Move(NextLeftLevel);
                level_UI.level_UI.SetActive(false);
            }
            else
            {
                IsMovingLeft = false;
                ChangeCurrentLevel(NextLeftLevel);
            }
        }
        if (IsMovingDown)
        {
            if (_player.gameObject.transform.position != NextDownLevel.transform.position)
            {
                Move(NextDownLevel);
                level_UI.level_UI.SetActive(false);
            }
            else
            {
                IsMovingDown = false;
                ChangeCurrentLevel(NextDownLevel);
            }
        }
        if (IsMovingRight)
        {
            if (_player.gameObject.transform.position != NextRightLevel.transform.position)
            {
                Move(NextRightLevel);
                level_UI.level_UI.SetActive(false);
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
        _player.gameObject.transform.position = Vector3.MoveTowards(_player.gameObject.transform.position, target.transform.position, _player.speed * Time.deltaTime);
    }
    private void ChangeCurrentLevel(GameObject target)
    {   
        LevelStarter ls = target.GetComponent<LevelStarter>();
        gameObject.GetComponent<LevelStarter>().enabled = false;
        ls.enabled = true;

        //update UI
        for (int i = 0; i < 3; i++)
        {
            level_UI.stars[i].sprite = level_UI.unselectedStar;
        }
        for (int i = 0; i < ls.level.starCount; i++)
        {
            level_UI.stars[i].sprite = level_UI.selectedStar;
        }
        level_UI.text_UI.text = ls.level.LevelName;
        level_UI.level_UI.SetActive(true);
    }

    public Level GetLevelInfo()
    {
        return level;
    }
}