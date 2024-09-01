using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPosition : MonoBehaviour
{
    private void Start()
    {
        Transform parentTransform = this.transform;
        Debug.Log("Start() : XR Origin transform : " + parentTransform.position);

        foreach (Transform child in parentTransform)
        {
            Debug.Log("Start() : Child Name: " + child.name + " | Child Position: " + child.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform parentTransform = this.transform;
        Debug.Log("XR Origin transform : " + parentTransform.position);

        foreach (Transform child in parentTransform) {
            Debug.Log("Child Name: " + child.name + " | Child Position: " + child.position);
        }
    }
}
