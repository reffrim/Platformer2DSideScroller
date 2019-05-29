using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallazController : MonoBehaviour
{
    public float Speed;
    public float AutoScroll;

    float Scroll;
    float Offset;
    Camera MainCamera;
    Vector3 ParallaxFollowCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        ParallaxFollowCamera = transform.position;
        Offset = transform.position.x;
    }

    void LateUpdate()
    {
        Scroll += AutoScroll;
        ParallaxFollowCamera.x = MainCamera.transform.position.x * Speed + Scroll + Offset;
        transform.position = ParallaxFollowCamera;
    }
}
