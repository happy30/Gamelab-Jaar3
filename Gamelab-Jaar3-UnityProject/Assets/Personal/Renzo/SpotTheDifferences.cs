using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotTheDifferences : MonoBehaviour {
    
    private bool isFound;
    private ParticleSystem pS;

	void Start () {
    #region 2ListMethod
        /*
        if (!leftItems.Contains(gameObject))
        { // is it in the right list?
            listIndex = rightItems.IndexOf(gameObject);
            print("Index in right list = " + listIndex);
            print(leftItems[listIndex]);
        }
        else
        { // no? then its in the left list.
            listIndex = leftItems.IndexOf(gameObject);
            print("Index in left list = " + listIndex);
            print(rightItems[listIndex]);
        }
        */
    #endregion
        if (pS.isEmitting)
            pS.Stop();
    }
	
	void Update () {
		
	}

    private void OnMouseDown()
    {
        GetComponent<ParticleSystem>().Play();
        isFound = true;
        
        //onclick,Get list van ander script, check de index, do stuff from there (zoals play PS, 

        print("Mouse is here");
    }
}
