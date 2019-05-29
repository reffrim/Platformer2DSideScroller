using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    int Direction;
    int Speed;
    float Timer;

    // Start is called before the first frame update
    void Start()
    {
        Direction = (int)PlayerState.Instance.DirectionFacing;
        transform.position = GameObject.Find("Fist").transform.position + new Vector3(0.3f, 0, 0) * Direction;
        Speed = 12;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime * Speed * Direction, transform.position.y);
        transform.Rotate(0, 0, 6 * Direction * -1 * Time.deltaTime * 60);
        Timer += Time.deltaTime * 60;

        if (Timer > 120)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            GameObject.Destroy(gameObject);

            Rigidbody2D enemyBody = coll.gameObject.GetComponent<Rigidbody2D>();
            enemyBody.velocity = new Vector2(0, 0);

            enemyBody.AddForce(new Vector2((float)PlayerState.Instance.DirectionFacing * 11, 14), ForceMode2D.Impulse);
            enemyBody.GetComponent<Enemy>().DoDamage(1);

            WorldManager.Score += 125;
        }
    }
}
