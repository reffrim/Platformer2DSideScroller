using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gigantor : Enemy
{
    private void Start()
    {
        HP = 3;
        ObjectScale = 5;

        Collider.radius = 0.21f;
        transform.localScale = new Vector3(ObjectScale, ObjectScale, 1);

        Body.gravityScale = 2;
        Body.mass = 2;

        GameObject cheeseHead = GameObject.Find("CheeseHead");
        Direction = (cheeseHead.transform.position.x - transform.position.x < 0) ? -1 : 1;
    }

    private void FixedUpdate()
    {
        MovementPattern();
        DestroyOutOfBorders();
    }

    protected override void MovementPattern()
    {
        Body.velocity = new Vector2(Speed * Direction, Body.velocity.y);
    }
}
