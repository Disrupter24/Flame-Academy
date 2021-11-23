using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "FlameGame/Level")]
public class Level : ScriptableObject
{
    public string LevelName;
    public bool IsComplete;
    public bool Star1;
    public bool Star2;
    public bool Star3;
    public int starCount;
}
