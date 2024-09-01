using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    public Transform characterTransform;
    private Vector3 lastPosition;
    private float totalDistance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = characterTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceCovered = Vector3.Distance(characterTransform.position, lastPosition);
        totalDistance += distanceCovered;
        lastPosition = characterTransform.position;
    }

    public void getTotalDistance()
    {
        Debug.Log("Total Distance covered : "+totalDistance);
    }

}
