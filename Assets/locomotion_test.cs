using UnityEngine;

public class locomotion_test : MonoBehaviour
{
    public Transform characterTransform;
    public OVRCameraRig cameraRig;

    void Start()
    {

        

    }

    private void Update()
    {
        int i = 0;

        while (true)
        {
            Debug.Log("move_test");
            Debug.Log("Iteration " + i);
            if (cameraRig == null)
            {
                Debug.LogError("cameraRig Transform not assigned!");
                return;
            }
            else
            {
                Debug.Log("cameraRig Transform is assigned");
                Debug.Log("Initial Position: " + cameraRig.transform.position);

                cameraRig.transform.Translate(Vector3.forward * 0.5f, Space.World);

                Debug.Log("New Position: " + cameraRig.transform.position);
            }
            i++;
            Debug.Log("move_test.Start() end.");
            if (i == 2)
            {
                break;
            }
        }

        while (true)
        {
            Debug.Log("move_test::Update()");
            Debug.Log("Position of camerRig: " + cameraRig.transform.position);
            break;
        }

    }

}
