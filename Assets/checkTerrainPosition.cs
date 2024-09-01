using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkTerrainPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start() : Terrain position : "+this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Terrain position : " + this.transform.position);
    }
}
