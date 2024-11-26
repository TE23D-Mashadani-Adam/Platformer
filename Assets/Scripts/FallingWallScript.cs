using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWallScript : MonoBehaviour
{
    public static bool wallFall = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wallFall)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 1;
        }

    }
}
