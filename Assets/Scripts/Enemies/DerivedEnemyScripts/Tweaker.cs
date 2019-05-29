using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweaker : Enemy
{
    public bool TweakOut;

    float Timer;

    // Start is called before the first frame update
    void Start()
    {
        HP = 1;
        ObjectScale = 1;
        Collider.radius = 0.16f;
        Sprite.material.color = new Color(1, 0.8f, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime * 60;

        if (Timer > 30)
        {
            TweakOut = true;
            Timer = 0;
        }
    }

    private void FixedUpdate()
    {
        MovementPattern();
        BorderHitCheck(50);
        DestroyOutOfBorders();
    }

    protected override void MovementPattern()
    {
        if (TweakOut)
        {
            float randomX = Random.Range(-29f, 29f);
            float randomY = Random.Range(-120f, 120f);

            Body.AddForce(new Vector2(randomX * Speed, randomY * Speed));
            TweakOut = false;
        }
    }
}
