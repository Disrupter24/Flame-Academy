using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenEnterWinLevel : MonoBehaviour
{
    public LayerMask _playerLayer;
    public TileStateManager _tileUnderneath; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_playerLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            _tileUnderneath.WinScreen.SetActive(true);
            Level level = LevelManager.Instance.GetCurrentLevel().GetLevelInfo();
            level.IsComplete = true;
            level.Star1 = true;
            level.Star2 = true;
            level.Star3 = true;
            level.starCount = 3;
        }
    }
}
