using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneSwitcher : MonoBehaviour
{
    public int switches = 0;
    public string sceneName = "EndScene"; // The name of the scene to switch to

    // Method to be called when the cube is interacted with

    public void SwitchOff()
    {
        Debug.Log("SceneSwitcher switchoff called.");
        gameObject.SetActive(false);
    }

    public void SwitchScene()
    {
        Debug.Log("SceneSwitcher SwitchScene called.");
        TouchCounter touchCounter = transform.parent.GetComponent<TouchCounter>();
        if (touchCounter != null)
        {
            Debug.Log("Touch Counter accessed.");
            touchCounter.Summarize();
            if (touchCounter.count >= touchCounter.threshold)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        else {
            Debug.Log("Touch Counter not accessible.");
        }
         
    }
}
