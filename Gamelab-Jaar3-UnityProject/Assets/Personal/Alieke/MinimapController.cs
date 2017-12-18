using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    public Transform player;
    public GameObject playerUI;

	void LateUpdate ()
    {
        Vector3 newPosition = player.position;
        newPosition.y = player.position.y;
        transform.position = newPosition;

        playerUI.transform.eulerAngles = new Vector3(0, 0, -player.eulerAngles.y);
	}
}
