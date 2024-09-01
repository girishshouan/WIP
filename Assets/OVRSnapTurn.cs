using UnityEngine;

public class OVRSnapTurn : MonoBehaviour
{
    public float rotationAmount = 45.0f; // Degrees per snap
    public float rotationDeadzone = 0.5f; // Deadzone to avoid accidental rotation
    private bool canRotate = true;

    void Update()
    {
        // Get thumbstick input for rotation (right hand by default)
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (canRotate)
        {
            if (secondaryAxis.x > rotationDeadzone)
            {
                RotateRig(rotationAmount);
            }
            else if (secondaryAxis.x < -rotationDeadzone)
            {
                RotateRig(-rotationAmount);
            }
        }

        // Detect when thumbstick is released to allow another rotation
        if (Mathf.Abs(secondaryAxis.x) < rotationDeadzone)
        {
            canRotate = true;
        }
    }

    void RotateRig(float angle)
    {
        transform.Rotate(0, angle, 0);
        canRotate = false; // Prevents continuous rotation while thumbstick is held
    }
}
