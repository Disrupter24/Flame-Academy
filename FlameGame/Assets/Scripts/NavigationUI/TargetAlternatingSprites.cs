using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAlternatingSprites : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeItTakesToAlternate = 0.1f;
    private float timeSinceLastAlternation = 0.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAlternation += Time.deltaTime;
        if (timeSinceLastAlternation > timeItTakesToAlternate)
        {
            timeSinceLastAlternation = 0.0f;
            Alternate();
        }

    }

    private void Alternate()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRenderer;
            spriteRenderer = child.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }
}
