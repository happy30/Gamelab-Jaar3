//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPuzzle : MonoBehaviour
{
    public int[] correctOrderArray;
    public int[] intOrderArray;
    public GameObject[] images;
    private int nextImage = 0;
	
	public void CheckOrder (int number)
    {
        /*if (number == 2 && !iconOrderArray[3] && iconOrderArray[2])
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
        }*/
    }

    void FillArray(int number)
    {
        intOrderArray[nextImage] = number;
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

    void Reset()
    {
        for(int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
    }
}
