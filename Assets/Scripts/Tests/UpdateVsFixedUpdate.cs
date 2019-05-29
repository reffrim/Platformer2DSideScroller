using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateVsFixedUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60 ;
        QualitySettings.vSyncCount = 0;

        Debug.Log("Test started");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
    }

    private void FixedUpdate()
    {
        Debug.Log("Fixed Update");
    }

}
