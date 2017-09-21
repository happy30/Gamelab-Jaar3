using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreStats : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float crouchWalkSpeed;
    public float interactRange;
    public float crouchDrop;

    [Range(0.0f, 0.5f)]
    public float bobSpeed;

    [Range(0.0f, 0.3f)]
    public float bobAmount;
}
