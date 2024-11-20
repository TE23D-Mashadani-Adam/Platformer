using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    [SerializeField]
    LayerMask transparentWall;
    [SerializeField]
    Transform checker;
    [SerializeField]
    float speed = 10f;
    [SerializeField]
    //GameObject explosion;
   
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(speed, 0);
        transform.Translate(movement * Time.deltaTime);
        if (touchtWall())
        {
            speed *= -1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
    }

    private bool touchtWall()
    {
        return Physics2D.OverlapCircle(checker.position, .9f, transparentWall);
    }
    public void Kill()
    {
        Destroy(gameObject);
       // Instantiate(explosion, transform.position, Quaternion.identity);
    }

    

}
