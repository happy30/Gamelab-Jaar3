//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPuzzle : MonoBehaviour
{
    public int[] correctOrderArray;
    public bool correct;
    public GameObject[] images;
    private int nextImage = 0;
	
	public void CheckOrder ()
    {
        if (correct)
        {
            //Get puzzle piece  
        }
        else
        {
            //WRONG BIATCH ( make some sound )
            Reset();
        }
    }

    public void FillArray(int number)
    {
        if (correctOrderArray[nextImage] != number)
        {
            correct = false;
        }
        ActiveImage();
    }

    void ActiveImage()
    {
        if (nextImage < images.Length)
        {
            images[nextImage].SetActive(true);
            nextImage++;
        }
    }

    public void Reset()
    {
        correct = true;
        for(int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }      
    }
}
