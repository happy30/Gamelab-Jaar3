//CameraBehaviour.cs by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    ExploreStats exploreStats;
    public Transform player;

    float fieldOfView;

    float yRotOffset;
    float yRot;

    public float conversationYRotation;
    ConversationStats conversationStats;

    private void Awake()
    {
        exploreStats = GameObject.Find("GameManager").GetComponent<ExploreStats>();
        conversationStats = GameObject.Find("GameManager").GetComponent<ConversationStats>();
    }

    void Start()
    {

    }

    void Update()
    {
        //If player's isn't talking. Lock the cursor and make it able to move the camera around.

        switch (PlayMode.gameMode)
        {
            case PlayMode.GameMode.Explore:
                ExploreCamera();
                SetFieldOfView();
                break;

            case PlayMode.GameMode.Conversation:
                if(conversationStats.interactedObject.interactType == Interact.InteractType.Conversation)
                {
                    ConversationCamera();
                }
                
                break;

            case PlayMode.GameMode.Menu:
                MenuCamera();
                break;
        }


        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }


    void ExploreCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.transform.Rotate(0, Input.GetAxis("Mouse X") * exploreStats.mouseSensivity, 0);
        float rotationX = ClampAngle((-Input.GetAxis("Mouse Y") * exploreStats.mouseSensivity) + transform.eulerAngles.x, -80, 90);
        transform.rotation = Quaternion.Euler(new Vector3(rotationX, transform.eulerAngles.y, transform.eulerAngles.z));
        if (Input.GetButton("Fire2"))
        {
            fieldOfView = exploreStats.zoomAmount;
        }
        else
        {
            fieldOfView = 60;
        }
    }

    void MenuCamera()
    {
        Cursor.lockState = CursorLockMode.None;
    }
        


    public void SetCameraOffset()
    {
        yRotOffset = player.transform.eulerAngles.y; 
        
    }

    public void SetCameraRotation()
    {
        yRot = yRotOffset + conversationYRotation;
    }


    void ConversationCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.eulerAngles = new Vector3(Mathf.Lerp(transform.eulerAngles.x, 0, 5f * Time.deltaTime), transform.eulerAngles.y, transform.eulerAngles.z);
        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, Mathf.Lerp(player.transform.eulerAngles.y, yRot, 7f * Time.deltaTime), player.transform.eulerAngles.z);
    }

    void SetFieldOfView()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fieldOfView, exploreStats.zoomSpeed * Time.deltaTime);
    }


    //Make sure we can't make loops with the camera.
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180)
            {
                angle -= 360;
            }// convert all angles to -180..+180
            if (max > 180)
            {
                max -= 360;
            }
            if (min > 180)
            {
                min -= 360;
            }
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0)
        {
            angle += 360;
        }   // if angle negative, convert to 0..360
        return angle;
    }
}
