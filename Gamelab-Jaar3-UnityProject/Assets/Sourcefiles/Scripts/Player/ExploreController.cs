using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreController : MonoBehaviour
{
    float hVelocity;
    float speed;
    float cameraHeight;
    float vVelocity;
    float timer;

    ExploreStats exploreStats;
    public Transform camTransform;
    Vector3 camPosition;

    Vector3 crouchPos;
    Vector3 bobPos;


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
            HeadBob();
            MoveCamera();
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
        if(Input.GetKey(KeyCode.LeftControl) || camTransform.localPosition.y < 0.99f)
        {
            cameraHeight = Input.GetKey(KeyCode.LeftControl) ? -exploreStats.crouchDrop : 0;
            crouchPos = new Vector3(0, cameraHeight, 0);
        }
    }

    void HeadBob()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + exploreStats.bobSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * exploreStats.bobAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;

            bobPos = new Vector3(camTransform.localPosition.x, 0 + translateChange, camTransform.localPosition.z);
        }
        else
        {
            bobPos = new Vector3(camTransform.localPosition.x, 0, camTransform.localPosition.z);
        }
    }

    void MoveCamera()
    {
        camPosition = crouchPos + bobPos;
        camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, camPosition, exploreStats.crouchSpeed * Time.deltaTime);
    }

}
