using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotTheDifferences : MonoBehaviour
{
    public int totalSpots;
    public int foundSpots;
    private bool isFound;
    private ParticleSystem pS;

    void Start ()
    {
    }

    public void Clicker(Image img)
    {
        isFound = true;
        foundSpots++;
        Debug.Log(foundSpots);
        
        StartCoroutine(ChangeAlpha(img));
        if (foundSpots == totalSpots)
            print("Job's Done");
    }

    public IEnumerator ChangeAlpha(Image imgg)
    {
        {
            for (float f = 0f; f <= 2f; f += .1f)
            {
                Color tempColor = imgg.color;
                tempColor.a = f;
                imgg.color = tempColor;
                yield return null;
            }
        }
    }
}
