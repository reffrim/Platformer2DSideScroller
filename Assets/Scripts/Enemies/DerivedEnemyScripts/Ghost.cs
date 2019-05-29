using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        HP = 1;
        ObjectScale = 1.8f;
        //Destroy(Collider);
        //Destroy(Body);

        Collider.radius = 0.16f;
        Body.isKinematic = true;

        Sprite.sortingOrder = 1;
        Sprite.material.color = new Color(1, 1, 1, 0.5f);
        transform.localScale = new Vector3(ObjectScale, ObjectScale, 1);
    }

    private void Update()
    {
        MovementPattern();
        DestroyOutOfBorders();
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            PlayerController.IsSpooked = true;
            PlayerController.MoveSpeed -= 1;
            GameObject.Destroy(gameObject);
        }
    }

    protected override void MovementPattern()
    {
            transform.position = Vector3.MoveTowards(
                transform.position,
                GameObject.Find("CheeseHead").transform.position,
                Speed * Time.deltaTime
                ); 
    }
}
