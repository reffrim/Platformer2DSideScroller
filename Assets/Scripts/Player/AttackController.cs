using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject CheeseHead;
    public GameObject Projectile;

    bool Recoiling;
    bool Projectilizing;
    int MaxPause;
    float StartingPunchPosition;
    float EndingPunchPosition;
    float PunchMotion;
    float AttackForce;
    float AttackPause;
    float Accumulator;

    private void Start()
    {
        AttackForce = 5;
        AttackPause = 1;
        Accumulator = 0.02f;
        PunchMotion = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentsInChildren<SpriteRenderer>()[1].enabled = (PlayerState.Instance.Attack == Attack.Projectile) ? true : false;

        if (Input.GetButtonDown("Punch") && PlayerState.Instance.Attack == Attack.Passive)
        {
            PlayerState.Instance.Attack = Attack.Punch;
            StartingPunchPosition = CheeseHead.transform.position.x;
            EndingPunchPosition = StartingPunchPosition + (int)PlayerState.Instance.DirectionFacing * 0.7f;

            MaxPause = 10;
            GetComponents<AudioSource>()[0].Play();
        }
        else if (Input.GetButton("Projectile") && PlayerState.Instance.Attack == Attack.Passive && GameObject.Find("Projectile(Clone)") == null)
        {
            PlayerState.Instance.Attack = Attack.Projectile;
            StartingPunchPosition = CheeseHead.transform.position.x;
            EndingPunchPosition = StartingPunchPosition + (int)PlayerState.Instance.DirectionFacing * 0.5f;

            MaxPause = 20;
            GetComponents<AudioSource>()[1].Play();
        }

        if (PlayerState.Instance.Attack == Attack.Punch || PlayerState.Instance.Attack == Attack.Projectile)
        {
            Accumulator += (Recoiling) ? Time.deltaTime : Time.deltaTime * 3;

            if (PunchMotion == EndingPunchPosition)
            {
                AttackPause += Time.deltaTime * 60;
            }

            if (AttackPause > MaxPause)
            {
                if (PlayerState.Instance.Attack == Attack.Projectile)
                {
                    GameObject.Instantiate(Projectile);
                    Projectilizing = true;
                }
                AttackPause = 1;
                Accumulator = 0.02f;
                Recoiling = true;
            }

            if(!Recoiling)
            {
                PunchMotion = Mathf.Lerp(StartingPunchPosition, EndingPunchPosition, Accumulator * 7);
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Accumulator * 5);
            }
            else
            {
                PunchMotion = Mathf.Lerp(EndingPunchPosition, StartingPunchPosition, Accumulator * 5);
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.6f, 0.5f, 1), Accumulator * 4);

                if(transform.position.x == StartingPunchPosition)
                {
                    Recoiling = false;
                    PlayerState.Instance.Attack = Attack.Passive;
                    Accumulator = 0.02f;
                }
            }

            transform.position = new Vector3(PunchMotion, CheeseHead.transform.position.y, transform.position.z);
            GetComponent<SpriteRenderer>().enabled = true;
        }

        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void FixedUpdate()
    {
        ProjectizingReturn();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            Rigidbody2D enemyBody = coll.gameObject.GetComponent<Rigidbody2D>();
            enemyBody.velocity = new Vector2(0, 0);
            enemyBody.AddForce(new Vector2((float)PlayerState.Instance.DirectionFacing * AttackForce, AttackForce), ForceMode2D.Impulse);

            enemyBody.GetComponent<Enemy>().DoDamage(2);
            WorldManager.Score += 200;
        }
    }

    void ProjectizingReturn()
    {
        if (Projectilizing)
        {
            PlayerController.CheesyBody.AddForce(
                    new Vector2((float)PlayerState.Instance.DirectionFacing * -1 * 750, 0)); // try to add implulse instead 
            Projectilizing = false;
        }
    }

}
