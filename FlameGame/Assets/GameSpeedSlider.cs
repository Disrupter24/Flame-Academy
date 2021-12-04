using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSlider : MonoBehaviour
{
    private Slider _gameSpeedSlider;

    private void Awake()
    {
        _gameSpeedSlider = gameObject.GetComponent<Slider>();
    }

    public void UpdateSlider()
    {
        Time.timeScale = _gameSpeedSlider.value;
    }
}
