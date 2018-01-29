using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
	public int band;
	public float startScale, scaleMultiplier;
	public bool useBuffer;

    public AudioPeer peer;


	// Use this for initialization
	void Start ()
    {
        peer = GameObject.Find("HUDCanvas").GetComponent<AudioPeer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(useBuffer)
		{
			GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x, (peer.bandBuffer[band] * scaleMultiplier) + startScale, GetComponent<RectTransform>().localScale.z);
		}
		else
		{
			GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x, (peer.freqBand[band] * scaleMultiplier) + startScale, GetComponent<RectTransform>().localScale.z);
		}

		
	}
}
