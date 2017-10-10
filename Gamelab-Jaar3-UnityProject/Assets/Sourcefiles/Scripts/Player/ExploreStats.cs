//ExploreStats.cs by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreStats : MonoBehaviour
{
    [Header("View")]
    public float mouseSensivity;

    [Header("Move")]
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float crouchWalkSpeed;
    public float crouchDrop;

    [Header("Zoom")]
    public float zoomSpeed;
    public float zoomAmount;


    [Header("HeadBobbing")]
    [Range(0.0f, 0.15f)]
    public float bobSpeed;

    [Range(0.0f, 0.15f)]
    public float bobAmount;

    [Header("Interacting")]
    public float interactRange;
}
