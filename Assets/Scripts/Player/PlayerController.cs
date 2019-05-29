using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool IsSpooked;
    public static int MoveSpeed;
    public static Rigidbody2D CheesyBody;

    bool JumpActivated;
    float HorizontalMotion;
    float SpookTimer;
    SpriteRenderer[] AllRenderers;

    private void Awake()
    {
        PlayerState.Instance.Attack = Attack.Passive;
        PlayerState.Instance.Vertical = Vertical.Airborn;
        PlayerState.Instance.Horizontal = Horizontal.Idle;
        PlayerState.Instance.LifeCondition = LifeCondition.Alive;
        PlayerState.Instance.DirectionFacing = DirectionFacing.Right;
    }

    // Start is called before the first frame update
    void Start()
    {
        HorizontalMotion = 0;
        MoveSpeed = 3;

        CheesyBody = GetComponent<Rigidbody2D>();
        AllRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SpookedCheck();
        if (PlayerState.Instance.Attack != Attack.Passive)
        {
            CheesyBody.velocity = new Vector2(CheesyBody.velocity.x, CheesyBody.velocity.y); // second option: y == 0.1f because otherwise we'll be able to jump after every punch.
            HorizontalMotion = 0;
        }
        else
        {
            HorizontalMotion = Input.GetAxisRaw("Horizontal");

            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    ScriptPOF pofScript = GameObject.Instantiate(Resources.Load<GameObject>("POF"), new Vector3(0,0,0), Quaternion.identity).GetComponent<ScriptPOF>();
            //    pofScript.ObjectScale = 1;

            //}

            if (HorizontalMotion != 0)
            {
                transform.localScale = new Vector3(HorizontalMotion, 1, 1);
                PlayerState.Instance.DirectionFacing = (DirectionFacing)HorizontalMotion;
            }

            if (Input.GetButton("Jump") && PlayerState.Instance.Vertical == Vertical.Grounded)
                JumpActivated = true;
        }

        if (CheesyBody.velocity.y == 0 && PlayerState.Instance.Attack == Attack.Passive)
            PlayerState.Instance.Vertical = Vertical.Grounded;

        Horizontal previousMotion = PlayerState.Instance.Horizontal;
        PlayerState.Instance.Horizontal = (Horizontal)HorizontalMotion;

        if(previousMotion != PlayerState.Instance.Horizontal)
        {
            PlayerState.Instance.Horizontal = Horizontal.Idle;
        }
    }
    private void FixedUpdate()
    {
        WalkMotion();
        JumpMotion();
        VerticalSpeedLimit();
    }

    private void WalkMotion()
    {
        CheesyBody.velocity = new Vector2(HorizontalMotion * MoveSpeed, CheesyBody.velocity.y);
    }

    private void JumpMotion()
    {
        if (JumpActivated)
        {
            if (PlayerState.Instance.Vertical == Vertical.Grounded)
            {
                PlayerState.Instance.Vertical = Vertical.Airborn;
                CheesyBody.AddForce(new Vector2(CheesyBody.velocity.x, 6), ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();
            }
            JumpActivated = false;
        }
    }  

    private void SpookedCheck()
    {
        int lerpTo;
        float lerpSpeed;

        if(IsSpooked)
        {
            SpookTimer += Time.deltaTime * 60;
            lerpTo = 0;
            lerpSpeed = 0.6f;
        }
        else
        {
            lerpTo = 1;
            lerpSpeed = 0.8f;
        }

        foreach (SpriteRenderer renderer in AllRenderers)
            renderer.color = Color.Lerp(renderer.color, new Color(lerpTo, 1, 1, 1), lerpSpeed * Time.deltaTime);

        if (SpookTimer > 180)
        {
            SpookTimer = 0;
            MoveSpeed = 3;
            IsSpooked = false;
        }
    }

    private void VerticalSpeedLimit()
    {
        if (CheesyBody.velocity.y > 12)
            CheesyBody.velocity -= new Vector2(0, 1);      
    }
}
