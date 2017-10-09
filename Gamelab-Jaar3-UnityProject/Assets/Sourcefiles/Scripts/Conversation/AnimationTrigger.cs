using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{

    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Activate()
    {
        if (animator.GetBool("Activate") == false)
        {
            animator.SetBool("Activate", true);
        }
        else animator.SetBool("Activate", false);
    }
	
}
