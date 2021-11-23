using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFire : MonoBehaviour
{

    public GameObject FirePrefab;

    private void Start()
    {
        FirePrefab = Instantiate(FirePrefab,FindObjectOfType<Canvas>().transform);
    }


    private void Update()
    {
        FirePrefab.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3 (0,0.1f,0));

    }
}
