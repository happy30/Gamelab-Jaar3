﻿//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationEffects : MonoBehaviour
{

	public void Strobe()
	{
		GameObject.Find("Canvas").GetComponent<ConversationUI>().Strobe();
	}

	public void Black()
	{
		GameObject.Find("Canvas").GetComponent<ConversationUI>().Black();
	}

	public void FadeOutBlack()
	{
		GameObject.Find("Canvas").GetComponent<ConversationUI>().FadeOutBlack();
	}

	public void CheckItem(string name, string newCode)
	{
        HeldItem heldItem = GameObject.Find("HeldItemController").GetComponent<HeldItem>();
        InventoryManager inv = GetComponent<InventoryManager>();
        {
            if (heldItem.itemName.text == name)
            {
                GetComponent<ConversationStats>().interactedObject.interactionCodeName = newCode;
                for (int i = 0; i < GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().cc.interactions.Count; i++)
                {
                    if (GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().cc.interactions[i].interactionCodeName == newCode)
                    {
                        GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().currentConversation = GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().cc.interactions[i];
                        foreach (Item item in inv.inventory.ToArray())
                        {
                            if(item.name == heldItem.itemName.text)
                            {
                                inv.RemoveHeldItem(item);
                                break;
                            }
                        }
                    }
                }
            }
        }



        /*
		
		
			if(item.name == name)
			{
				GetComponent<ConversationStats>().interactedObject.interactionCodeName = newCode;
				for (int i = 0; i < GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().cc.interactions.Count; i++)
				{
					if (GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().cc.interactions[i].interactionCodeName == newCode)
					{
						GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().currentConversation = GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().cc.interactions[i];
						inv.RemoveHeldItem(item);
						break;
					}
				}
			}
		}
        */
	}

	public void PickUp(string name)
	{
		GetItem(name);
		//GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject.Trigger(false);
		GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().SetInactiveAfterConversaton = true;
	}

	public void GetItem(string name)
	{
		InventoryManager inv = GetComponent<InventoryManager>();


		switch (PlayMode.escapeRoom)
		{
			case PlayMode.EscapeRoom.ClientsCell:

				foreach(Item item in inv.ccItems)
				{
					if(item.name == name)
					{
						inv.inventory.Add(item);
					}
				}
				break;
		}
	}

	public void AddObject(string id)
	{
		int obj = int.Parse(id);

		GameObject.Find("SceneSettings").GetComponent<SceneObjects>().ActivateObject(obj);
			
	}

	public void RemoveObject(string id)
	{
		int obj = int.Parse(id);

		GameObject.Find("SceneSettings").GetComponent<SceneObjects>().DeactivateObject(obj);

	}

}
