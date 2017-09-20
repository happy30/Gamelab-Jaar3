using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreController : MonoBehaviour
{
    public float hVelocity;
    public float speed;

    public float vVelocity;

    public ExploreStats exploreStats;


	// Use this for initialization
	void Awake()
    {
        exploreStats = GameObject.Find("GameManager").GetComponent<ExploreStats>();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //speed = (Input.GetKey(KeyCode.LeftShift)) ? exploreStats.walkSpeed : exploreStats.runSpeed;
        speed = 2f;

		if(PlayMode.gameMode == PlayMode.GameMode.Explore)
        {
            hVelocity = Input.GetAxis("Horizontal");
            vVelocity = Input.GetAxis("Vertical");

            Vector3 moveTowardsPos = new Vector3(hVelocity, 0, vVelocity);
            transform.Translate(moveTowardsPos * speed * Time.deltaTime);
        }
	}
}
