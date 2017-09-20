using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreController : MonoBehaviour
{
    float hVelocity;
    float speed;
    float cameraHeight;
    float vVelocity;

    ExploreStats exploreStats;
    public Transform camTransform;


	// Use this for initialization
	void Awake()
    {
        exploreStats = GameObject.Find("GameManager").GetComponent<ExploreStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayMode.gameMode == PlayMode.GameMode.Explore)
        {
            Move();
            Crouch();
        }
	}

    void Move()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? exploreStats.runSpeed : exploreStats.walkSpeed;


        hVelocity = Input.GetAxis("Horizontal");
        vVelocity = Input.GetAxis("Vertical");

        Vector3 moveTowardsPos = new Vector3(hVelocity, 0, vVelocity);
        transform.Translate(moveTowardsPos * speed * Time.deltaTime);
    }

    void Crouch()
    {
        cameraHeight = Input.GetKey(KeyCode.LeftControl) ? -exploreStats.crouchDrop : 0;
        Vector3 camPosition = new Vector3(0, cameraHeight, 0);

        print(cameraHeight);

        camTransform.localPosition = Vector3.Lerp(new Vector3(0, 1, 0), camPosition, exploreStats.crouchSpeed * Time.deltaTime);
    }

}
