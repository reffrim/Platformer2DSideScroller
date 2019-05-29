using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torque : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectScale = 1;
        HP = 2;
        Collider.radius = 0.19f;
    }

    private void FixedUpdate()
    {
        MovementPattern();
        BorderHitCheck(80);
        DestroyOutOfBorders();
    }

     protected override void MovementPattern()
    {
        if (HP > 0)
        {
            if (Body.velocity.y < 0)
                Body.AddTorque((float)Speed / 2 * Direction);

            else if (Body.velocity.y == 0)
                Body.AddForce(new Vector2(0, 100)); 
        }
    }
}
