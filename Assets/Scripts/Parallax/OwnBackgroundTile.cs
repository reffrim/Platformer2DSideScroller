using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnBackgroundTile : MonoBehaviour
{
    public GameObject SpriteRendererObject;

    int LeftSpriteObjectIndex;
    int MidleSpriteObjectIndex;
    int RightSpriteObjectIndex;
    float SpriteWidthX;
    float CameraPositionX;
    GameObject[] spriteCopies;
    
    // Start is called before the first frame update
    void Start()
    {
        LeftSpriteObjectIndex = 0;
        MidleSpriteObjectIndex = 1;
        RightSpriteObjectIndex = 2;

        SpriteWidthX = SpriteRendererObject.GetComponent<SpriteRenderer>().bounds.size.x;
        CameraPositionX = Camera.main.transform.position.x;

        spriteCopies = new GameObject[3];
        spriteCopies[MidleSpriteObjectIndex] = SpriteRendererObject;

        Vector3 leftSpawnPosition = new Vector3(SpriteRendererObject.transform.position.x - SpriteWidthX,
            SpriteRendererObject.transform.position.y,
            SpriteRendererObject.transform.position.z);

        spriteCopies[LeftSpriteObjectIndex] = Instantiate(
            SpriteRendererObject,
            leftSpawnPosition,
            SpriteRendererObject.transform.rotation,
            SpriteRendererObject.transform.parent);

        Vector3 rightSpawnPosition = new Vector3(
            SpriteRendererObject.transform.position.x + SpriteWidthX,
            SpriteRendererObject.transform.position.y,
            SpriteRendererObject.transform.position.z);

        spriteCopies[RightSpriteObjectIndex] = Instantiate(
            SpriteRendererObject,
            rightSpawnPosition,
            SpriteRendererObject.transform.rotation,
            SpriteRendererObject.transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        CameraPositionX = Camera.main.transform.position.x;

        if (CameraPositionX <= spriteCopies[LeftSpriteObjectIndex].transform.position.x  )
        {
            ShiftSpriteToLeft();
        }

        if (CameraPositionX >= spriteCopies[RightSpriteObjectIndex].transform.position.x)
        {
            ShiftSpriteToRight();
        }
    }

    void ShiftSpriteToLeft()
    {
        Vector3 leftSpawnPosition = new Vector3(
            spriteCopies[LeftSpriteObjectIndex].transform.position.x - SpriteWidthX,
            spriteCopies[LeftSpriteObjectIndex].transform.position.y,
            spriteCopies[LeftSpriteObjectIndex].transform.position.z);

        spriteCopies[RightSpriteObjectIndex].transform.position = leftSpawnPosition;

        int tmp = LeftSpriteObjectIndex;
        LeftSpriteObjectIndex = RightSpriteObjectIndex;
        RightSpriteObjectIndex = MidleSpriteObjectIndex;
        MidleSpriteObjectIndex = tmp;
    }

    void ShiftSpriteToRight()
    {
        Vector3 rightSpawnPosition = new Vector3(
            spriteCopies[RightSpriteObjectIndex].transform.position.x + SpriteWidthX,
            spriteCopies[RightSpriteObjectIndex].transform.position.y,
            spriteCopies[RightSpriteObjectIndex].transform.position.z);

        spriteCopies[LeftSpriteObjectIndex].transform.position = rightSpawnPosition;

        int tmp = RightSpriteObjectIndex;
        RightSpriteObjectIndex = LeftSpriteObjectIndex;
        LeftSpriteObjectIndex = MidleSpriteObjectIndex;
        MidleSpriteObjectIndex = tmp;
    }
}
