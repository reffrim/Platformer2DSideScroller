using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTest : MonoBehaviour
{
    bool isLokingRight;
    int jumpTimer;
    float lerpAcumulator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        isLokingRight = true;
        jumpTimer = 0;
        lerpAcumulator = 0.5f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float cursorHorizontalDirection = transform.position.x - cursorPosition.x;

        JumpTowardsCursor(cursorHorizontalDirection);
        LookTowardsCursor(cursorHorizontalDirection);
        BodyAnimate();
    }

    void JumpTowardsCursor(float cursorHorizontalDirection)
    {
        jumpTimer++;
        if (jumpTimer > 150 && rb.velocity.y == 0)
        {
            float jumpDirection = cursorHorizontalDirection > 0 ? -150 : 150;
            rb.AddForce(new Vector2(jumpDirection, 300));
            jumpTimer = 0;
        }
    }

    void LookTowardsCursor(float cursorHorizontalDirection)
    {
        if (cursorHorizontalDirection > 0 && transform.localScale.x >= 0)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            isLokingRight = false;
        }
        else if (cursorHorizontalDirection < 0 && transform.localScale.x <= 0)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            isLokingRight = true;
        }

    }

    void BodyAnimate()
    {
        float downPoint = 0.75f;
        float upPoint = 1;
        if(!isLokingRight)
        {
            downPoint *= -1;
            upPoint *= -1;
        }
        if (transform.localScale.y <= 0.75 || transform.localScale.y >= 1)
            lerpAcumulator = 0.5f;
        else
            lerpAcumulator += 0.05f;
        bool up = transform.localScale.x <= 0.75f ? true : false;
        if (up)
            transform.localScale = Vector2.Lerp(new Vector2(downPoint, 0.75f), new Vector2(upPoint, 1f), lerpAcumulator);
        else
            transform.localScale = Vector2.Lerp(new Vector2(upPoint, 1), new Vector2(downPoint, 0.75f), lerpAcumulator);
        Debug.Log(upPoint);
    }
}
