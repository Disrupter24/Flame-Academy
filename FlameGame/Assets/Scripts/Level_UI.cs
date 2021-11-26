using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Level_UI : MonoBehaviour
{
    [SerializeField] public GameObject level_UI;
    [SerializeField] public TextMeshProUGUI text_UI;
    [SerializeField] public Image[] stars;
    [SerializeField] public Sprite selectedStar;
    [SerializeField] public Sprite unselectedStar;
}
