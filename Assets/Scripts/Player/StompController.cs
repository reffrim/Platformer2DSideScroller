using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompController : MonoBehaviour
{
    public Rigidbody2D CheeseHeadBody;
    private void Start()
    {
        //CheeseCollider = GameObject.Find("CheeseHead").GetComponent<BoxCollider2D>();
        CheeseHeadBody = GameObject.Find("CheeseHead").GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            //if(CheeseCollider != null)
            //    CheeseCollider.enabled = false;

            coll.gameObject.GetComponent<Enemy>().DoDamage(10);
            //GameObject.Destroy(coll.gameObject);

            //Rigidbody2D cheeseHeadBody = GameObject.Find("CheeseHead").GetComponent<Rigidbody2D>();

            CheeseHeadBody.velocity = new Vector2(CheeseHeadBody.velocity.x, 0);
            CheeseHeadBody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            //GetComponent<AudioSource>().Play();

            WorldManager.Score += 300;

            //if (CheeseCollider != null)
            //    CheeseCollider.enabled = true;
        }
    }
}
