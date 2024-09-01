using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position) + 1.0f, transform.position.z);
        transform.position = startPosition;
    }

    
}
