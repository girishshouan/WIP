using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TouchCounter : MonoBehaviour
{
    public int count = 0;
    public int threshold = 5;
    public TextMeshProUGUI scoreText;
    private int gold = 0;
    private int red = 0;

    private void Start()
    {
        UpdateTextMesh();
    }

    public void incrementCount() 
    {
        Debug.Log("Before incrementing : "+ count );
        count++;
        gold++;
        Debug.Log("After incrementing : " + count);
        UpdateTextMesh();
    }

    public void decrementCount()
    {
        Debug.Log("Before incrementing : " + count);
        count--;
        red++;
        Debug.Log("After incrementing : " + count);
        UpdateTextMesh();
    }

    public void UpdateTextMesh()
    {
        scoreText.text = "Score: " + count.ToString();
    }

    public void Summarize()
    {
        Debug.Log("Summary :\nGold gems collected : " + gold + "\nRed gems collected : " + red);
    }
}
