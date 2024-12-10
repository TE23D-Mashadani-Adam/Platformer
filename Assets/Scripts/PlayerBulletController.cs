using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField]
    float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(speed, 0) * Time.deltaTime;
        transform.Translate(movement);

        if (transform.position.x > 0)
        {
            Destroy(gameObject);
        }
    }
}
