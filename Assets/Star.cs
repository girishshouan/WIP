using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        XRRig rig = other.GetComponent<XRRig>();

        if (rig != null)
        {
            Debug.Log("Found XRRig");
        }
        else
        {
            Debug.Log("XRRig not found.");
        }

        if(inventory != null)
        {
            Debug.Log("Collider is player.");
            inventory.StarCollected();
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Collider is not player.");
        }
    }
}
