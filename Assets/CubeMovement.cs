using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 1.0f;
    public bool movement_x = true;

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);
        if (movement_x)
        {
            transform.position = new Vector3(startPosition.x + movement, startPosition.y, startPosition.z);
        }
        else 
        { 
            transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + movement);
        }
    }
}
