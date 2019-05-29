using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputAndPhysicsCalculations : MonoBehaviour
{
    bool upForceFlag;

    private void FixedUpdate()
    {
        if (upForceFlag)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
        upForceFlag = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            upForceFlag = true;

        //Create an "AirBrake" type of input
        if (Input.GetKeyDown(KeyCode.B))
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
    }
}
