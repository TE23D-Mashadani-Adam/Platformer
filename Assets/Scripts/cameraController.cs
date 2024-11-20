using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField]
    Transform player;
    
    Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 positon = transform.position;
       positon.x = player.position.x + offset.x;
       transform.position = positon;
    }
}
