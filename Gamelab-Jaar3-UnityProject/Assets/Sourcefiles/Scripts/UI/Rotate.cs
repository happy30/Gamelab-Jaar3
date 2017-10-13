using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float speed;

	void Update()
	{
		GetComponent<RectTransform>().Rotate(0, speed * Time.deltaTime, 0);
	}
}
