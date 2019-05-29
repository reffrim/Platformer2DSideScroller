using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        HP = 1;
        ObjectScale = 1.2f;
        Collider.radius = 0.19f;
        transform.localScale = new Vector3(ObjectScale, ObjectScale, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementPattern();
        BorderHitCheck(50);
        DestroyOutOfBorders();
    }

    protected override void MovementPattern()
    {
            Body.velocity = new Vector2(1 * Direction, Body.velocity.y);

            if (Body.velocity.y == 0)
                Body.AddForce(new Vector2(0, 300)); 
    }
}
