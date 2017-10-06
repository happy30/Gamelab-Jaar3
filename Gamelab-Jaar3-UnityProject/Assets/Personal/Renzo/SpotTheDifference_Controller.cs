using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotTheDifference_Controller : MonoBehaviour {

    //public List<GameObject> spots = new List<GameObject>();
    public int totalSpots;
    public int foundSpots;

    public static SpotTheDifference_Controller spotController = null;

	void Start () {
        spotController = this;
	}
	
	void Update () {
		
	}
}