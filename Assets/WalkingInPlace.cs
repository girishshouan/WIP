using Oculus.Movement.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static OVRUnityHumanoidSkeletonRetargeter;

public interface IFullBodyVisualizerAdapter
{
    Transform GetBoneTransform(int boneId);
}

public class FullBodyVisualizerAdapter : IFullBodyVisualizerAdapter
{
    private readonly FullBodyOVRSkeletonBoneVisualizer _fullBodyVisualizer;

    public FullBodyVisualizerAdapter(FullBodyOVRSkeletonBoneVisualizer fullBodyVisualizer)
    {
        _fullBodyVisualizer = fullBodyVisualizer;
    }

    public Transform GetBoneTransform(int boneId)
    {
        // Use reflection inside the adapter
        var methodInfo = _fullBodyVisualizer.GetType().GetMethod("GetBoneTransform", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo != null)
        {
            return (Transform)methodInfo.Invoke(_fullBodyVisualizer, new object[] { boneId });
        }
        return null;
    }
}



public class WalkingInPlace : MonoBehaviour
{
    public RPMCalculator rpmCalculator;
    public float speedMultiplier = 0.1f; // Adjust the speed multiplier as needed
    public FullBodyOVRSkeletonBoneVisualizer fullBodyVisualizer;
    public float stepsPerMinute = 120f; // Steps per minute (SPM)
    public float stepLength = 2.0f; // Length of one step in meters

    private IFullBodyVisualizerAdapter fullBodyVisualizerAdapter;
    public Transform characterTransform;
    public OVRCameraRig cameraRig;
    private Vector3 initialPosition;
    private void Start()
    {
        //Debug.Log("testWWIP Start.");
        if (fullBodyVisualizer == null)
        {
            Debug.LogError("FullBodyOVRSkeletonBoneVisualizer is not assigned!");
            return;
        }

        fullBodyVisualizerAdapter = new FullBodyVisualizerAdapter(fullBodyVisualizer);
        if (characterTransform == null)
        {
            //Debug.Log("Assigning character transform");
            GameObject characterObject = GameObject.Find("passive_marker_man");
            if (characterObject != null)
            {
                characterTransform = characterObject.transform;
                initialPosition = characterTransform.position;
            }
            else
            {
                Debug.Log("Character Object not found.");
            }
        }
        else
        {
            Debug.Log("CharacterTransform is assigned.");
        }
    }

    void Update()
    {

        if (fullBodyVisualizerAdapter != null)
        {
            //Transform headTransform = fullBodyVisualizerAdapter.GetBoneTransform(7);
            /*
                From Body Tracking API specification
                XR_FULL_BODY_JOINT_LEFT_UPPER_LEG_META = 70,
                XR_FULL_BODY_JOINT_LEFT_LOWER_LEG_META = 71,
                XR_FULL_BODY_JOINT_LEFT_FOOT_ANKLE_TWIST_META = 72,
                XR_FULL_BODY_JOINT_LEFT_FOOT_ANKLE_META = 73,
                XR_FULL_BODY_JOINT_LEFT_FOOT_SUBTALAR_META = 74,
                XR_FULL_BODY_JOINT_LEFT_FOOT_TRANSVERSE_META = 75,
                XR_FULL_BODY_JOINT_LEFT_FOOT_BALL_META = 76,
                XR_FULL_BODY_JOINT_RIGHT_UPPER_LEG_META = 77,
                XR_FULL_BODY_JOINT_RIGHT_LOWER_LEG_META = 78,
                XR_FULL_BODY_JOINT_RIGHT_FOOT_ANKLE_TWIST_META = 79,
                XR_FULL_BODY_JOINT_RIGHT_FOOT_ANKLE_META = 80,
                XR_FULL_BODY_JOINT_RIGHT_FOOT_SUBTALAR_META = 81,
                XR_FULL_BODY_JOINT_RIGHT_FOOT_TRANSVERSE_META = 82,
                XR_FULL_BODY_JOINT_RIGHT_FOOT_BALL_META = 83,  
             */
            
            Transform leftFootTransform = fullBodyVisualizerAdapter.GetBoneTransform(73); 
            Transform rightFootTransform = fullBodyVisualizerAdapter.GetBoneTransform(80);


            if (leftFootTransform != null && rightFootTransform != null)
            {
                float rpm = rpmCalculator.CalculateRPM(Time.deltaTime, leftFootTransform.position, rightFootTransform.position);
                float distancePerFrame = rpm / 60f * stepLength * Time.deltaTime;

                Vector3 forwardDirection = cameraRig.centerEyeAnchor.forward;
                forwardDirection.y = 0;
                Vector3.Normalize(forwardDirection);
                characterTransform.Translate(forwardDirection * distancePerFrame, Space.World);
            }
            else
            {
                Debug.Log("Transform for head or feet is null.");
            }
        }
        else
        {
            Debug.LogError("FullBodyVisualizerAdapter is not initialized!");
        }
    }

}
