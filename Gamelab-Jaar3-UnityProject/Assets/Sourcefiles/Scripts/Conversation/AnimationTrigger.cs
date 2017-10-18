using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{

    public Animator animator;
	public bool isDoor;
	bool withinDoor;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Activate()
    {
		if (isDoor == false) 
		{
			if (animator.GetBool("Activate") == false)
			{
				animator.SetBool("Activate", true);
			}
			else animator.SetBool("Activate", false);
		}
    }
	void OnTriggerEnter (Collider other)
	{
		if (isDoor == true) 
		{
			if (other.tag == "Player") 
			{
				gameObject.GetComponentInParent<AnimationTrigger>().animator.SetBool ("Activate", true);
			}
		}
	}
	void OnTriggerLeave (Collider other)
	{
		if (isDoor == true) 
		{
			if (other.tag == "Player") 
			{
				gameObject.GetComponentInParent <AnimationTrigger>().animator.SetBool ("Activate", false);
			}
		}
	}
}
