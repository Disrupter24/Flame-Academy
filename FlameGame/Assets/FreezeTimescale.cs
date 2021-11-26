using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTimescale : MonoBehaviour
{
    // Freeze game on awake
    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
