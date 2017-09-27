using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotTheDifferences : MonoBehaviour {
    /*
     * 2 list, 1 voor links ander voor rechts
     * on mouse down, zet render op active, bij de geclickte en de mirror
     */

    public List<GameObject> leftItems = new List<GameObject>();
    public List<GameObject> rightItems = new List<GameObject>();

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
        
    }
}
