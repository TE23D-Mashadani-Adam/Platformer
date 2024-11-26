using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    [SerializeField]
    public float bulletSpeed = -20;
    void Start()
    {

    }

    void Update()
    {
        print("Bullet speed " + bulletSpeed);
        Vector2 movement = new Vector2(bulletSpeed, 0) * Time.deltaTime;
        transform.Translate(movement);

        if (transform.position.x < -25)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Reflecting Wall")
        {
            print("collide!");
            GetComponent<bulletController>().bulletSpeed *= -1;
        }
    }
}
