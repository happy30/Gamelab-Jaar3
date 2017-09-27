//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPuzzle : MonoBehaviour
{
    public bool[] iconOrderArray;
    public GameObject[] images;
    private int nextImage = 0;
	
	public void CheckOrder (int number)
    {
        if (number == 2 && !iconOrderArray[3] && iconOrderArray[2])
        {
            iconOrderArray[3] = true;
            ActiveImage();
        }

        if (iconOrderArray[3])
        {
            number++;
        }
 
        for (int i = 0; i <= number; i++)
        {    
            if(!iconOrderArray[i] && number == i)
            {
                iconOrderArray[i] = true;
                ActiveImage();
                if (iconOrderArray[4])
                {
                    print("puzzle completed");
                    //complete puzzle
                }
                break;
            }
            else if(iconOrderArray[i] == false && number != i)
            {
                break;
            }
        }
    }

    void ActiveImage()
    {
        if (nextImage < images.Length)
        {
            images[nextImage].SetActive(true);
            nextImage++;
        }
    }
}
