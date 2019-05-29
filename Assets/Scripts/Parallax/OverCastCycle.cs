using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverCastCycle : MonoBehaviour
{
    SpriteRenderer Renderer;
    float minOpacity;
    float maxOpacity;
    float cycleSpeed;

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        minOpacity = 0.6f;
        maxOpacity = 1;
        cycleSpeed = 0.05f;
    }

    private void Update()
    {
        Renderer.color = Color.Lerp(
            new Color(1, 1, 1, maxOpacity),
            new Color(1, 1, 1, minOpacity),
            Mathf.PingPong(Time.time * cycleSpeed, maxOpacity)
            );
    }
}
