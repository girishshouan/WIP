using UnityEngine;
using System.Collections.Generic;
using Oculus.Movement.Utils;
using System.Reflection;



public class RPMCalculator : MonoBehaviour
{
    public FullBodyOVRSkeletonBoneVisualizer fullBodyVisualizer;
    private IFullBodyVisualizerAdapter fullBodyVisualizerAdapter;
    private const float RPM_SMOOTHING = 0.5f;
    private readonly Dictionary<string, float> RPM_PARAMS = new Dictionary<string, float>
    {
        {"t_vv_y", 0.10654f},
        {"w0", 0.373254f},
        {"w1", 0.39774f},
        {"w2", 0.057184f},
        {"w_rpm", 0.826223f},
        {"o_rpm", 0.0055f }
    };

    private float time_since_step = 0.0f;
    private Vector3 head = Vector3.zero;
    private Vector3 head_prev = Vector3.zero;
    private Vector3 v = Vector3.zero;
    private Vector3 v_prev = Vector3.zero;
    private Vector3 vv = Vector3.zero;
    private Vector3 vv_prev = Vector3.zero;
    private float mov_prev = 0.0f;
    public float rpm = 0.0f;
    private float rpm_prev = 0.0f;

    private Vector3 left_foot = Vector3.zero;
    private Vector3 left_foot_prev = Vector3.zero;
    private Vector3 right_foot = Vector3.zero;
    private Vector3 right_foot_prev = Vector3.zero;
    private Vector3 pos_prev = Vector3.zero;
    private Vector3 pos = Vector3.zero;

    private Vector3 v_left = Vector3.zero;
    private Vector3 v_left_prev = Vector3.zero;
    private Vector3 v_right = Vector3.zero;
    private Vector3 v_right_prev = Vector3.zero;
    private Vector3 vv_left = Vector3.zero;
    private Vector3 vv_left_prev = Vector3.zero;
    private Vector3 vv_right = Vector3.zero;
    private Vector3 vv_right_prev = Vector3.zero;
    private float mov_left_prev = 0.0f;
    private float mov_right_prev = 0.0f;


    void Start()
    {
        if (fullBodyVisualizer == null)
        {
            Debug.LogError("FullBodyOVRSkeletonBoneVisualizer is not assigned!");
            return;
        }

        fullBodyVisualizerAdapter = new FullBodyVisualizerAdapter(fullBodyVisualizer);
    }





    public float CalculateRPM(float delta, Vector3 left_pos_raw, Vector3 right_pos_raw)
    {
        time_since_step += delta;
        if (delta == 0.0f)
        {
            delta = 0.001f;
        }

        // Smooth positions
        left_foot = (1 - RPM_PARAMS["w0"]) * left_foot_prev + RPM_PARAMS["w0"] * left_pos_raw;
        right_foot = (1 - RPM_PARAMS["w0"]) * right_foot_prev + RPM_PARAMS["w0"] * right_pos_raw;
        pos = (left_pos_raw + right_pos_raw) / 2;

        // Calculate velocities

        float e_distance = Vector3.Distance(pos, pos_prev);
        if(e_distance < RPM_PARAMS["o_rpm"])
        {
            return 0.0f;
        }

        //Debug.Log("Euclidean distance between pos and pos_prev : "+e_distance.ToString("F10"));
        v = (pos - pos_prev) / delta;
        v = (1 - RPM_PARAMS["w1"]) * v_prev + RPM_PARAMS["w1"] * v;


        vv = (v - v_prev) / delta;
        vv = (1 - RPM_PARAMS["w2"]) * vv_prev + RPM_PARAMS["w2"] * vv;

        float mov = v.y;


        if (Mathf.Abs(vv.y) < RPM_PARAMS["t_vv_y"])
        {
            mov = 0;
        }

        // Zero crossing, count as step
        if ((mov_prev >= 0 && mov < 0) || (mov_prev <= 0 && mov > 0))
        {
            float new_rpm = 30 / time_since_step;
            time_since_step = 0;

            // Too fast outlier detection
            if (new_rpm > 120 && new_rpm > 1.2f * rpm_prev)
            {
                rpm = rpm_prev;
            }
            else
            {
                time_since_step = 0;

                // Too slow outlier detection
                if (new_rpm < 0.8f * rpm_prev)
                {
                    rpm = rpm_prev;
                }
                else
                {
                    // Exponential smoothing of RPM
                    rpm = RPM_PARAMS["w_rpm"] * rpm_prev + (1 - RPM_PARAMS["w_rpm"]) * new_rpm;
                }
            }
        }
        else
        {
            // If the next step comes slower than the last, reduce rpm, otherwise keep it
            float new_rpm = 30 / time_since_step;
            if (new_rpm < rpm)
            {
                //Exponential decay of rpm to mimic natural slowdown
                rpm = rpm - RPM_SMOOTHING * delta * rpm;
            }
        }

        pos_prev = pos;
        v_prev = v;
        vv_prev = vv;
        mov_prev = mov;
        rpm_prev = rpm;

        return rpm;
    }
}
