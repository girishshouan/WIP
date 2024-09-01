using UnityEngine;

public class OVRMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public Transform cameraRig;
    public Transform centerEyeAnchor;
    public Transform characterTransform;
    private CharacterController characterController;

    void Start()
    {
        //characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // Get input from Oculus thumbstick (left hand by default)
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // Calculate movement direction relative to the camera's forward direction
        moveDirection += centerEyeAnchor.forward * primaryAxis.y;
        moveDirection += centerEyeAnchor.right * primaryAxis.x;

        // Ensure movement is only on the XZ plane
        moveDirection.y = 0;

        // Apply movement
        characterTransform.Translate(moveDirection * speed * Time.deltaTime);
        //characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}
