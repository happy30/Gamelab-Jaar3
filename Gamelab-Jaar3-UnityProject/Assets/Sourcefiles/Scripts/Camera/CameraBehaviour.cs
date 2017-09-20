using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform player;
    public float rotateSpeed;

    void Start()
    {
        rotateSpeed = 3;
    }

    void Update()
    {
        //If player's isn't talking. Lock the cursor and make it able to move the camera around.
        if (PlayMode.gameMode == PlayMode.GameMode.Explore)
        {
            Cursor.lockState = CursorLockMode.Locked;
            player.transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
            float rotationX = ClampAngle((-Input.GetAxis("Mouse Y") * rotateSpeed) + transform.eulerAngles.x, -80, 90);
            transform.rotation = Quaternion.Euler(new Vector3(rotationX, transform.eulerAngles.y, transform.eulerAngles.z));
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
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
