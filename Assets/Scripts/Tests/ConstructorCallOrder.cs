using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorCallOrder : MonoBehaviour
{
    public ConstructorCallOrder()
    {
        Debug.Log("Constructor is called" + this.gameObject);
    }

    void Awake()
    {
        Debug.Log("Awake is called");
    }

    void Start()
    {
        Debug.Log("Start is called");
    }

    void Update()
    {
    }
}
