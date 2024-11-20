using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    [SerializeField]
    float bulletSpeed = 20;
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 movement = new Vector2(-bulletSpeed, 0) * Time.deltaTime;
        transform.Translate(movement);

        if (transform.position.x < -25)
        {
            Destroy(gameObject);
        }
    }

    
}
