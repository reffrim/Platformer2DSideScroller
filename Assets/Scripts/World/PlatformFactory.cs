using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFactory : MonoBehaviour
{
    public GameObject StoneyPlatform;
    public GameObject GrassyPlatform;
    public GameObject RubbyPlatform;

    private void Awake()
    {
        CleanSlate();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x != 0)
        {
            int maxRows = 3;
            int maxColumns = 5;

            Vector3[,] possiblePositions = new Vector3[maxRows, maxColumns];

            float positionY = -2.5f;
            float positionX = -8.9f;


            for (int i = 0; i < maxRows; i++)
            {
                for (int n = 0; n < maxColumns; n++)
                {
                    float randomOffsetX = Random.Range(0, 1.4f);
                    float randomOffsetY = Random.Range(0, 0.5f);
                    possiblePositions[i, n] = new Vector3(transform.position.x + positionX + randomOffsetX, positionY + randomOffsetY, 1);
                    positionX += (positionX >= 7.1f) ? -16f : 4f;
                }
                positionY += 1.6f;
            }

            // Generate all the possible places. 
            //foreach (Vector3 actualPosition in possiblePositions)
            //{
            //        GameObject createdPlatform = (GameObject)GameObject.Instantiate(GrassyPlatform, actualPosition, transform.rotation);
            //        createdPlatform.transform.parent = this.gameObject.transform;
            //}

            GameObject[] randomPlatforms = new GameObject[3] { StoneyPlatform, GrassyPlatform, RubbyPlatform };

            int patternOrChaotic = Random.Range(1, 4);

            if (patternOrChaotic < 3)
                CreatePatterned(possiblePositions, randomPlatforms);
            else
                CreateChaotic(possiblePositions, randomPlatforms);
        }
    }

    void CreateChaotic(Vector3[,] possiblePositions, GameObject[] randomPlatforms)
    {
        int percentChance = 75;

        foreach (Vector3 actualPosition in possiblePositions)
        {
            int diceRoll = Random.Range(1, 100);

            if (diceRoll < percentChance)
            {
                GameObject createdPlatform = (GameObject)GameObject.Instantiate(randomPlatforms[Random.Range(0, 3)], actualPosition, transform.rotation);
                createdPlatform.transform.parent = this.gameObject.transform;
                percentChance -= 5;
            }
            percentChance -= 5;
        }
    }

    void CreatePatterned(Vector3[,] possiblePositions, GameObject[] randomPlatforms)
    {
        int patternCounter = 0;
        int evenOrOdd = Random.Range(0, 2);

        foreach (Vector3 actualPosition in possiblePositions)
        {
            if (patternCounter % 2 == evenOrOdd && patternCounter < Random.Range(5, 15)) 
            {
                GameObject createdPlatform = (GameObject)GameObject.Instantiate(randomPlatforms[Random.Range(0, 3)], actualPosition, transform.rotation);
                createdPlatform.transform.parent = this.gameObject.transform;
            }
            patternCounter++;
        }
    }

    void CleanSlate()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}