using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{
    private void Awake()
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.offset = new Vector2(0, 5);
        collider.size = new Vector2(0.5f, 9.5f);

        //Enemy<Gigantor> gigantGeorge = new Enemy<Gigantor>("GiantGeorge");
        //gigantGeorge.ScriptComponent.Initializate(speed: 1, position: new Vector3(-6, -1, 1));

        //Enemy<Tweaker> tweakyTim = new Enemy<Tweaker>("TweakyTim");
        //tweakyTim.ScriptComponent.Initializate(speed: 4, position: new Vector3(-3, -1, 1));

        //Enemy<Lush> lushyLinda = new Enemy<Lush>("LushyLinda");
        //lushyLinda.ScriptComponent.Initializate(speed: Random.Range(6, 18), position: new Vector3(-3, -1, 1));

        //Enemy<Bouncer> bouncyBill = new Enemy<Bouncer>("bouncyBill");
        //bouncyBill.ScriptComponent.Initializate(4, Random.Range(0, 2) * 2 - 1, new Vector3(-3, -1, 1));

        //Enemy<Torque> torqyTom = new Enemy<Torque>("TorqyTom");
        //torqyTom.ScriptComponent.Initializate(speed: 3, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(-3, -1, 1));

        //Enemy<Ghost> ghostGayle = new Enemy<Ghost>("GhostlyGayle");
        //ghostGayle.ScriptComponent.Initializate(speed: 2, position: new Vector3(-3, -1, 1));
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("ColisionTrigger");
        if (coll.gameObject.tag == "Player")
        {
            Destroy(GetComponent<Collider2D>());
            WorldManager.Level++;

            //Debug.Log(WorldManager.Level);

            for(int i = 0; i < WorldManager.Difficulty; i++)
            {
                int randomEnemy = Random.Range(0, 6); // Random.Range(0, 6)
                float randomX = transform.position.x + (6 * (Random.Range(0, 2) * 2 - 1));
                float randomY = Random.Range(4, 8);

                switch (randomEnemy)
                {
                    case 0:
                        //Debug.Log(randomEnemy);
                        Enemy<Gigantor> gigantGeorge = new Enemy<Gigantor>("GiantGeorge");
                        gigantGeorge.ScriptComponent.Initializate(speed: 1, position: new Vector3(randomX, randomY, 1));
                        break;
                    case 1:
                        //Debug.Log(randomEnemy);
                        Enemy<Tweaker> tweakyTim = new Enemy<Tweaker>("TweakyTim");
                        tweakyTim.ScriptComponent.Initializate(speed: 4, position: new Vector3(randomX, randomY, 1));
                        break;
                    case 2:
                        //Debug.Log(randomEnemy);
                        Enemy<Lush> lushyLinda = new Enemy<Lush>("LushyLinda");
                        lushyLinda.ScriptComponent.Initializate(speed: Random.Range(6, 18), position: new Vector3(randomX, randomY, 1));
                        break;
                    case 3:
                        //Debug.Log(randomEnemy);
                        Enemy<Bouncer> bouncyBill = new Enemy<Bouncer>("bouncyBill");
                        bouncyBill.ScriptComponent.Initializate(4, Random.Range(0, 2) * 2 - 1, new Vector3(randomX, randomY, 1));
                        break;
                    case 4:
                        //Debug.Log(randomEnemy);
                        Enemy<Torque> torqyTom = new Enemy<Torque>("TorqyTom");
                        torqyTom.ScriptComponent.Initializate(speed: 3, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(randomX, randomY, 1));
                        break;
                    case 5:
                        //Debug.Log(randomEnemy);
                        Enemy<Ghost> ghostGayle = new Enemy<Ghost>("GhostlyGayle");
                        ghostGayle.ScriptComponent.Initializate(speed: 2, position: new Vector3(randomX, randomY, 1));
                        break;
                }
            }
        }
    }
}
