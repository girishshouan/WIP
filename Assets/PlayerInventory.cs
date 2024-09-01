using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfStars { get; private set; }

    public void StarCollected() {
        NumberOfStars++;
    }

}
