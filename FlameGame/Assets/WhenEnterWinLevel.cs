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
        }
    }
}
