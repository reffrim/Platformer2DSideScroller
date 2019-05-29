using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenCycle : MonoBehaviour
{
    float minBrightness;
    float maxBrightness;
    float cycleSpeed;
    SpriteRenderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        minBrightness = 0.86f;
        maxBrightness = 1;
        cycleSpeed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer.color = Color.Lerp(
            new Color(maxBrightness, maxBrightness, maxBrightness, 1),
            new Color(minBrightness, minBrightness, minBrightness, 1),
            Mathf.PingPong(Time.time * cycleSpeed, maxBrightness)
            );
    }
}
