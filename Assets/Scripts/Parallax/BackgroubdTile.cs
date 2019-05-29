using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class BackgroubdTile : MonoBehaviour
{
    public bool HasRightCopy = false;
    public bool HasLeftCopy = false;

    const int LookAheadOffset = 2;


    float CamWidthX;
    float SpriteWidthX;
    Transform CopiedTo;
    Transform CopiedFrom;

    // Start is called before the first frame update
    void Start()
    {
        SpriteWidthX = GetComponent<SpriteRenderer>().bounds.size.x;
        CamWidthX = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        float spriteRightEdge = transform.position.x + SpriteWidthX / 2;
        float spriteLeftEdge = transform.position.x - SpriteWidthX / 2;

        float camRightEdge = Camera.main.transform.position.x + CamWidthX;
        float camLeftEdge = Camera.main.transform.position.x - CamWidthX;

        if (camRightEdge + LookAheadOffset > spriteRightEdge) //making a new coppy of the sprite if true
            if (!HasRightCopy)
                MadeCopy(CopyTo.right);

        if(camLeftEdge - LookAheadOffset < spriteLeftEdge)
            if (!HasLeftCopy)
                MadeCopy(CopyTo.left);

        DestroyIfInvisible(camRightEdge, camLeftEdge, spriteRightEdge, spriteLeftEdge); 
    }

    void MadeCopy(CopyTo side)
    {
        Vector3 copyPosition = new Vector3(transform.position.x + SpriteWidthX * (int)side, transform.position.y, transform.position.z);

        CopiedTo = (Transform)Instantiate(transform, copyPosition, transform.rotation); //the entrie object will be copied when we're copying only this transform.
        CopiedTo.GetComponent<BackgroubdTile>().CopiedFrom = this.transform;
        CopiedTo.parent = this.transform.parent;

        if (side == CopyTo.right)
            this.HasRightCopy = CopiedTo.GetComponent<BackgroubdTile>().HasLeftCopy = true;
        else if (side == CopyTo.left)
            this.HasLeftCopy = CopiedTo.GetComponent<BackgroubdTile>().HasRightCopy = true;

    }

    private void DestroyIfInvisible(float camRightEdge, float camLeftEdge, float spriteRightEdge, float spriteLeftEdge)
    {
        bool isSpriteInvisibleToRightOfCamera = (spriteLeftEdge - camLeftEdge > SpriteWidthX);
        bool isSpriteInvisibleToLeftOfCamera = (camLeftEdge - spriteRightEdge > SpriteWidthX);

        if (isSpriteInvisibleToRightOfCamera)
        {
            if (CopiedFrom != null)
                CopiedFrom.GetComponent<BackgroubdTile>().HasRightCopy = false;

            if(CopiedTo != null)
                CopiedTo.GetComponent<BackgroubdTile>().HasRightCopy = false;

            GameObject.Destroy(gameObject);
        }
        else if(isSpriteInvisibleToLeftOfCamera)
        {
            if (CopiedFrom != null)
                CopiedFrom.GetComponent<BackgroubdTile>().HasLeftCopy = false;

            if (CopiedTo != null)
                CopiedTo.GetComponent<BackgroubdTile>().HasLeftCopy = false;

            GameObject.Destroy(gameObject);
        }
    }

    enum CopyTo
    {
        right = 1,
        left = -1
    }
}


