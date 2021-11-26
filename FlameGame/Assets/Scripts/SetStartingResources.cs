using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetStartingResources : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SetResources(0.01f));
    }

    IEnumerator SetResources(float time)
    {
        yield return new WaitForSeconds(time);
        StorehouseManager.Instance.SetStartingResources(400, 400);

    }
}
