using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    void Update()
    {
        Vector3 position = Input.mousePosition;
        position.z = Camera.main.transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }
}
