using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAchievementsManager : MonoBehaviour
{
    [SerializeField] Level level;
    // Update is called once per frame
    void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            level.IsComplete = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) && !level.Star1)
        {
            level.Star1 = true;
            level.starCount++;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && !level.Star2)
        {
            level.Star2 = true;
            level.starCount++;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) && !level.Star3)
        {
            level.Star3 = true;
            level.starCount++;
        }
    }
}
