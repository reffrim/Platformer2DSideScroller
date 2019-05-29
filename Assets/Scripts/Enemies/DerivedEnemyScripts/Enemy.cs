using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy<T> where T : Enemy // Restriction on types that can be passed in. This case is called "Restriction of the basic type"
{
    public GameObject GameObject;
    public T ScriptComponent;

    public Enemy(string name) // Enemy <T> doesn't inherit from Monobehavior so we can use constructor here for okay.
    {
        GameObject = new GameObject(name);
        ScriptComponent = GameObject.AddComponent<T>();
        //Debug.Log("Enemy constructor has been exectued");
    }
}
    

public abstract class Enemy : MonoBehaviour
{
    public int Speed;
    public int Direction;
    public Rigidbody2D Body;
    public SpriteRenderer Sprite;
    public CircleCollider2D Collider;
    public AudioSource PlayerSlayed;

    protected int HP;
    protected float ObjectScale;

    private void Awake()
    {
        // Add common components
        Body = gameObject.AddComponent<Rigidbody2D>();
        Sprite = gameObject.AddComponent<SpriteRenderer>();
        Collider = gameObject.AddComponent<CircleCollider2D>();

        // Set common sprite
        Sprite.sprite = Resources.Load<Sprite>("EyeBall");
        Sprite.sortingLayerName = "Enemy";
        Sprite.sortingOrder = 1;
        Body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("EyeBall");

    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && PlayerState.Instance.LifeCondition == LifeCondition.Alive)
        {
            //GameObject.Destroy(GameObject.Find("Stomper"));
            //GameObject.Destroy(GameObject.Find("Fist"));

            StartCoroutine(DestroyPlayerDelayed(coll));
        }
    }

    public void DoDamage(int DamageAmount)
    {
        //Debug.Log("Damage");
        HP -= DamageAmount;

        if (HP <= 0)
        {
            //GameObject.Find("Stomper").GetComponent<AudioSource>().Play();
            ScriptPOF pofScript = GameObject.Instantiate(Resources.Load<GameObject>("POF"), transform.position, Quaternion.identity).GetComponent<ScriptPOF>();
            pofScript.ObjectScale = this.ObjectScale;
            Destroy(gameObject);
        }
    }

    //Insert all unic values to be determined at instantiation here instead a constructor
    public void Initializate(int speed, int direction, Vector3 position)
    {
        Speed = speed;
        Direction = direction;
        transform.position = position;
    }

    public void Initializate(int speed, Vector3 position)
    {
        Speed = speed;
        transform.position = position;
    }

    protected void BorderHitCheck(float force)
    {
        force *= Speed;
        Vector3 enemyPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (enemyPosition.x < 0f)
        {
            Body.velocity = new Vector2(0, Body.velocity.y);
            Body.AddForce(new Vector2(force, 0));
            Direction = 1;
        }
        else if (enemyPosition.x > 1f)
        {
            Body.velocity = new Vector2(0, Body.velocity.y);
            Body.AddForce(new Vector2(force * -1, 0));
            Direction = -1;
        }
    }

    protected void DestroyOutOfBorders()
    {
        if (transform.position.y < -6 || transform.position.y > 20)
        {
            GameObject.Destroy(gameObject);
            //Debug.Log("Out of borders. " + gameObject.name + " has beed desttroyed");
        }
    }

    protected abstract void MovementPattern();

    IEnumerator DestroyPlayerDelayed(Collision2D coll)
    {
        yield return null;

        GameObject.Find("GameOver").GetComponent<AudioSource>().Play();

        Rigidbody2D cheeseBody = coll.gameObject.GetComponent<Rigidbody2D>();
        cheeseBody.velocity = new Vector2(0, 0);
        cheeseBody.AddForce(new Vector2(0, 300));

        coll.gameObject.GetComponent<PlayerController>().enabled = false;
        coll.gameObject.GetComponent<Collider2D>().enabled = false;

        foreach (Transform child in coll.gameObject.transform)
            GameObject.Destroy(child.gameObject);

        PlayerState.Instance.LifeCondition = LifeCondition.Dead;

        GameOverManager.IsGameOver = true;

        //Debug.Log("Destroyed");
    }
    

}       
        