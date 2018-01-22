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
	Settings settings;
	public Transform camTransform;
	Vector3 camPosition;

	Vector3 crouchPos;
	Vector3 bobPos;

	ExploreUI exploreUI;

	public AudioSource playerSFX;
	public AudioClip footstepConcrete;

	public float walkBobSpeed;
	public float runBobSpeed;
	public float bobSpeed;

    [HideInInspector]
    public bool crouchAvailable;


	// Use this for initialization
	void Awake()
	{
		exploreStats = GameObject.Find("GameManager").GetComponent<ExploreStats>();
		exploreUI = GameObject.Find("HUDCanvas").GetComponent<ExploreUI>();
		settings = GameObject.Find("GameManager").GetComponent<Settings>();
	}

	void Start()
	{
		walkBobSpeed = exploreStats.bobSpeed;
		runBobSpeed = exploreStats.bobSpeed * 2f;

        if(exploreStats.forceCrouch)
        {
            camTransform.localPosition = new Vector3(camTransform.localPosition.x, -exploreStats.crouchDrop, camTransform.localPosition.z);
        }
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
			CheckForInteraction();
		}
	}

	void FixedUpdate ()
	{
		if (PlayMode.gameMode == PlayMode.GameMode.Explore)
		{
			hVelocity = Input.GetAxis("Horizontal");
			vVelocity = Input.GetAxis("Vertical");

			Vector3 moveTowardsPos = new Vector3(hVelocity, 0, vVelocity);
			transform.Translate(moveTowardsPos * speed * Time.deltaTime);
		}
		
	}

	//Waits for input and makes character move
	void Move()
	{
        if(exploreStats.forceCrouch)
        {
            speed = exploreStats.crouchWalkSpeed;
        }
        else if (!Input.GetKey(KeyCode.LeftControl) || exploreStats.forceCrouch)
        {
            speed = Input.GetKey(KeyCode.LeftShift) ? exploreStats.runSpeed : exploreStats.walkSpeed;
            bobSpeed = Input.GetKey(KeyCode.LeftShift) ? runBobSpeed : walkBobSpeed;

        }



    }

	//Checks input for crouching and then fill crouchPos for Camera
	void Crouch()
	{
		if(Input.GetKey(KeyCode.LeftControl) && !exploreStats.forceCrouch || camTransform.localPosition.y < 0.99f && !exploreStats.forceCrouch)
		{
			cameraHeight = Input.GetKey(KeyCode.LeftControl) ? -exploreStats.crouchDrop : 0;
		}
        else if(exploreStats.forceCrouch)
        {
            cameraHeight = -exploreStats.crouchDrop;
        }
        crouchPos = new Vector3(0, cameraHeight, 0);

        if(crouchAvailable && exploreStats.forceCrouch)
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                exploreStats.forceCrouch = false;
            }
        }
    }

	//Checks if we're moving, then fill bobPos for Camera
	void HeadBob()
	{
		if(settings.enableHeadBobbing)
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
				timer = timer + bobSpeed;
				if (timer > Mathf.PI * 2)
				{
					timer = timer - (Mathf.PI * 2);
                    playerSFX.pitch = Random.Range(0.8f, 1.2f);
                    playerSFX.PlayOneShot(footstepConcrete, 0.50f);

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
		
	}

	//Add Crouch and Bob together and then set the position of the Camera
	void MoveCamera()
	{
		camPosition = crouchPos + bobPos;
		camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, camPosition, exploreStats.crouchSpeed * Time.deltaTime);
	}

	void CheckForInteraction()
	{
		RaycastHit hit;
		if(Physics.Raycast(camTransform.position, camTransform.forward, out hit, exploreStats.interactRange))
		{
			if(hit.collider.tag == "Interact")
			{
				exploreUI.ShowInteractCursor(true);
				if(Input.GetButtonDown("Fire1"))
				{
					hit.collider.GetComponent<Interact>().Trigger(true);
				}
				return;
			}
		}
		exploreUI.ShowInteractCursor(false);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "InteractOnTrigger" && PlayMode.gameMode == PlayMode.GameMode.Explore)
        {
            col.GetComponent<Interact>().Trigger(true);
        }
    }
}
