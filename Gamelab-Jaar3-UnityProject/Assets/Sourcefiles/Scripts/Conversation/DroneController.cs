using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Actors.Actor character;

    public Material[] mats;
    Material mat;


    void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

	// Use this for initialization
	void Start ()
    {
	    switch  (character)
        {
            case Actors.Actor.Dust:
                mat = mats[0];
                break;

            case Actors.Actor.Blaze:
                mat = mats[1];
                break;

            case Actors.Actor.Livie:
                mat = mats[2];
                break;

            case Actors.Actor.Violet:
                mat = mats[3];
                break;

            case Actors.Actor.Scarlet:
                mat = mats[4];
                break;

            case Actors.Actor.Marine:
                mat = mats[5];
                break;

            case Actors.Actor.Moss:
                mat = mats[6];
                break;

            case Actors.Actor.Auburn:
                mat = mats[7];
                break;

            case Actors.Actor.Ash:
                mat = mats[8];
                break;

            case Actors.Actor.Amber:
                mat = mats[9];
                break;

            case Actors.Actor.Unknown:
                mat = mats[10];
                break;
        }	

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
