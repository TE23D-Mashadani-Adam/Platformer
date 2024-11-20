using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    Transform checker;
    [SerializeField]
    LayerMask barrier;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(speed, 0) * Time.deltaTime;
        transform.Translate(movement);

        if (touchedBarrier())
        {
            speed *= -1;    
        }
    }

    bool touchedBarrier()
    {
        return Physics2D.OverlapCircle(checker.position, .5f, barrier);
    }
}
