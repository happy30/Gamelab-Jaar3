using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SegmentData : MonoBehaviour
{
	public Text day;
	public Text segment;


	// Use this for initialization
	void Start ()
	{
		day.text = SceneManager.GetActiveScene().name;
		segment.text = GameObject.Find("SceneSettings").GetComponent<SceneEventData>().gamePart.ToString();
	}
	
}
