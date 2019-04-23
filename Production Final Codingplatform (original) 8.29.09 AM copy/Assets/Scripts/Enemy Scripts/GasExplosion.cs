using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasExplosion : MonoBehaviour
{
    private Vector3 scale;

    void Start()
    {
        scale = transform.localScale;
    }
    void Update()
    {
        transform.localScale = scale;
            scale -= new Vector3(.01f, .01f, .01f);
        if (scale.y <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
