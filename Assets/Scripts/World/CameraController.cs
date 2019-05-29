using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public GameObject CheeseHead;
    public CameraState CameraState;

    // Start is called before the first frame update
    void Start()
    {
        CameraState = CameraState.Stationary;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float offset = Camera.main.orthographicSize * Camera.main.aspect / 2;
        Vector3 CheeseScreenPosition = Camera.main.WorldToViewportPoint(CheeseHead.transform.position);

        if (CheeseScreenPosition.x < 0.25f || CheeseScreenPosition.x > 0.75f)
            CameraState = CameraState.Following;

        if (CameraState == CameraState.Following && PlayerState.Instance.Horizontal == Horizontal.Idle)
            CameraState = CameraState.Recenting;
        else if (CameraState == CameraState.Following)
            transform.position = new Vector3(
                CheeseHead.transform.position.x - offset * (int)PlayerState.Instance.DirectionFacing,
                transform.position.y,
                transform.position.z);

        if (CameraState == CameraState.Recenting)
        {
            float x = Mathf.Lerp(transform.position.x, CheeseHead.transform.position.x, 0.02f * Time.deltaTime * 60);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            if (Math.Round(CheeseScreenPosition.x, 1) == 0.05f)
                CameraState = CameraState.Stationary;
        }

    }
}

public enum CameraState
{
    Stationary, 
    Following, 
    Recenting
}