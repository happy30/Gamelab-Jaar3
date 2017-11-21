//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationStats : MonoBehaviour
{
    public float slowTextSpeed;
    public float fastTextSpeed;
    public Interact interactedObject;

    public List<Interaction> interactions = new List<Interaction>();


    public void AddInteraction(string icn)
    {
        interactions.Add(new Interaction(icn, true));
    }
	
}
