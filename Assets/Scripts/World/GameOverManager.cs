using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    GameStartManager reStart;
    SpriteRenderer GameOverRenderer;
    public static bool IsGameOver;

    float Accumulator;
    // Start is called before the first frame update
    void Start()
    {
        GameOverRenderer = GetComponent<SpriteRenderer>();
        reStart = GetComponentInChildren<GameStartManager>();
        IsGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            reStart.enabled = true;
            GameOverRenderer.color = Color.Lerp(GameOverRenderer.color, new Color(1, 1, 1, 0.8f), 0.5f * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), 0.05f * Time.deltaTime * 60);
        }
    }
}