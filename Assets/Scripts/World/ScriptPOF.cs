using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPOF : MonoBehaviour
{
    public float ObjectScale;

    float Acumulator;
    Vector3 ScaleTo;

    // Start is called before the first frame update
    void Start()
    {
        ObjectScale *= 0.05f;
        transform.localScale = new Vector3 (transform.localScale.x + ObjectScale, transform.localScale.y + ObjectScale, transform.localScale.z);
        ScaleTo = transform.localScale * 1.75f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, ScaleTo, Acumulator);
        Acumulator += Time.deltaTime * 3;
        if (transform.localScale.x >= ScaleTo.x * 0.92f)
            Destroy(gameObject);
    }
}
