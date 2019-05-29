using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    private void FixedUpdate()
    {
        Rigidbody2D cheeseBody = GetComponent<Rigidbody2D>();
        cheeseBody.velocity = new Vector2(1, cheeseBody.velocity.y);


        if (cheeseBody.velocity.y == 0)
        {
            cheeseBody.AddForce(new Vector2(0, 300)); 
        }
    }
}
