using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private LevelStarter _currentLevel;

    // Singleton pattern
    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                LevelManager singleton = GameObject.FindObjectOfType<LevelManager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }



    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    public void SetCurrentLevel(LevelStarter level)
    {
        _currentLevel = level;

    }

    public LevelStarter GetCurrentLevel()
    {
        return _currentLevel;
    }
}
