using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotTheDifferences : MonoBehaviour {
    /*
     * New
     * 1 list met objects, elk object heeft een child die aan de mirror kant zit
     * 
     * Old
     * 2 list, 1 voor links ander voor rechts
     * on mouse down, zet render op active, bij de geclickte en de mirror
     */
     
    
    private int listIndex;

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

    }
	
	void Update () {
		
	}

    private void OnMouseDown()
    { // private int = i. i = list.index. get mirror list & index, do same action
        print("Mouse is here");
    }
}
